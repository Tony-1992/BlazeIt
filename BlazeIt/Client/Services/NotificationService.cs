using MudBlazor;

namespace BlazeIt.Client.Services
{
    public class NotificationService
    {
        private readonly ISnackbar _snackbar;
        public NotificationService(ISnackbar snackbar)
        {
            _snackbar = snackbar;
        }

        public void ShowSnackbar(string msg, Severity severity)
        {
            _snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomRight;
            _snackbar.Add(msg, severity, config =>
            {
                config.ShowCloseIcon = false;
            });
        }
    }
}
