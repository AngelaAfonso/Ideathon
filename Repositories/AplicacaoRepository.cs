using ChatbotApi.Context;
using ChatBotTeste.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatBotTeste.Repositories
{
    public class AplicacaoRepository : IAplicacaoRepository
    {
        private readonly AppDbContext _context;

        public AplicacaoRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Add(Aplicacao aplicacao)
        {
            _context.Aplicacao.Add(aplicacao);
            _context.SaveChanges();
        }

        public void DeleteByID(int id)
        {
            var aplicacaoExiste = _context.Aplicacao.Find(id);
            if(aplicacaoExiste == null)
            {
                throw new Exception("Aplication not found");
            }

            _context.Aplicacao.Remove(aplicacaoExiste);
            _context.SaveChanges();
        }

        public List<Aplicacao> Get()
        {
            return _context.Aplicacao.ToList();
        }

        public Aplicacao? GetByID(int id)
        {
            return _context.Aplicacao.Find(id);
        }

        public string GetNomeDaAplicacaoPorURL(string url)
        {
            return _context.Aplicacao
                           .Where(a => a.url == url)
                           .Select(a => a.NomeAplicacao)
                           .FirstOrDefault();
        }

        public string GetTImeDevPorURL(string url)
        {
            return _context.Aplicacao
                           .Where(a => a.url == url)
                           .Select(a => a.NomeAplicacao)
                           .FirstOrDefault();
        }

        public IEnumerable<string> GetUrl()
        {
            return _context.Aplicacao.Select( a => a.url).ToList();
        }

        public void PutByID(int id, Aplicacao aplicacao)
        {
            var aplicacaoExiste = _context.Aplicacao.Find(id);
            if( aplicacaoExiste == null)
            {
                throw new Exception("Aplication not found");
            }
            aplicacaoExiste.url = aplicacao.url;
            aplicacaoExiste.NomeAplicacao = aplicacao.NomeAplicacao;

            _context.Aplicacao.Update(aplicacaoExiste);
            _context.SaveChanges();
        }
    }
}
