using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace SimpleMailCLI
{
    public class AppConfiguration
    {
        private readonly int _mailPort = 0;
        private readonly string _mailHost = string.Empty;
        private readonly string _mailUser = string.Empty;
        private readonly string _mailPassword = string.Empty;
        private static IConfiguration _configuration;

        public AppConfiguration()
        {
            SetConfiguration();

            _mailPort = Convert.ToInt32(_configuration.GetSection("Mail").GetSection("Port").Value);
            _mailHost = _configuration.GetSection("Mail").GetSection("Host").Value;
            _mailUser = _configuration.GetSection("Mail").GetSection("User").Value;
            _mailPassword = _configuration.GetSection("Mail").GetSection("Password").Value;
        }

        private void SetConfiguration()
        {
            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);
            _configuration = configurationBuilder.Build();
        }

        public IConfiguration Configuration
        {
            get
            {
                return _configuration;
            }
        }
        public string MailUser
        {
            get
            {
                return _mailUser;
            }
        }
        public int MailPort
        {
            get
            {
                return _mailPort;
            }
        }
        public string MailHost
        {
            get
            {
                return _mailHost;
            }
        }
        public string MailPassword
        {
            get
            {
                return _mailPassword;
            }
        }
    }
}