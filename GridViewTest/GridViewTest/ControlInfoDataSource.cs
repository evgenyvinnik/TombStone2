using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridViewTest
{
    /// <summary>
    /// Generic item data model.
    /// </summary>
    public class ControlInfoDataItem
    {
        public ControlInfoDataItem(String uniqueId, String title, String subtitle, String imagePath, String description, String content, bool isNew)
        {
            this.UniqueId = uniqueId;
            this.Title = title;
            this.Subtitle = subtitle;
            this.Description = description;
            this.ImagePath = imagePath;
            this.Content = content;
            this.IsNew = isNew;
            //this.Docs = new ObservableCollection<ControlInfoDocLink>();
            //this.RelatedControls = new ObservableCollection<string>();
        }

        public string UniqueId { get; private set; }
        public string Title { get; private set; }
        public string Subtitle { get; private set; }
        public string Description { get; private set; }
        public string ImagePath { get; private set; }
        public string Content { get; private set; }
        public bool IsNew { get; private set; }
        //public ObservableCollection<ControlInfoDocLink> Docs { get; private set; }
        //public ObservableCollection<string> RelatedControls { get; private set; }

        public override string ToString()
        {
            return this.Title;
        }
    }

    public class ControlInfoDocLink
    {
        public ControlInfoDocLink(string title, string uri)
        {
            this.Title = title;
            this.Uri = uri;
        }
        public string Title { get; private set; }
        public string Uri { get; private set; }
    }
}
