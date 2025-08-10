using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoCall.DataAccess.Entities;

namespace VideoCall.Service.Users
{
    public interface IUserService
    {
        public List<User> GetAllUsers();
        public User Login(string username, string pass);


    }
}
