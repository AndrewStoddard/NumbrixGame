using System;

namespace NumbrixGame.Model
{

    /// <summary>
    ///   Class for the Numbrix Player Score
    /// </summary>
    public class NumbrixPlayerScore
    {
        #region Properties

        /// <summary>Gets or sets the username.</summary>
        /// <value>The username.</value>
        public string Username { get; set; }

        /// <summary>Gets or sets the time taken.</summary>
        /// <value>The time taken.</value>
        public TimeSpan TimeTaken { get; set; }

        /// <summary>Gets or sets the puzzle number.</summary>
        /// <value>The puzzle number.</value>
        public int PuzzleNumber { get; set; }

        #endregion

        #region Constructors

        /// <summary>Initializes a new instance of the <see cref="NumbrixPlayerScore" /> class.</summary>
        /// <param name="username">The username.</param>
        /// <param name="timeTaken">The time taken.</param>
        /// <param name="puzzleNumber">The puzzle number.</param>
        public NumbrixPlayerScore(string username, TimeSpan timeTaken, int puzzleNumber)
        {
            this.Username = username;
            this.TimeTaken = timeTaken;
            this.PuzzleNumber = puzzleNumber;
        }

        #endregion
    }
}