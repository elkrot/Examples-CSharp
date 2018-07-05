using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVVMDemo
{
    enum WidgetType
    {
        TypeA,
        TypeB
    };

    class Widget
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public WidgetType WidgetType { get; set; }

        public Widget(int id, string name, WidgetType type)
        {
            this.Id = id;
            this.Name = name;
            this.WidgetType = type;
        }
    }
}
