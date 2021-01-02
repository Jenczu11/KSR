using KSR.Tools.Enums;
using KSR.Tools.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace KSR
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            
        }

        private void saveXML(string path, ServiceSettings settings)
        {
            XmlSerializer xs = new XmlSerializer(typeof(ServiceSettings));
            TextWriter tw = new StreamWriter(path);
            xs.Serialize(tw, settings);
            tw.Dispose();
            tw.Close();
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

        private void loadSettings(ServiceSettings settings)
        {
            SetServiceStatus("ServiceDesk_Classification");
            cmbClassifer.SelectedIndex = cmbClassifer.FindStringExact(settings.classifiers.ToString());
            cmbFrequeny.SelectedIndex = cmbFrequeny.FindStringExact(settings.frequency.ToString());
            cmbMetrics.SelectedIndex = cmbMetrics.FindStringExact(settings.metrics.ToString());
            cmbParts.SelectedIndex = cmbParts.FindStringExact(settings.parts.ToString());
            chSets.Checked = settings.onwSets;
            chStoplist.Checked = settings.stoplist;
            chStemmization.Checked = settings.stemmize;
            chNormalization.Checked = settings.normalization;
            teStoplistPath.Text = settings.stoplistPath;
            featur1.Checked = settings.features.Contains(FeaturesEnum.feature_1);
            featur2.Checked = settings.features.Contains(FeaturesEnum.feature_2);
            featur3.Checked = settings.features.Contains(FeaturesEnum.feature_3);
            featur4.Checked = settings.features.Contains(FeaturesEnum.feature_4);
            featur5.Checked = settings.features.Contains(FeaturesEnum.feature_5);
            featur6.Checked = settings.features.Contains(FeaturesEnum.feature_6);
        }

        private ServiceSettings saveSettings()
        {



            var result = new ServiceSettings();
            result.normalization = chNormalization.Checked;
            result.stoplist = chStoplist.Checked;
            result.stemmize = chStemmization.Checked;
            result.stoplistPath = teStoplistPath.Text;
            result.features = new HashSet<FeaturesEnum>();
            result.onwSets = chSets.Checked;
            if (featur1.Checked) result.features.Add(FeaturesEnum.feature_1);
            if (featur2.Checked) result.features.Add(FeaturesEnum.feature_2);
            if (featur3.Checked) result.features.Add(FeaturesEnum.feature_3);
            if (featur4.Checked) result.features.Add(FeaturesEnum.feature_4);
            if (featur5.Checked) result.features.Add(FeaturesEnum.feature_5);
            if (featur6.Checked) result.features.Add(FeaturesEnum.feature_6);

            ClassifiersEnum classifiersEnum;
            Enum.TryParse(cmbClassifer.Text, out classifiersEnum);
            result.classifiers = classifiersEnum;

            FrequencyEnum frequencyEnum;
            Enum.TryParse(cmbFrequeny.Text, out frequencyEnum);
            result.frequency = frequencyEnum;

            MetricsEnum metricsEnum;
            Enum.TryParse(cmbMetrics.Text, out metricsEnum);
            result.metrics = metricsEnum;

            PartsEnum partsEnum;
            Enum.TryParse(cmbParts.Text, out partsEnum);
            result.parts = partsEnum;

            return result;
        }



        private void btnInstall_Click(object sender, EventArgs e)
        {
            if(serviceStatusLabel.Text == "Odinstalowana")
            {
                WinServiceUtlis.InstallService(Application.ExecutablePath);
                WinServiceUtlis.RunService("ServiceDesk_Classification");
                MessageBox.Show("Usługa zainstalowana");
            }
            else
            {
                WinServiceUtlis.StopService("ServiceDesk_Classification");
                WinServiceUtlis.DeleteService("ServiceDesk_Classification");
                MessageBox.Show("Usługa odinstalowana");
            }
            Thread.Sleep(1000);
            SetServiceStatus("ServiceDesk_Classification");
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if(serviceStatusLabel.Text == "Uruchomiona")
            {
                WinServiceUtlis.StopService("ServiceDesk_Classification");
                MessageBox.Show("Usługa zatrzymana");
            }
            else
            {
                WinServiceUtlis.RunService("ServiceDesk_Classification");
                MessageBox.Show("Usługa uruchomiona");
            }
            Thread.Sleep(1000);
            SetServiceStatus("ServiceDesk_Classification");
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (!WinServiceUtlis.IsAnAdministrator())
            {
                MessageBox.Show("Aplikacja do prawidłowego działania wymaga uprawnień administratora");
                Close();
            }
            SetCombobox(cmbClassifer, typeof(ClassifiersEnum));
            SetCombobox(cmbFrequeny, typeof(FrequencyEnum));
            SetCombobox(cmbMetrics, typeof(MetricsEnum));
            SetCombobox(cmbParts, typeof(PartsEnum));

            var path = AppDomain.CurrentDomain.BaseDirectory;
            var settingsPath = Path.Combine(path, "settings.xml");
            var settings = new ServiceSettings();
            if (!File.Exists(settingsPath))
            {
                MessageBox.Show("Brak pliku konfiguracyjnego. Proszę skonfigurować aplikację");
            }
            else
            {
                settings = loadXML(settingsPath);
                loadSettings(settings);
            }
        }


        private void SetCombobox(ComboBox comboBox, Type type, int selected = 0)
        {
            comboBox.Items.Clear();
            var values = Enum.GetValues(type);
            foreach (var item in values)
            {
                comboBox.Items.Add(item);
            }
            comboBox.SelectedIndex = selected;
        }

        private void teStoplistPath_MouseClick(object sender, MouseEventArgs e)
        {
            using (var form = new OpenFileDialog())
            {
                form.Filter = "Plik txt|*.txt";
                if (form.ShowDialog() == DialogResult.OK)
                {
                    teStoplistPath.Text = form.FileName;
                }
            }
        }

        private void btnSaveSettings_Click(object sender, EventArgs e)
        {
            if ((string.IsNullOrWhiteSpace(teStoplistPath.Text) && chStoplist.Checked))
            {
                MessageBox.Show("Brak wybranego pliku ze stoplistą");
                return;
            }
            if (!File.Exists(teStoplistPath.Text) && chStoplist.Checked)
            {
                MessageBox.Show("Plik stoplisty nie istnieje");
                return;
            }



            var settings = saveSettings();
            if(settings.features.Count == 0)
            {
                MessageBox.Show("Brak wybranych cech do ekstrakcji");
                return;
            }
            var path = AppDomain.CurrentDomain.BaseDirectory;
            var settingsPath = Path.Combine(path, "settings.xml");
            saveXML(settingsPath, settings);
            MessageBox.Show("Ustawienia aplikacji zapisane");
        }

        private void SetServiceStatus(string serwis)
        {
            switch (WinServiceUtlis.GetServiceStatus(serwis))
            {
                case WinServiceUtlis.ServiceStatus.NotInstaled:
                    {
                        //MessageBox.Show("Usługa ServiceDesk_Classification nie jest zainstalowana");
                        serviceStatusLabel.Text = "Odinstalowana";
                        btnInstall.Text = "Zainstaluj";
                        btnStart.Text = "Uruchom";
                        btnStart.Enabled = false;
                        break;
                    }
                case WinServiceUtlis.ServiceStatus.Running:
                    {
                        //MessageBox.Show("Usługa ServiceDesk_Classification jest uruchomiona");
                        serviceStatusLabel.Text = "Uruchomiona";
                        btnInstall.Text = "Odinstaluj";
                        btnStart.Text = "Zatrzymaj";
                        btnStart.Enabled = true;
                        break;
                    }
                default:
                    {
                        //MessageBox.Show("Usługa ServiceDesk_Classification nie jest uruchomiona");
                        serviceStatusLabel.Text = "Zatrzymana";
                        btnInstall.Text = "Odinstaluj";
                        btnStart.Text = "Uruchom";
                        btnStart.Enabled = true;
                        break;
                    }

            }
        }
    }
}
