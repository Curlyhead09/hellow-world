using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test_CreateParams
{
    class myIconButton : Button
    {
        private Icon icon;

        public myIconButton()
        {
            FlatStyle = FlatStyle.System;
        }


        public myIconButton(Icon buttonIcon) : this()
        {
            // Assign the icon to the private field.   
            this.icon = buttonIcon;

            // Size the button to 4 pixels larger than the icon.
            this.Height = icon.Height + 4;
            this.Width = icon.Width + 4;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                // Extend the CreateParams property of the Button class.
                CreateParams cp = base.CreateParams;
                // Update the button Style.
                //창 스타일 값의 비트 조합을 가져오거나 설정합니다.
                cp.Style |= 0x00000040; // BS_ICON value 

                return cp;
            }
        }

        public Icon Icon
        {
            get
            {
                return icon;
            }

            set
            {
                icon = value;
                UpdateIcon();
                // Size the button to 4 pixels larger than the icon.
                this.Height = icon.Height + 4;
                this.Width = icon.Width + 4;
            }
        }

        private void UpdateIcon()
        {
            IntPtr iconHandle = IntPtr.Zero;

            // Get the icon's handle.
            if (icon != null)
            {
                iconHandle = icon.Handle;
            }

            // Send Windows the message to update the button. 
            SendMessage(Handle, 0x00F7 /*BM_SETIMAGE value*/, 1 /*IMAGE_ICON value*/, (int)iconHandle);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);


        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            // Update the icon on the button if there is currently an icon assigned to the icon field.
            if (icon != null)
            {
                UpdateIcon();
            }
        }

    }
}
