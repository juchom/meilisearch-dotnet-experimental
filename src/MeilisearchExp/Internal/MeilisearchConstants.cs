namespace MeilisearchExp
{
    internal class MeilisearchConstants
    {
        internal static string ClientName = "Meilisearch.NET";
        internal static string ClientVersion = typeof(MeilisearchConstants).Assembly.GetName().Version.ToString(3) ?? "0.0.0";
    }
}