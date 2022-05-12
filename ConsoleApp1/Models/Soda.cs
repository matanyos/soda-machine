using SodaMachine.Models.Enums;

namespace SodaMachine.Models;

public class Soda
{
    public Soda(SodaType type, decimal price, int quantity)
    {
        Type = type;
        Price = price;
        Quantity = quantity;
    }

    public SodaType Type { get; }
    public decimal Price { get; }
    public int Quantity { get; }
}