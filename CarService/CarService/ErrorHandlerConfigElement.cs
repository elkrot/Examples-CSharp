using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace CarService
{
    public class ErrorHandlerConfigElement : BehaviorExtensionElement
    {
        public override Type BehaviorType =>  typeof(MainErrorHandlerBehavior);

        protected override object CreateBehavior()
        {
            return new MainErrorHandlerBehavior(Type.GetType(this["handlerType"].ToString()));
        }

        public string HandlerType {
            get { return (string)this["handlerType"]; }
            set { this["handlerType"] = value; } }
    }
}
