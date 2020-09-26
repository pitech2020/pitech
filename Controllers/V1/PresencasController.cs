using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PiTech.API.Contracts.V1;
using PiTech.API.Contracts.V1.Requests;
using PiTech.API.Contracts.V1.Responses;
using PiTech.API.Domain;
using PiTech.API.Extensions;
using PiTech.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PiTech.API.Controllers.V1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PresencasController : Controller
    {
        private IPresencaService _presencaService;
        public PresencasController(IPresencaService presencaService)
        {
            this._presencaService = presencaService;
        }

        [HttpGet(ApiRoutes.Presencas.ObterTodas)]
        public async Task<IActionResult> ObterTodasPresencas()
        {
            return Ok((await _presencaService.ObterPresencasAsync()).Select(p => 
                new ObterPresencaResponse
                {
                    Id = p.Id,
                    Observacao = p.Observacao
                }));
        }

        [HttpDelete(ApiRoutes.Presencas.Deletar)]
        public async Task<IActionResult> DeleterPresenca([FromRoute] Guid id)
        {
            var presencaPertenceAoUsuario = await _presencaService.PresencaPertenceAoUsuarioAsync(id, HttpContext.ObterIdUsuario());

            if (!presencaPertenceAoUsuario)
            {
                return BadRequest(new { error = "Você não é proprietário desta presença" });
            }

            var deletou = await _presencaService.DeletarPresencaAsync(id);

            if (!deletou)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPut(ApiRoutes.Presencas.Atualizar)]
        public async Task<IActionResult> AtualizarPresenca([FromRoute] Guid id ,[FromBody] AtualizarPresencaPutRequest presencaPutRequest)
        {
            var presencaPertenceAoUsuario = await _presencaService.PresencaPertenceAoUsuarioAsync(id, HttpContext.ObterIdUsuario());

            if (!presencaPertenceAoUsuario)
            {
                return BadRequest(new { error = "Você não é proprietário desta presença" });
            }

            var presenca = await _presencaService.ObterPresencaAsync(id);
            presenca.Observacao = presencaPutRequest.Observacao;

            var atualizou = await _presencaService.AtualizaPresencaAsync(presenca);

            if (!atualizou)
            {
                return NotFound();
            }

            return Ok(presenca);
        }

        [HttpGet(ApiRoutes.Presencas.Obter)]
        public async Task<IActionResult> ObterPresenca([FromRoute]string id)
        {
            var presenca = await _presencaService.ObterPresencaAsync(Guid.Parse(id));

            if (presenca == null)
            {
                return NotFound();
            }

            var response = new ObterPresencaResponse
            {
                Id = presenca.Id,
                Observacao = presenca.Observacao
            };

            return Ok(response);
        }

        [HttpPost(ApiRoutes.Presencas.Criar)]
        public async Task<IActionResult> CriarPresenca([FromBody] CriarPresencaPostRequest presencaContract)
        {
            var presenca = new Presenca
            {
                Observacao = presencaContract.Observacao,
                UserId = HttpContext.ObterIdUsuario(),
            };

            var inserido = await _presencaService.CriarPresencaAsync(presenca);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var location = baseUrl + ApiRoutes.Presencas.Obter.Replace("{id}", presenca.Id.ToString());

            var response = new ObterPresencaResponse
            {
                Id = presenca.Id,
                Observacao = presenca.Observacao,
            };

            return Created(location, response);
        }
    }
}
