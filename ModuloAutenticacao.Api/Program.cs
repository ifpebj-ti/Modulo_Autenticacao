using Microsoft.EntityFrameworkCore;
using ModuloAutenticacao.Api.Repository;
using ModuloAutenticacao.Api.Repository.Implementation;
using ModuloAutenticacao.Api.Repository.Interface;
using ModuloAutenticacao.Api.Services.AutenticacaoService;
using ModuloAutenticacao.Api.Services.Interface;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var logger = new LoggerConfiguration()
  .ReadFrom.Configuration(builder.Configuration)
  .Enrich.FromLogContext()
  .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

// Add services to the container.
builder.Services.AddControllers();

//insira a string connection
builder.Services.AddDbContext<DbContexto>(options =>
	options.UseNpgsql(builder.Configuration["ConnectionString"])
);

//Adicionar inje��es dos repositories
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

//Injeções dos services
builder.Services.AddScoped<IAutenticacaoService, AutenticacaoService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("corsapp");

app.UseAuthorization();

app.MapControllers();

app.Run();
