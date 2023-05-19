using NetFwTypeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_Firewall
{
    class FirewallAppInfo
    {

        public bool mListAdded;
        public bool mEnabled;
    }

    class MyFirewallManager
    {
        private const string CLSID_FIREWALL_MANAGER = "{304CE942-6E39-40D8-943A-B913C40C9CD4}";
        private INetFwMgr mFirewallMng = null;
        public MyFirewallManager()
        {
            // firewall manager 객체 생성            
            Type objectType = Type.GetTypeFromCLSID(new Guid(CLSID_FIREWALL_MANAGER));
            mFirewallMng = Activator.CreateInstance(objectType) as INetFwMgr;
        }

        public FirewallAppInfo getAppInfo(string appPathName)
        {
            INetFwAuthorizedApplication authoredApp = findApp(appPathName);
            FirewallAppInfo appInfo = new FirewallAppInfo();
            if (authoredApp == null)
            {
                appInfo.mListAdded = false; appInfo.mEnabled = false;
            }
            else
            {
                appInfo.mListAdded = true;
                appInfo.mEnabled = authoredApp.Enabled;
            }
            return appInfo;
        }

        private INetFwAuthorizedApplication findApp(string appPathName)
        {
            foreach (INetFwAuthorizedApplication app in mFirewallMng.LocalPolicy.CurrentProfile.AuthorizedApplications)
            {
                // 일치하는 앱을 찾음               
                if (app.ProcessImageFileName.ToLower().Equals(appPathName.ToLower())) { return app; }
            }
            return null;
        }

    }
}
