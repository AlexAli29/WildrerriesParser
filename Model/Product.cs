using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WildrerriesParser.Model
{
    internal class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Brand { get; set; }

        public decimal PriceU { get; set; }

        public int Feedbacks { get; set; }
    }
}
