// See https://aka.ms/new-console-template for more information

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Swimmer.Domain.Entities;
using Swimmer.Domain.Enums;
using Swimmer.Infrastructure.Persistance;
using Swimmer.Services.CompetitionImportSerivce;

var optionsBuilder = new DbContextOptionsBuilder<SwimmerContext>();
var options = optionsBuilder
    .UseSqlite(@"Data Source=C:\Users\Max\RiderProjects\Swimmer\Testing\testingDb;")
    .Options;

await using var ctx = new SwimmerContext(options);
var repo = new EfRepository<Competition>(ctx);

var comp = await repo.Get(1);

var a = 1;


