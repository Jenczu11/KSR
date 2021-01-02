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
        private ServiceSettings settings { get; set; }
        public ClassificationService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // TODO: Add code here to start your service.
            var path = AppDomain.CurrentDomain.BaseDirectory;
            var settingsPath = Path.Combine(path, "settings.xml");
            settings = loadXML(settingsPath);
        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
        }



        private ServiceSettings loadXML(string path)
        {
            var result = new ServiceSettings();
            XmlSerializer xs = new XmlSerializer(typeof(ServiceSettings));
            using (var sr = new StreamReader(path))
            {
                result = (ServiceSettings)xs.Deserialize(sr);
            }
            return result;
        }
    }
}
