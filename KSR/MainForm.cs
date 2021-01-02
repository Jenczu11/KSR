using KSR.Tools.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
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
            cmbClassifer.SelectedItem = settings.classifiers.ToString();
            cmbFrequeny.SelectedItem = settings.frequency.ToString();
            cmbMetrics.SelectedItem = settings.metrics.ToString();
            cmbParts.SelectedItem = settings.parts.ToString();
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

        }

        private void btnStart_Click(object sender, EventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {

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
    }
}
