using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModeloController : ControllerBase, IModeloController
    {
        private readonly IModeloFlujo _modeloCarroFlujo;

        public ModeloController(IModeloFlujo modeloCarroFlujo)
        {
            _modeloCarroFlujo = modeloCarroFlujo;
        }

        [HttpGet("{IdMarca}")]
        public async Task<IActionResult> ObtenerPorMarca(Guid IdMarca)
        {
            var resultado = await _modeloCarroFlujo.ObtenerPorMarca(IdMarca);
            return Ok(resultado);
        }
    }
}

