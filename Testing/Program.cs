// See https://aka.ms/new-console-template for more information

using Microsoft.EntityFrameworkCore;
using Swimmer.Domain.Entities;
using Swimmer.Infrastructure.Persistance;

var optionsBuilder = new DbContextOptionsBuilder<SwimmerContext>();
var options = optionsBuilder
    .UseSqlite(@"Data Source=C:\Users\Max\RiderProjects\Swimmer\Testing\test.db;")
    .Options;

await using var ctx = new SwimmerContext(options);
var repo = new EfRepository<Competition>(ctx);

var comp = await repo.GetOrDefault(1);

var a = 1;


