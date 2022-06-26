using System.Collections.Generic;
using System.Threading.Tasks;

using UWPDemoApp.Models;

namespace UWPDemoApp.Services {

    public static class GithubService {

        private const string GITHUB_URL = "https://api.github.com";

        public static async Task<IEnumerable<GithubRepository>> GetRepositories(string user) {
            string uri = $"users/{user}/repos";

            HttpBase http = new HttpBase(GITHUB_URL);
            return await http.GetAsync<IEnumerable<GithubRepository>>(uri);
        }
    }
}
