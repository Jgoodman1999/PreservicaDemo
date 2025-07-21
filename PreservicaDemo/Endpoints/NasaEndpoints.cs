using RestSharp;
public static class NasaEndpoints
{
    public static RestClient CreateClient() => new RestClient("https://api.nasa.gov");

    public static RestRequest CreateRequest(string path)
    {
        return new RestRequest(path, Method.Get);
    }
}
