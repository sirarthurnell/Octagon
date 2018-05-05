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
        /// Creates a new instance of WorkingDay.
        /// </summary>
        public WorkingDay()
        {
            Checkings = new List<Checking>();
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
