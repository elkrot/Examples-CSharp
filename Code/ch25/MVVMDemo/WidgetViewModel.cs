using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVVMDemo
{
    class WidgetViewModel : BaseViewModel
    {
        private Widget _widget;

        public int Id { get { return _widget.Id; } }
        public string Name { get { return _widget.Name; } }
        public string WidgetType
        {
            get
            {
                return _widget.WidgetType.ToString();
            }
        }

        public WidgetViewModel(Widget widget)
            :base("Widget")
        {
            _widget = widget;
        }
    }
}
