using Microsoft.AspNetCore.Mvc;
using ChatBotTeste.Models;
using ChatBotTeste.ViewModel;

namespace ChatBotTeste.Controllers
{
    [ApiController]
    [Route("api/aplicacao")]
    public class AplicacaoController : ControllerBase
    {
        private readonly IAplicacaoRepository _iaplicacaoRepository;

        public AplicacaoController(IAplicacaoRepository iaplicacaoRepository)
        {
            _iaplicacaoRepository = iaplicacaoRepository ?? throw new ArgumentNullException(nameof(iaplicacaoRepository));
        }

        [HttpPost]
        public IActionResult Add([FromForm] AplicacaoViewModel aplicacaoview)
        {
            var aplicacao = new Aplicacao(aplicacaoview.url, aplicacaoview.NomeAplicacao, aplicacaoview.TimeDev);
            _iaplicacaoRepository.Add(aplicacao);
            return Ok(aplicacao);
        }

        [HttpGet]
        public IActionResult Get()
        {
            var aplicacao = _iaplicacaoRepository.Get();
            return Ok(aplicacao);
        }

        [HttpGet("{id}")]
        public IActionResult GetByID(int id)
        {
            var aplicacao = _iaplicacaoRepository.GetByID(id);

            if(aplicacao == null)
            {
                return NotFound();
            }
            return Ok(aplicacao);
        }

        [HttpPut("{id}")]
        public IActionResult PutByID(int id, AplicacaoViewModel aplicacaoView)
        {
            var aplicacao = new Aplicacao(aplicacaoView.url, aplicacaoView.NomeAplicacao, aplicacaoView.TimeDev);
            try
            {
                _iaplicacaoRepository.PutByID(id, aplicacao);
                return Ok(aplicacao);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteByID(int id)
        {
            var aplicacao = _iaplicacaoRepository.GetByID(id);
            if(aplicacao == null)
            {
                return NotFound();
            }
            _iaplicacaoRepository.DeleteByID(id);
            return Ok(aplicacao);
        }
    }
}
