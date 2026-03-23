CREATE PROCEDURE EditarVehiculo

@Id AS uniqueidentifier
,@IdModelo AS uniqueidentifier
,@Placa AS varchar(max)
,@Color AS varchar(max)
,@Anio AS int
,@Precio AS decimal(18,0)
,@CorreoPropietario AS varchar(max)
,@TelefonoPropietario AS varchar(max)

AS
BEGIN

	SET NOCOUNT ON;
	
BEGIN TRANSACTION
UPDATE [dbo].[Vehiculo]
   SET 
       [idModelo] = @idModelo
      ,[Placa] = @Placa
      ,[Color] = @Color
      ,[Anio] = @Anio
      ,[Precio] = @Precio
      ,[CorreoPropietario] = @CorreoPropietario
      ,[TelefonoPropietario] = @TelefonoPropietario
 WHERE Id = @Id
 SELECT @Id
 COMMIT TRANSACTION
END