using System.Data;
using FastEndpoints;
using GridStatusHub.Domain.Context;
using GridStatusHub.Domain.Entities;
using GridStatusHub.Domain.HandlerRequests.Query;
using GridStatusHub.Infra.Repo;
using Npgsql;                

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IDbConnection>(db => new NpgsqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped(typeof(IBaseRepo<>), typeof(BaseRepo<>));
builder.Services.AddScoped<IGridSystemRepo<GridSystem>, GridSystemRepo>();
builder.Services.AddScoped<IGetAllGridSystemsQueryHandler, GetAllGridSystemsQueryHandler>();


builder.Services.AddHttpContextAccessor();
builder.Services.AddFastEndpoints();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("https://localhost:44420")
            .AllowAnyHeader()
            .WithMethods("GET", "POST", "PUT");
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseFastEndpoints();
app.UseCors();

app.Run();
