using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

using UWPDemoApp.ViewModels;

using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace UWPDemoApp.Views {

    /// <summary>
    /// Displays the list of repositories.
    /// </summary>
    public sealed partial class RepositoriesPage : Page {

        /// <summary>
        /// We use this object to bind the UI to our data. 
        /// </summary>
        public RepositoriesPageViewModel ViewModel { get; } = new RepositoriesPageViewModel();

        public RepositoriesPage() {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            if (ViewModel.Repositories.Count < 1) {
                ViewModel.LoadRepositories("kevin-doolaeghe");
            }
        }

        private void RepositoriesListView_Click(object sender, RoutedEventArgs e) {
            Frame.Navigate(typeof(MainPage), ViewModel.SelectedRepository);
        }
    }
}
