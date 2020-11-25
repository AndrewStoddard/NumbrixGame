using System.Collections.Generic;

namespace NumbrixGame.Model
{
    public class NumbrixScoreBoard
    {
        #region Properties

        public IList<NumbrixPlayerScore> PlayerScores { get; set; }

        #endregion

        #region Constructors

        public NumbrixScoreBoard()
        {
            this.PlayerScores = new List<NumbrixPlayerScore>();
        }

        #endregion
    }
}