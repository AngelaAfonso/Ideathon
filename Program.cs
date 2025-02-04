using ChatbotApi.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ChatBotTeste.Models;
using ChatBotTeste.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Configurando o banco de dados
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer("Server=Quesia_NOTE;Database=ChatBot;Integrated Security=True;TrustServerCertificate=True;"));

// Adicionando repositórios e serviços
builder.Services.AddScoped<IAplicacaoRepository, AplicacaoRepository>();

// Adicionando Controllers
builder.Services.AddControllers();

// Adicionando CORS (Antes do `builder.Build()`)
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MyPolicy",
        policy =>
        {
            policy.WithOrigins("http://localhost:8080", "http://localhost:8081", "http://localhost:5000", "http://localhost:5179")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

// Adicionando Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Construindo o app
var app = builder.Build();

// Configuração do pipeline HTTP, se tiver

// Ativando o Swagger apenas no ambiente de desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}
// Configurando arquivos HTML, CSS, JS
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")),
    RequestPath = ""
});

// Ativando e aplicando tudo
app.UseHttpsRedirection();
app.UseCors("MyPolicy");
app.UseAuthorization();
app.MapControllers();
app.UseStaticFiles(); // Permite servir arquivos HTML, CSS e JS
app.Run();
