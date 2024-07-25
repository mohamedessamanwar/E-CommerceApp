using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.Helper
{
    public static class Helper
    {
        public const string ImagesPath = "C:\\Users\\AL-FAJR\\Desktop\\E-CommerceApp\\E-CommerceApp\\wwwroot\\Images\\Product";
        public static  string AllowedExtensions = ".jpg";
        public const int MaxFileSizeInMB = 1;
        public const int MaxFileSizeInBytes = MaxFileSizeInMB * 1024 * 1024;
        public static string URL = "https://localhost:7138/Images/Product"; 
    }
}
