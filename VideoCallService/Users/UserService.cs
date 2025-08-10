using VideoCall.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoCall.DataAccess.Interface;
using VideoCall.Common.Configuration;

namespace VideoCall.Service.Users
{
    public class UserService : IUserService
    {
        private readonly ICommonRepository<User> _userRepository;
        private readonly ICommonUoW _commonUoW;

        public UserService(ICommonRepository<User> userRepository, ICommonUoW commonUoW)
        {
            _userRepository = userRepository;
            _commonUoW = commonUoW;
        }
        public List<User> GetAllUsers()
        {
            return AppConfigs.list;//_userRepository.FindAll().ToList();
        }
        public User Login(string username, string pass)
        {
            var user = AppConfigs.list.FirstOrDefault(x => x.Username == username && x.Password == pass);//_userRepository.FindSingle(u => u.Username == username && u.Password == pass);
            if (user!=null )
            {
                return user; // Đăng nhập thành công
            }
            return null; // Đăng nhập thất bại
        }
    }
}
