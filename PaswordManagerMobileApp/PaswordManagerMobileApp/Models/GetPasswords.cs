using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaswordManagerMobileApp.Models
{
    class GetPasswords
    {
        public int passwordId { get; set; }
        public string serviceName { get; set; }
        public string login { get; set; }
        public string passwordValue { get; set; }
        public string serviceUrl { get; set; }
        public string timestamp { get; set; }
        public string userId { get; set; }

    }
}
