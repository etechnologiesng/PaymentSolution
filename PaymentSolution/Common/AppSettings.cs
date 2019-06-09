using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentSolution.Common
{
    public class AppSettings
    {
        public string Site_URL { get; set; }
        public string Audience { get; set; }
        public string ExpireTime { get; set; }
        public string JWT_Secret { get; set; }
        public string DbConnection { get; set; }
    }
}
