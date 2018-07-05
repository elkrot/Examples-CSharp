using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PluginInterfaces
{
    public interface IImagePlugin
    {
        System.Drawing.Image RunPlugin(System.Drawing.Image image);
        string Name { get; }
    }
}
