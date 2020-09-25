using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presence.API.Contracts.V1;
using Presence.API.Contracts.V1.Requests;
using Presence.API.Contracts.V1.Responses;
using Presence.API.Domain;
using Presence.API.Extensions;
using Presence.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presence.API.Controllers.V1
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PresencasController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        private IPresencaService _presencaService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="presencaService"></param>
        public PresencasController(IPresencaService presencaService)
        {
            this._presencaService = presencaService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>/
        /// <param name="presencaPutRequest"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="presencaContract"></param>
        /// <returns></returns>
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
