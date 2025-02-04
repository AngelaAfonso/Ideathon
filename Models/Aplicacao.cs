using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ChatBotTeste.Models
{
    public class Aplicacao
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string? url { get; set; }
        public string? NomeAplicacao { get; set; }

        public string? TimeDev {  get; set; }

        public Aplicacao() { }

        public Aplicacao(string url, string NomeAplicacao, string TimeDev)
        {
            this.url = url;
            this.NomeAplicacao = NomeAplicacao;
            this.TimeDev = TimeDev;
        }
    }
}
