namespace TombStone2
{
    public class Constants
    {
        public class Instagram
        {
            public const string ClientId = "d4cc366d4eb54a0cb5cb6a9950e240d2";
            public static string AuthTokenUrl =
                $"https://api.instagram.com/oauth/authorize/?client_id={ClientId}&redirect_uri=REDIRECT-URI&response_type=token";
        }
    }
}