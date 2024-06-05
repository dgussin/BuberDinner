using BuberDinner.Application.Services.Authentication;
using BuberDinner.Application;
using BuberDinner.Infrastructure;
using BuberDinner.Api.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using BuberDinner.Api.Errors;
//using BuberDinner.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);
{
    // Add services to the container.

    builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration);

    builder.Services.AddControllers();


    builder.Services.AddSingleton<ProblemDetailsFactory, BuberDinnerProblemDetailsFactory>();
}

var app = builder.Build();
{
    app.UseExceptionHandler("/error");

    app.UseHttpsRedirection();
    app.MapControllers();

    app.Run();

}
