using System;
using Xunit;
using BLL;

namespace TestProject
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Class1 a = new Class1();
            a.calc();
        }
    }
}
