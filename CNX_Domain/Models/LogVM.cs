using CNX_Domain.Entities.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CNX_Domain.Models
{
    public abstract class LogVM
    {
        public DateTime LogDate { get; } = DateTime.Now;
        public IList<object> Parameters { get; set; }
        [JsonIgnore]
        public EnumLogType LogType { get; protected set; }
        public string LogTypeDescription { get => this.LogType.ToString("g"); }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }

        public LogVM()
        {
            if (null == this.Parameters)
                this.Parameters = new List<object>();
        }

        public override string ToString() =>
            JsonConvert.SerializeObject(this);
    }
}
