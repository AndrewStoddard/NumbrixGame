using System.Collections.Generic;
using System.Text;

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

        #region Methods

        public string AsCSV()
        {
            var stringBuilder = new StringBuilder();
            foreach (var score in this.PlayerScores)
            {
                stringBuilder.AppendLine(
                    $"{score.Username},{score.PuzzleNumber},{score.TimeTaken.ToString()}");
            }

            return stringBuilder.ToString();
        }

        public void AddScore(NumbrixPlayerScore score)
        {
            this.PlayerScores.Add(score);
        }

        #endregion
    }
}