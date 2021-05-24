using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RCT3Pal
{
    class ConfigFile
    {
        private const string MainNodeName = "RCT3PalConfig";
        private const string NodeName_ExecutableDirectory = "ExecutableDirectory";
        private const string NodeName_OptionsDirectory = "OptionsDirectory";
        private const string NodeName_SaveDirectory = "SaveDirectory";

        public static bool Save(string path,
            string executableDirectory,
            string optionsDirectory,
            string saveDirectory)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement mainNode = xmlDoc.CreateElement(MainNodeName);
            xmlDoc.AppendChild(mainNode);

            XmlElement node_ExecutableDirectory = xmlDoc.CreateElement(
                NodeName_ExecutableDirectory);
            node_ExecutableDirectory.InnerText =
                executableDirectory;
            mainNode.AppendChild(node_ExecutableDirectory);

            XmlElement node_OptionsDirectory = xmlDoc.CreateElement(
                NodeName_OptionsDirectory);
            node_OptionsDirectory.InnerText =
                optionsDirectory;
            mainNode.AppendChild(node_OptionsDirectory);

            XmlElement node_SaveDirectory = xmlDoc.CreateElement(
                NodeName_SaveDirectory);
            node_SaveDirectory.InnerText =
                saveDirectory;
            mainNode.AppendChild(node_SaveDirectory);

            try
            {
                xmlDoc.Save(path);
            }
            catch (Exception ex)
            {

                System.Windows.Forms.MessageBox.Show(
                    ex.Message,
                    "Error Saving Config",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        
        public static bool Open(string path,
            out string executableDirectory,
            out string optionsDirectory,
            out string saveDirectory)
        {
            executableDirectory = "";
            optionsDirectory = "";
            saveDirectory = "";

            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.Load(path);
            }
            catch (Exception ex)
            {

                System.Windows.Forms.MessageBox.Show(
                    ex.Message,
                    "Error Loading Config",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error);
                return false;
            }

            XmlNode mainNode = xmlDoc.SelectSingleNode(MainNodeName);

            XmlNode node_ExecutableDirectory = mainNode.SelectSingleNode(
                NodeName_ExecutableDirectory);
            if (node_ExecutableDirectory != null)
                executableDirectory =
                    node_ExecutableDirectory.InnerText;

            XmlNode node_OptionsDirectory = mainNode.SelectSingleNode(
                NodeName_OptionsDirectory);
            if (node_OptionsDirectory != null)
                optionsDirectory =
                    node_OptionsDirectory.InnerText;

            XmlNode node_SaveDirectory = mainNode.SelectSingleNode(
                NodeName_SaveDirectory);
            if (node_SaveDirectory != null)
                saveDirectory =
                    node_SaveDirectory.InnerText;

            return true;
        }
    }
}
