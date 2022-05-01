using System;
using System.Runtime.InteropServices.ComTypes;

namespace TitaniumAS.Opc.Client.Interop.Helpers
{
    internal static class FileTimeConverter
    {
        public static DateTimeOffset FromFileTime(FILETIME fileTime)
        {
            //源代码有BUG
            //var lft = (((long) fileTime.dwHighDateTime) << 32) + fileTime.dwLowDateTime;
            //BUG Fixed: https://github.com/titanium-as/TitaniumAS.Opc.Client/issues/12
            var lft = (((long)fileTime.dwHighDateTime) << 32) + (fileTime.dwLowDateTime & 0xffffffff);
            return DateTimeOffset.FromFileTime(lft);
        }

        public static FILETIME ToFileTime(DateTimeOffset fileTime)
        {
            var lft = fileTime.ToFileTime();
            FILETIME ft;
            ft.dwLowDateTime = (int) (lft & 0xFFFFFFFF);
            ft.dwHighDateTime = (int) (lft >> 32);
            return ft;
        }
    }
}
