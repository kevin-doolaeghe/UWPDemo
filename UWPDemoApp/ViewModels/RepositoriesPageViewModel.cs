using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Toolkit.Uwp;

using Windows.System;

using UWPDemoApp.Models;
using UWPDemoApp.Services;
using UWPDemoApp.Utils;

namespace UWPDemoApp.ViewModels {

    /// <summary>
    /// Encapsulates data for the RepositoriesPage.
    /// The page UI binds to the properties defined here.
    /// </summary>
    public class RepositoriesPageViewModel : BindableBase {

        private DispatcherQueue dispatcherQueue = DispatcherQueue.GetForCurrentThread();

        /// <summary>
        /// Initializes a new instance of the OrderListPageViewModel class.
        /// </summary>
        public RepositoriesPageViewModel() => IsLoading = false;

        private bool _isLoading;

        /// <summary>
        /// Gets or sets a value that specifies whether orders are being loaded.
        /// </summary>
        public bool IsLoading {
            get => _isLoading;
            set => Set(ref _isLoading, value);
        }

        /// <summary>
        /// Gets the repositories to display.
        /// </summary>
        public ObservableCollection<GithubRepository> Repositories { get; private set; } = new ObservableCollection<GithubRepository>();

        private GithubRepository _selectedRepository;

        /// <summary>
        /// Gets or sets the selected repository.
        /// </summary>
        public GithubRepository SelectedRepository {
            get => _selectedRepository;
            set => Set(ref _selectedRepository, value);
        }

        /// <summary>
        /// Retrieves repositories from the data source
        /// </summary>
        /// <param name="user">The users's repositories to load.</param>
        public async void LoadRepositories(string user) {
            if (!string.IsNullOrEmpty(user)) {
                await dispatcherQueue.EnqueueAsync(() => {
                    IsLoading = true;
                    Repositories.Clear();
                });

                var repositories = await Task.Run(() => GithubService.GetRepositories(user));

                await dispatcherQueue.EnqueueAsync(() => {
                    foreach (var repository in repositories) {
                        Repositories.Add(repository);
                    }
                    IsLoading = false;
                });
            }
        }
    }
}
