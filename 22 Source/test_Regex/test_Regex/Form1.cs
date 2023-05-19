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

namespace test_Regex
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //https://www.csharpstudy.com/Practical/Prac-regex-1.aspx
            //Regex 문자열 패턴 찾기
        }

        string str = "서울시 강남구 역삼동 강남아파트";


        //특정 문자 패턴을 찾기
        private void btnStrPattern_Click(object sender, EventArgs e)
        {

            // Ex1. 첫 매치 문자열 출력
            Regex regex = new Regex("강남");
            Match m = regex.Match(str);
            if (m.Success)
            {
                Console.WriteLine("{0}:{1}", m.Index, m.Value);
            }

            // Ex2. 매치된 문자열 계속 출력
            regex = new Regex("강남");
            m = regex.Match(str);
            while (m.Success)
            {
                Console.WriteLine("{0}:{1}", m.Index, m.Value);
                m = m.NextMatch();
            }

            // Ex3. Matches() 메서드
            regex = new Regex("강남");
            MatchCollection mc = regex.Matches(str);
            foreach (Match match in mc)
            {
                Console.WriteLine("{0}:{1}", match.Index, match.Value);
            }
        }

        private void btnMetaCharacter_Click(object sender, EventArgs e)
        {
            MatchCollection mc = Regex.Matches(str, @"^강\w*구$");

            //            메타문자 의미
            //------------------------
            //^라인의 처음
            //$        라인의 마지막    
            //\w 문자(영숫자) [a-zA - Z_0 - 9]
            //\s Whitespace(공백, 뉴라인, 탭..)
            //\d 숫자
            //*Zero 혹은 그 이상
            //+ 하나 이상
            //? Zero 혹은 하나
            //.        Newline을 제외한 한 문자
            //[]      가능한 문자들
            //[^ ]     가능하지 않은 문자들
            //[- ]    가능 문자 범위
            //{ n,m}
            //            최소 n개, 최대 m개
            //   ()     그룹
            // | 논리 OR
        }

        private void btnStrSplit_Click(object sender, EventArgs e)
        {
            Regex regex = new Regex(" ");
            string[] vals = regex.Split(str); //Regex 의 Split은 String의 Split보다 15배 가량 더 느립니다
            foreach (string s in vals)
            {
                Console.WriteLine(s);
            }
        }

        private void btnMatchGroup_Click(object sender, EventArgs e)
        {
            // Ex1
            string str = "강남빌라 역삼아파트 서초APT";
            Regex regex = new Regex(@"(아파트|APT)");
            MatchCollection mc = regex.Matches(str);
            foreach (Match m in mc)
            {
                // (Captured) Group은 1부터
                Group g = m.Groups[1];
                Console.WriteLine("{0}:{1}", g.Index, g.Value);
            }

            // Ex2
            str = "<ul><li>홈페이지</li><li>주문메뉴</li></ul>";
            regex = new Regex(@"<li>(\w+)</li>");
            mc = regex.Matches(str);
            foreach (Match m in mc)
            {
                Group g = m.Groups[1];
                Console.WriteLine("{0}:{1}", g.Index, g.Value);
            }

            // Ex3
            str = "02-632-5432; 032-645-7361";
            regex = new Regex(@"(\d+)-(\d+-\d+)");
            mc = regex.Matches(str);
            foreach (Match m in mc)
            {
                for (int i = 1; i < m.Groups.Count; i++)
                {
                    Group g = m.Groups[i];
                    Console.WriteLine("{0}:{1}", g.Index, g.Value);
                }
            }
        }

        private void btnNamedGroup_Click(object sender, EventArgs e)
        {
            // Ex1. named group (?<name> )
            string str = "02-632-5432; 032-645-7361";
            Regex regex = new Regex(@"(?<areaNo>\d+)-(?<phoneNo>\d+-\d+)");
            MatchCollection mc = regex.Matches(str);
            foreach (Match m in mc)
            {
                string area = m.Groups["areaNo"].Value;
                string phone = m.Groups["phoneNo"].Value;
                Console.WriteLine("({0}) {1}", area, phone);
            }

            // Ex2
            str = "<div><a href='www.sqlmgmt.com'>SQL Tools</a></div>";
            string patt = @"<a[^>]*href\s*=\s*[""']?(?<href>[^""'>]+)[""']?";
            Match match = Regex.Match(str, patt);
            Group g = match.Groups["href"];
            Console.WriteLine(g.Value);
        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            // Ex1. 앞 공백 제거
            string str = "   서울시 강남구 역삼동 강남아파트 1  ";
            string patten = @"^\s+";
            // 앞뒤 공백 모두 제거시:  @"^\s+|\s+$";

            Regex regex = new Regex(patten);
            string s = regex.Replace(str, "");
            Console.WriteLine(s);

            // Ex2
            str = "02-632-5432; 032-645-7361";
            patten = @"(?<areaNo>\d+)-(?<phoneNo>\d+-\d+)";
            regex = new Regex(patten);
            s = regex.Replace(str, @"(${areaNo}) ${phoneNo}");
            Console.WriteLine(s);
        }

        private void btnValidEmail_Click(object sender, EventArgs e)
        {
            bool valid = IsValidEmail("");
        }

        public bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?");
        }
    }
}
