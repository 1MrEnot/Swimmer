// See https://aka.ms/new-console-template for more information

using Swimmer.StartParser;
using Swimmer.StartParser.Builder;

System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

//var res = StartBuilder.BatchDistanceAthletes(new Athlete[12], 5);


BuildStart();

return;

/*
 * Порядок 
    25 м н/сп 
    25 м в/ст 
    50 н/сп
    50 брасс
    50 в/ст
    100 к/пл
    50 кл/л
    100 кл/л
    200 кл/л 
    25 дайвинг
 */

void BuildStart()
{
    var b = new StartBuilder();
    
    var opts = new BuilderOptions(
        @"C:\Users\Max\Desktop\ResClear.xlsx",
        4, 
        new DistanceFile[]
        {
            new ("Дистанция 25м н/сп",@"C:\Users\Max\Desktop\res\femn 25м нсп.xlsx"),
            new ("Дистанция 25м н/сп",@"C:\Users\Max\Desktop\res\masc 25м нсп.xlsx"),
            
            new ("Дистанция 25м в/ст",@"C:\Users\Max\Desktop\res\femn 25м вст.xlsx"),
            new ("Дистанция 25м в/ст",@"C:\Users\Max\Desktop\res\masc 25м вст.xlsx"),
            
            new ("Дистанция 50м н/сп",@"C:\Users\Max\Desktop\res\femn 50м нсп.xlsx"),
            new ("Дистанция 50м н/сп",@"C:\Users\Max\Desktop\res\masc 50м нсп.xlsx"),
            
            new ("Дистанция 50м брасс",@"C:\Users\Max\Desktop\res\femn 50м брасс.xlsx"),
            new ("Дистанция 50м брасс",@"C:\Users\Max\Desktop\res\masc 50м брасс.xlsx"),
            
            new ("Дистанция 50м в/ст",@"C:\Users\Max\Desktop\res\femn 50м вст.xlsx"),
            new ("Дистанция 50м в/ст",@"C:\Users\Max\Desktop\res\masc 50м вст.xlsx"),
            
            new ("Дистанция 100м к/пл",@"C:\Users\Max\Desktop\res\femn 100м кпл.xlsx"),
            new ("Дистанция 100м к/пл",@"C:\Users\Max\Desktop\res\masc 100м кпл.xlsx"),
            
            new ("Дистанция 50м кл/л",@"C:\Users\Max\Desktop\res\femn 50м клл.xlsx"),
            new ("Дистанция 50м кл/л",@"C:\Users\Max\Desktop\res\masc 50м клл.xlsx"),
            
            new ("Дистанция 100м кл/л",@"C:\Users\Max\Desktop\res\femn 100м клл.xlsx"),
            new ("Дистанция 100м кл/л",@"C:\Users\Max\Desktop\res\masc 100м клл.xlsx"),
            
            new ("Дистанция 200м кл/л",@"C:\Users\Max\Desktop\res\femn 200м клл.xlsx"),
            new ("Дистанция 200м кл/л",@"C:\Users\Max\Desktop\res\masc 200м клл.xlsx"),
                        
            new ("Дистанция 25м под/гр",@"C:\Users\Max\Desktop\res\femn 25м под.гр.xlsx"),
            new ("Дистанция 25м под/гр",@"C:\Users\Max\Desktop\res\masc 25м под.гр.xlsx"),
        });

    b.BuildStarts(opts);
}

void StartParse()
{
    var fileStructure = new FileStructureDescription(0, 1, 2, new Range(3, 13))
    {
        StartRow = 3
    };

    const string startPath = @"C:\Users\Max\OneDrive\Документы\Соревнования\21 мая 22\Заявки\!ОБЩАЯ.xlsx";
    using var file = File.Open(startPath, FileMode.Open);
    var parser = new StartParser();
    var result = parser.Parse(file, fileStructure);
}