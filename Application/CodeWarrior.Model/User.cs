using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.AspNet.Identity;

namespace CodeWarrior.Model
{
    public class User : IdentityUser
    {
        public string Email { get; set; }
    }
}
