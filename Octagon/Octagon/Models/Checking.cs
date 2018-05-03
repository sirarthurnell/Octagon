using SQLite;
using System;

namespace Octagon.Models
{
    /// <summary>
    /// Represents the direction of the
    /// checking.
    /// </summary>
    public enum CheckingDirection
    {
        /// <summary>
        /// Checking of entrance.
        /// </summary>
        In,

        /// <summary>
        /// Checking of exit.
        /// </summary>
        Out
    }

    /// <summary>
    /// Represents a checking 
    /// (checking in or checking out).
    /// </summary>
    public class Checking
    {
        /// <summary>
        /// Id.
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        /// <summary>
        /// Time at which the checking was done.
        /// </summary>        
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Direction of the checking.
        /// </summary>
        public CheckingDirection Direction { get; set; }
    }
}
