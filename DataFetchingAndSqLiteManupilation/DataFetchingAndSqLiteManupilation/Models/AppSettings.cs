using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFetchingAndSqLiteManupilation.Models
{
    public class AppSettings
    {
        public string email { get; set; }
        public string password { get; set; }
        public string getTokenEndPoint { get; set; }
        public string getProductEndPoint { get; set; }
    }
}
