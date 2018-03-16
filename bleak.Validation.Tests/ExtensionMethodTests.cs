using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace bleak.Validation.Tests
{
    [TestClass]
    public class ExtensionMethodTests
    {
        [TestMethod]
        public void TestRequiredString_Valid()
        {
            var inst = new TestClass1();
            inst.RequiredString = "Valid";
            inst.Validate();
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void TestRequiredString_Empty_ThrowsException()
        {
            try
            {
                var inst = new TestClass1();
                inst.RequiredString = string.Empty;
                inst.Validate();
                Assert.IsTrue(false);
            }
            catch
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void TestRequiredString_Whitespace_Valid()
        {
            try
            {
                var inst = new TestClass1();
                inst.RequiredString = " \n\t";
                inst.Validate();
                Assert.IsTrue(false);
            }
            catch
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void TestRequiredString_Null_ThrowsException()
        {
            try
            {
                var inst = new TestClass1();
                inst.RequiredString = null;
                inst.Validate();
                Assert.IsTrue(false);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is ValidationException);
            }
        }

        [TestMethod]
        public void TestRequiredString_Null_ValidExceptionMessage()
        {
            try
            {
                var inst = new TestClass1();
                inst.RequiredString = null;
                inst.Validate();
                Assert.IsTrue(false);
            }
            catch (System.Exception ex)
            {
                Assert.AreEqual("The RequiredString field is required.", ex.Message);
            }
        }

        [TestMethod]
        public void TestIntegerRange_JustRight()
        {
            var inst = new TestClass1();
            inst.RequiredString = "Valid";
            inst.IntegerRange = 10;
            inst.Validate();
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void TestIntegerRange_TooLow_Exception()
        {
            try
            {
                var inst = new TestClass1();
                inst.RequiredString = "Valid";
                inst.IntegerRange = -6;
                inst.Validate();
                Assert.IsTrue(false);
            }
            catch (System.Exception ex)
            {
                Assert.AreEqual("The field IntegerRange must be between -5 and 10.", ex.Message);
            }
        }

        [TestMethod]
        public void TestIntegerRange_TooHigh_Exception()
        {
            try
            {
                var inst = new TestClass1();
                inst.RequiredString = "Valid";
                inst.IntegerRange = 11;
                inst.Validate();
                Assert.IsTrue(false);
            }
            catch (System.Exception ex)
            {
                Assert.AreEqual("The field IntegerRange must be between -5 and 10.", ex.Message);
            }
        }

        [TestMethod]
        public void TestWholeObject_Validation()
        {
            try
            {
                var inst = new TestClass1();
                inst.RequiredString = null;
                inst.IntegerRange = 11;
                inst.Validate();
                Assert.IsTrue(false);
            }
            catch (System.Exception ex)
            {
                Assert.AreEqual("The RequiredString field is required.\nThe field IntegerRange must be between -5 and 10.", ex.Message);
            }
        }
    }

    public class TestClass1
    {
        [Required()]
        public string RequiredString { get; set; }

        [Range(-5, 10)]
        public int IntegerRange { get; set; }
    }
}
