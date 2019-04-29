namespace FoodIdentifier.Interfaces
{
    public interface IDeviceService
    {
        string GetModel();
        string GetManufacturer();
        string GetVersion();
        string GetPlatform();
    }
}
