CREATE   PROCEDURE ObtenerModelosPorMarca
    @IdMarca UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
 
    SELECT
        Id,
        IdMarca,
        Nombre
    FROM dbo.Modelo
    WHERE IdMarca = @IdMarca
    ORDER BY Nombre;
END