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

        public bool DefaultValue { get { return Var_OptionControl.DefaultValue; } }

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

        public int DefaultIndex { get { return Var_OptionControl.DefaultIndex; } }

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

        public int DefaultValue { get { return Var_OptionControl.DefaultValue; } }

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

            string[] knownOptions_keys = KnownOptions.Keys.ToArray();
            for (int n = 0; n < knownOptions_keys.Length; n += 1)
            {
                object obj = KnownOptions[knownOptions_keys[n]];
                Type objType = obj.GetType();
                if (objType == typeof(OptionValue_Bool))
                {
                    OptionValue_Bool objBool = (OptionValue_Bool)obj;
                    objBool.IsTrue = objBool.DefaultValue;
                }
                if (objType == typeof(OptionValue_Choice))
                {
                    OptionValue_Choice objChoice = (OptionValue_Choice)obj;
                    objChoice.SelectedIndex = objChoice.DefaultIndex;
                }
                if (objType == typeof(OptionValue_Int))
                {
                    OptionValue_Int objInt = (OptionValue_Int)obj;
                    objInt.Value = objInt.DefaultValue;
                }
            }

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

        //Custom assets
        private const string Prefix_Custom = "RCT3Pal_Custom_";
        private const string Prefix_Original = "RCT3Pal_Original_";
        private string[] Exe_RCT3Folders = {
            "ACAM",
            "Animals",
            "AttractSequences",
            "Avatars",
            "Campaigns",
            "CarriedItems",
            "Cars",
            "Characters",
            "Coaster Designs",
            "ContentPacks",
            "Enclosures",
            "FireworkDisplays",
            "Fireworks",
            "gui",
            "Inventions",
            "Lasers",
            "LaserWriting",
            "Lights",
            "Movies",
            "Music",
            "Objects",
            "ParticleEffects",
            "Particles",
            "Path",
            "People",
            "Pool",
            "Queue",
            "Rain",
            "RideIcons",
            "Sky",
            "Slideshow",
            "Sounds",
            "StaffUniforms",
            "Style",
            "Supports",
            "terrain",
            "test",
            "tracks",
            "Tutorials",
            "Water",
            "Waterfall",
            "WaterFlow",
            "WaterJets",
            "WildAnimals",
        };
        private string[] Exe_RCT3Files = {
            "CrashDump.txt",
            "GraphFix.log",
            "Graphics.fix",
            "ijl15.dll",
            "m4d.dll",
            "Main.common.ovl",
            "Main.common.ovl.CHK",
            "Main.unique.ovl",
            "Main.unique.ovl.CHK",
            "msvcr71.dll",
            "nullbmp.common.ovl",
            "nullbmp.unique.ovl",
            "Options.txt",
            "rct3.dgf",
            "RCT3.exe",
            "SCCache.bin",
            "STCache.bin",
        };
        private string[] Sav_RCT3Folders = {
            "Campaigns",
            "Coasters",
            "FireworkEffects",
            "Fireworks",
            "LaserEffects",
            "LaserWriting",
            "Parks",
            "Peeps",
            "Pools",
            "Scenarios",
            "Start New Scenarios",
            "Structures",
            "WaterJetEffects",
        };
        private string[] Sav_RCT3Files = {
        };
        private bool UseCustomAssets;
        private void Set_UseCustomAssets(bool value)
        {
            UseCustomAssets = value;
            Label_UseCustomAssets.Text = (value ?
                "Use Custom Assets" :
                "Use Original Assets");
        }

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
        private bool PathIsValid_ExecutableDirectory(bool silent)
        {
            if (!Directory.Exists(Config_ExecutableDirectory))
            {
                if (!silent)
                    MessageBox.Show(
                        "Could not find RCT3 directory",
                        "Could Not Find RCT3 Directory",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                return false;
            }
            if (!File.Exists(Config_ExecutableDirectory_Executable))
            {
                if (!silent)
                    MessageBox.Show(
                        "Could not find RCT3 executable",
                        "Could Not Find RCT3 Executable",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private string Config_OptionsDirectory;
        private string Config_OptionsDirectory_Options;
        private bool PathIsValid_OptionsDirectory(bool silent)
        {
            if (!Directory.Exists(Config_OptionsDirectory))
            {
                if (!silent) 
                    MessageBox.Show(
                        "Could not find Options directory",
                        "Could Not Find Options Directory",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                return false;
            }
            if (!File.Exists(Config_OptionsDirectory_Options))
            {
                if (!silent)
                    MessageBox.Show(
                        "Could not find Options File",
                        "Could Not Find Options File",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private string Config_SaveDirectory;
        private bool PathIsValid_SaveDirectory(bool silent)
        {
            if (!Directory.Exists(Config_SaveDirectory))
            {
                if (!silent)
                    MessageBox.Show(
                        "Could not find Save directory",
                        "Could Not Find Save Directory",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        
        private bool PathsAreValid_ExecutableDirectoryAndSaveDirectory(bool exe_Silent, bool sav_Silent)
        {
            if (PathIsValid_ExecutableDirectory(exe_Silent) & PathIsValid_SaveDirectory(sav_Silent))
                return true;
            return false;
        }

        private bool Config_DontShowBeginningWarning;

        private enum LookThruExecutableAndSaveDirectoryReturn
        {
            ///<summary>Error finding files and folders in save directory or executable directory</summary>
            CouldNotSearch = 0,

            ///<summary>Found files and folders labeled as Original (Ex: RCT3Pal_Original_&lt;Filename&gt;)</summary>
            FoundOriginalFilesAndFolders = 1,

            ///<summary>Found files and folders labeled as Custom (Ex: RCT3Pal_Custom_&lt;Filename&gt;)</summary>
            FoundCustomFilesAndFolders = 2,

            ///<summary>Found files and folders labeled as Original as well as files and folders labeled as Custom</summary>
            FoundOriginalAndCustomFilesAndFolders = 3,
            
            ///<summary>Found neither files and folders labeled as Original nor files and folders labeled as Custom</summary>
            FoundNeitherOriginalNorCustomFilesAndFolders = 4,
        }
        private LookThruExecutableAndSaveDirectoryReturn LookThruExecutableAndSaveDirectory(
            out string[] exe_OriginalFolders, out string[] exe_OriginalFiles,
            out string[] exe_CustomFolders, out string[] exe_CustomFiles,
            out string[] exe_UnmarkedFolders, out string[] exe_UnmarkedFiles,
            out string[] sav_OriginalFolders, out string[] sav_OriginalFiles,
            out string[] sav_CustomFolders, out string[] sav_CustomFiles,
            out string[] sav_UnmarkedFolders, out string[] sav_UnmarkedFiles)
        {
            string[] exe_Folders;
            string[] exe_Files;
            string[] sav_Folders;
            string[] sav_Files;
            try
            {
                exe_Folders = Directory.GetDirectories(Config_ExecutableDirectory);
                exe_Files = Directory.GetFiles(Config_ExecutableDirectory);
                sav_Folders = Directory.GetDirectories(Config_SaveDirectory);
                sav_Files = Directory.GetFiles(Config_SaveDirectory);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Could not search through Executable and Save directories",
                    "Error Searching",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                exe_OriginalFolders = null;
                exe_OriginalFiles = null;
                exe_CustomFolders = null;
                exe_CustomFiles = null;
                exe_UnmarkedFolders = null;
                exe_UnmarkedFiles = null;
                sav_OriginalFolders = null;
                sav_OriginalFiles = null;
                sav_CustomFolders = null;
                sav_CustomFiles = null;
                sav_UnmarkedFolders = null;
                sav_UnmarkedFiles = null;
                return LookThruExecutableAndSaveDirectoryReturn.CouldNotSearch;
            }

            void sort(string[] folders, string[] files,
                out string[] originalFolders, out string[] originalFiles,
                out string[] customFolders, out string[] customFiles,
                out string[] unmarkedFolders, out string[] unmarkedFiles)
            {
                List<string> originalFolders_List = new List<string>();
                List<string> customFolders_List = new List<string>();
                List<string> unmarkedFolders_List = new List<string>();
                for (int n = 0; n < folders.Length; n += 1)
                {
                    string folder = folders[n];
                    string folderName = Path.GetFileName(folder);
                    if (folderName.IndexOf(Prefix_Original) == 0)
                        originalFolders_List.Add(folder);
                    else if (folderName.IndexOf(Prefix_Custom) == 0)
                        customFolders_List.Add(folder);
                    else
                        unmarkedFolders_List.Add(folder);
                }

                List<string> originalFiles_List = new List<string>();
                List<string> customFiles_List = new List<string>();
                List<string> unmarkedFiles_List = new List<string>();
                for (int n = 0; n < files.Length; n += 1)
                {
                    string file = files[n];
                    string fileName = Path.GetFileName(file);
                    if (fileName.IndexOf(Prefix_Original) == 0)
                        originalFiles_List.Add(file);
                    else if (fileName.IndexOf(Prefix_Custom) == 0)
                        customFiles_List.Add(file);
                    else
                        unmarkedFiles_List.Add(file);
                }

                originalFolders = originalFolders_List.ToArray();
                originalFiles = originalFiles_List.ToArray();
                customFolders = customFolders_List.ToArray();
                customFiles = customFiles_List.ToArray();
                unmarkedFolders = unmarkedFolders_List.ToArray();
                unmarkedFiles = unmarkedFiles_List.ToArray();
            }
            sort(exe_Folders, exe_Files,
                out exe_OriginalFolders, out exe_OriginalFiles,
                out exe_CustomFolders, out exe_CustomFiles,
                out exe_UnmarkedFolders, out exe_UnmarkedFiles);
            sort(sav_Folders, sav_Files,
                out sav_OriginalFolders, out sav_OriginalFiles,
                out sav_CustomFolders, out sav_CustomFiles,
                out sav_UnmarkedFolders, out sav_UnmarkedFiles);

            void write(string path,
                string[] originalFolders, string[] originalFiles,
                string[] customFolders, string[] customFiles,
                string[] unmarkedFolders, string[] unmarkedFiles)
            {
                using (StreamWriter strWriter = new StreamWriter(path))
                {
                    strWriter.WriteLine("Original");
                    foreach (string line in originalFolders)
                        strWriter.WriteLine(line);
                    foreach (string line in originalFiles)
                        strWriter.WriteLine(line);
                    strWriter.WriteLine();

                    strWriter.WriteLine("Custom");
                    foreach (string line in customFolders)
                        strWriter.WriteLine(line);
                    foreach (string line in customFiles)
                        strWriter.WriteLine(line);
                    strWriter.WriteLine();

                    strWriter.WriteLine("Unmarked");
                    foreach (string line in unmarkedFolders)
                        strWriter.WriteLine(line);
                    foreach (string line in unmarkedFiles)
                        strWriter.WriteLine(line);
                    strWriter.WriteLine();
                }
            }
            /*write("Executable.txt",
                exe_OriginalFolders, exe_OriginalFiles,
                exe_CustomFolders, exe_CustomFiles,
                exe_UnmarkedFolders, exe_UnmarkedFiles);
            write("Save.txt",
                sav_OriginalFolders, sav_OriginalFiles,
                sav_CustomFolders, sav_CustomFiles,
                sav_UnmarkedFolders, sav_UnmarkedFiles);*/

            bool exe_HasOriginalFilesAndFolders = (
                (exe_OriginalFolders.Length > 0) |
                (exe_OriginalFiles.Length > 0));
            bool exe_HasCustomFilesAndFolders = (
                (exe_CustomFolders.Length > 0) |
                (exe_CustomFiles.Length > 0));
            bool sav_HasOriginalFilesAndFolders = (
                (sav_OriginalFolders.Length > 0) |
                (sav_OriginalFiles.Length > 0));
            bool sav_HasCustomFilesAndFolders = (
                (sav_CustomFolders.Length > 0) |
                (sav_CustomFiles.Length > 0));
            bool hasOriginalFilesAndFolders = (exe_HasOriginalFilesAndFolders | sav_HasOriginalFilesAndFolders);
            bool hasCustomFilesAndFolders = (exe_HasCustomFilesAndFolders | sav_HasCustomFilesAndFolders);
            if (hasOriginalFilesAndFolders & (!hasCustomFilesAndFolders))
                return LookThruExecutableAndSaveDirectoryReturn.FoundOriginalFilesAndFolders;
            if ((!hasOriginalFilesAndFolders) & hasCustomFilesAndFolders)
                return LookThruExecutableAndSaveDirectoryReturn.FoundCustomFilesAndFolders;
            if (hasOriginalFilesAndFolders & hasCustomFilesAndFolders)
            {
                MessageBox.Show(
                    "There are both files/folders marked as original and files/folders marked as custom",
                    "Found Both Original and Custom Files/Folders",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return LookThruExecutableAndSaveDirectoryReturn.FoundOriginalAndCustomFilesAndFolders;
            }
            return LookThruExecutableAndSaveDirectoryReturn.FoundNeitherOriginalNorCustomFilesAndFolders;
        }

        private void Set_Config(string executableDirectory, string optionsDirectory, string saveDirectory, bool dontShowBeginningWarning)
        {
            Config_ExecutableDirectory = executableDirectory;
            Config_ExecutableDirectory_Executable = Fun.DirectoryAndFile(
                executableDirectory,
                "RCT3.exe");

            Config_OptionsDirectory = optionsDirectory;
            Config_OptionsDirectory_Options = Fun.DirectoryAndFile(
                optionsDirectory,
                "Options.txt");

            Config_SaveDirectory = saveDirectory;

            Config_DontShowBeginningWarning = dontShowBeginningWarning;

            Button_RCT3.Enabled = PathIsValid_ExecutableDirectory(false);
            
            if (PathIsValid_OptionsDirectory(false))
            {
                GroupBox_Options.Enabled = true;
                Options_Load();
            }
            else
            {
                GroupBox_Options.Enabled = false;
            }
            
            if (PathsAreValid_ExecutableDirectoryAndSaveDirectory(true, false))
            {
                GroupBox_CustomAssets.Enabled = true;
                LookThruExecutableAndSaveDirectoryReturn lookThruExecutableAndSaveDirectoryReturn = 
                    LookThruExecutableAndSaveDirectory(
                        out string[] exe_OriginalFolders, out string[] exe_OriginalFiles,
                        out string[] exe_CustomFolders, out string[] exe_CustomFiles,
                        out string[] exe_UnmarkedFolders, out string[] exe_UnmarkedFiles,
                        out string[] sav_OriginalFolders, out string[] sav_OriginalFiles,
                        out string[] sav_CustomFolders, out string[] sav_CustomFiles,
                        out string[] sav_UnmarkedFolders, out string[] sav_UnmarkedFiles);

                if ((lookThruExecutableAndSaveDirectoryReturn ==
                        LookThruExecutableAndSaveDirectoryReturn.CouldNotSearch) |
                    (lookThruExecutableAndSaveDirectoryReturn ==
                        LookThruExecutableAndSaveDirectoryReturn.FoundOriginalAndCustomFilesAndFolders))
                {
                    GroupBox_CustomAssets.Enabled = false;
                }
                else if (lookThruExecutableAndSaveDirectoryReturn ==
                    LookThruExecutableAndSaveDirectoryReturn.FoundOriginalFilesAndFolders)
                {
                    Set_UseCustomAssets(true);
                }
                else if (lookThruExecutableAndSaveDirectoryReturn ==
                    LookThruExecutableAndSaveDirectoryReturn.FoundCustomFilesAndFolders)
                {
                    Set_UseCustomAssets(false);
                }
                else
                {
                    Set_UseCustomAssets(false);
                }
            }
            else
            {
                GroupBox_CustomAssets.Enabled = false;
            }
            
        }

        private void OpenConfig()
        {
            Form_Config config = new Form_Config(
                Config_ExecutableDirectory, 
                Config_OptionsDirectory,
                Config_SaveDirectory,
                Config_DontShowBeginningWarning);
            if (config.ShowDialog() == DialogResult.Cancel)
                Set_Config(Config_ExecutableDirectory, Config_OptionsDirectory, Config_SaveDirectory, Config_DontShowBeginningWarning);
            else
                Set_Config(config.ExecutableDirectory, config.OptionsDirectory, config.SaveDirectory, config.DontShowBeginningWarning);
            ConfigFile.Save(ConfigFilePath,
                Config_ExecutableDirectory,
                Config_OptionsDirectory,
                Config_SaveDirectory,
                Config_DontShowBeginningWarning);
        }
        #endregion

        public Form_Main(string configFilePath, string programDirectory)
        {
            RCT3Process = null;
            RCT3Process_IsRunning = false;

            ProgramDirectory = programDirectory;

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
            Set_UseCustomAssets(false);
            ConfigFilePath = configFilePath;
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
                    out string saveDirectory,
                    out bool dontShowBeginningWarning))
                {
                    Set_Config(executableDirectory, optionsDirectory, saveDirectory, dontShowBeginningWarning);
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

            //Custom Assets
            Button_CustomAssets.Enabled = (!rct3IsRunning);
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
            
            bool mod_Options = true;
            DateTime timeOptionsModified = new DateTime();
            if (!PathIsValid_OptionsDirectory(false))
                mod_Options = false;

            //Backup and modify Options file (if modding options)
            #region
            if (mod_Options)
            {
                Console.WriteLine(File.GetLastWriteTime(Config_OptionsDirectory_Options).Ticks);
                Options_Save();
                timeOptionsModified = File.GetLastWriteTime(Config_OptionsDirectory_Options);
                Console.WriteLine(timeOptionsModified.Ticks);
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
                for (int n = 0; n < 10; n += 1)
                {
                    if (timeOptionsModified != File.GetLastWriteTime(Config_OptionsDirectory_Options))
                        break;
                    System.Threading.Thread.Sleep(1000);
                }
                Console.WriteLine(File.GetLastWriteTime(Config_OptionsDirectory_Options).Ticks);
                Options_Load();
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

            if (MessageBox.Show(
                    "Any unsaved changes to the options will be lost. Is this OK?", 
                    "Any Unsaved Changes",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning) == DialogResult.No)
            {
                e.Cancel = true;
                return;
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

        private void Button_CustomAssets_Click(object sender, EventArgs e)
        {
            Form_CustomAssets form = new Form_CustomAssets(
                UseCustomAssets,
                Config_ExecutableDirectory,
                Config_SaveDirectory,
                Prefix_Custom,
                Prefix_Original);

            if (form.ShowDialog() == DialogResult.Cancel)
                return;


            string[] exe_directories = Directory.GetDirectories(Config_ExecutableDirectory);
            string[] exe_files = Directory.GetFiles(Config_ExecutableDirectory);
            bool tryToRenameFilesAndFolders(
                string[] oldFolderNames, string[] newFolderNames,
                string[] oldFileNames, string[] newFileNames)
            {
                if (oldFolderNames.Length != newFolderNames.Length)
                    return false;
                if (oldFileNames.Length != newFileNames.Length)
                    return false;

                List<(bool, string, string)> thingsToRename = 
                    new List<(bool, string, string)>(); //(isAFile, oldName, newName)
                for (int n = 0; n < oldFolderNames.Length; n += 1)
                {
                    thingsToRename.Add((
                        false,
                        oldFolderNames[n],
                        newFolderNames[n]));
                }
                for (int n = 0; n < oldFileNames.Length; n += 1)
                {
                    thingsToRename.Add((
                        true,
                        oldFileNames[n],
                        newFileNames[n]));
                }

                bool tryToRename(bool isAFile, string oldName, string newName)
                {
                    if (isAFile)
                    {
                        try
                        {
                            File.Move(oldName, newName);
                        }
                        catch
                        {
                            return false;
                        }
                        return true;
                    }
                    else
                    {
                        try
                        {
                            Directory.Move(oldName, newName);
                        }
                        catch
                        {
                            return false;
                        }
                        return true;
                    }
                }
                int x = 0;
                while (x < thingsToRename.Count)
                {
                    if (!tryToRename(
                        thingsToRename[x].Item1,
                        thingsToRename[x].Item2,
                        thingsToRename[x].Item3))
                    {
                        x -= 1;
                        while (x >= 0)
                        {
                            if (!tryToRename(
                                thingsToRename[x].Item1,
                                thingsToRename[x].Item3,
                                thingsToRename[x].Item2))
                            {
                                MessageBox.Show(
                                    "Could not successfully rename files and folders. Also failed to name fles and folders back.",
                                    "Could Not Rename Files and Folders",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                                return false;
                            }
                            x -= 1;
                        }
                        MessageBox.Show(
                            "Could not successfully rename files and folders. Files and folders were named back.",
                            "Could Not Rename Files and Folders",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return false;
                    }
                    x += 1;
                }
                return true;
            }
            Dictionary<string, (string, string)> sortByBaseFilename(string[] unmarkedFiles, string[] markedFiles, string prefixOfMarkedFiles)
            {
                //Key: Base Filename
                //Value: Unmarked File, Marked File
                Dictionary<string, (string, string)> dict = new Dictionary<string, (string, string)>();
                for (int n = 0; n < unmarkedFiles.Length; n += 1)
                {
                    string unmarkedFile = unmarkedFiles[n];
                    string basename = Path.GetFileName(unmarkedFile);
                    dict.Add(basename, (unmarkedFile, ""));
                }
                for (int n = 0; n < markedFiles.Length; n += 1)
                {
                    string markedFile = markedFiles[n];
                    string basename = Path.GetFileName(markedFile).Substring(prefixOfMarkedFiles.Length);
                    if (dict.ContainsKey(basename))
                    {
                        dict[basename] = (dict[basename].Item1, markedFile);
                    }
                    else
                    {
                        dict.Add(basename, ("", markedFile));
                    }
                }
                return dict;
            }
            Dictionary<string, (string, string)> sortByBaseFoldername(string[] unmarkedFolders, string[] markedFolders, string prefixOfMarkedFolders)
            {
                //Key: Base Foldername
                //Value: Unmarked Folder, Marked Folder
                Dictionary<string, (string, string)> dict = new Dictionary<string, (string, string)>();
                for (int n = 0; n < unmarkedFolders.Length; n += 1)
                {
                    string unmarkedFolder = unmarkedFolders[n];
                    string basename = Path.GetFileName(unmarkedFolder);
                    dict.Add(basename, (unmarkedFolder, ""));
                }
                for (int n = 0; n < markedFolders.Length; n += 1)
                {
                    string markedFolder = markedFolders[n];
                    string basename = Path.GetFileName(markedFolder).Substring(prefixOfMarkedFolders.Length);
                    if (dict.ContainsKey(basename))
                    {
                        dict[basename] = (dict[basename].Item1, markedFolder);
                    }
                    else
                    {
                        dict.Add(basename, ("", markedFolder));
                    }
                }
                return dict;
            }
            void makeArraysForRenamingFilesOrFolders(
                Dictionary<string, (string, string)> filesOrFoldersByBase,
                string[] rct3FilesOrFolders,
                string prefixOfOldMarkedFilesOrFolders,
                string prefixOfNewMarkedFilesOrFolders,
                out string[] oldNames, out string[] newNames)
            {
                string addPrefixToFileOrFolderName(string path, string prefix)
                {
                    return Path.GetFullPath(Path.GetDirectoryName(path)) + "\\" + prefix + Path.GetFileName(path);
                }
                string removePrefixFromFileOrFolderName(string path, string prefix)
                {
                    return Path.GetFullPath(Path.GetDirectoryName(path)) + "\\" + (
                        (Path.GetFileName(path).IndexOf(prefix) == 0) ?
                            Path.GetFileName(path).Substring(prefix.Length) :
                            Path.GetFileName(path));
                }
                List<string> oldNames_List = new List<string>();
                List<string> newNames_List = new List<string>();
                foreach (KeyValuePair<string, (string, string)> kvp in filesOrFoldersByBase)
                {
                    string baseName = kvp.Key;
                    string unmarkedFile = kvp.Value.Item1;
                    string markedFile = kvp.Value.Item2;
                    if (unmarkedFile != "")
                    {
                        if (markedFile != "")
                        {
                            oldNames_List.Add(unmarkedFile);
                            newNames_List.Add(addPrefixToFileOrFolderName(unmarkedFile, prefixOfNewMarkedFilesOrFolders));
                            oldNames_List.Add(markedFile);
                            newNames_List.Add(removePrefixFromFileOrFolderName(markedFile, prefixOfOldMarkedFilesOrFolders));
                        }
                        else if (Array.IndexOf(rct3FilesOrFolders, baseName) == -1)
                        {
                            oldNames_List.Add(unmarkedFile);
                            newNames_List.Add(addPrefixToFileOrFolderName(unmarkedFile, prefixOfNewMarkedFilesOrFolders));
                        }
                    }
                    else if (markedFile != "")
                    {
                        oldNames_List.Add(markedFile);
                        newNames_List.Add(removePrefixFromFileOrFolderName(markedFile, prefixOfOldMarkedFilesOrFolders));
                    }
                }
                oldNames = oldNames_List.ToArray();
                newNames = newNames_List.ToArray();
            }
            
            Set_UseCustomAssets(false);
            if (!PathsAreValid_ExecutableDirectoryAndSaveDirectory(false, false))
            {
                GroupBox_CustomAssets.Enabled = false;
                return;
            }
            LookThruExecutableAndSaveDirectoryReturn lookThruExecutableAndSaveDirectoryReturn =
                LookThruExecutableAndSaveDirectory(
                    out string[] exe_OriginalFolders, out string[] exe_OriginalFiles,
                    out string[] exe_CustomFolders, out string[] exe_CustomFiles,
                    out string[] exe_UnmarkedFolders, out string[] exe_UnmarkedFiles,
                    out string[] sav_OriginalFolders, out string[] sav_OriginalFiles,
                    out string[] sav_CustomFolders, out string[] sav_CustomFiles,
                    out string[] sav_UnmarkedFolders, out string[] sav_UnmarkedFiles);
            if ((lookThruExecutableAndSaveDirectoryReturn ==
                    LookThruExecutableAndSaveDirectoryReturn.CouldNotSearch) |
                (lookThruExecutableAndSaveDirectoryReturn ==
                    LookThruExecutableAndSaveDirectoryReturn.FoundOriginalAndCustomFilesAndFolders))
            {
                GroupBox_CustomAssets.Enabled = false;
                return;
            }
            if (lookThruExecutableAndSaveDirectoryReturn ==
                LookThruExecutableAndSaveDirectoryReturn.FoundOriginalFilesAndFolders)
            {
                //Custom assets were being used
                if (!form.UseCustomAssets)
                {
                    //User wants to use original assets
                    Dictionary<string, (string, string)> exe_FilesByBase = sortByBaseFilename(
                        exe_UnmarkedFiles,
                        exe_OriginalFiles,
                        Prefix_Original);
                    Dictionary<string, (string, string)> exe_FoldersByBase = sortByBaseFoldername(
                        exe_UnmarkedFolders,
                        exe_OriginalFolders,
                        Prefix_Original);
                    Dictionary<string, (string, string)> sav_FilesByBase = sortByBaseFilename(
                        sav_UnmarkedFiles,
                        sav_OriginalFiles,
                        Prefix_Original);
                    Dictionary<string, (string, string)> sav_FoldersByBase = sortByBaseFoldername(
                        sav_UnmarkedFolders,
                        sav_OriginalFolders,
                        Prefix_Original);

                    makeArraysForRenamingFilesOrFolders(
                        exe_FilesByBase,
                        Exe_RCT3Files,
                        Prefix_Original,
                        Prefix_Custom,
                        out string[] exe_OldFiles, out string[] exe_NewFiles);
                    makeArraysForRenamingFilesOrFolders(
                        exe_FoldersByBase,
                        Exe_RCT3Folders,
                        Prefix_Original,
                        Prefix_Custom,
                        out string[] exe_OldFolders, out string[] exe_NewFolders);
                    makeArraysForRenamingFilesOrFolders(
                        sav_FilesByBase,
                        Sav_RCT3Files,
                        Prefix_Original,
                        Prefix_Custom,
                        out string[] sav_OldFiles, out string[] sav_NewFiles);
                    makeArraysForRenamingFilesOrFolders(
                        sav_FoldersByBase,
                        Sav_RCT3Folders,
                        Prefix_Original,
                        Prefix_Custom,
                        out string[] sav_OldFolders, out string[] sav_NewFolders);

                    List<string> oldFiles = exe_OldFiles.ToList();
                    oldFiles.AddRange(sav_OldFiles);
                    List<string> newFiles = exe_NewFiles.ToList();
                    newFiles.AddRange(sav_NewFiles);
                    List<string> oldFolders = exe_OldFolders.ToList();
                    oldFolders.AddRange(sav_OldFolders);
                    List<string> newFolders = exe_NewFolders.ToList();
                    newFolders.AddRange(sav_NewFolders);

                    if (!tryToRenameFilesAndFolders(
                        oldFolders.ToArray(),
                        newFolders.ToArray(),
                        oldFiles.ToArray(),
                        newFiles.ToArray()))
                    {
                        GroupBox_CustomAssets.Enabled = false;
                        return;
                    }
                }
            }
            if (lookThruExecutableAndSaveDirectoryReturn ==
                LookThruExecutableAndSaveDirectoryReturn.FoundCustomFilesAndFolders)
            {
                //Original assets were being used
                if (form.UseCustomAssets)
                {
                    //User wants to use custom assets
                    Dictionary<string, (string, string)> exe_FilesByBase = sortByBaseFilename(
                        exe_UnmarkedFiles,
                        exe_CustomFiles,
                        Prefix_Custom);
                    Dictionary<string, (string, string)> exe_FoldersByBase = sortByBaseFoldername(
                        exe_UnmarkedFolders,
                        exe_CustomFolders,
                        Prefix_Custom);
                    Dictionary<string, (string, string)> sav_FilesByBase = sortByBaseFilename(
                        sav_UnmarkedFiles,
                        sav_CustomFiles,
                        Prefix_Custom);
                    Dictionary<string, (string, string)> sav_FoldersByBase = sortByBaseFoldername(
                        sav_UnmarkedFolders,
                        sav_CustomFolders,
                        Prefix_Custom);

                    makeArraysForRenamingFilesOrFolders(
                        exe_FilesByBase,
                        Exe_RCT3Files,
                        Prefix_Custom,
                        Prefix_Original,
                        out string[] exe_OldFiles, out string[] exe_NewFiles);
                    makeArraysForRenamingFilesOrFolders(
                        exe_FoldersByBase,
                        Exe_RCT3Folders,
                        Prefix_Custom,
                        Prefix_Original,
                        out string[] exe_OldFolders, out string[] exe_NewFolders);
                    makeArraysForRenamingFilesOrFolders(
                        sav_FilesByBase,
                        Sav_RCT3Files,
                        Prefix_Custom,
                        Prefix_Original,
                        out string[] sav_OldFiles, out string[] sav_NewFiles);
                    makeArraysForRenamingFilesOrFolders(
                        sav_FoldersByBase,
                        Sav_RCT3Folders,
                        Prefix_Custom,
                        Prefix_Original,
                        out string[] sav_OldFolders, out string[] sav_NewFolders);

                    List<string> oldFiles = exe_OldFiles.ToList();
                    oldFiles.AddRange(sav_OldFiles);
                    List<string> newFiles = exe_NewFiles.ToList();
                    newFiles.AddRange(sav_NewFiles);
                    List<string> oldFolders = exe_OldFolders.ToList();
                    oldFolders.AddRange(sav_OldFolders);
                    List<string> newFolders = exe_NewFolders.ToList();
                    newFolders.AddRange(sav_NewFolders);

                    if (!tryToRenameFilesAndFolders(
                        oldFolders.ToArray(),
                        newFolders.ToArray(),
                        oldFiles.ToArray(),
                        newFiles.ToArray()))
                    {
                        GroupBox_CustomAssets.Enabled = false;
                        return;
                    }
                }
            }
            if (lookThruExecutableAndSaveDirectoryReturn ==
                LookThruExecutableAndSaveDirectoryReturn.FoundNeitherOriginalNorCustomFilesAndFolders)
            {
                return;
            }
            Set_UseCustomAssets(form.UseCustomAssets);
        }
    }
}
