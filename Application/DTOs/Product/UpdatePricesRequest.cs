using Application.Constants;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Product
{
    public record UpdatePricesRequest(
        [Range(0.01, double.MaxValue, ErrorMessage = ErrorMessages.RetailPriceGreaterThanZero)]
        decimal RetailPrice,

        [Range(0.01, double.MaxValue, ErrorMessage = ErrorMessages.WholesalePriceGreaterThanZero)]
        decimal WholesalePrice
    );
}