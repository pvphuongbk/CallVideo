using Microsoft.Extensions.Configuration;
using System.ComponentModel;
using VideoCall.DataAccess.Entities;

namespace VideoCall.Common.Configuration
{
	public class AppConfigs
	{
		private static IConfiguration _configuration;
		public static void LoadAll(IConfiguration configuration)
		{
			_configuration = configuration;
            SqlConnection = GetConfigValue("ConnectionStrings:VideoCallDb", "No connection");

        }

        public static string SqlConnection { get; set; }=string.Empty;
        public static List<User> list = new List<User>
        {
            new User
            {
                Id = Guid.NewGuid(),
                Username = "tam",
                Password = "123",
                Fullname = "Tâm",
                CallId = string.Empty,
                Status = false,
            },
            new User
            {
                Id = Guid.NewGuid(),
                Username = "phuong",
                Password = "123",
                Fullname = "Phương",
                CallId = string.Empty,
                Status = false,
            },
            new User
            {
                Id = Guid.NewGuid(),
                Username = "duc",
                Password = "123",
                Fullname = "Đức",
                CallId = string.Empty,
                Status = false,
            },
            new User
            {
                Id = Guid.NewGuid(),
                Username = "cong",
                Password = "123",
                Fullname = "Công",
                CallId = string.Empty,
                Status = false,
            },
            new User
            {
                Id = Guid.NewGuid(),
                Username = "hung",
                Password = "123",
                Fullname = "Hùng",
                CallId = string.Empty,
                Status = false,
            },
        };
        public static string SecretKey { get; set; } = "12345678901234567890123456789012"; // 32 ký tự
        public static string CurrentUserCK = "CallUser";
        public static string CurrentUserAdmin = "CallAdmin";

        /// <summary>
        /// Lấy ra giá trị config trong file .config
        /// </summary>
        private static T GetConfigValue<T>(string configKey, T defaultValue)
		{
			var value = defaultValue;
			var converter = TypeDescriptor.GetConverter(typeof(T));
			try
			{
				if (converter != null)
				{
					var setting = _configuration.GetSection(configKey).Value;
					if (!string.IsNullOrEmpty(setting))
					{
						value = (T)converter.ConvertFromString(setting);
					}
				}
			}
			catch
			{
				value = defaultValue;
			}
			return value;
		}

        public static string Activecss(int tab, int tabactive)
		{
			if(tab == tabactive)
			{
				return "active";
			}
			else
			{
				return "";
			}
		}
    }
}
