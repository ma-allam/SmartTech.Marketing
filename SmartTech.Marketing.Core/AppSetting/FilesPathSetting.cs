using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTech.Marketing.Core.AppSetting
{
    public class FilesPathSetting
    {
        public static string SectionName { get; set; } = "UploadPath";
        public string? Path { get; set; }
    }
}
