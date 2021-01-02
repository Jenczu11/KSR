using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceProcess;// reference to System.ServiceProcess.dll
using System.Configuration.Install;
using System.Diagnostics;
using System.Security.Principal;

namespace KSR.Tools.Helpers
{
    public class WinServiceUtlis
    {

        /// <summary>
        /// Mozliwe statusy seriwsu
        /// </summary>
        public enum ServiceStatus
        {

            Stopped = 1,

            StartPending = 2,

            StopPending = 3,

            Running = 4,

            ContinuePending = 5,

            PausePending = 6,

            Paused = 7,

            NotInstaled = 8
        }

        /// <summary>
        /// Zwróć status windows service
        /// </summary>
        /// <param name="ServiceName"></param>
        /// <returns>Status serwisu lub NULL jeżeli nie zainstalowany</returns>
        public static ServiceStatus GetServiceStatus(string ServiceName)
        {

            ServiceController ctl =
                ServiceController.GetServices().Where(s => s.ServiceName.ToLower() == ServiceName.ToLower()).FirstOrDefault();

            if (ctl == null)
                return ServiceStatus.NotInstaled;
            else
                return (ServiceStatus)ctl.Status;
        }

        /// <summary>
        /// Uruchom wskazany serwis
        /// </summary>
        /// <param name="ServiceName"></param>
        /// <returns></returns>
        public static void RunService(string ServiceName)
        {

            ServiceController service = new ServiceController(ServiceName);
            if (service.Status == ServiceControllerStatus.Stopped)
                service.Start();
        }

        /// <summary>
        /// Uruchom wskazany serwis
        /// </summary>
        /// <param name="ServiceName"></param>
        /// <returns></returns>
        public static void RunService(string ServiceName, string[] args)
        {
            ServiceController service = new ServiceController(ServiceName);
            if (service.Status == ServiceControllerStatus.Stopped)
                service.Start(args);
        }

        /// <summary>
        /// Uruchamia wskazy serwis
        /// </summary>
        /// <param name="ServiceName"></param>
        public static void StopService(string ServiceName)
        {

            ServiceController service = new ServiceController(ServiceName);
            if (service.Status == ServiceControllerStatus.Running)
                service.Stop();
        }

        /// <summary>
        /// Istaluje usługę ze wskazanego obrazu
        /// </summary>
        /// <param name="ServicePath"></param>
        public static void InstallService(string ServicePath)
        {
            ManagedInstallerClass.InstallHelper(new string[] { ServicePath });
        }

        /// <summary>
        /// Istaluje usługę ze wskazanego obrazu
        /// </summary>
        /// <param name="ServicePath"></param>
        public static void InstallService(string ServicePath, string[] args)
        {
            string[] arg = new string[args.Length + 1];
            int i = 0;
            for (i = 0; i < args.Length; i++)
            {
                arg[i] = args[i];
            }

            arg[i] = ServicePath;

            ManagedInstallerClass.InstallHelper(arg);






        }

        /// <summary>
        /// Odinstaluj usługę ze wskazanego orbazu
        /// </summary>
        /// <param name="ServicePath"></param>
        public static void UninstallService(string ServicePath)
        {
            ManagedInstallerClass.InstallHelper(new string[] { "/u", ServicePath });
        }

        /// <summary>
        /// Usuwa wskazany serwis
        /// </summary>
        /// <param name="ServiceName"></param>
        public static void DeleteService(string ServiceName)
        {

            System.Diagnostics.ProcessStartInfo procStartInfo = new System.Diagnostics.ProcessStartInfo(Environment.SystemDirectory + @"\SC.EXE", " DELETE " + ServiceName);

            procStartInfo.RedirectStandardOutput = true;
            procStartInfo.UseShellExecute = false;

            // Do not create the black window.
            procStartInfo.CreateNoWindow = true;
            procStartInfo.WindowStyle = ProcessWindowStyle.Hidden;

            System.Diagnostics.Process process = new System.Diagnostics.Process();
            process.StartInfo = procStartInfo;
            process.Start();

            // System.Diagnostics.Process.Start(Environment.SystemDirectory + @"\SC.EXE"," DELETE " + ServiceName);
        }

        public static bool IsAnAdministrator()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }


    }
}
