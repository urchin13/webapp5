using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication5.EF;
using WebApplication5.Models;

namespace WebApplication5.Services
{
    public class CatalogService
    {
        private readonly ApplicationContext _context;

        public CatalogService(ApplicationContext context)
        {
            _context = context;
        }

        public List<Phone> GetAllPhones()
        {
            var result = _context.Phones.ToList();
            _context.Users.ToList();
            return  result;
        }

    }
}
