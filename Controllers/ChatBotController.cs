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
            string response = GetBotResponse(message.message, message.url);
            return Ok(new { response });
        }

        private string GetBotResponse(string userMessage, string urlDoUsuario)
        {
            string lowerMessage = userMessage.ToLower();
            // Obtendo dados de aplicação e time
            var nomeAplicacao = _iaplicacaoRepository.GetNomeDaAplicacaoPorURL(urlDoUsuario);
            var timeDev = _iaplicacaoRepository.GetTimeDevPorURL(urlDoUsuario);

            if (string.IsNullOrEmpty(userMessage))
            {
                if (urlDoUsuario == null)
                {
                    return "Aplicação não encontrada.";
                }

                // Pergunta sobre a abertura do ticket
                return $"Você está na aplicação {nomeAplicacao}. \nGostaria de abrir um ticket para o time {timeDev}? Responda 'sim' ou 'não'.";
            }
            else if (lowerMessage == "abrir ticket")
            {
                return $"Você está na aplicação {nomeAplicacao}. \nGostaria de abrir um ticket para o time {timeDev}? Responda 'sim' ou 'não'.";
            }
            else if (lowerMessage == "sim" || lowerMessage == "s" || lowerMessage == "yes" || lowerMessage == "y")
            {
                // Se o usuário disser 'sim', pede os dados do ticket
                return "Por favor, digite seu nome e descrição do problema separados por vírgula.";
            }
            else if (lowerMessage == "não" || lowerMessage == "nao" || lowerMessage == "no" || lowerMessage == "n")
            {
                // Se o usuário disser 'não', o bot diz que o ticket não será aberto
                return "Entendido. Não abriremos o ticket. Se mudar de ideia, é só me avisar!";
            }

            else if (lowerMessage.Contains(","))
            {
                var parts = userMessage.Split(',');
                if (parts.Length == 2)
                {
                    string nome = parts[0].Trim();
                    string descricao = parts[1].Trim();
                    return $"Ticket NC01020 aberto com sucesso, no nome de {nome}, sobre {descricao}, para o time {timeDev}.";
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


        // Modelo para receber mensagens do frontend
        public class UserMessage
        {
            public string message { get; set; }
            public string url { get; set; }
        }
    }
}