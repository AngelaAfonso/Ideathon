using ChatBotTeste.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChatbotApi.Controllers
{
    [ApiController]
    [Route("api/chatbot")]
    public class ChatbotController : ControllerBase
    {
        private readonly IAplicacaoRepository _iaplicacaoRepository;

        public ChatbotController(IAplicacaoRepository iaplicacaoRepository)
        {
            _iaplicacaoRepository = iaplicacaoRepository ?? throw new ArgumentNullException(nameof(iaplicacaoRepository));
        }

        // Método para processar mensagens do usuário
        [HttpPost("message")]
        public IActionResult ProcessMessage([FromBody] UserMessage message)
        {
            string response = GetBotResponse(message.Text);
            return Ok(new { response });
        }

        private string GetBotResponse(string userMessage)
        {
            string lowerMessage = userMessage.ToLower();

            if (lowerMessage.Contains("abrir ticket"))
            {
                /*
                adicionar implementar a validação de url, pra descobir qual a aplicação o usuario esta
                 */
                var urlDoUsuario = "site que o usuario esta"; // descobrir como pegar esse dado
                var nomeAplicacao = _iaplicacaoRepository.GetNomeDaAplicacaoPorURL(urlDoUsuario);
                var timeDev = _iaplicacaoRepository.GetTimeDevPorURL(urlDoUsuario);
                if(urlDoUsuario == null)
                {
                    return "Aplication Not Found";
                }

                return $"Você gostaria de abrir um ticket sobre a aplicacao {nomeAplicacao} para o time {timeDev}?\nPor favor, digite seu nome e descrição separados por vírgula.";
            }
            else if (lowerMessage.Contains(","))
            {
                var parts = userMessage.Split(',');
                if (parts.Length == 2)
                {
                    string nome = parts[0].Trim();
                    string descricao = parts[1].Trim();
                    return $"Ticket NC01020 aberto com sucesso, no nome de {nome}, sobre {descricao}."; //sera alterado usando a automação com service now
                }
                else
                {
                    return "Entrada inválida. Certifique-se de digitar seu nome e descrição separados por vírgula.";
                }
            }
            else
            {
                return "Desculpe, não entendi sua mensagem. Você pode digitar 'abrir ticket' para iniciar.";
            }
        }
    }

    // Modelo para receber mensagens do frontend
    public class UserMessage
    {
        public string Text { get; set; }
    }
}