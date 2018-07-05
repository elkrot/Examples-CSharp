using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;

namespace GenericService
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer
    {
        private System.ServiceProcess.ServiceProcessInstaller genericProcessInstaller;
        private System.ServiceProcess.ServiceInstaller genericServiceInstaller;

        public ProjectInstaller()
        {
            genericProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();

            genericProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            genericProcessInstaller.Password = null;
            genericProcessInstaller.Username = null;

            genericServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            genericServiceInstaller.ServiceName = "GenericService";
            genericServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;

            this.Installers.AddRange(
                new System.Configuration.Install.Installer[] {
                    genericProcessInstaller,
                    genericServiceInstaller});

        }
    }
}
