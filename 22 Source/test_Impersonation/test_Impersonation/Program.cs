using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace test_Impersonation
{
    class Program
    {
        static void Main(string[] args)
        {
            IntPtr token = IntPtr.Zero;
            IntPtr dupToken = IntPtr.Zero;
            WindowsIdentity impUser;
            WindowsImpersonationContext impCtx;

            //다른 사용자 계정으로 로그인
            //("User1", "Domain1", "Pwd1",
            if (!LogonUser("","","",
                LOGON32_LOGON_INFORACTIVE,
                LOGON32_PROVIDER_DEFAULT,
                ref token
                ))
            {
                Console.WriteLine("Logon failed");
                return;
            }
            
            if (!DuplicateToken(token, 2, ref dupToken))
            {
                Console.WriteLine("Dup failed");
                return;
            }

            // WindowsIdentity 객체 생성
            impUser = new WindowsIdentity(dupToken);

            // Impersonation : 
            // 여기서부터 다른 계정으로 사용됨
            impCtx = impUser.Impersonate();

            //... Impersonation 사용자 작업
            var mydoc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            foreach (var fn in Directory.GetFiles(mydoc))
            {
                Console.WriteLine(fn);
            }
            //... Imp 사용자 작업 완료

            // 원래 사용자로 복귀
            impCtx.Undo();

            CloseHandle(dupToken);
            CloseHandle(token);

        }

        const int LOGON32_LOGON_INFORACTIVE = 2;
        const int LOGON32_PROVIDER_DEFAULT = 0;
        const int SecurityImpersonation = 2;

        [DllImport("advapi32.dll")]
        public static extern bool LogonUser(String lpszUsername, String lpszDomain, String lpszPassword,
            int dwLogonType, int dwLogonProvider, ref IntPtr phToken);
        [DllImport("advapi32.dll", CharSet = CharSet.Auto)]
        public extern static bool DuplicateToken(IntPtr ExistingTokenHandle,
            int SECURITY_IMPERSONATION_LEVEL, ref IntPtr DuplicateTokenHandle);
        [DllImport("kernel32.dll")]
        public extern static bool CloseHandle(IntPtr handle);

    }
}
