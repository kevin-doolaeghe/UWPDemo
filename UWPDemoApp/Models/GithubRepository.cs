using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPDemoApp.Models {

    public class GithubRepository {

        private readonly int id;

        public int Id { get => id; }

        private readonly string name;

        public string Name { get => name; }

        private readonly string full_name;

        public string FullName { get => full_name; }

        private readonly string description;

        public string Description { get => description; }

        private readonly string html_url;

        public string Url { get => html_url; }

        private readonly ICollection<string> topics;

        public ICollection<string> Topics { get => topics; }

        private readonly int stargazers_count;

        public int StarsCount { get => stargazers_count; }

        private readonly int watchers_count;

        public int WatchersCount { get => watchers_count; }

        private readonly int forks_count;

        public int ForksCount { get => forks_count; }

        public GithubRepository(int id, string name, string full_name, string description, string html_url, ICollection<string> topics, int stargazers_count, int watchers_count, int forks_count) {
            this.id = id;
            this.name = name;
            this.full_name = full_name;
            this.description = description;
            this.html_url = html_url;
            this.topics = topics;
            this.stargazers_count = stargazers_count;
            this.watchers_count = watchers_count;
            this.forks_count = forks_count;
        }
    }
}
