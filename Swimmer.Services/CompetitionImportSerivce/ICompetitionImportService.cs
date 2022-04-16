namespace Swimmer.Services.CompetitionImportSerivce;

using Domain.Entities;

public interface ICompetitionImportService
{
    Competition ParseFile(Stream stream);
}