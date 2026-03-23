

using Abstracciones.Models;
using Microsoft.AspNetCore.Mvc;

namespace Abstracciones.Interfaces.API
{
    public interface IVehiculoController
    {
        Task<ActionResult> Obtener();
        Task<ActionResult> Obtener(Guid Id);
        Task<ActionResult> Agregar(VehiculoRequest vehiculo);
        Task<ActionResult> Editar(Guid Id, VehiculoRequest vehiculo);
        Task<ActionResult> Eliminar(Guid Id);
    }
}
