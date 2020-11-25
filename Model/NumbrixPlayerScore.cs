using System;

namespace NumbrixGame.Model
{
    public class NumbrixPlayerScore
    {
        #region Properties

        public string Username { get; set; }
        public TimeSpan TimeTaken { get; set; }
        public int PuzzleNumber { get; set; }

        #endregion

        #region Constructors

        public NumbrixPlayerScore(string username, TimeSpan timeTaken, int puzzleNumber)
        {
            this.Username = username;
            this.TimeTaken = timeTaken;
            this.PuzzleNumber = puzzleNumber;
        }

        #endregion
    }
}