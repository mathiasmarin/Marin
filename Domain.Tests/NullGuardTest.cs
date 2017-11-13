using System;
using Domain.Common;
using Domain.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Domain.Tests
{
    [TestClass]
    public class NullGuardTest
    {
        [TestMethod]
        public void TestPropertyNullCheck()
        {
            #region Arrange

            var emptystring = String.Empty;


            #endregion

            #region Act



            #endregion

            #region Assert

            Assert.ThrowsException<NullGuardException>(() => new User(emptystring, emptystring, emptystring));

            #endregion
        }
    }
}
