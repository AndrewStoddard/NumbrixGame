using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.Storage;
using NumbrixGame.Annotations;
using NumbrixGame.Datatier;

namespace NumbrixGame.ViewModel
{
    public class NumbrixMainPageViewModel : INotifyPropertyChanged
    {
        #region Data members

        private List<StorageFile> savedGames;
        private List<StorageFile> prebuiltGames;

        private StorageFile selectedGameFile;

        #endregion

        #region Properties

        public StorageFile SelectedGameFile
        {
            get => this.selectedGameFile;
            set
            {
                if (value != null)
                {
                    Debug.WriteLine(value.Name);
                }

                this.selectedGameFile = value;

                this.OnPropertyChanged(nameof(this.selectedGameFile));
            }
        }

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
            var savedGames = await NumbrixGameBoardReader.GetSavedGames();
            var prebuiltGames = await NumbrixGameBoardReader.GetPrebuiltGames();
            return new NumbrixMainPageViewModel {PrebuiltGames = prebuiltGames, SavedGames = savedGames};
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}