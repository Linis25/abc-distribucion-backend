namespace Application.Constants
{
    public static class ErrorMessages
    {
        //General
        public const string RequiredField = "Este campo es obligatorio.";
        public const string InvalidCredentials = "Credenciales inválidas.";

        //Productos
        public const string ProductNotFound = "Producto no existente.";
        public const string ProductExists = "El producto ya existe";
        public const string ProductCreated = "Producto creado correctamente.";
        public const string ProductUpdated = "Precios actualizados correctamente.";
        public const string ProductStockUpdated = "Existencias actualizadas correctamente.";

        //Estibas / Inventario
        public const string PalletNotFound = "Estiba no encontrada.";
        public const string PositionOccupied = "La posición ya está ocupada en esta estiba.";
        public const string InventoryNotFound = "No se encontró el producto en la estiba o posición especificada.";

        //Validaciones de dominio
        public const string RetailPriceGreaterThanZero = "El precio al detal debe ser mayor que cero.";
        public const string WholesalePriceGreaterThanZero = "El precio al por mayor debe ser mayor que cero.";
        public const string PositionGreaterThanZero = "La posición debe ser mayor que cero.";
        public const string QuantityGreaterThanZero = "La cantidad debe ser mayor que cero.";
        public const string InvalidQuantityOrPrice = "El precio y la cantidad deben ser mayores que cero.";
        public const string NegativeQuantity = "La cantidad no puede ser negativa.";
        public const string InvalidPrice = "El precio debe ser mayor que cero.";
    }
}
