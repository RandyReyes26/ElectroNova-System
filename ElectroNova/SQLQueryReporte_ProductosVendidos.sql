CREATE PROCEDURE usp_Reporte_ProductosVendidos
    @ID_Marca INT = NULL,
    @ID_Modelo INT = NULL,
    @ID_Tipo INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        p.ID_Producto,
        p.Codigo_Barras,
        m.Nombre_Marca,
        mo.Codigo_Modelo,
        td.Nombre_TipoDispositivo,
        SUM(df.Cantidad) AS CantidadVendida,
        SUM(df.Total) AS TotalVendido,
        p.Fotografia
    FROM Producto p
    INNER JOIN Marca m 
        ON p.ID_Marca = m.ID_Marca
    INNER JOIN Modelo mo 
        ON p.ID_Modelo = mo.ID_Modelo
    INNER JOIN TipoDispositivo td 
        ON p.ID_TipoDispositivo = td.ID_TipoDispositivo
    INNER JOIN DetalleFactura df 
        ON p.ID_Producto = df.ID_Producto
    INNER JOIN Factura f 
        ON df.ID_Factura = f.ID_Factura
    WHERE
        (@ID_Marca IS NULL OR p.ID_Marca = @ID_Marca)
        AND (@ID_Modelo IS NULL OR p.ID_Modelo = @ID_Modelo)
        AND (@ID_Tipo IS NULL OR p.ID_TipoDispositivo = @ID_Tipo)
    GROUP BY
        p.ID_Producto,
        p.Codigo_Barras,
        m.Nombre_Marca,
        mo.Codigo_Modelo,
        td.Nombre_TipoDispositivo,
        p.Fotografia
    ORDER BY
        m.Nombre_Marca,
        mo.Codigo_Modelo,
        td.Nombre_TipoDispositivo;
END
GO