using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test_Value_Reference_Type
{


    //    값형식
    //스택
    //입력한 데이터
    //해당 메소드 실행이 종료되면 사라짐int a = 1; int b = a; 위의 a, b의 값중 어느 하나의 값을 수정해도 다른쪽에는 영향을 끼치지 않는다
    //   값형식 -> 참조형식Boxing
    //   깊은복사
    //   bool, char, byte, decimal, double, enum, float, int long, short, sbyte, struct, uint, ulong, ushort

    //    참조형식
    //힙 메모리
    //할당된 곳에 저장되는 데이터
    //데이터가 위치한곳의 참조
    //   GC에 의해 정리됨.서로 다른 두 변수가 같은 데이터를 참조한다면 한개의 변수값을 변경하면 다른 변수도 영향을 받는다.
    //   참조형식 -> 값형식UnBoxing
    //   얕은복사
    //class, interface, delegate, object, string


    //    Call By Value
    //Call By Value에 의한 호출은 인수 값을 해당 함수의 형식 매개 변수로 복사합니다.
    //매개 변수는 다른 메모리 위치에 저장됩니다.
    //따라서 함수 내에서 변경된 사항은 호출자의 실제 매개 변수에 반영되지 않습니다.

    //    Call By Reference
    //Call By Reference에 의한 호출은 인수의 주소를 형식 매개 변수에 복사합니다.
    //이 메서드에서 주소는 함수 호출에 사용되는 실제 인수에 접근하는 데 사용됩니다. 
    //함수의 모든 연산은 실제 파라미터의 주소에 저장된 값에 대해 수행되며 수정된 값은 동일한 주소에 저장됩니다.
    //ref 키워드를 사용해서 메서드 호출에서 인수를 참조로 전달합니다.

    //-------------------------------------------
    //Call By Value에 의한 호출은 변수의 값을 함수에 전달합니다.함수 내부에서 전달 받은 값을 변경해도 원래 변수의 값은 변경되지 않습니다.
    //Call By Reference에 의한 호출은 변수 자체를 함수에 전달합니다.함수 내부에서 전달 받은 값의 변경 사항이 있을 경우 원래 변수에도 영향을 줍니다.
    //-------------------------------------------

    //    Call By Value와 Call By Reference의 주요 차이점
    //Call By Value 방식은 원래 값이 변경되지 않지만 Call By Reference 방식은 원래 값이 변경됩니다.
    //Call By Value 방식에서는 변수의 복사본이 전달되는 반면 Call By Reference 방식에서는 변수 자체가 전달됩니다.
    //Call By Value 방식에서는 인수는 다른 메모리 위치에서 생성되는 반면 Call By Reference 방식에서는 인수가 동일한 메모리 위치에 생성됩니다.
    //Call By Value 방식에서 인수의 수신 매개 변수는 데이터 유형과 함께 변수 이름입니다.반면 Call By Reference방식에서는 수신 매개 변수가 항상 데이터 유형과 함께 포인터 변수이고, 객체의 경우 클래스 유형과 함께 객체 이름입니다.


    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
    }
}
