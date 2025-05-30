namespace MFormatik.Application.Helpers;

public static partial class OrderCalculationHelper
{
    // le montant = Prix * Quantité
    public static decimal CalculateAmount(decimal price, int quantity)
    {
        return price * quantity;
    }

    // le montant net = Montant - (Montant * Pourcentage de remise / 100)
    public static decimal CalculateNetAmount(decimal amount, decimal discountPercentage)
    {
        var discountDecimal = discountPercentage / 100m;
        return amount - (amount * discountDecimal);
    }

    // le total = Somme des montants nets
    public static decimal CalculateTotal(IEnumerable<decimal> netAmounts)
    {
        return netAmounts.Sum();
    }

    // le total net = Total - (Total * Pourcentage de remise global / 100)
    public static decimal CalculateTotalNet(decimal total, decimal overallDiscountPercentage)
    {
        var discountDecimal = overallDiscountPercentage / 100m;
        return total * (1 - discountDecimal);
    }
}

