using System.Data;
using FastEndpoints;
using GridStatusHub.Domain.Context.BaseRepo;
using GridStatusHub.Infra.Repo;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IDbConnection>(db => new NpgsqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

app.UseFastEndpoints();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
