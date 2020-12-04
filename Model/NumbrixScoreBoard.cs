using System.Collections.Generic;
using System.Text;

namespace NumbrixGame.Model
{
    /// <summary>
    ///     Class NumbrixScoreBoard.
    /// </summary>
    public class NumbrixScoreBoard
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the player scores.
        /// </summary>
        /// <value>The player scores.</value>
        public IList<NumbrixPlayerScore> PlayerScores { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="NumbrixScoreBoard" /> class.
        /// </summary>
        public NumbrixScoreBoard()
        {
            this.PlayerScores = new List<NumbrixPlayerScore>();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Ases the CSV.
        /// </summary>
        /// <returns>System.String.</returns>
        public string AsCsv()
        {
            var stringBuilder = new StringBuilder();
            foreach (var score in this.PlayerScores)
            {
                stringBuilder.AppendLine(
                    $"{score.Username},{score.PuzzleNumber},{score.TimeTaken.ToString()}");
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        ///     Adds the score.
        /// </summary>
        /// <param name="score">The score.</param>
        public void AddScore(NumbrixPlayerScore score)
        {
            this.PlayerScores.Add(score);
        }

        #endregion
    }
}