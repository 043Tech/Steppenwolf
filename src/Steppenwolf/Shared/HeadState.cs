using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Steppenwolf.Shared
{
    public class HeadState
    {
        // internal store
        private string title = string.Empty;

        public event Action HeadChanged;

        public ISet<string> HeadItems { get; } = new HashSet<string>();

        public string Title => this.title;

        public void SetTitle(string title)
        {
            if (!string.Equals(this.title, title))
            {
                this.title = title;
                this.HeadChanged?.Invoke();
            }
        }

        public void AddStyleItem(string item)
        {
            var linkValue = $"<link rel=\"stylesheet\" href=\"{item}\" />";

            if (!this.HeadItems.Contains(linkValue))
            {
                this.HeadItems.Add(linkValue);
                this.HeadChanged?.Invoke();
            }
        }
    }
}
