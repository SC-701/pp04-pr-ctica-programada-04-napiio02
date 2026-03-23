using Abstracciones.Modelos;

namespace Abstracciones.Interfaces.DA
{
    public interface IModeloDA
    {
        Task<IEnumerable<ModeloResponse>> ObtenerPorMarca(Guid IdMarca);
    }
}

