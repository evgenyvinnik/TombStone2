﻿using Microsoft.ProjectOxford.Vision;
using Microsoft.ProjectOxford.Vision.Contract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace TombStone2.Helpers
{
    public static class VisionServiceHelper
    {
        public static int RetryCountOnQuotaLimitError = 6;
        public static int RetryDelayOnQuotaLimitError = 500;

        private static VisionServiceClient visionClient { get; set; }

        static VisionServiceHelper()
        {
            InitializeVisionService();
        }

        public static Action Throttled;

        // TODO: Add apikey, Computer Vision API for Cognitive Services
        public static string ApiKey
        {
            get
            {
                return Globals.apiKeyCognitive;
            }
        }

        //TODO: Add locale of api endpoint, should be there with your trial keys
        public static string ApiKeyRegion
        {
            get
            {
                return Globals.apiKeyRegionCognitive;
            }
        }

        private static void InitializeVisionService()
        {
            visionClient = ApiKeyRegion != null ?
                new VisionServiceClient(ApiKey, string.Format("https://{0}.api.cognitive.microsoft.com/vision/v1.0", ApiKeyRegion)) :
                new VisionServiceClient(ApiKey);
        }

        private static async Task<TResponse> RunTaskWithAutoRetryOnQuotaLimitExceededError<TResponse>(Func<Task<TResponse>> action)
        {
            int retriesLeft = 5;
            int delay = 1000;

            TResponse response = default(TResponse);

            while (true)
            {
                try
                {
                    response = await action();
                    break;
                }
                catch (Microsoft.ProjectOxford.Vision.ClientException exception) when (exception.HttpStatus == (System.Net.HttpStatusCode)429 && retriesLeft > 0)
                {
                    if (retriesLeft == 1 && Throttled != null)
                    {
                        Throttled();
                    }

                    await Task.Delay(delay);
                    retriesLeft--;
                    delay *= 2;
                    continue;
                }
            }

            return response;
        }

        private static async Task RunTaskWithAutoRetryOnQuotaLimitExceededError(Func<Task> action)
        {
            await RunTaskWithAutoRetryOnQuotaLimitExceededError<object>(async () => { await action(); return null; });
        }

        public static async Task<AnalysisResult> DescribeAsync(Func<Task<Stream>> imageStreamCallback)
        {
            return await RunTaskWithAutoRetryOnQuotaLimitExceededError<AnalysisResult>(async () => await visionClient.DescribeAsync(await imageStreamCallback()));
        }

        public static async Task<AnalysisResult> AnalyzeImageAsync(string imageUrl, IEnumerable<VisualFeature> visualFeatures = null, IEnumerable<string> details = null)
        {
            return await RunTaskWithAutoRetryOnQuotaLimitExceededError<AnalysisResult>(() => visionClient.AnalyzeImageAsync(imageUrl, visualFeatures, details));
        }

        public static async Task<AnalysisResult> AnalyzeImageAsync(Func<Task<Stream>> imageStreamCallback, IEnumerable<VisualFeature> visualFeatures = null, IEnumerable<string> details = null)
        {
            return await RunTaskWithAutoRetryOnQuotaLimitExceededError<AnalysisResult>(async () => await visionClient.AnalyzeImageAsync(await imageStreamCallback(), visualFeatures, details));
        }

        public static async Task<AnalysisResult> DescribeAsync(string imageUrl)
        {
            return await RunTaskWithAutoRetryOnQuotaLimitExceededError<AnalysisResult>(() => visionClient.DescribeAsync(imageUrl));
        }

        public static async Task<OcrResults> RecognizeTextAsync(string imageUrl)
        {
            return await RunTaskWithAutoRetryOnQuotaLimitExceededError<OcrResults>(() => visionClient.RecognizeTextAsync(imageUrl));
        }

        public static async Task<OcrResults> RecognizeTextAsync(Func<Task<Stream>> imageStreamCallback)
        {
            return await RunTaskWithAutoRetryOnQuotaLimitExceededError<OcrResults>(async () => await visionClient.RecognizeTextAsync(await imageStreamCallback()));
        }
    }
}