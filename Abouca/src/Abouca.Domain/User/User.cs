using System;
using System.Collections.Generic;
using System.Text;

namespace Abouca.Domain.User
{
    public class User:AggregateRoot,IUserDetails
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }

        public Information.Information About { get; set; }
        public int AboutId { get; set; }
    }
}
