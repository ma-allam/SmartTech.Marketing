using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTech.Marketing.Core.Auth.User
{
    public class ActiveContext
    {
        public string UserName { get; set; }
        public int ClientId { get; set; }
        public static ActiveContext Build() => new ActiveContext();

    }
}
