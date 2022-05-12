namespace SodaMachine.Models
{
    public class Inventory
    {
        public Inventory(ICollection<Soda> sodas)
        {
            Sodas = sodas;
        }

        public ICollection<Soda> Sodas { get; }
    }
}
