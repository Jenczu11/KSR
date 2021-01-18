using SDC.Tools.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace SDC
{
    [RunInstaller(true)]
    public partial class WindowsServiceInstaller : Installer
    {
        ServiceProcessInstaller serviceProcessInstaller = new ServiceProcessInstaller();
        ServiceInstaller serviceInstaller = new ServiceInstaller();
        public WindowsServiceInstaller()
        {
            InitializeComponent();
            BaseLogs.WriteLog("Installing...");

            //# Service Account Information

            serviceProcessInstaller.Account = ServiceAccount.LocalSystem;
            serviceProcessInstaller.Username = null;
            serviceProcessInstaller.Password = null;

            //# Service Information

            //serviceInstaller.DisplayName = RunInstanceSettings.GetInstanceDesc();
            //serviceInstaller.Description = RunInstanceSettings.GetInstanceDesc();// "FoodSoft.PL Replication Service";
            serviceInstaller.StartType = ServiceStartMode.Automatic;
            // serviceInstaller.ServiceName = RunInstanceSettings.GetServiceName(); 

            //# This must be identical to the WindowsService.ServiceBase name

            //# set in the constructor of WindowsService.cs


            this.Installers.Add(serviceProcessInstaller);
            this.Installers.Add(serviceInstaller);
        }

        public override void Install(IDictionary stateSaver)
        {
            ConfigureInstaller(stateSaver);
            base.Install(stateSaver);
        }

        public override void Rollback(IDictionary savedState)
        {
            ConfigureInstaller(savedState);
            base.Rollback(savedState);
        }

        public override void Uninstall(IDictionary savedState)
        {
            ConfigureInstaller(savedState);
            base.Uninstall(savedState);
        }

        private void ConfigureInstaller(IDictionary savedState)
        {
            serviceInstaller.ServiceName = "ServiceDesk_Classification";
            serviceInstaller.DisplayName = "ServiceDesk_Classification";
        }

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr OpenSCManager(
            string lpMachineName,
            string lpDatabaseName,
            uint dwDesiredAccess);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr OpenService(
            IntPtr hSCManager,
            string lpServiceName,
            uint dwDesiredAccess);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private struct QUERY_SERVICE_CONFIG
        {
            public uint dwServiceType;
            public uint dwStartType;
            public uint dwErrorControl;
            public string lpBinaryPathName;
            public string lpLoadOrderGroup;
            public uint dwTagId;
            public string lpDependencies;
            public string lpServiceStartName;
            public string lpDisplayName;
        }

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool QueryServiceConfig(
            IntPtr hService,
            IntPtr lpServiceConfig,
            uint cbBufSize,
            out uint pcbBytesNeeded);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool ChangeServiceConfig(
            IntPtr hService,
            uint dwServiceType,
            uint dwStartType,
            uint dwErrorControl,
            string lpBinaryPathName,
            string lpLoadOrderGroup,
            IntPtr lpdwTagId,
            string lpDependencies,
            string lpServiceStartName,
            string lpPassword,
            string lpDisplayName);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool CloseServiceHandle(IntPtr hSCObject);

        private const uint SERVICE_NO_CHANGE = 0xffffffffu;
        private const uint SC_MANAGER_ALL_ACCESS = 0xf003fu;
        private const uint SERVICE_ALL_ACCESS = 0xf01ffu;
    }
}
