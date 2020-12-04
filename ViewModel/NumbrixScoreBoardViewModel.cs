using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using Windows.Storage;
using NumbrixGame.Datatier;
using NumbrixGame.Model;

namespace NumbrixGame.ViewModel
{
    public class NumbrixScoreBoardViewModel : INotifyPropertyChanged
    {
        #region Data members

        private const string ScoreboardSave = "scoreboard_save.csv";

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

        public NumbrixScoreBoard Model { get; set; }

        #endregion

        #region Constructors

        public NumbrixScoreBoardViewModel()
        {
            this.PlayerScores = new List<NumbrixPlayerScoreViewModel>();
            this.LoadScores();
        }

        #endregion

        #region Methods

        public event PropertyChangedEventHandler PropertyChanged;

        public void AddPlayerScore(NumbrixPlayerScoreViewModel playerScore)
        {
            this.playerScores.Add(playerScore);
            this.Model.AddScore(playerScore.Model);
        }

        public void SaveScores()
        {
            NumbrixScoreBoardWriter.WriteGameboard(this.Model, ScoreboardSave);
        }

        public async void LoadScores()
        {
            try
            {
                var file = await ApplicationData.Current.LocalFolder.GetFileAsync(ScoreboardSave);
                var scores =
                    await NumbrixScoreBoardReader.LoadPuzzle(file);
                this.addAllScores(scores);
            }
            catch (FileNotFoundException)
            {
                await ApplicationData.Current.LocalFolder.CreateFileAsync(ScoreboardSave);
            }
        }

        private void addAllScores(NumbrixScoreBoard scoreboard)
        {
            foreach (var score in scoreboard.PlayerScores)
            {
                this.AddPlayerScore(new NumbrixPlayerScoreViewModel(score));
            }
        }

        #endregion
    }
}