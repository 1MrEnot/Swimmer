namespace Swimmer.Services.CompetitionImportSerivce;

using Domain.Entities;
using Domain.Enums;

public class FakeCompetitionImportService : ICompetitionImportService
{
    public Competition ParseFile(Stream stream)
    {
        var competition = new Competition("2 мая - 5 мая", 6);
        var a1 = new Athlete("Арсений Осипов", 2005, Gender.Male);
        var a2 = new Athlete("Илья Кузьмин", 2006, Gender.Male);
        var a3 = new Athlete("Георгий Богданов", 2002, Gender.Male);
        var a4 = new Athlete("Иван Анисимов", 2005, Gender.Male);

        var b1 = new Athlete("Виктория Сафонова", 2005, Gender.Female);
        var b2 = new Athlete("Алисия Князева", 2005, Gender.Female);
        var b3 = new Athlete("Вероника Максимова", 2005, Gender.Female);
        var b4 = new Athlete("София Шилова", 2005, Gender.Female);

        competition.AddAthleteIfNotExist(a1);
        competition.AddAthleteIfNotExist(a2);
        competition.AddAthleteIfNotExist(a3);
        competition.AddAthleteIfNotExist(a4);

        competition.AddAthleteIfNotExist(b1);
        competition.AddAthleteIfNotExist(b2);
        competition.AddAthleteIfNotExist(b3);
        competition.AddAthleteIfNotExist(b4);

        var swimA = competition.CreateSwim(Gender.Male, "100 кл/л");
        swimA.AddAthleteForSwim(a1, new TimeSpan(0, 0, 0, 45, 500));
        swimA.AddAthleteForSwim(a2, new TimeSpan(0, 0, 0, 44, 700), 4);
        swimA.AddAthleteForSwim(a3, null, 5);
        swimA.AddAthleteForSwim(a4, null, 6);

        var swimB = competition.CreateSwim(Gender.Female, "100 кл/л");
        swimB.AddAthleteForSwim(b1, null, 2);
        swimB.AddAthleteForSwim(b2, new TimeSpan(0, 0, 0, 48, 500), 3);
        swimB.AddAthleteForSwim(b3);
        swimB.AddAthleteForSwim(b4);

        return competition;
    }
}