CREATE PROCEDURE AgregarVehiculo

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
INSERT INTO [dbo].[Vehiculo]
           ([id]
           ,[idModelo]
           ,[Placa]
           ,[Color]
           ,[Anio]
           ,[Precio]
           ,[CorreoPropietario]
           ,[TelefonoPropietario])
     VALUES
    (@Id
    ,@IdModelo
    ,@Placa
    ,@Color
    ,@Anio
    ,@Precio
    ,@CorreoPropietario
    ,@TelefonoPropietario)

   SELECT @Id
   COMMIT TRANSACTION
END