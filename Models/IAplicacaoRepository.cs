namespace ChatBotTeste.Models
{
    public interface IAplicacaoRepository
    {
        void Add(Aplicacao aplicacao);

        List<Aplicacao> Get();

        IEnumerable<string> GetUrl();

        string GetNomeDaAplicacaoPorURL(string url);

        string GetTImeDevPorURL(string url);
        Aplicacao? GetByID(int id);
        void PutByID(int id, Aplicacao aplicacao);
        void DeleteByID(int id);
    }
}
