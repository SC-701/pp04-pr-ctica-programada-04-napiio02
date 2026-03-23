CREATE   PROCEDURE dbo.ObtenerVehiculo
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
 
    SELECT
        v.Id,
        v.IdModelo,
        v.Placa,
        v.Color,
        v.Anio,
        v.Precio,
        v.CorreoPropietario,
        v.TelefonoPropietario,
        mo.Nombre AS Modelo,
        ma.Nombre AS Marca
    FROM dbo.Vehiculo v
    INNER JOIN dbo.Modelo mo ON v.IdModelo = mo.Id
    INNER JOIN dbo.Marca  ma ON mo.IdMarca = ma.Id
    WHERE v.Id = @Id;
END