using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assignment;

namespace AssignmentTesting
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestdoesMajorExist()
        {
            MainPage obj = new MainPage();

            obj.getMajors();
        }
    }
}
