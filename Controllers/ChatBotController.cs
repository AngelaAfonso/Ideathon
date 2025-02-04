using Microsoft.AspNetCore.Mvc;

namespace ChatbotApi.Controllers
{
    [ApiController]
    [Route("api/chatbot")]
    public class ChatbotController : ControllerBase
    {
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
                return "Você gostaria de abrir um ticket? Por favor, digite seu nome e descrição separados por vírgula.";
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