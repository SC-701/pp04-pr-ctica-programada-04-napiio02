CREATE TABLE [dbo].[Vehiculo] (
    [id]                  UNIQUEIDENTIFIER NOT NULL,
    [idModelo]            UNIQUEIDENTIFIER NOT NULL,
    [Placa]               VARCHAR (MAX)    NOT NULL,
    [Color]               VARCHAR (MAX)    NOT NULL,
    [Anio]                INT              NOT NULL,
    [Precio]              DECIMAL (18)     NOT NULL,
    [CorreoPropietario]   VARCHAR (MAX)    NOT NULL,
    [TelefonoPropietario] VARCHAR (MAX)    NOT NULL,
    CONSTRAINT [PK_Vehiculo] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_Vehiculo_Modelos] FOREIGN KEY ([idModelo]) REFERENCES [dbo].[Modelo] ([id])
);

