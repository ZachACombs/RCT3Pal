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
    public enum OptionValueType
    {
        UnknownValue = -1,
        BoolValue = 0,
        IntValue = 1,
        FloatValue = 2,
        StringValue = 3,
    }
    public struct OptionValue
    {
        public OptionValueType ValueType;
        public object Value;

        public OptionValue(object value)
        {
            Type tt = value.GetType();
            if (tt == typeof(bool))
                ValueType = OptionValueType.BoolValue;
            else if (tt == typeof(int))
                ValueType = OptionValueType.IntValue;
            else if (tt == typeof(float))
                ValueType = OptionValueType.FloatValue;
            else if (tt == typeof(string))
                ValueType = OptionValueType.StringValue;
            else
                ValueType = OptionValueType.UnknownValue;
            Value = value;
        }
        public OptionValue(OptionValueType valueType, object value)
        {
            ValueType = valueType;
            Value = value;
        }
    }

    public partial class Form_Main : Form
    {
        //Options
        #region
        private Dictionary<string, (Control, List<OptionValue>)> KnownOptions =
            new Dictionary<string, (Control, List<OptionValue>)>();
        private Dictionary<string, List<OptionValue>> Options =
            new Dictionary<string, List<OptionValue>>();
        private Dictionary<string, string> UnknownOptions = 
            new Dictionary<string, string>();
        public void AddKnownOption_Bool(string optionName, bool isTrue)
        {
            OptionControl_Bool control = new OptionControl_Bool(this, optionName, isTrue);
            KnownOptions.Add(optionName, (
                control, ListOfValues(
                new OptionValue(OptionValueType.BoolValue, isTrue)
                )));
            SetOption(optionName, new OptionValue(isTrue));
            Panel_Options.Controls.Add(control);
        }
        public void SetOption(string optionName, params OptionValue[] values)
        {
            if (!Options.ContainsKey(optionName))
                Options.Add(optionName, values.ToList());
            else
                Options[optionName] = values.ToList();
        }
        private List<OptionValue> ListOfValues(params OptionValue[] values)
        {
            return values.ToList();
        }
        private string OptionAndValues(string option, List<OptionValue> values)
        {
            string str = option;
            foreach (OptionValue value in values)
            {
                if (value.ValueType == OptionValueType.UnknownValue)
                    str = str + " " + (string)value.Value;
                if (value.ValueType == OptionValueType.BoolValue)
                    str = str + ((bool)value.Value ? " 1" : " 0");
                if (value.ValueType == OptionValueType.IntValue)
                    str = str + " " + ((int)value.Value).ToString();
                if (value.ValueType == OptionValueType.FloatValue)
                    str = str + " " + ((float)value.Value).ToString();
                if (value.ValueType == OptionValueType.StringValue)
                    str = str + " " + StringToString((string)value.Value);
            }
            return str;
        }
        private string StringToString(string str)
        {
            return "\"" + str + "\"";
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
            List<(string, bool)> extractArguments(string line)//value, whether or not value is a string
            {
                List<(string, bool)> args = new List<(string, bool)>();

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
                            args.Add((ReadStringFromString(line, pos, out pos), true));
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
                            args.Add((new string(arg_Chars.ToArray()), false));
                        }
                    }
                    pos += 1;
                }

                return args;
            }
            List<OptionValue> retrieveArguments(List<(string, bool)> args_AsStrings, List<OptionValue> valueTypes)
            {
                //Only the value types are examined in valueTypes; not the values
                List<OptionValue> args = new List<OptionValue>();
                for (int n = 0; n < valueTypes.Count; n += 1)
                {
                    if (n >= args_AsStrings.Count)
                    {
                        if (valueTypes[n].ValueType == OptionValueType.StringValue)
                            args.Add(new OptionValue(OptionValueType.StringValue, ""));
                        else
                            args.Add(new OptionValue(valueTypes[n].ValueType, 1));
                    }
                    else
                    {
                        if (valueTypes[n].ValueType == OptionValueType.BoolValue)
                        {
                            bool isValid = int.TryParse(args_AsStrings[n].Item1, out int val);
                            if (isValid & (val != 0 & val != 1))
                                isValid = false;
                            if ((!args_AsStrings[n].Item2) & isValid)
                            {
                                args.Add(new OptionValue(OptionValueType.BoolValue, val == 1));
                            }
                            else
                                args.Add(new OptionValue(OptionValueType.BoolValue, true));
                        }
                        else if (valueTypes[n].ValueType == OptionValueType.IntValue)
                        {
                            bool isValid = int.TryParse(args_AsStrings[n].Item1, out int val);
                            if ((!args_AsStrings[n].Item2) & isValid)
                            {
                                args.Add(new OptionValue(OptionValueType.IntValue, val));
                            }
                            else
                                args.Add(new OptionValue(OptionValueType.IntValue, 1));
                        }
                        else if (valueTypes[n].ValueType == OptionValueType.FloatValue)
                        {
                            bool isValid = float.TryParse(args_AsStrings[n].Item1, out float val);
                            if ((!args_AsStrings[n].Item2) & isValid)
                            {
                                args.Add(new OptionValue(OptionValueType.FloatValue, val));
                            }
                            else
                                args.Add(new OptionValue(OptionValueType.FloatValue, 1f));
                        }
                        else if (valueTypes[n].ValueType == OptionValueType.StringValue)
                        {
                            if (args_AsStrings[n].Item2)
                                args.Add(new OptionValue(OptionValueType.StringValue, args_AsStrings[n].Item1));
                            else
                                args.Add(new OptionValue(OptionValueType.StringValue, ""));
                        }
                        else
                        {
                            if (!args_AsStrings[n].Item2)
                                args.Add(new OptionValue(OptionValueType.UnknownValue, args_AsStrings[n].Item1));
                            else
                                args.Add(new OptionValue(OptionValueType.UnknownValue, 0));
                        }
                    }
                }
                return args;
            }

            for (int ll = 0; ll < lines.Length; ll += 1)
            {
                string line = lines[ll];
                int optionNameLength = line.IndexOf(" ");
                string name;
                string value;
                if (optionNameLength == -1)
                {
                    name = line;
                    value = "";
                }
                else
                {
                    name = line.Substring(0, optionNameLength);
                    value = line.Substring(optionNameLength + 1).TrimStart((char)0x20);
                }
                if (KnownOptions.ContainsKey(name))
                {
                    List<(string, bool)> args_AsStrings = extractArguments(value);
                    Control control = KnownOptions[name].Item1;
                    Type controlType = control.GetType();
                    //Retrieve and fix option values
                    List<OptionValue> args = retrieveArguments(args_AsStrings, KnownOptions[name].Item2);
                    //Update control
                    if (controlType == typeof(OptionControl_Bool))
                    {
                        if (args.Count >= 1)
                        {
                            ((OptionControl_Bool)control).SetValues((bool)args[0].Value);
                        }
                    }
                    //Set option
                    SetOption(name, args.ToArray());
                }
                else
                {
                    UnknownOptions.Add(name, value);
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
            foreach (KeyValuePair<string, List<OptionValue>> kvp in Options)
            {
                lines_List.Add(OptionAndValues(kvp.Key, kvp.Value));
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
            foreach (KeyValuePair<string, (Control, List<OptionValue>)> kvp in KnownOptions)
                kvp.Value.Item1.Enabled = (!rct3IsRunning);

            //Custom content
            CheckBox_UseCustomContent.Enabled = (!rct3IsRunning);
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

            bool mod_CustomContent = false;
            if (IfExists_SaveDirectory())
            {
                mod_CustomContent = CheckBox_UseCustomContent.Checked;
                if (!TryToDeleteDirectories(Config_SaveDirectory, Prefix_Original + "*"))
                    return;
            }

            //Change Resource Folders (if using Custom Content)
            List<string> dirs_Exe_NotRCT3Pal = null;
            List<string> dirs_Sav_NotRCT3Pal = null;
            List<string> dirs_Exe_Custom = null;
            List<string> dirs_Sav_Custom = null;
            List<string> dirs_Exe_Original = null;
            List<string> dirs_Sav_Original = null;
            #region
            if (mod_CustomContent)
            {
                if (!Fun.TryToFindRCT3PalFolders(
                    Config_ExecutableDirectory,
                    Config_SaveDirectory,
                    Prefix_Custom,
                    Prefix_Original,
                    out dirs_Exe_NotRCT3Pal,
                    out dirs_Sav_NotRCT3Pal,
                    out dirs_Exe_Custom,
                    out dirs_Sav_Custom,
                    out dirs_Exe_Original,
                    out dirs_Sav_Original))
                {
                    return;
                }
                
                //Rename <NonRCT3PalFolder> -> <Prefix_Original><NonRCT3PalFolder>
                foreach (string dir in dirs_Exe_NotRCT3Pal)
                    Directory.Move(dir,
                        Fun.DirectoryAndFile(
                            Config_ExecutableDirectory,
                            Prefix_Original + Path.GetFileName(dir))
                        );
                foreach (string dir in dirs_Sav_NotRCT3Pal)
                    Directory.Move(dir,
                        Fun.DirectoryAndFile(
                            Config_SaveDirectory,
                            Prefix_Original + Path.GetFileName(dir))
                        );

                //Rename <Prefix_Custom><CustomFolder> -> <CustomFolder>
                foreach (string dir in dirs_Exe_Custom)
                    Directory.Move(dir,
                        Fun.DirectoryAndFile(
                            Config_ExecutableDirectory,
                            Path.GetFileName(dir).Substring(Prefix_Custom.Length))
                        );
                foreach (string dir in dirs_Sav_Custom)
                    Directory.Move(dir,
                        Fun.DirectoryAndFile(
                            Config_SaveDirectory,
                            Path.GetFileName(dir).Substring(Prefix_Custom.Length))
                        );
            }
            #endregion

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


            //Change Resource Folders Back
            #region
            if (mod_CustomContent)
            {
                //Rename <CustomFolder> -> <Prefix_Custom><CustomFolder>
                foreach (string dir in dirs_Exe_Custom)
                    Directory.Move(
                        Fun.DirectoryAndFile(
                            Config_ExecutableDirectory,
                            Path.GetFileName(dir).Substring(Prefix_Custom.Length)),
                        dir);
                foreach (string dir in dirs_Sav_Custom)
                    Directory.Move(
                        Fun.DirectoryAndFile(
                            Config_SaveDirectory,
                            Path.GetFileName(dir).Substring(Prefix_Custom.Length)),
                        dir);

                //Rename <Prefix_Original><NonRCT3PalFolder> -> <NonRCT3PalFolder>
                foreach (string dir in dirs_Exe_NotRCT3Pal)
                    Directory.Move(
                        Fun.DirectoryAndFile(
                            Config_ExecutableDirectory,
                            Prefix_Original + Path.GetFileName(dir)),
                        dir);
                foreach (string dir in dirs_Sav_NotRCT3Pal)
                {
                    Directory.Move(
                          Fun.DirectoryAndFile(
                              Config_SaveDirectory,
                              Prefix_Original + Path.GetFileName(dir)),
                          dir);
                }
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
    }
}
