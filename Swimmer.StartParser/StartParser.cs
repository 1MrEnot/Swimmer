namespace Swimmer.StartParser;

using System.Data;
using ExcelDataReader;

#nullable enable

public class StartParser
{
    static StartParser()
    {
        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
    }
    
    public List<ParsedAthlete> Parse(Stream fileStream, FileStructureDescription description)
    {
        using var reader = ExcelReaderFactory.CreateReader(fileStream);
        var dataset = reader.AsDataSet();
        var table = dataset.Tables[0];

        var result = new List<ParsedAthlete>(table.Rows.Count);
        
        for (var i = description.StartRow; i < table.Rows.Count; i++)
        {
            var row = table.Rows[i];
            var athlete = ParseRow(row, description);
            if (athlete is not null)
                result.Add(athlete);
        }

        return result;
    }

    private ParsedAthlete? ParseRow(DataRow row, FileStructureDescription description)
    {
        try
        {
            var name = row[description.NameColumn].ToString();
            var birthYear = int.Parse(row[description.YearColumn].ToString());
            var trainer = row[description.TrainerIndex].ToString();

            var timings = ParsTimes(row, description.Timings);
                
            return new ParsedAthlete(name, birthYear, trainer, timings);
        }
        catch
        {
            return null;
        }
    }
    
    private TimeSpan?[] ParsTimes(DataRow row, Range timeIndexes)
    {
        var timeItems = row.ItemArray[timeIndexes];
        var parsedTimings = new TimeSpan?[timeItems.Length];

        for (var i = 0; i < timeItems.Length; i++)
        {
            var element = timeItems[i];
            if (element is DBNull or null)
                parsedTimings[i] = null;
            else if (TryParseTime(element.ToString(), out var timeSpan))
                parsedTimings[i] = timeSpan;
            else
                return Array.Empty<TimeSpan?>();
        }

        return parsedTimings;
    }

    private bool TryParseTime(string? str, out TimeSpan timeSpan)
    {
        timeSpan = TimeSpan.Zero;
        
        if (str is null)
            return false;
        
        timeSpan = TimeSpan.FromMinutes(1);
        return true;
    }
}