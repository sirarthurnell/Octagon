using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Octagon.Models;

namespace Octagon.Test.Models
{
    [TestClass]
    public class WorkedTimeCalculatorTest
    {
        private WorkedTimeCalculator _calculator;

        [TestInitialize]
        public void TestInitialize()
        {
            _calculator = new WorkedTimeCalculator();
        }

        [TestMethod]
        public void ShouldReturnZeroTimeWithOneChecking()
        {
            var checkings = new List<Checking>
            {
                new Checking
                {
                    Direction = CheckingDirection.In,
                    Timestamp = new DateTime(2018, 5, 5)
                }
            };

            var timeAmount = _calculator.CalculateTotalTime(checkings);

            Assert.AreEqual(TimeSpan.Zero, timeAmount);
        }

        [TestMethod]
        public void ShouldReturnZeroTimeWithDegeneratedCheckings()
        {
            var checkings = new List<Checking>
            {
                new Checking
                {
                    Direction = CheckingDirection.In,
                    Timestamp = new DateTime(2018, 5, 5, 0, 0, 0)
                },
                new Checking
                {
                    Direction = CheckingDirection.In,
                    Timestamp = new DateTime(2018, 5, 5, 2, 0, 0)
                },
                new Checking
                {
                    Direction = CheckingDirection.In,
                    Timestamp = new DateTime(2018, 5, 5, 4, 0, 0)
                }
            };

            var timeAmount = _calculator.CalculateTotalTime(checkings);

            Assert.AreEqual(TimeSpan.Zero, timeAmount);
        }

        [TestMethod]
        public void ShouldReturnTimeBetweenTwoCheckings()
        {
            var checkings = new List<Checking>
            {
                new Checking
                {
                    Direction = CheckingDirection.In,
                    Timestamp = new DateTime(2018, 5, 5, 0, 0, 0)
                },
                new Checking
                {
                    Direction = CheckingDirection.Out,
                    Timestamp = new DateTime(2018, 5, 5, 2, 0, 0)
                },
                new Checking
                {
                    Direction = CheckingDirection.Out,
                    Timestamp = new DateTime(2018, 5, 5, 4, 0, 0)
                },
                new Checking
                {
                    Direction = CheckingDirection.In,
                    Timestamp = new DateTime(2018, 5, 5, 6, 0, 0)
                },
                new Checking
                {
                    Direction = CheckingDirection.Out,
                    Timestamp = new DateTime(2018, 5, 5, 8, 0, 0)
                },
                new Checking
                {
                    Direction = CheckingDirection.In,
                    Timestamp = new DateTime(2018, 5, 5, 10, 0, 0)
                }
            };

            var timeAmount = _calculator.CalculateTotalTime(checkings);

            Assert.AreEqual(4, timeAmount.Hours);
        }
    }
}
