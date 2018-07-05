using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace EnumerateAssemblyTypes
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Assemblies (*.exe,*.dll)|*.exe;*.dll|All files (*.*)|*.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                labelAssembly.Text = ofd.FileName;
                ReflectAssembly(labelAssembly.Text);
            }
        }

        private void ReflectAssembly(string filename)
        {
            treeView.Nodes.Clear();

            Assembly assembly = Assembly.LoadFrom(filename);
            foreach (Type t in assembly.GetTypes())
            {
                TreeNode typeNode = new TreeNode("(T) " + t.Name); 
                treeView.Nodes.Add(typeNode);
                //get methods
                foreach (MethodInfo mi in t.GetMethods())
                {
                    typeNode.Nodes.Add(new TreeNode("(M) "+mi.Name));
                }
                //get properties
                foreach (PropertyInfo pi in t.GetProperties())
                {
                    typeNode.Nodes.Add(new TreeNode("(P) "+pi.Name));
                }
                //get fields
                foreach (FieldInfo fi in t.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
                {
                    typeNode.Nodes.Add(new TreeNode("(F) "+fi.Name));
                }
                //get events
                foreach (EventInfo ei in t.GetEvents())
                {
                    typeNode.Nodes.Add(new TreeNode("(E) "+ei.Name));
                }

                //instead of all that, you could just use t.GetMembers to return an array MemberInfo (base class to all the above)
            }
        }
    }
}
