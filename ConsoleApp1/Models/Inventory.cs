using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
