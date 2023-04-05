using FisioFlores.Hubs;
using FisioFlores.Models;
using Microsoft.EntityFrameworkCore;

string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins,
    builder =>
    {
        builder.WithOrigins("http://backendfisioflores.fsdev.link/",
                                    "http://backendfisioflores.fsdev.link",
                                    "https://backendfisioflores.fsdev.link/",
                                    "http://backendfisioflores.fsdev.link",
                                    "https://fisioflores.fsdev.link/",
                                    "https://fisioflores.fsdev.link",
                                    "http://fisioflores.fsdev.link/",
                                    "http://fisioflores.fsdev.link",
                                    "http://localhost:4200",
                                    "http://localhost:4200/",
                                    "https://localhost:4200",
                                    "https://localhost:4200/")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});
// Add services to the container.
builder.Services.AddSignalR();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var connectionString = "server=185.254.205.124,3306:;user=remote;password=0983496205Caro*;database=bdd_fisio_flores";

// Replace with your server version and type.
// Use 'MariaDbServerVersion' for MariaDB.
// Alternatively, use 'ServerVersion.AutoDetect(connectionString)'.
// For common usages, see pull request #1233.
var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
builder.Services.AddDbContext<bdd_fisio_floresContext>(
x => x
.UseMySql(connectionString, serverVersion)
.LogTo(Console.WriteLine, LogLevel.Information)
.EnableSensitiveDataLogging()
.EnableDetailedErrors()
);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthorization();
app.MapControllers();
app.MapHub<MensajeHub>("/mensajehub");
app.Run();
