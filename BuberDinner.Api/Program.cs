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

    //builder.Services.AddControllers(options => options.Filters.Add<ErrorHandlingFilterAttribute>());
    builder.Services.AddControllers();

    // // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    // builder.Services.AddEndpointsApiExplorer();
    // builder.Services.AddSwaggerGen();

    // another approach to global error handling
    // for adding customer properties to ProblemDetails
    //builder.Services.AddSingleton<ProblemDetailsFactory, BuberDinnerProblemDetailsFactory>();
}

var app = builder.Build();
{
    // Configure the HTTP request pipeline.
    // if (app.Environment.IsDevelopment())
    // {
    //     app.UseSwagger();
    //     app.UseSwaggerUI();
    // }

    // 1st approach to global error handling
    //app.UseMiddleware<ErrorHandlingMiddleware>();

    app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();

    // app.UseAuthorization();

    app.MapControllers();

    app.Run();

}
