using System;

namespace TombStone2.Helpers
{
    /// <summary>
    /// Generic item data model.
    /// </summary>
    public class ControlInfoDataItem
    {
        private static int randSeed = 0;
        public ControlInfoDataItem(string title, string subtitle, string imagePath, string description = "", string content = "", bool isNew = false)
        {
            if (randSeed == 0)
            {
                randSeed = DateTime.Now.Millisecond;
            }
            else
            {
                randSeed += 1;
            }
            SortOrder = new Random(randSeed).Next();
            UniqueId = Guid.NewGuid().ToString();
            Title = title;
            Subtitle = subtitle;
            Description = description;
            ImagePath = imagePath;
            Content = content;
            IsNew = isNew;
            //this.Docs = new ObservableCollection<ControlInfoDocLink>();
            //this.RelatedControls = new ObservableCollection<string>();
        }

        public int SortOrder { get; private set; }
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
            return Title;
        }
    }

    public class ControlInfoDocLink
    {
        public ControlInfoDocLink(string title, string uri)
        {
            Title = title;
            Uri = uri;
        }
        public string Title { get; private set; }
        public string Uri { get; private set; }
    }
}
