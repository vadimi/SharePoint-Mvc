namespace SPMvc.Core
{
    public interface IAreaConfiguration
    {
        string AreaName { get; }
        void RegisterRoutes(RoutesMapper routesMapper);
    }
}
