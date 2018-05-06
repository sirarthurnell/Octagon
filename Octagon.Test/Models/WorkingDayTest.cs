using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Octagon.Models;

namespace Octagon.Test.Models
{
    [TestClass]
    public class WorkingDayTest
    {
        [TestMethod]
        public void ShouldDayStartBeLessThanDayEnd()
        {
            var day = new WorkingDay(2018, 5, 6);

            Assert.IsTrue(day.DayStart < day.DayEnd);
        }
    }
}
