namespace Swimmer.Infrastructure;

using System.Data;
using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects;
using ExcelDataReader;
using Services.CompetitionImportSerivce;

public class ExcelCompetitionImportService : ICompetitionImportService
{
    static ExcelCompetitionImportService()
    {
        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
    }

    public Competition ParseFile(Stream stream)
    {
        using var excelReader = ExcelReaderFactory.CreateReader(stream);
        return ParseDataTableReader(excelReader);
    }

    private Competition ParseDataTableReader(IDataReader tableReader)
    {
        var competition = new Competition("TEST NAME", 6);

        tableReader.Read();

        do
        {
            ParseDisnace(competition, tableReader);
        } while (tableReader.Read());

        return competition;
    }

    private static void ParseDisnace(Competition competition, IDataReader dataReader)
    {
        string? currentDistance = null;

        while (dataReader.Read())
        {
            if (dataReader.IsDBNull(0))
                continue;

            if (dataReader[0] is string && dataReader.IsDBNull(1))
            {
                currentDistance = dataReader.GetString(0);
                continue;
            }

            if (currentDistance is null)
                throw new Exception("No parsed distance before met a swim");

            while (true)
            {
                ParseSwim(competition, currentDistance, dataReader);
            }
        }
    }

    private static void ParseSwim(Competition competition, string distance, IDataReader dataReader)
    {
        // If we get a number, then string, then we assume, it is new swim
        var currentSwim = competition.CreateSwim(Gender.Unknown, distance, dataReader.Get<int>(0));

        while (dataReader.Read())
        {
            // Ignore empty rows
            // If we got an empty row, but we already got a swim, then assume, swim ended and return
            if (dataReader.IsDBNull(0))
            {
                if (currentSwim is null)
                    continue;
                break;
            }

            if (dataReader.AnyDbNull(0, 1, 2, 4))
                continue;

            var rowIndex = dataReader.Get<int>(0);
            var athleteName = dataReader.GetString(1);
            var birthYear = dataReader.Get<int>(2);
            var preliminaryTime = dataReader.GetString(4);

            var athlete = AddOrCreateAthlete(competition, athleteName, birthYear);

            if (currentSwim is null)
                throw new Exception("Did not create a swim, but got an athlete already");

            var athleteOnSwim = currentSwim.AddAthleteForSwim(
                athlete,
                preliminaryTime.SmartParse(),
                rowIndex
            );

            Console.WriteLine(athleteOnSwim.ToString());
        }
    }

    private static Athlete AddOrCreateAthlete(Competition competition, string athleteName, int birthYear)
    {
        var athlete = new Athlete(
            Name.FromString(athleteName),
            Year.FromInt(birthYear),
            Gender.Unknown
        );
        competition.AddAthleteIfNotExist(athlete);
        return athlete;
    }
}