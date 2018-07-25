using Microsoft.ProjectOxford.Vision;
using System;
using System.IO;
using System.Threading.Tasks;

namespace TombStone2.Helpers
{
    public class ImageAnalyzer
    {
        private static VisualFeature[] DefaultVisualFeatures = new VisualFeature[] { VisualFeature.Tags, VisualFeature.Faces, VisualFeature.Categories, VisualFeature.Description, VisualFeature.Color };

        public event EventHandler ComputerVisionAnalysisCompleted;

        public static string PeopleGroupsUserDataFilter = null;

        public Func<Task<Stream>> GetImageStreamCallback { get; set; }
        public string LocalImagePath { get; set; }
        public string ImageUrl { get; set; }

        public Microsoft.ProjectOxford.Vision.Contract.AnalysisResult AnalysisResult { get; set; }

        // Default to no errors, since this could trigger a stream of popup errors since we might call this
        // for several images at once while auto-detecting the Bing Image Search results.

        public int DecodedImageHeight { get; private set; }
        public int DecodedImageWidth { get; private set; }
        public byte[] Data { get; set; }

        public ImageAnalyzer(string url)
        {
            this.ImageUrl = url;
        }

        public ImageAnalyzer(Func<Task<Stream>> getStreamCallback, string path = null)
        {
            this.GetImageStreamCallback = getStreamCallback;
            this.LocalImagePath = path;
        }

        public ImageAnalyzer(byte[] data)
        {
            this.Data = data;
            this.GetImageStreamCallback = () => Task.FromResult<Stream>(new MemoryStream(this.Data));
        }

        public void UpdateDecodedImageSize(int height, int width)
        {
            this.DecodedImageHeight = height;
            this.DecodedImageWidth = width;
        }

        public async Task DescribeAsync()
        {
            try
            {
                if (this.ImageUrl != null)
                {
                    this.AnalysisResult = await VisionServiceHelper.DescribeAsync(this.ImageUrl);
                }
                else if (this.GetImageStreamCallback != null)
                {
                    this.AnalysisResult = await VisionServiceHelper.DescribeAsync(this.GetImageStreamCallback);
                }
            }
            catch (Exception e)
            {
                this.AnalysisResult = new Microsoft.ProjectOxford.Vision.Contract.AnalysisResult();
            }
        }
    }
}