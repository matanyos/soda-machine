using SodaMachine.Models.Enums;
using SodaMachine.Models;

namespace SodaMachine
{
    public static class InventoryExtensionMethods
    {
        public static void TakeOneSodaOut(this Inventory inventory, SodaType type)
        {
            var toUpdate = inventory.Sodas.Single(x => x.Type == type);
            var newSoda = new Soda(toUpdate.Type, toUpdate.Price, toUpdate.Quantity - 1);

            inventory.Sodas.Remove(toUpdate);
            inventory.Sodas.Add(newSoda);
        }
    }
}
