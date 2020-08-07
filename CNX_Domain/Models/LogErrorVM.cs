using CNX_Domain.Entities.Enums;

namespace CNX_Domain.Models
{
    public class LogErrorVM : LogVM
    {
        public string ErrorMessage { get; set; }
        public string StackTrace { get; set; }

        public LogErrorVM(string errorMessage, string stackTrace, string controllerName, string actionName) : base()
        {
            this.ErrorMessage = errorMessage;
            this.StackTrace = stackTrace;
            this.LogType = EnumLogType.Error;
            this.ControllerName = controllerName;
            this.ActionName = actionName;
        }
    }
}
