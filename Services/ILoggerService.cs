namespace AquaServiceSPA.Services
{
    public interface ILoggerService
    {
        void Log(string message);
        bool IsLoggerEnabled { get; set; }
    }
}