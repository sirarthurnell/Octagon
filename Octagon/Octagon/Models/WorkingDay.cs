using System;
using System.Collections.Generic;
using System.Text;

namespace Octagon.Models
{
    /// <summary>
    /// Represents a day of work.
    /// </summary>
    public class WorkingDay
    {
        /// <summary>
        /// Gets the checkings that correspond to this day.
        /// </summary>
        public List<Checking> Checkings { get; private set; }

        /// <summary>
        /// Gets the year of the day.
        /// </summary>
        public int Year { get; private set; }

        /// <summary>
        /// Gets the month of the day.
        /// </summary>
        public int Month { get; private set; }

        /// <summary>
        /// Gets the day number.
        /// </summary>
        public int Day { get; private set; }

        /// <summary>
        /// Gets the date at which the day starts.
        /// </summary>
        public DateTime DayStart { get; private set; }

        /// <summary>
        /// Gets the date at which the day ends.
        /// </summary>
        public DateTime DayEnd { get; private set;  }

        /// <summary>
        /// Creates a new instance of WorkingDay.
        /// </summary>
        /// <param name="year">Year of the day.</param>
        /// <param name="month">Month of the day.</param>
        /// <param name="day">Number of the day.</param>
        public WorkingDay(int year, int month, int day)
        {
            Checkings = new List<Checking>();
            Year = year;
            Month = month;
            Day = day;

            DayStart = new DateTime(year, month, day, 0, 0, 0);
            var almost24Hours = new TimeSpan(0, 0, 59, 59, 99);
            DayEnd = DayStart.Add(almost24Hours);
        }

        /// <summary>
        /// Gets the worked time of the day.
        /// </summary>
        /// <returns>Worked time.</returns>
        public TimeSpan GetWorkedTime()
        {
            var calculator = new WorkedTimeCalculator();
            return calculator.CalculateTotalTime(Checkings);
        }
    }
}
