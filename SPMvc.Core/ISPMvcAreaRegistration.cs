namespace SPMvc.Core
{
    public interface ISPMvcAreaRegistration
    {
        string AreaName { get; }
        void RegisterRoutes(SPMvcAreaRegistrationContext areaRegistrationContext);
    }
}
