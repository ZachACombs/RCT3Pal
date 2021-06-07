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
        private const string NodeName_DontShowBeginningWarning = "DontShowBeginningWarning";

        public static bool Save(string path,
            string executableDirectory,
            string optionsDirectory,
            string saveDirectory,
            bool dontShowBeginningWarning)
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

            if (dontShowBeginningWarning)
            {
                XmlElement node_DontShowBeginningWarning = xmlDoc.CreateElement(
                    NodeName_DontShowBeginningWarning);
                mainNode.AppendChild(node_DontShowBeginningWarning);
            }

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
            out string saveDirectory,
            out bool dontShowBeginningWarning)
        {
            executableDirectory = "";
            optionsDirectory = "";
            saveDirectory = "";
            dontShowBeginningWarning = false;

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

            XmlNode node_DontShowBeginningWarning = mainNode.SelectSingleNode(
                NodeName_DontShowBeginningWarning);
            dontShowBeginningWarning = (node_DontShowBeginningWarning != null);

            return true;
        }
    }
}
