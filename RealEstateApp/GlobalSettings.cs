namespace RealEstateApp;

public class GlobalSettings
{
    public static GlobalSettings Instance { get; } = new GlobalSettings();

    public string ImageBaseUrl => AppContext.BaseDirectory + "Resources/Images/";
    public string NoImageUrl => ImageBaseUrl + "no_image.jpg";
}
