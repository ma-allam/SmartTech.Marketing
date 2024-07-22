using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTech.Marketing.Application.Contract
{
    [AttributeUsage(AttributeTargets.Method)]
    public class NoCacheAttribute : Attribute
    {
    }
}
