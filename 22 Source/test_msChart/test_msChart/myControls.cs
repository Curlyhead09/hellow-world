using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test_msChart
{
    class myTableLayoutPanel : TableLayoutPanel
    {

        public myTableLayoutPanel()
        {
            //SetStyle(ControlStyles.AllPaintingInWmPaint |
            //  ControlStyles.OptimizedDoubleBuffer |
            //  ControlStyles.UserPaint, true);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                //Window Style
                // https://docs.microsoft.com/ko-kr/windows/win32/winmsg/window-styles?redirectedfrom=MSDN

                //참고
                //https://something-is-code.tistory.com/47

                CreateParams handleParam = base.CreateParams;
                //확장 창 스타일 값의 비트 조합을 가져오거나 설정합니다.
                //확장 창 스타일 값의 비트 조합입니다.
                handleParam.ExStyle |= 0x02000000;   // WS_EX_COMPOSITED       
                return handleParam;
            }
        }

    }
}
