using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.Storage;
using NumbrixGame.Datatier;
using NumbrixGame.Properties;

namespace NumbrixGame.ViewModel
{
    /// <summary>
    ///     Class NumbrixMainPageViewModel.
    ///     Implements the <see cref="System.ComponentModel.INotifyPropertyChanged" />
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class NumbrixMainPageViewModel : INotifyPropertyChanged
    {
        #region Data members

        /// <summary>
        ///     The saved games
        /// </summary>
        private List<StorageFile> savedGames;

        /// <summary>
        ///     The prebuilt games
        /// </summary>
        private List<StorageFile> prebuiltGames;

        /// <summary>
        ///     The selected game file
        /// </summary>
        private StorageFile selectedGameFile;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the selected game file.
        /// </summary>
        /// <value>The selected game file.</value>
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

        /// <summary>
        ///     Gets or sets the saved games.
        /// </summary>
        /// <value>The saved games.</value>
        public List<StorageFile> SavedGames
        {
            get => this.savedGames;
            set
            {
                this.savedGames = value;
                this.OnPropertyChanged(nameof(this.savedGames));
            }
        }

        /// <summary>
        ///     Gets or sets the prebuilt games.
        /// </summary>
        /// <value>The prebuilt games.</value>
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

        /// <summary>
        ///     Occurs when a property value changes.
        /// </summary>
        /// <returns></returns>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     build view model as an asynchronous operation.
        /// </summary>
        /// <returns>NumbrixMainPageViewModel.</returns>
        public static async Task<NumbrixMainPageViewModel> BuildViewModelAsync()
        {
            var savedGames = await NumbrixGameBoardReader.GetSavedGames();
            var prebuiltGames = await NumbrixGameBoardReader.GetPrebuiltGames();
            return new NumbrixMainPageViewModel {PrebuiltGames = prebuiltGames, SavedGames = savedGames};
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

        #endregion
    }
}