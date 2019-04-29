namespace FoodIdentifier.Interfaces
{
    public interface IAppInfoService
    {
        string GetVersion();
        string GetBuild();
        string GetName();
    }
}
