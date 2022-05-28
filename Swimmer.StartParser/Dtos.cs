namespace Swimmer.StartParser;

public record ParsedAthlete(string Name, int BirthYear, string Trainer, TimeSpan?[] Timings);

public class FileStructureDescription
{
    public FileStructureDescription(int nameColumn, int yearColumn, int trainerIndex, Range timings)
    {
        NameColumn = nameColumn;
        YearColumn = yearColumn;
        TrainerIndex = trainerIndex;
        Timings = timings;
    }

    public int NameColumn { get; }

    public int YearColumn { get; }
    
    public int TrainerIndex { get; }

    public int GenderIndex { get; } = -1;
    
    public Range Timings { get; }

    public int StartRow { get; init; } = 1;
}