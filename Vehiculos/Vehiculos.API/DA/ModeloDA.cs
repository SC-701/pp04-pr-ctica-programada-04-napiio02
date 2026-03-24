using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;

namespace DA
{
    public class ModeloDA : IModeloDA
    {
        private IRepositorioDapper _repositorioDapper;
        private SqlConnection _sqlConnection;

        #region Constructor
        public ModeloDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorio();
        }
        #endregion

        #region Operaciones
        public async Task<IEnumerable<ModeloResponse>> ObtenerPorMarca(Guid IdMarca)
        {
            string query = @"ObtenerModelosPorMarca";

            var resultadoConsulta = await _sqlConnection.QueryAsync<ModeloResponse>(
                query,
                new { IdMarca = IdMarca }
            );

            return resultadoConsulta;
        }
        #endregion
    }
}
