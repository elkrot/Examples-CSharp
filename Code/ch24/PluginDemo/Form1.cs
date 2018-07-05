using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using PluginInterfaces;

namespace PluginDemo
{
    public partial class Form1 : Form
    {
        private Dictionary<string, PluginInterfaces.IImagePlugin> _plugins = new Dictionary<string, PluginInterfaces.IImagePlugin>();
        public Form1()
        {
            InitializeComponent();

            Assembly assembly = Assembly.GetExecutingAssembly();
            string folder = Path.GetDirectoryName(assembly.Location);
            LoadPlugins(folder);
            CreatePluginMenu();
        }

        private void LoadPlugins(string folder)
        {
            _plugins.Clear();

            //grab each dll
            foreach(string dll in Directory.GetFiles(folder, "*.dll"))
            {
                try
                {
                    Assembly assembly = Assembly.LoadFrom(dll);
                    //find every type in each assembly that implements IImagePlugin
                    foreach (Type type in assembly.GetTypes())
                    {
                        if (type.GetInterface("IImagePlugin") == typeof(PluginInterfaces.IImagePlugin))
                        {
                            IImagePlugin plugin = Activator.CreateInstance(type) as IImagePlugin;
                            _plugins[plugin.Name] = plugin;
                        }
                    }
                }
                catch (BadImageFormatException )
                {
                    //log--not one of ours!
                }
            }
        }

        private void CreatePluginMenu()
        {
            //dynamically create our menu from the plugin

            pluginsToolStripMenuItem.DropDownItems.Clear();
            
            foreach (KeyValuePair<string, PluginInterfaces.IImagePlugin> pair in _plugins)
            {
                ToolStripMenuItem menuItem = new ToolStripMenuItem(pair.Key);
                menuItem.Click += new EventHandler(menuItem_Click);
                pluginsToolStripMenuItem.DropDownItems.Add(menuItem);
            }
        }

        void menuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            PluginInterfaces.IImagePlugin plugin = _plugins[menuItem.Text];

            try
            {
                this.Cursor = Cursors.WaitCursor;
                pictureBox1.Image = plugin.RunPlugin(pictureBox1.Image);
            }
            catch (Exception ex)
            {
                //Never trust plugins!
                MessageBox.Show(ex.Message, "Plugin error");
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "All images (*.bmp, *.png, *.jpg)|*.bmp;*.png;*.jpg";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(ofd.FileName);
            }
        }
    }
}
