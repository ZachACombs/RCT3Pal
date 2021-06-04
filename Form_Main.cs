using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace RCT3Pal
{
    public struct OptionValue_Bool
    {
        private OptionControl_Bool Var_OptionControl;
        public bool ControlEnabled
        {
            get { return Var_OptionControl.Enabled; }
            set { Var_OptionControl.Enabled = value; }
        }
        public void PlaceControlIntoAnotherControl(Control parentControl)
        {
            parentControl.Controls.Add(Var_OptionControl);
            Var_OptionControl.Size = new Size(
                parentControl.Size.Width - 25,
                Var_OptionControl.Size.Height);
        }

        public bool IsTrue
        {
            get
            {
                return Var_OptionControl.IsTrue;
            }
            set
            {
                Var_OptionControl.IsTrue = value;
            }
        }

        public OptionValue_Bool(string optionName, bool isTrue)
        {
            Var_OptionControl = new OptionControl_Bool(optionName, isTrue);
        }
    }
    public struct OptionValue_Choice
    {
        private OptionControl_Choice Var_OptionControl;
        public bool ControlEnabled
        {
            get { return Var_OptionControl.Enabled; }
            set { Var_OptionControl.Enabled = value; }
        }
        public void PlaceControlIntoAnotherControl(Control parentControl)
        {
            parentControl.Controls.Add(Var_OptionControl);
            Var_OptionControl.Size = new Size(
                parentControl.Size.Width - 25,
                Var_OptionControl.Size.Height);
        }

        public int SelectedIndex
        {
            get
            {
                return Var_OptionControl.SelectedIndex;
            }
            set
            {
                Var_OptionControl.SelectedIndex = value;
            }
        }

        public OptionValue_Choice(string optionName, int defaultIndex, string[] choices)
        {
            Var_OptionControl = new OptionControl_Choice(optionName, defaultIndex, choices);
        }
    }
    public struct OptionValue_Int
    {
        private OptionControl_Int Var_OptionControl;
        public bool ControlEnabled
        {
            get { return Var_OptionControl.Enabled; }
            set { Var_OptionControl.Enabled = value; }
        }
        public void PlaceControlIntoAnotherControl(Control parentControl)
        {
            parentControl.Controls.Add(Var_OptionControl);
            Var_OptionControl.Size = new Size(
                parentControl.Size.Width - 25,
                Var_OptionControl.Size.Height);
        }

        public int MinValue
        {
            get
            {
                return Var_OptionControl.Min;
            }
        }
        public int MaxValue
        {
            get
            {
                return Var_OptionControl.Max;
            }
        }
        public int Value
        {
            get
            {
                return Var_OptionControl.Get_Value();
            }
            set
            {
                Var_OptionControl.Set_Value(value);
            }
        }

        public OptionValue_Int(string optionName, int defaultValue, int min, int max)
        {
            Var_OptionControl = new OptionControl_Int(optionName, defaultValue, min, max);
        }
    }

    public partial class Form_Main : Form
    {
        //Options
        #region
        private struct OptionFileArgument
        {
            ///<summary>Literal value of argument</summary>
            public string Value;
            ///<summary>Whether or not value is a string</summary>
            public bool IsAString;

            public OptionFileArgument(string value, bool isAString)
            {
                Value = value;
                IsAString = isAString;
            }
        }
        private Dictionary<string, object> KnownOptions =
            new Dictionary<string, object>();
        private Dictionary<string, string> UnknownOptions = 
            new Dictionary<string, string>();
        public void AddKnownOption_Bool(string optionName, bool isTrue)
        {
            OptionValue_Bool optionValue = new OptionValue_Bool(optionName, isTrue);
            KnownOptions.Add(optionName, optionValue);
            optionValue.PlaceControlIntoAnotherControl(Panel_Options_Main);
        }
        public void AddKnownOption_Choice(string optionName, int defaultIndex, params string[] choices)
        {
            OptionValue_Choice optionValue = new OptionValue_Choice(optionName, defaultIndex, choices);
            KnownOptions.Add(optionName, optionValue);
            optionValue.PlaceControlIntoAnotherControl(Panel_Options_Main);
        }
        public void AddKnownOption_Int(string optionName, int defaultValue)
        {
            OptionValue_Int optionValue = new OptionValue_Int(optionName, defaultValue, int.MinValue, int.MaxValue);
            KnownOptions.Add(optionName, optionValue);
            optionValue.PlaceControlIntoAnotherControl(Panel_Options_Main);
        }
        public void AddKnownOption_Int(string optionName, int defaultValue, int min)
        {
            OptionValue_Int optionValue = new OptionValue_Int(optionName, defaultValue, min, int.MaxValue);
            KnownOptions.Add(optionName, optionValue);
            optionValue.PlaceControlIntoAnotherControl(Panel_Options_Main);
        }
        public void AddKnownOption_Int(string optionName, int defaultValue, int min, int max)
        {
            OptionValue_Int optionValue = new OptionValue_Int(optionName, defaultValue, min, max);
            KnownOptions.Add(optionName, optionValue);
            optionValue.PlaceControlIntoAnotherControl(Panel_Options_Main);
        }
        private string ReadStringFromString(string str, int startPos, out int endPos) //endPos is end of string chunk (prev char is closing ")
        {
            //First position must have quotation marks
            endPos = startPos;
            char[] ccc = str.ToCharArray();
            if (ccc[startPos] != 0x22)
                return "";
            endPos += 1;
            List<char> newStr_Char = new List<char>();
            while (endPos < ccc.Length)
            {
                char cccc = ccc[endPos];
                if (cccc == 0x22)
                {
                    endPos += 1;
                    break;
                }
                newStr_Char.Add(cccc);
                endPos += 1;
            }
            return new string(newStr_Char.ToArray());
        }

        //Options_Load
        #region
        private void Fun_Options_Load(string[] lines)
        {
            List<OptionFileArgument> extractArguments(string line)//value, whether or not value is a string
            {
                List<OptionFileArgument> args = new List<OptionFileArgument>();

                char[] line_char = line.ToCharArray();
                int pos = 0;
                int valueIndex = 0;
                while (pos < line_char.Length)
                {
                    char cc = line_char[pos];
                    if (cc != 0x20)
                    {
                        if (cc == 0x22)
                        {
                            args.Add(new OptionFileArgument(ReadStringFromString(line, pos, out pos), true));
                        }
                        else
                        {
                            List<char> arg_Chars = new List<char>();
                            arg_Chars.Add(cc);
                            pos += 1;
                            while (pos < line_char.Length)
                            {
                                cc = line_char[pos];
                                if (cc == 0x20)
                                    break;
                                arg_Chars.Add(cc);
                                pos += 1;
                            }
                            args.Add(new OptionFileArgument(new string(arg_Chars.ToArray()), false));
                        }
                    }
                    pos += 1;
                }

                return args;
            }

            UnknownOptions.Clear();

            for (int ll = 0; ll < lines.Length; ll += 1)
            {
                string line = lines[ll];
                int optionNameLength = line.IndexOf(" ");
                string line_name;
                string line_value;
                if (optionNameLength == -1)
                {
                    line_name = line;
                    line_value = "";
                }
                else
                {
                    line_name = line.Substring(0, optionNameLength);
                    line_value = line.Substring(optionNameLength + 1).TrimStart((char)0x20);
                }
                if (KnownOptions.ContainsKey(line_name))
                {
                    List<OptionFileArgument> args_AsStrings = extractArguments(line_value);

                    object optionValue = KnownOptions[line_name];

                    if (optionValue.GetType() == typeof(OptionValue_Bool))
                    {
                        OptionValue_Bool v = (OptionValue_Bool)optionValue;
                        
                        if (args_AsStrings.Count == 0)
                        {
                            v.IsTrue = true;
                        }
                        else if (args_AsStrings[0].IsAString)
                        {
                            v.IsTrue = false;
                        }
                        else
                        {
                            v.IsTrue = (args_AsStrings[0].Value == "1");
                        }
                    }
                    else if (optionValue.GetType() == typeof(OptionValue_Choice))
                    {
                        OptionValue_Choice v = (OptionValue_Choice)optionValue;

                        if (args_AsStrings.Count == 0)
                        {
                            v.SelectedIndex = 0;
                        }
                        else if (args_AsStrings[0].IsAString)
                        {
                            v.SelectedIndex = 0;
                        }
                        else
                        {

                            if (int.TryParse(args_AsStrings[0].Value, out int vv))
                                v.SelectedIndex = vv;
                            else
                                v.SelectedIndex = 0;
                        }
                    }
                    else if (optionValue.GetType() == typeof(OptionValue_Int))
                    {
                        OptionValue_Int v = (OptionValue_Int)optionValue;

                        if (args_AsStrings.Count == 0)
                        {
                            v.Value = v.MinValue;
                        }
                        else if (args_AsStrings[0].IsAString)
                        {
                            v.Value = v.MinValue;
                        }
                        else
                        {
                            if (int.TryParse(args_AsStrings[0].Value, out int vv))
                                v.Value = vv;
                            else
                                v.Value = v.MinValue;
                        }
                    }
                }
                else
                {
                    UnknownOptions.Add(line_name, line_value);
                }
            }
        }
        private void Options_Load()
        {
            Fun_Options_Load(File.ReadAllLines(Config_OptionsDirectory_Options));
        }
        private void Options_Load(string[] lines)
        {
            Fun_Options_Load(lines);
        }
        #endregion

        //Options_Save
        #region
        private void Fun_Options_Save(out string[] lines)
        {
            List<string> lines_List = new List<string>();
            foreach (KeyValuePair<string, object> kvp in KnownOptions)
            {
                string line = kvp.Key;
                
                if (kvp.Value.GetType() == typeof(OptionValue_Bool))
                {
                    line = line + " " + (((OptionValue_Bool)kvp.Value).IsTrue ?
                        "1" : "0");
                }
                else if (kvp.Value.GetType() == typeof(OptionValue_Choice))
                {
                    line = line + " " + ((OptionValue_Choice)kvp.Value).SelectedIndex.ToString();
                }
                else if (kvp.Value.GetType() == typeof(OptionValue_Int))
                {
                    line = line + " " + ((OptionValue_Int)kvp.Value).Value.ToString();
                }

                lines_List.Add(line);
            }
            foreach (KeyValuePair<string, string> kvp in UnknownOptions)
            {
                lines_List.Add(kvp.Key + " " + kvp.Value);
            }
            lines = lines_List.ToArray();
        }
        private void Options_Save()
        {
            Fun_Options_Save(out string[] lines);
            File.WriteAllLines(Config_OptionsDirectory_Options, lines);
        }
        private void Options_Save(string path)
        {
            Fun_Options_Save(out string[] lines);
            File.WriteAllLines(path, lines);
        }
        private void Options_Save(out string[] lines)
        {
            Fun_Options_Save(out lines);
        }
        #endregion

        #endregion

        //Custom Content
        private const string Prefix_Custom = "RCT3Pal_Custom_";
        private const string Prefix_Original = "RCT3Pal_Original_";

        //RCT3 Process
        private System.Diagnostics.Process RCT3Process;
        private bool RCT3Process_IsRunning;
        private bool TryToKillRCT3()
        {
            try
            {
                RCT3Process.Kill();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error Stopping RCT3",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        //Directory of this program
        private string ProgramDirectory; 

        //Config
        #region
        private string ConfigFilePath;


        private string Config_ExecutableDirectory;
        private string Config_ExecutableDirectory_Executable;
        private bool IfExists_ExecutableDirectory()
        {
            if (!Directory.Exists(Config_ExecutableDirectory))
            {
                MessageBox.Show(
                    "Could not find RCT3 directory",
                    "Could Not Find RCT3 Directory",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        private bool IfExists_ExecutableDirectory_Executable()
        {
            if (!File.Exists(Config_ExecutableDirectory_Executable))
            {
                MessageBox.Show(
                    "Could not find RCT3 executable",
                    "Could Not Find RCT3 Executable",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        private void Set_ExecutableDirectory(string executableDirectory)
        {
            Config_ExecutableDirectory = executableDirectory;
            Config_ExecutableDirectory_Executable = Fun.DirectoryAndFile(
                executableDirectory,
                "RCT3.exe");

            Button_RCT3.Enabled = false;
            if (!IfExists_ExecutableDirectory())
                return;
            if (!IfExists_ExecutableDirectory_Executable())
                return;
            Button_RCT3.Enabled = true;
        }
        

        private string Config_OptionsDirectory;
        private string Config_OptionsDirectory_Options;
        private bool IfExists_OptionsDirectory()
        {
            if (!Directory.Exists(Config_OptionsDirectory))
            {
                MessageBox.Show(
                    "Could not find Options directory",
                    "Could Not Find Options Directory",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        private bool IfExists_OptionsDirectory_Options()
        {
            if (!File.Exists(Config_OptionsDirectory_Options))
            {
                MessageBox.Show(
                    "Could not find Options File",
                    "Could Not Find Options File",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        private void Set_OptionsDirectory(string optionsDirectory)
        {
            Config_OptionsDirectory = optionsDirectory;
            Config_OptionsDirectory_Options = Fun.DirectoryAndFile(
                optionsDirectory,
                "Options.txt");

            GroupBox_Options.Enabled = false;
            if (!IfExists_OptionsDirectory())
                return;
            if (!IfExists_OptionsDirectory_Options())
                return;
            GroupBox_Options.Enabled = true;

            Options_Load();
        }


        private string Config_SaveDirectory;
        private bool IfExists_SaveDirectory()
        {
            if (!Directory.Exists(Config_SaveDirectory))
            {
                MessageBox.Show(
                    "Could not find Save directory",
                    "Could Not Find Save Directory",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        private void Set_SaveDirectory(string saveDirectory)
        {
            Config_SaveDirectory = saveDirectory;

            GroupBox_CustomContent.Enabled = false;
            if (!IfExists_SaveDirectory())
                return;
            GroupBox_CustomContent.Enabled = true;
        }


        private void OpenConfig()
        {
            Form_Config config = new Form_Config(
                Config_ExecutableDirectory, 
                Config_OptionsDirectory,
                Config_SaveDirectory);
            if (config.ShowDialog() == DialogResult.Cancel)
                return;
            Set_ExecutableDirectory(config.ExecutableDirectory);
            Set_OptionsDirectory(config.OptionsDirectory);
            Set_SaveDirectory(config.SaveDirectory);
            ConfigFile.Save(ConfigFilePath,
                Config_ExecutableDirectory,
                Config_OptionsDirectory,
                Config_SaveDirectory);
        }
        #endregion

        public Form_Main()
        {
            RCT3Process = null;
            RCT3Process_IsRunning = false;

            ProgramDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            InitializeComponent();

            AddKnownOption_Bool("TrackAllowSameTrackIntersect", false);
            AddKnownOption_Bool("AttractionSceneryAllowSceneryIntersect", false);
            AddKnownOption_Bool("AttractionSceneryAllowTerrainIntersect", false);
            AddKnownOption_Choice("TemperatureUnits", 1, "Celsius", "Fahrenheit");
            AddKnownOption_Choice("Units", 1, "Metric", "Imperial", "SI");
            AddKnownOption_Choice("Currency", 1, "£", "$", "€", "¥", "Kr", "NT$", "AU$", "NZ$", "HK$", "SG$");
            AddKnownOption_Choice("ScreenGFDriver", 0, "GF2", "GF4");
            AddKnownOption_Choice("ScreenPixelFormat", 0, "A8R8G8B8", "R5G6B5", "X8R8G8B8");
            AddKnownOption_Int("MusicVolume", 100, 0, 100);
            AddKnownOption_Int("GUIVolume", 100, 0, 100);
            AddKnownOption_Int("GameVolume", 100, 0, 100);


            //Load configurations
            #region
            ConfigFilePath = ProgramDirectory + "\\RCT3Pal_Config.xml";
            Config_ExecutableDirectory = "";
            Config_OptionsDirectory = "";
            if (!File.Exists(ConfigFilePath))
            {
                OpenConfig();
            }
            else
            {
                if (ConfigFile.Open(ConfigFilePath,
                    out string executableDirectory,
                    out string optionsDirectory,
                    out string saveDirectory))
                {
                    Set_ExecutableDirectory(executableDirectory);
                    Set_OptionsDirectory(optionsDirectory);
                    Set_SaveDirectory(saveDirectory);
                }

            }
            #endregion
        }


        private void SetButtons(bool rct3IsRunning)
        {
            Button_RCT3.Text = rct3IsRunning ? "Stop RCT3" : "Play RCT3";

            MenuItem_Config.Enabled = (!rct3IsRunning);

            //Options
            Button_OtherOptions.Enabled = (!rct3IsRunning);
            Button_UpdateOptions.Enabled = (!rct3IsRunning);
            foreach (KeyValuePair<string, object> kvp in KnownOptions)
            {
                if (kvp.Value.GetType() == typeof(OptionValue_Bool))
                {
                    OptionValue_Bool value = ((OptionValue_Bool)kvp.Value);
                    value.ControlEnabled = (!rct3IsRunning);
                }
                else if (kvp.Value.GetType() == typeof(OptionValue_Choice))
                {
                    OptionValue_Choice value = ((OptionValue_Choice)kvp.Value);
                    value.ControlEnabled = (!rct3IsRunning);
                }
                else if (kvp.Value.GetType() == typeof(OptionValue_Int))
                {
                    OptionValue_Int value = ((OptionValue_Int)kvp.Value);
                    value.ControlEnabled = (!rct3IsRunning);
                }
            }

            //Custom content
            Button_CreateCustom.Enabled = (!rct3IsRunning);
        }
        private async void RunRCT3()
        {
            string NotSoRandomFileName(string pathPrefix, string ext) //When you just need a file name that isn't already taken
            {
                int n = 0;
                while (File.Exists(pathPrefix + n.ToString() + ext))
                    n += 1;
                return pathPrefix + n.ToString() + ext;
            }
            bool TryToDeleteDirectories(string path, string searchPattern)
            {
                try
                {
                    string[] dirs = Directory.GetDirectories(path, searchPattern);
                    foreach (string dir in dirs)
                        Directory.Delete(dir, true);
                }
                catch(Exception ex)
                {
                    Fun.ShowErrorMessage("Could not remove specified folders from \"" + path + "\".", "Error Removing Folders", ex);
                    return false;
                }
                return true;
            }

            if (!TryToDeleteDirectories(Config_ExecutableDirectory, Prefix_Original + "*"))
                return;

            bool mod_Options = true;
            if (!IfExists_OptionsDirectory())
                mod_Options = false;
            else if (!IfExists_OptionsDirectory_Options())
                mod_Options = false;

            //Backup and modify Options file (if modding options)
            string temp_OptionsFile;
            #region
            temp_OptionsFile = "";
            if (mod_Options)
            {
                temp_OptionsFile = NotSoRandomFileName(ProgramDirectory + "\\Options", ".txt");
                File.Move(Config_OptionsDirectory_Options, temp_OptionsFile);
                Options_Save();
            }
            #endregion

            //Run RCT3 
            #region
            try
            {
                using (System.Diagnostics.Process myProcess = new Process())
                {
                    myProcess.StartInfo.FileName = Config_ExecutableDirectory_Executable;
                    myProcess.StartInfo.WorkingDirectory = Config_ExecutableDirectory;
                    myProcess.Start();
                    SetButtons(true);
                    Status_Main.Text = "Running RCT3";
                    RCT3Process = myProcess;
                    RCT3Process_IsRunning = true;
                    await Task.Run(() =>
                    {
                        while (!myProcess.HasExited)
                        {

                        }
                    }
                    );
                    RCT3Process = null;
                    RCT3Process_IsRunning = false;
                    SetButtons(false);
                    Status_Main.Text = "Program exited with code: " + myProcess.ExitCode.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error Running RCT3",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                RCT3Process = null;
                RCT3Process_IsRunning = false;
                SetButtons(false);
                Status_Main.Text = "Error running RCT3";
            }
            #endregion

            //Replace Backed Up Options File (if modding options)
            #region
            if (mod_Options)
            {
                File.Delete(Config_OptionsDirectory_Options);
                File.Move(temp_OptionsFile, Config_OptionsDirectory_Options);
            }
            #endregion
        }
        private void Button_RCT3_Click(object sender, EventArgs e)
        {
            if (!RCT3Process_IsRunning)
            {
                RunRCT3();
            }
            else
            {
                if (MessageBox.Show(
                        "Are you sure you want to stop RCT3",
                        "Are you sure",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning) == DialogResult.No)
                    return;

                TryToKillRCT3();
            }
        }

        private void MenuItem_Config_Click(object sender, EventArgs e)
        {
            OpenConfig();
        }

        private void Form_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (RCT3Process_IsRunning)
            {
                DialogResult d = MessageBox.Show(
                    "Close RCT3 first?",
                    "Close RCT3",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Warning);
                if (d == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
                if (d == DialogResult.Yes)
                {
                    if (!TryToKillRCT3())
                    {
                        e.Cancel = true;
                        return;
                    }
                }
            }
        }

        private void Button_UpdateOptions_Click(object sender, EventArgs e)
        {
            Options_Save();
        }

        private void Button_ExportOptions_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = ".txt";
            saveFileDialog.Filter = "Text File|*.txt|All Files|*.*";
            saveFileDialog.Title = "Export Options";
            saveFileDialog.AddExtension = true;
            saveFileDialog.OverwritePrompt = true;
            if (saveFileDialog.ShowDialog() != DialogResult.OK)
                return;
            Options_Save(saveFileDialog.FileName);
        }

        private void Button_CreateCustom_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
                "This will delete any existing Custom Folders in Executable Directory and Save Directory. Is this OK?",
                "Is This OK",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning) == DialogResult.No)
                return;

            if (Form_CreateCustomFolders.Perform(
                Config_ExecutableDirectory,
                Config_SaveDirectory,
                Prefix_Custom,
                Prefix_Original) == DialogResult.Cancel)
                return;
        }

        private void MenuItem_About_Click(object sender, EventArgs e)
        {
            (new Form_About()).ShowDialog();
        }

        private void Button_OtherOptions_Click(object sender, EventArgs e)
        {
            Form_OtherOptions.EditOtherOptions(UnknownOptions);
        }

        private void Panel_Options_Main_Resize(object sender, EventArgs e)
        {
            for (int n = 0; n < Panel_Options_Main.Controls.Count; n += 1)
            {
                Panel_Options_Main.Controls[n].Size = new Size(
                    Panel_Options_Main.Width - 25,
                    Panel_Options_Main.Controls[n].Size.Height);
            }
        }
    }
}
