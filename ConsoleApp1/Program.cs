using SodaMachine.Models;
using SodaMachine.Models.Enums;

namespace SodaMachine;

internal class Program
{
    private static void Main(string[] args)
    {
        var inventory = InitializeInventory();

        new SodaMachine(inventory).Start();
    }

    private static Inventory InitializeInventory()
    {
        return new Inventory(new List<Soda>
        {
            new(SodaType.Coke, 20, 5),
            new(SodaType.Fanta, 15, 3),
            new(SodaType.Sprite, 15, 3)
        });
    }
}