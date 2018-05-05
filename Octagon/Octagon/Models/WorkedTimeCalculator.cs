using System;
using System.Collections.Generic;
using System.Linq;

namespace Octagon.Models
{
    /// <summary>
    /// Performs calculations based on a collection
    /// of checkings.
    /// </summary>
    public class WorkedTimeCalculator
    {
        /// <summary>
        /// Calculates the total amount of time of the collection
        /// of checkings.
        /// </summary>
        /// <param name="checkings">Collection of checkings.</param>
        /// <returns>Total amount of time.</returns>
        public TimeSpan CalculateTotalTime(IEnumerable<Checking> checkings)
        {
            var totalTime = new TimeSpan();
            var checkingsList = checkings.ToList();
            var inIndex = 0;
            var outIndex = 0;
            var shouldContinue = true;

            while (shouldContinue)
            {
                inIndex = NextCheckingDirectionIndex(checkingsList, CheckingDirection.In, outIndex);
                outIndex = NextCheckingDirectionIndex(checkingsList, CheckingDirection.Out, inIndex + 1);

                shouldContinue = inIndex > -1 && outIndex > -1 && outIndex > inIndex;

                if(shouldContinue)
                {
                    var inTimestamp = checkingsList[inIndex].Timestamp;
                    var outTimestamp = checkingsList[outIndex].Timestamp;

                    totalTime += outTimestamp.Subtract(inTimestamp);
                }
            }

            return totalTime;
        }

        /// <summary>
        /// Gets the index of the checking which
        /// direction is specified.
        /// </summary>
        /// <param name="checkings">List of checkings.</param>
        /// <param name="direction">Direction of the checking.</param>
        /// <param name="fromIndex">Index from what the searching will
        /// start.</param>
        /// <returns>Index of the checking. -1 if it's not found.</returns>
        private int NextCheckingDirectionIndex(List<Checking> checkings, CheckingDirection direction, int fromIndex)
        {
            for(var i = fromIndex; i < checkings.Count; i++)
            {
                var checking = checkings[i];
                if(checking.Direction == direction)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
