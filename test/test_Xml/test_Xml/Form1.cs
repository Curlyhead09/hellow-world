/*


 요소(Element)와 특성(Attribute)
    데이터를 설명하고 특성은 요소에 대한 다양한 추가 기능들을 제공 

 Element
    포함된 데이터에 대해 설명하는 기능
    다른 요소들을 포함할 수 있음

    주의사항
        - XML은 대/소문자를 구별
        - 요소의 이름은 숫자나 밑줄 또는 "XML"이라는 문자로 시작될 수 없음
        - 요소의 이름에는 공백이 포함될 수 없음   
 
 Attribute
    요소를 설명
    기본 값을 할당할 수도 있고 순서에 상관없이 선언 될 수 있음
  */


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace test_Xml
{
    public partial class Form1 : Form
    {
        string m_startPath;
        string m_fullPath2;
        public Form1()
        {
            InitializeComponent();

            m_startPath = Application.StartupPath;

            m_fullPath2 = Path.Combine(m_startPath, "test2.xml");
        }



        private void btnXmlWriter_Click(object sender, EventArgs e)
        {
            string fullPath = Path.Combine(m_startPath, "test.xml");



            using (XmlWriter wr = XmlWriter.Create(fullPath))
            {
                wr.WriteStartDocument();
                wr.WriteStartElement("Employees");

                // Employee#1001
                wr.WriteStartElement("Employee");
                wr.WriteAttributeString("Id", "1001");  // attribute 쓰기
                wr.WriteElementString("Name", "Tim");   // Element 쓰기
                wr.WriteElementString("Dept", "Sales");
                wr.WriteEndElement();

                // Employee#1002
                wr.WriteStartElement("Employee");
                wr.WriteAttributeString("Id", "1002");
                wr.WriteElementString("Name", "John");
                wr.WriteElementString("Dept", "HR");
                wr.WriteEndElement();

                wr.WriteEndElement();
                wr.WriteEndDocument();
            }
        }

        private void btnXmlReader_Click(object sender, EventArgs e)
        {
            string fullPath = Path.Combine(m_startPath, "test.xml");


            using (XmlReader rd = XmlReader.Create(fullPath))
            {
                while (rd.Read())
                {
                    if (rd.IsStartElement())
                    {
                        // attribute 읽기                            
                        string id = rd["Id"]; // rd.GetAttribute("Id");

                        rd.Read();   // 다음 노드로 이동        

                        // Element 읽기
                        string name = rd.ReadElementContentAsString("Name", "");
                        string dept = rd.ReadElementContentAsString("Dept", "");

                        Console.WriteLine(id + "," + name + "," + dept);
                    }
                }

            }
        }


        /*
          Xml Document
            XML을 메모리에 로딩, DOM(Document Object Model)을 빌드 해당 방식은 특정 노드 검색, 필터링에 편리
            XML을 쉽게 생성

            큰 XML 데이타를 처리할 때 메모리가 많이 소비 됨
         */


        private void btnXmlDWriter_Click(object sender, EventArgs e)
        {
            XmlDocument xdoc = new XmlDocument();
            //루트 노드
            XmlNode root = xdoc.CreateElement("Employees");
            xdoc.AppendChild(root);

            //Employee #1001
            XmlNode emp1 = xdoc.CreateElement("Employee");
            XmlAttribute attr = xdoc.CreateAttribute("Id");
            attr.Value = "1001";
            emp1.Attributes.Append(attr);

            XmlNode name1 = xdoc.CreateElement("Name");
            name1.InnerText = "Tim";
            emp1.AppendChild(name1);

            XmlNode dept1 = xdoc.CreateElement("Dept");
            dept1.InnerText = "Sales";
            emp1.AppendChild(dept1);

            root.AppendChild(emp1);

            // Employee#1002
            var emp2 = xdoc.CreateElement("Employee");
            var attr2 = xdoc.CreateAttribute("Id");
            attr2.Value = "1002";
            emp2.Attributes.Append(attr2);

            var name2 = xdoc.CreateElement("Name");
            name2.InnerText = "John";
            emp2.AppendChild(name2);

            XmlNode dept2 = xdoc.CreateElement("Dept");
            dept2.InnerText = "HR";
            emp2.AppendChild(dept2);

            root.AppendChild(emp2);

            string fullPath = Path.Combine(m_startPath, "test2.xml");

            xdoc.Save(fullPath);
        }

        private void btnXmlDReader_Click(object sender, EventArgs e)
        {         
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(m_fullPath2);

            //특정 노드들을 필터링
            XmlNodeList nodes = xdoc.SelectNodes("/Employees/Employee");

            foreach (XmlNode emp in nodes)
            {
                string id = emp.Attributes["Id"].Value;

                // 특정 자식 Element 읽기
                string name = emp.SelectSingleNode("./Name").InnerText; //Relative Path 사용
                string dept = emp.SelectSingleNode("Dept").InnerText;   //간단히 자식 Element명 사용
                Console.WriteLine(id + "," + name + "," + dept);

                // 자식 노드들에 대해 Loop를 도는 예
                foreach (XmlNode child in emp.ChildNodes)
                {
                    Console.WriteLine("{0}: {1}", child.Name, child.InnerText);
                }
            }

            // 특정 Id 속성으로 하나의 Employee 검색 예
            XmlNode emp1002 = xdoc.SelectSingleNode("/Employees/Employee[@Id='1002']");
            Console.WriteLine(emp1002.InnerXml);
        }

        /*
            XML 노드들을 XPath를 사용하여 편리하게 엑세스
            XML 수정, 편집 용이
            XmlDocument의 SelectNodes() 혹은 SelectSingleNode() 메서드에서 사용하는 XPath는 실제 내부적으로 XPathNavigator를 사용
         */

        private void btnXmlNavigator_Click(object sender, EventArgs e)
        {
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(m_fullPath2);

            XPathNavigator nav = xdoc.CreateNavigator();
            XPathNodeIterator nodes = nav.Select("/Employees/Employee");

            while (nodes.MoveNext())
            {
                nodes.Current.MoveToChild("Name", "");

                // Tim을 Timothy로 변경
                if (nodes.Current.Value == "Tim")
                {
                    nodes.Current.SetValue("Timothy");
                }
            }

            // XML 전체 트리를 저장
            using (XmlWriter wr = XmlWriter.Create(m_startPath +"\\test3.xml"))
            {
                nav.WriteSubtree(wr);
            }
        }

        private void btnXElementWriter_Click(object sender, EventArgs e)
        {
            //using System.Xml.Linq;

            XDocument xdoc = new XDocument(new XDeclaration("1.0", "UTF-16", null));
            XElement xroot = new XElement("Employees");
            xdoc.Add(xroot);

            XElement xe1 = new XElement("Employee",
                new XAttribute("Id", "1001"),
                new XElement("Name", "Tim"),
                new XElement("Dept", "Sales")
            );

            XElement xe2 = new XElement("Employee",
                new XAttribute("Id", "1002"),
                new XElement("Name", "John"),
                new XElement("Dept", "HR")
            );

            xroot.Add(xe1);
            xroot.Add(xe2);

            xdoc.Save(m_startPath+"\\test4.xml");
        }

        private void btnXElementReader_Click(object sender, EventArgs e)
        {
            XDocument xdoc = XDocument.Load(m_startPath + "\\test4.xml");
            //XElement xe = XElement.Load(@"C:\Temp\Emp.xml");

            // <Employees> 노드 하나 리턴
            IEnumerable<XElement> elems = xdoc.Elements();

            // 복수 개의 <Employee> 노드들 리턴
            IEnumerable<XElement> emps = xdoc.Root.Elements();
            foreach (var emp in emps)
            {
                string id = emp.Attribute("Id").Value;
                string name = emp.Element("Name").Value;
                string dept = emp.Element("Dept").Value;

                Console.WriteLine(id + "," + name + "," + dept);
            }
        }

        private void btnLinq_Click(object sender, EventArgs e)
        {
            XElement xElem = XElement.Load(m_startPath + "\\test4.xml");

            // Id가 1002인 Employee 검색
            var result = from xe in xElem.Elements("Employee")
                         where xe.Attribute("Id").Value == "1002"
                         select xe;

            var emp = result.SingleOrDefault();
            if (emp != null)
            {
                string name = emp.Element("Name").Value;
                string dept = emp.Element("Dept").Value;
                Console.WriteLine("{0},{1}", name, dept);
            }

            // Id가 1000 보다 큰 Employee들 검색
            var emps = from xe in xElem.Elements("Employee")
                       where int.Parse(xe.Attribute("Id").Value) > 1000
                       select xe;

            foreach (var item in emps)
            {
                Console.WriteLine(item);
            }

            // LINQ 메서드 방식
            var empList = xElem.Elements("Employee").Where(p => p.Element("Name").Value == "Tim");
            foreach (var item in empList)
            {
                Console.WriteLine(item);
            }
        }

    }
}
