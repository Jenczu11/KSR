using KSR.Tools.Helpers;
using System;
using System.ServiceProcess;
using System.Threading;

namespace KSR
{
    partial class ClassificationService : ServiceBase
    {
        public ClassificationService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            System.Threading.Tasks.Task.Run(() =>
            {
                while (Thread.CurrentThread.ThreadState != ThreadState.AbortRequested)
                {
                    try
                    {
                        var serviceThread = new ServiceThread();
                        serviceThread.classify();
                    }
                    catch (Exception ex)
                    {
                        BaseLogs.WriteLog(ex);
                        Thread.Sleep(60000);
                    }
                }
            });
            
            

        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
        }



    }
}
