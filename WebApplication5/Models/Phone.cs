using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication5.Models
{
    public class Phone
    {
        public int Id { get; set; }
        public string ModelName { get; set; }
        public string CompanyName { get; set; }
        public decimal Price { get; set; }
        public int Year { get; set; }
    }
}
