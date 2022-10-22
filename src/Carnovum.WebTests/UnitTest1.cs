using System;
using Web.Helpers;
using Web.Helpers.CargaSii;
using Xunit;

namespace Carnovum.WebTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var target = new CargaSiiHelper();
            var result = target.LeerRegistrosCsv();
            Assert.True(result.Count > 0);
        }
    }
}
