using System.Data;
using Npgsql;
using FastEndpoints;
using GridStatusHub.Domain.Context;
using GridStatusHub.Domain.Entities;
using GridStatusHub.Domain.HandlerRequests.Query;
using GridStatusHub.Domain.HandlerRequests.Command;
using GridStatusHub.Infra.Repo;
using GridStatusHub.Domain.Service.Rules;
using GridStatusHub.Domain.Responses.GridSystem;
using GridStatusHub.Domain.Requests.GridSystem;
using GridStatusHub.Domain.Utilities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IDbConnection>(db => new NpgsqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped(typeof(IBaseRepo<>), typeof(BaseRepo<>));
builder.Services.AddScoped<IGridSystemRepo<GridSystem>, GridSystemRepo>();
builder.Services.AddScoped<IGetAllGridSystemsQueryHandler, GetAllGridSystemsQueryHandler>();
builder.Services.AddScoped<IGetGridSystemByIdQueryHandler, GetGridSystemByIdQueryHandler>();
builder.Services.AddScoped<IDeleteGridSystemCommandHandler, DeleteGridSystemCommandHandler>();
builder.Services.AddScoped<IUpdateGridSystemCommandHandler<GridSystemRequest, GridSystemResponse>, UpdateGridSystemCommandHandler>();
builder.Services.AddScoped<ICreateGridSystemCommandHandler<GridSystemRequest, GridSystemResponse>, CreateGridSystemCommandHandler>();
builder.Services.AddScoped<GridCellColorService>();
builder.Services.AddScoped<GridSystemIntegrityService>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddFastEndpoints();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("DevelopmentCORS", policy =>
        {
            policy.WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .WithMethods("GET", "POST", "PUT", "DELETE");
        });
});

var app = builder.Build();

app.UseCors("DevelopmentCORS");

app.UseHttpsRedirection();
app.UseFastEndpoints();

app.Run();
