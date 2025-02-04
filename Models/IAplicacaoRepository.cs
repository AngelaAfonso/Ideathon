namespace ChatBotTeste.Models
{
    public interface IAplicacaoRepository
    {
        void Add(Aplicacao aplicacao);

        List<Aplicacao> Get();

        Aplicacao? GetByID(int id);
        void PutByID(int id, Aplicacao aplicacao);
        void DeleteByID(int id);
    }
}
