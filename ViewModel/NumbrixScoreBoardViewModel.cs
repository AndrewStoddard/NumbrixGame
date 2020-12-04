using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Windows.Storage;
using Microsoft.Toolkit.Uwp.UI.Controls;
using NumbrixGame.Datatier;
using NumbrixGame.Model;
using NumbrixGame.Properties;

namespace NumbrixGame.ViewModel
{
    /// <summary>
    ///     Class NumbrixScoreBoardViewModel.
    ///     Implements the <see cref="System.ComponentModel.INotifyPropertyChanged" />
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class NumbrixScoreBoardViewModel : INotifyPropertyChanged
    {
        #region Data members

        /// <summary>
        ///     The scoreboard save
        /// </summary>
        private const string ScoreboardSave = "scoreboard_save.csv";

        /// <summary>
        ///     The username column tag
        /// </summary>
        private const string UsernameColumnTag = "Username";

        /// <summary>
        ///     The puzzle number column tag
        /// </summary>
        private const string PuzzleNumberColumnTag = "PuzzleNumber";

        /// <summary>
        ///     The time taken column tag
        /// </summary>
        private const string TimeTakenColumnTag = "TimeTaken";

        /// <summary>
        ///     The player scores
        /// </summary>
        private IList<NumbrixPlayerScoreViewModel> playerScores;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the player scores.
        /// </summary>
        /// <value>The player scores.</value>
        public IList<NumbrixPlayerScoreViewModel> PlayerScores
        {
            get => this.playerScores;
            set
            {
                this.playerScores = value;
                this.OnPropertyChanged(nameof(this.PlayerScores));
            }
        }

        /// <summary>
        ///     Gets or sets the model.
        /// </summary>
        /// <value>The model.</value>
        public NumbrixScoreBoard Model { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="NumbrixScoreBoardViewModel" /> class.
        /// </summary>
        public NumbrixScoreBoardViewModel()
        {
            this.Model = new NumbrixScoreBoard();
            this.PlayerScores = new List<NumbrixPlayerScoreViewModel>();
            this.LoadScores();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Occurs when a property value changes.
        /// </summary>
        /// <returns></returns>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Adds the player score.
        /// </summary>
        /// <param name="playerScore">The player score.</param>
        public void AddPlayerScore(NumbrixPlayerScoreViewModel playerScore)
        {
            this.playerScores.Add(playerScore);
            this.Model.AddScore(playerScore.Model);
            this.SortDescending(TimeTakenColumnTag);
        }

        /// <summary>
        ///     Resets the scores.
        /// </summary>
        public void ResetScores()
        {
            this.PlayerScores = new List<NumbrixPlayerScoreViewModel>();
            this.Model.PlayerScores = new List<NumbrixPlayerScore>();
            NumbrixScoreBoardWriter.ResetScoreboard(ScoreboardSave);
        }

        /// <summary>
        ///     Saves the scores.
        /// </summary>
        public void SaveScores()
        {
            NumbrixScoreBoardWriter.WriteScoreBoard(this.Model, ScoreboardSave);
        }

        /// <summary>
        ///     Loads the scores.
        /// </summary>
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

        /// <summary>
        ///     Adds all scores.
        /// </summary>
        /// <param name="scoreboard">The scoreboard.</param>
        private void addAllScores(NumbrixScoreBoard scoreboard)
        {
            foreach (var score in scoreboard.PlayerScores)
            {
                this.AddPlayerScore(new NumbrixPlayerScoreViewModel(score));
            }
        }

        /// <summary>
        ///     Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        ///     Handles the <see cref="E:SortByColumn" /> event.
        /// </summary>
        /// <param name="columns">The columns.</param>
        /// <param name="e">The <see cref="DataGridColumnEventArgs" /> instance containing the event data.</param>
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

        /// <summary>
        ///     Sorts the ascending.
        /// </summary>
        /// <param name="sort">The sort.</param>
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

        /// <summary>
        ///     Sorts the descending.
        /// </summary>
        /// <param name="sort">The sort.</param>
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