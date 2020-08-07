using CNX_Domain.Entities.Enums;

namespace CNX_Domain.Models
{
    public class LogTraceVM : LogVM
    {
        public string Message { get; set; }

        public LogTraceVM(string message, string controllerName, string actionName) : base()
        {
            this.Message = message;
            this.LogType = EnumLogType.Trace;
            this.ControllerName = controllerName;
            this.ActionName = actionName;
        }
    }
}
