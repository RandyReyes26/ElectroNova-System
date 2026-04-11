CREATE OR ALTER PROCEDURE usp_UPDATE_Producto_RestarExistencia
(
    @ID_Producto INT,
    @Cantidad INT
)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Producto
    SET Existencia = Existencia - @Cantidad
    WHERE ID_Producto = @ID_Producto
      AND Existencia >= @Cantidad;

    IF @@ROWCOUNT = 0
    BEGIN
        RAISERROR('No hay existencia suficiente para rebajar el producto.', 16, 1);
    END
END
GO