using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Windows.Storage;
using Microsoft.Toolkit.Uwp.UI.Controls;
using NumbrixGame.Annotations;
using NumbrixGame.Datatier;
using NumbrixGame.Model;

namespace NumbrixGame.ViewModel
{
    public class NumbrixScoreBoardViewModel : INotifyPropertyChanged
    {
        #region Data members

        private const string ScoreboardSave = "scoreboard_save.csv";
        private const string UsernameColumnTag = "Username";
        private const string PuzzleNumberColumnTag = "PuzzleNumber";
        private const string TimeTakenColumnTag = "TimeTaken";

        private IList<NumbrixPlayerScoreViewModel> playerScores;

        #endregion

        #region Properties

        public IList<NumbrixPlayerScoreViewModel> PlayerScores
        {
            get => this.playerScores;
            set
            {
                this.playerScores = value;
                this.OnPropertyChanged(nameof(this.PlayerScores));
            }
        }

        public NumbrixScoreBoard Model { get; set; }

        #endregion

        #region Constructors

        public NumbrixScoreBoardViewModel()
        {
            this.Model = new NumbrixScoreBoard();
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
            this.SortDescending(TimeTakenColumnTag);
        }

        public void ResetScores()
        {
            this.PlayerScores = new List<NumbrixPlayerScoreViewModel>();
            this.Model.PlayerScores = new List<NumbrixPlayerScore>();
            NumbrixScoreBoardWriter.ResetScoreboard(ScoreboardSave);
        }

        public void SaveScores()
        {
            NumbrixScoreBoardWriter.WriteScoreBoard(this.Model, ScoreboardSave);
        }

        public async void LoadScores()
        {
            try
            {
                var file = await ApplicationData.Current.LocalFolder.GetFileAsync(ScoreboardSave);
                var scores =
                    await NumbrixScoreBoardReader.LoadScoreBoard(file);
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

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void OnSortByColumn(ObservableCollection<DataGridColumn> columns, DataGridColumnEventArgs e)
        {
            if (e.Column.SortDirection == null || e.Column.SortDirection == DataGridSortDirection.Ascending)
            {
                this.SortDescending(e.Column.Tag.ToString());
                e.Column.SortDirection = DataGridSortDirection.Descending;
            }
            else
            {
                this.SortAscending(e.Column.Tag.ToString());
                e.Column.SortDirection = DataGridSortDirection.Ascending;
            }

            foreach (var column in columns)
            {
                if (!e.Column.Tag.ToString().Equals(column.Tag.ToString()))
                {
                    column.SortDirection = null;
                }
            }
        }

        private void SortAscending(string sort)
        {
            switch (sort)
            {
                case UsernameColumnTag:
                    this.PlayerScores = this.PlayerScores.OrderBy(score => score.Username)
                                            .ThenBy(score => score.TimeTaken).ThenBy(score => score.PuzzleNumber)
                                            .ToList();
                    break;
                case PuzzleNumberColumnTag:
                    this.PlayerScores = this.PlayerScores.OrderBy(score => score.PuzzleNumber)
                                            .ThenBy(score => score.TimeTaken).ThenBy(score => score.Username)
                                            .ToList();
                    break;
                case TimeTakenColumnTag:
                    this.PlayerScores = this.PlayerScores.OrderBy(score => score.TimeTaken)
                                            .ThenBy(score => score.PuzzleNumber).ThenBy(score => score.Username)
                                            .ToList();
                    break;
            }
        }

        private void SortDescending(string sort)
        {
            switch (sort)
            {
                case UsernameColumnTag:
                    this.PlayerScores = this.PlayerScores.OrderByDescending(score => score.Username)
                                            .ThenByDescending(score => score.TimeTaken)
                                            .ThenByDescending(score => score.PuzzleNumber)
                                            .ToList();
                    break;
                case PuzzleNumberColumnTag:
                    this.PlayerScores = this.PlayerScores.OrderByDescending(score => score.PuzzleNumber)
                                            .ThenByDescending(score => score.TimeTaken)
                                            .ThenByDescending(score => score.Username)
                                            .ToList();
                    break;
                case TimeTakenColumnTag:
                    this.PlayerScores = this.PlayerScores.OrderByDescending(score => score.TimeTaken)
                                            .ThenByDescending(score => score.PuzzleNumber)
                                            .ThenByDescending(score => score.Username)
                                            .ToList();
                    break;
            }
        }

        #endregion
    }
}