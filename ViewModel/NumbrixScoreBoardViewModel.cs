using System.Collections.Generic;
using System.ComponentModel;

namespace NumbrixGame.ViewModel
{
    public class NumbrixScoreBoardViewModel : INotifyPropertyChanged
    {
        #region Data members

        private IList<NumbrixPlayerScoreViewModel> playerScores;

        #endregion

        #region Properties

        public IList<NumbrixPlayerScoreViewModel> PlayerScores
        {
            get => this.playerScores;
            set
            {
                this.playerScores = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.playerScores)));
            }
        }

        #endregion

        #region Constructors

        public NumbrixScoreBoardViewModel()
        {
            this.PlayerScores = new List<NumbrixPlayerScoreViewModel>();
        }

        #endregion

        #region Methods

        public event PropertyChangedEventHandler PropertyChanged;

        public void AddPlayerScore(NumbrixPlayerScoreViewModel playerScore)
        {
            this.playerScores.Add(playerScore);
        }

        #endregion
    }
}