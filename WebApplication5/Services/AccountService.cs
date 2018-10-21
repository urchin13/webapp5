using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebApplication5.EF;
using WebApplication5.Models;
using WebApplication5.Models.Base;

namespace WebApplication5.Services
{
    public class AccountService
    {
        
        private readonly ApplicationContext _context;

        public AccountService(ApplicationContext context)
        {
            _context = context;
        }

        //public HttpStatusCode CreateUser(RegistrationModel user)
        //{
        //    try
        //    {
        //        //_context.Users.Add(user);
        //        _context.SaveChanges();
        //        return HttpStatusCode.OK;
        //    }
        //    catch(Exception ex)
        //    {
        //        return HttpStatusCode.ExpectationFailed;
        //    }
        //}
    }
}
