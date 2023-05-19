using Microsoft.Win32;
using System;
using System.Reflection;
using System.Runtime.InteropServices;
using 

namespace MyCOMObj
{
    public class Register
    {
        //[Guid("BBFE9B53-3067-49F8-B257-43B8372187B6")]
        const string CLSID_KEY = "{BBFE9B53-3067-49F8-B257-43B8372187B6}";

        //[DllExport("DllRegisterServer", CallingConventions = CallingConvention.StdCall)]
        [DllExport]
        public static int DllRegisterServer()
        {
            try
            {
                if (IntPtr.Size == 4)
                {
                    RegisterDLL(RegistryView.Registry32);
                }
                else
                {
                    RegisterDLL(RegistryView.Registry64);
                }
            }
            catch (Exception e)
            {
                return Marshal.GetHRForException(e);
            }

            return 0;
        }

        private static void RegisterDLL(RegistryView regView)
        {
            using (RegistryKey clsRoot = RegistryKey.OpenBaseKey(RegistryHive.ClassesRoot, regView))
            {
                using (RegistryKey clsKey = clsRoot.OpenSubKey("CLSID", true))
                using (RegistryKey clsIdKey = clsKey.CreateSubKey(CLSID_KEY))
                {
                    using (RegistryKey inprocKey = clsIdKey.CreateSubKey("InProcServer32"))
                    {
                        inprocKey.SetValue(null, typeof(Register).Assembly.Location);
                        inprocKey.SetValue("ThreadingModel", "Apartment");
                    }
                }
            }
        }

    }
}
