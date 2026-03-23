using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiculoController : ControllerBase, IVehiculoController
    {
        private IVehiculoFlujo _vehiculoFlujo;
        private ILogger<VehiculoController> _logger;

        public VehiculoController(IVehiculoFlujo vehiculoFlujo, ILogger<VehiculoController> logger)
        {
            _vehiculoFlujo = vehiculoFlujo;
            _logger = logger;
        }

        #region Operaciones
        [HttpPost]
        public async Task<ActionResult> Agregar(VehiculoRequest vehiculo)
        {
            var resultado = await _vehiculoFlujo.Agregar(vehiculo);
            return CreatedAtAction(nameof(Obtener), new { Id = resultado }, null);
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult> Editar(Guid Id, VehiculoRequest vehiculo)
        {
            if (!await VerificarVehiculoExiste(Id))
                return NotFound("El vehiculo no existe");

            var resultado = await _vehiculoFlujo.Editar(Id, vehiculo);
            return Ok(resultado);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> Eliminar(Guid Id)
        {
            if (!await VerificarVehiculoExiste(Id))
                return NotFound("El vehiculo no existe");

            var resultado = await _vehiculoFlujo.Eliminar(Id);
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult> Obtener()
        {
            var resultado = await _vehiculoFlujo.Obtener();
            if (!resultado.Any())
                return NoContent();
            return Ok(resultado);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult> Obtener(Guid Id)
        {
            var resultado = await _vehiculoFlujo.Obtener(Id);
            return Ok(resultado);
        }
        #endregion Operaciones

        #region Helpers
        private async Task<bool> VerificarVehiculoExiste(Guid Id)
        {
            var resultadoValidacion = false;
            var resultado = await _vehiculoFlujo.Obtener(Id);
            if (resultado != null)
                resultadoValidacion = true;
            return resultadoValidacion;
        }

        #endregion Helpers
    }
}
