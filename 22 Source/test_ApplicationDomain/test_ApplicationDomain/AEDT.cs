using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_ApplicationDomain
{
    [Serializable]
    class AEDT
    {
        public dynamic oAnsoftApp;
        public dynamic oDesktop;
        public bool connectAedtProgID()
        {
            try
            {
                Type ansoftType = Type.GetTypeFromProgID("Ansoft.ElectronicsDesktop.2022.2");


                if (ansoftType == null)
                    throw new Exception("Failed Run ElectronicsDesktop : " + "Ansoft.ElectronicsDesktop.2022.2");

                oAnsoftApp = Activator.CreateInstance(ansoftType);

                if (oAnsoftApp == null)
                {
                    return false;
                }
                restoreWindow();
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }

            return true;
        }

        private bool restoreWindow()
        {
            if (oAnsoftApp == null) return false;

            try
            {
                if (oDesktop == null)
                {
                    oDesktop = oAnsoftApp.GetAppDesktop();
                    oDesktop.RestoreWindow();
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

    }
}
