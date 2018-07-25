
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Models;
using Tweetinvi.Parameters;
using Windows.Storage;

namespace TombStone2.Helpers
{
    class PickImage
    {
        // Call this function to pick the image
        public static void LoadImagesFromFileClicked(StorageFile selectedFile)
        {
            try
            {
                if (selectedFile != null)
                {
                    UpdateResults(new ImageAnalyzer(selectedFile.OpenStreamForReadAsync, selectedFile.Path));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to read files");
            }
        }

        // Modify this function to send your results to where ever you need
        public static async void UpdateResults(ImageAnalyzer img)
        {
            await img.DescribeAsync();
            if (img.AnalysisResult.Description == null || !img.AnalysisResult.Description.Captions.Any(d => d.Confidence >= 0.2))
            {
                Console.WriteLine("Cognitive Services Failed, or no caption found");
            }
            else
            {
                // Modify here to post the results of your caption, the caption is stored in img.AnalysisResult.Description.Captions[0].Text, it will actually return you an array of captions but usually the first one is the most accurate one
                Console.WriteLine(img.AnalysisResult.Description.Captions[0].Text);

                await Task.Run(() =>
                {
                    Task.Yield();
                    ProcessTweet(img.AnalysisResult.Description.Captions[0].Text, img.LocalImagePath);
                });
            }
        }

        static Task ProcessTweet(string tweet, string LocalImagePath)
        {
            Task theTask = TweetAsync(tweet, LocalImagePath);

            return theTask;
        }


        static async Task TweetAsync(string tweet, string LocalImagePath)
        {
            await Tweeting(tweet, LocalImagePath);
        }



        static async Task Tweeting(string tweet, string LocalImagePath)
        {
            var creds = new TwitterCredentials(Globals.AccessToken, Globals.AccessTokenSecret, Globals.ConsumerKey, Globals.ConsumerSecret);
            Auth.SetUserCredentials(Globals.ConsumerKey, Globals.ConsumerSecret, Globals.AccessToken, Globals.AccessTokenSecret);
            Auth.ApplicationCredentials = creds;
            var authenticatedUser = User.GetAuthenticatedUser();

            PublishTweet(tweet, LocalImagePath);
        }

        static public void PublishTweet(string text, string LocalImagePath)
        {
            var image = File.ReadAllBytes(LocalImagePath);
            var media = Upload.UploadBinary(image);
            Tweet.PublishTweet(text, new PublishTweetOptionalParameters
            {
                Medias = new List<IMedia> { media }
            });
        }
    }
}
