//^\s *[\+-] ?\s ?\$?\s ? (\d *\.?\d{ 2}?){ 1}$

//  ^ 문자열의 시작 부분에서 시작합니다.
//  \s * 0개 이상의 공백 문자가 일치하는지 확인합니다.
//  [\+-]?	양수 기호 또는 음수 기호를 0개 또는 1개 일치합니다.
//  \s? 0번 이상 나오는 공백 문자를 찾습니다.
//  \$?	달러 기호가 0개 또는 1개 일치하는지 확인합니다.
//  \s? 0번 이상 나오는 공백 문자를 찾습니다.
//  \d* 0번 이상 나오는 10진수를 찾습니다.
//  \.?	0 또는 10진수 기호와 일치합니다.
//  (\d{ 2})?	그룹 1 캡처: 소수 자릿수 2자리를 0 또는 1회 일치시킵니다.
//  (\d *\.?(\d{ 2})?){ 1} 소수점 기호로 구분된 정수 및 소수 자릿수의 패턴을 한 번 이상 일치시킵니다.
//  $	문자열의 끝을 찾습니다.
//  \b 단어 경계에서 일치를 시작합니다.
//  (?< word >\w +)    단어 경계까지 하나 이상의 단어 문자를 찾습니다.캡처된 그룹의 word이름을 지정합니다.
//  \s + 하나 이상의 공백 문자를 찾습니다.
//  (\k<word>)	이름이 word지정된 캡처된 그룹과 일치합니다.
//  \b 단어 경계를 찾습니다.


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test_RegularExpression
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            sample();
        }


        void sample()
        {
            //https://learn.microsoft.com/ko-kr/dotnet/api/system.text.regularexpressions.regex?view=net-6.0

            //다음 예제에서는 정규식을 사용하여 문자열에서 반복되는 단어 발생을 확인합니다.정규식 \b(?< word >\w +)\s + (\k<word>)\b 은 다음 표와 같이 해석할 수 있습니다.

            //  \b 단어 경계에서 일치를 시작합니다.
            //  (?< word >\w +)    단어 경계까지 하나 이상의 단어 문자를 찾습니다.캡처된 그룹의 word이름을 지정합니다.
            //  \s + 하나 이상의 공백 문자를 찾습니다.
            //  (\k<word>)	이름이 word지정된 캡처된 그룹과 일치합니다.
            //  \b 단어 경계를 찾습니다.


            Regex rx = new Regex(@"\b(?<word>\w+)\s+(\k<word>)\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            string text = "The the quick brown fox  fox jumps over the lazy dog dog.";

            // Find matches.
            MatchCollection matches = rx.Matches(text);

            foreach (Match match in matches)
            {
                GroupCollection groups = match.Groups;
                Console.WriteLine("'{0}' repeated at positions {1} and {2}",
                                  groups["word"].Value,
                                  groups[0].Index,
                                  groups[1].Index);
            }

            // The example produces the following output to the console:
            //       3 matches found in:
            //          The the quick brown fox  fox jumps over the lazy dog dog.
            //       'The' repeated at positions 0 and 4
            //       'fox' repeated at positions 20 and 25
            //       'dog' repeated at positions 49 and 53
        }

        void sample2()
        {
       

        }



    }
}
