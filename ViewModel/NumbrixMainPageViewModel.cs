using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.Storage;
using NumbrixGame.Annotations;
using NumbrixGame.Datatier;
using NumbrixGame.PrebuiltGames;

namespace NumbrixGame.ViewModel
{
    public class NumbrixMainPageViewModel : INotifyPropertyChanged
    {
        #region Data members

        private List<StorageFile> savedGames;
        private List<StorageFile> prebuiltGames;

        #endregion

        #region Properties

        public List<StorageFile> SavedGames
        {
            get => this.savedGames;
            set
            {
                this.savedGames = value;
                this.OnPropertyChanged(nameof(this.savedGames));
            }
        }

        public List<StorageFile> PrebuiltGames
        {
            get => this.prebuiltGames;
            set
            {
                this.prebuiltGames = value;
                this.OnPropertyChanged(nameof(this.prebuiltGames));
            }
        }

        #endregion

        #region Methods

        public event PropertyChangedEventHandler PropertyChanged;

        public static async Task<NumbrixMainPageViewModel> BuildViewModelAsync()
        {
            var savedGames = await GetSavedGames();
            var prebuiltGames = await GetPrebuiltGames();
            return new NumbrixMainPageViewModel {PrebuiltGames = prebuiltGames, SavedGames = savedGames};
        }

        public static async Task<List<StorageFile>> GetSavedGames()
        {
            var files = await ApplicationData.Current.LocalFolder.GetFilesAsync();
            return files.Where(file => file.Name.StartsWith("save_")).ToList();
        }

        public static async Task<List<StorageFile>> GetPrebuiltGames()
        {
            var prebuiltSuffix = "puzzle_";
            for (var i = 1; i <= 8; i++)
            {
                var filename = prebuiltSuffix + i + ".csv";
                if (!NumbrixGameBoardWriter.FileExists(filename))
                {
                    NumbrixGameBoardWriter.WriteGameboard(
                        NumbrixGameBoardReader.LoadPuzzle(MainPuzzles.PuzzleList?[i - 1]), filename);
                }
            }

            var files = await ApplicationData.Current.LocalFolder.GetFilesAsync();
            return files.Where(file => file.Name.StartsWith("puzzle_")).ToList();
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}