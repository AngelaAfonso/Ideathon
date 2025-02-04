using Microsoft.EntityFrameworkCore;
using ChatbotApi.Controllers;
using ChatBotTeste.Models;

namespace ChatbotApi.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Aplicacao> Aplicacao { get; set; }
}

