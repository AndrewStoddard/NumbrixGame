using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Microsoft.Toolkit.Uwp.UI.Controls;
using NumbrixGame.Annotations;
using NumbrixGame.ViewModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace NumbrixGame.View
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NumbrixMainMenuPage : INotifyPropertyChanged
    {
        #region Data members

        private NumbrixMainPageViewModel numbrixMainPageViewModel;

        #endregion

        #region Constructors

        public NumbrixMainMenuPage()
        {
            this.createViewmodel();
            this.InitializeComponent();
        }

        #endregion

        #region Methods

        public event PropertyChangedEventHandler PropertyChanged;

        private async void createViewmodel()
        {
            this.numbrixMainPageViewModel = await NumbrixMainPageViewModel.BuildViewModelAsync();
            this.OnPropertyChanged(nameof(this.numbrixMainPageViewModel));
        }

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Control_OnFocusDisengaged(Control sender, FocusDisengagedEventArgs args)
        {
            var datagrid = sender as DataGrid;
            datagrid.SelectedItem = null;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(NumbrixGameBoardPage), this.numbrixMainPageViewModel.SelectedGameFile);
        }

        #endregion
    }
}