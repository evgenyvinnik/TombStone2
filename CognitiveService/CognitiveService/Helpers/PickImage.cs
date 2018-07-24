
using System;
using System.IO;
using System.Linq;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace CognitiveService
{
    class PickImage
    {
        // Call this function to pick the image
        public static async void LoadImagesFromFileClicked()
        {
            try
            {
                FileOpenPicker fileOpenPicker = new FileOpenPicker { SuggestedStartLocation = PickerLocationId.PicturesLibrary, ViewMode = PickerViewMode.Thumbnail };
                fileOpenPicker.FileTypeFilter.Add(".jpg");
                fileOpenPicker.FileTypeFilter.Add(".jpeg");
                fileOpenPicker.FileTypeFilter.Add(".png");
                fileOpenPicker.FileTypeFilter.Add(".bmp");
                StorageFile selectedFile = await fileOpenPicker.PickSingleFileAsync();


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
            }
        }
    }
}
