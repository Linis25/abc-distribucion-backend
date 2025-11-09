using Application.Constants;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Product
{
    public record CreateProductRequest(
        [Required(ErrorMessage = ErrorMessages.RequiredField)]
        [StringLength(20)]
        string Sku,

        [Required(ErrorMessage = ErrorMessages.RequiredField)]
        [StringLength(100)]
        string Name,

        string? Description,

        [Range(0.01, double.MaxValue, ErrorMessage = ErrorMessages.RetailPriceGreaterThanZero)]
        decimal RetailPrice,

        [Range(0.01, double.MaxValue, ErrorMessage = ErrorMessages.WholesalePriceGreaterThanZero)]
        decimal WholesalePrice,

        [Required(ErrorMessage = ErrorMessages.RequiredField)]
        int PalletId,

        [Required(ErrorMessage = ErrorMessages.RequiredField)]
        [Range(1, int.MaxValue, ErrorMessage = ErrorMessages.PositionGreaterThanZero)]
        int PositionNumber,

        [Range(1, int.MaxValue, ErrorMessage = ErrorMessages.QuantityGreaterThanZero)]
        int Quantity
    );
}