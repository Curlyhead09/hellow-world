using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace ComClassExample
{
    [Guid("7068AC34-DBB0-4e40-84A7-C2243355E2D7"),
    InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface IComClassExample
    {
        [DispId(1)]
        int AddTheseUp(int adder1, int adder2);
    }

    [Guid("863AEADA-EE73-4f4a-ABC0-3FB384CB41AA"),
    ClassInterface(ClassInterfaceType.None)]
    public class ComClassExample : IComClassExample
    {
        // constructor - does nothing in this example
        public ComClassExample() { }

        // a method that returns an int
        public int AddTheseUp(int adder1, int adder2)
        {
            return adder1 + adder2;
        }
    }


    //    Implementing COM OutOfProc Servers in C# .NET !!! 
    //; http://developerexperience.blogspot.kr/2006/04/implementing-com-outofproc-servers-in.html

    //C# DLL에서 Win32 C/C++처럼 dllexport 함수를 제공하는 방법
    //; https://www.sysnet.pe.kr/2/0/11052


}

