using KSR.Tools.Factories;
using KSR.Tools.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

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
                }
            }
            

        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
        }



    }
}
