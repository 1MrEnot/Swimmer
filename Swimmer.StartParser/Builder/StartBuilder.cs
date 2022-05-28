namespace Swimmer.StartParser.Builder;

using System.Data;
using ExcelDataReader;
using OfficeOpenXml;
using OfficeOpenXml.Style;

public record BuilderOptions(string ResultFileName, int TrackCount, DistanceFile[] DistanceFiles, int StartRow=1, HashSet<string>? Ignored=null);

public record DistanceFiles(string Distance, string[] FileNames);

public record DistanceFile(string Distance, string FileName);

public record FileAthletes(string Distance, Athlete[] Athletes);

public readonly record struct Athlete(string Name, int BirthYear, string Trainer, string Time);

public record Distance(string Name, Swim[] Swims);

public record Swim(int Number, Athlete[] Athletes);


public class StartBuilder
{
    static StartBuilder()
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
    }
    
    public void BuildStarts(BuilderOptions options)
    {
        var athletesByDistance = GetAthletesByFiles(options);
        var distances = GetDistances(athletesByDistance, options.TrackCount);
        WriteToExcel(distances, options.ResultFileName, options.TrackCount);
    }

    private void WriteToExcel(List<Distance> distances, string filename, int trackCount)
    {
        var fileInfo = new FileInfo(filename);

        using var excel = new ExcelPackage(fileInfo);
        const string sheetName = "День 1";
        excel.Workbook.Worksheets.Add(sheetName);
        var sheet = excel.Workbook.Worksheets[sheetName]!;
        
        var cellIndex = 1;
        var createdDistances = new HashSet<string>();
        for (var i = 0; i < distances.Count; i++)
        {
            var distance = distances[i];
            cellIndex = WriteDistance(sheet, distance, cellIndex, trackCount, createdDistances);
        }

        excel.Save();
    }

    private int WriteDistance(ExcelWorksheet worksheet, Distance distance, int startCellIndex, int trackCount, HashSet<string> createdDistances)
    {
        if (!createdDistances.Contains(distance.Name))
        {
            worksheet.Cells[$"A{startCellIndex}"].Value = distance.Name;
            worksheet.Cells[$"A{startCellIndex}:F{startCellIndex}"].Merge = true;
            worksheet.Cells[$"A{startCellIndex}:F{startCellIndex}"].Style.HorizontalAlignment
                = ExcelHorizontalAlignment.Center;
        }
        else
            createdDistances.Add(distance.Name);

        foreach (var (number, athletes) in distance.Swims)
        {
            var swimHeaderIndex = startCellIndex + 1;
            var athletesDataStartIndex = startCellIndex + 2;
            
            worksheet.Cells[$"A{swimHeaderIndex}"].Value = number.ToString();
            worksheet.Cells[$"B{swimHeaderIndex}"].Value = "заплыв";

            for (var i = 0; i < trackCount; i++)
                worksheet.Cells[$"A{athletesDataStartIndex + i}"].Value = (i + 1).ToString();

            var swimRange = $"A{athletesDataStartIndex}:F{athletesDataStartIndex+trackCount-1}";
            var swimWorkspace = worksheet.Cells[swimRange];

            swimWorkspace.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            swimWorkspace.Style.Border.Top.Style = ExcelBorderStyle.Thin;
            swimWorkspace.Style.Border.Left.Style = ExcelBorderStyle.Thin;
            swimWorkspace.Style.Border.Right.Style = ExcelBorderStyle.Thin;

            var athletesToShow = GetAthletesForShow(athletes, trackCount);
            for (var i = 0; i < athletesToShow.Length; i++)
            {
                if (athletesToShow[i] is null)
                    continue;

                var athlete = athletesToShow[i]!.Value;
                var index = athletesDataStartIndex + i;
                worksheet.Cells[$"B{index}:F{index}"].LoadFromArrays(new List<string[]>()
                {
                    new [] {athlete.Name, athlete.BirthYear.ToString(), athlete.Trainer, string.Empty, athlete.Time}
                });
            }

            startCellIndex += trackCount + 2;
        }

        return startCellIndex + 1;
    }

    private static Athlete?[] GetAthletesForShow(Athlete[] athletes, int trackCount)
    {
        var mid = (trackCount / 2);
        var shift = 0;

        athletes = athletes.Reverse().ToArray();
        var res = new Athlete?[trackCount];
        for (var i = 0; i < athletes.Length; i++)
        {
            var athlete = athletes[i];
            var resIndex = mid + shift;
            res[resIndex] = athlete;

            shift *= -1;
            if (i % 2 == 0) 
                shift--;
        }

        return res;
    }
    
    private List<Distance> GetDistances(IEnumerable<FileAthletes> athletesByDistance, int trackCount)
    {
        var swimIndex = 1;
        var distances = new List<Distance>();

        foreach (var athetesOnDistance in athletesByDistance)
        {
            var batches = BatchDistanceAthletes(athetesOnDistance.Athletes, trackCount);
            var swims = batches
                .Select(athetesOnSwim => new Swim(swimIndex++, athetesOnSwim.ToArray()))
                .ToArray();

            var distance = new Distance(athetesOnDistance.Distance, swims);
            distances.Add(distance);
        }

        return distances;
    }
    
    private List<FileAthletes> GetAthletesByFiles(BuilderOptions options)
    {
        var batchedAthletes = new List<FileAthletes>();
        
        foreach (var distance in options.DistanceFiles)
        {
            using var file = File.Open(distance.FileName, FileMode.Open);
            using var reader = ExcelReaderFactory.CreateReader(file);
            var dataset = reader.AsDataSet();
            var table = dataset.Tables[0];

            var fileAthletes = new FileAthletes(
                distance.Distance,
                ParseAthletes(table, options).OrderByDescending(a => a.Time).ToArray()
                );
            batchedAthletes.Add(fileAthletes);
        }

        return batchedAthletes;
    }
    
    private IEnumerable<Athlete> ParseAthletes(DataTable table, BuilderOptions options)
    {
        var ignored = 0;
        for (var i = options.StartRow; i < table.Rows.Count; i++)
        {
            var athlete = ParseAthlete(table.Rows[i]);

            if (options.Ignored is not null && options.Ignored.Contains(athlete.Name))
                ignored++;
            else
                yield return athlete;
        }
        
        if (ignored >= 1)
            Console.WriteLine($"Ignored {ignored} swims");
    }

    public static IEnumerable<IEnumerable<Athlete>> BatchDistanceAthletes(Athlete[] athletes, int trackCount)
    {
        var swimCount = (int) Math.Ceiling((double)athletes.Length / trackCount);
        var minAthletesPerSwim = athletes.Length / swimCount;

        var ranges = new int[swimCount];
        for (var i = 0; i < swimCount; i++)
            ranges[i] = minAthletesPerSwim;

        var doneAthleteCount = minAthletesPerSwim * swimCount;

        for (var i = doneAthleteCount; i < athletes.Length; i++)
        {
            var rangeIndex = ^(i % swimCount + 1);
            ranges[rangeIndex]++;
        }

        var athleteIndex = 0;
        foreach (var swimAthleteCount in ranges)
        {
            yield return athletes[athleteIndex..(athleteIndex + swimAthleteCount)];
            athleteIndex += swimAthleteCount;
        }
    }

    private Athlete ParseAthlete(DataRow row)
    {
        var name = (string)row[0];
        var bithYear = int.Parse(row[1].ToString().Trim());
        var trainer = row[2] is DBNull ? string.Empty : (string)row[2];

        var timeEl = row.ItemArray.Length == 4 ? row[3] : row[4];
        var time = (string)timeEl;
        
        return new Athlete(
            name.Trim(),
            bithYear,
            trainer.Trim(),
            time.Trim()
            );
    }
}