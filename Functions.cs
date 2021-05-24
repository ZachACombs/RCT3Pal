using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RCT3Pal
{
    public class Fun
    {
        public static string DirectoryAndFile(string directory, string file)
        {
            string path = (directory + "\\" + file).Replace("/", "\\");
            while (path.IndexOf("\\\\") != -1)
                path = path.Replace("\\\\", "\\");
            return path;
        }


        public static string MakeOptionDisplayName(string optionName)
        {
            List<char> newStr_Chars = new List<char>();
            char[] str_Chars = optionName.ToCharArray();
            int pos = 0;
            while (pos < str_Chars.Length)
            {
                char ccc = str_Chars[pos];
                //Is uppercase 
                if (ccc >= 0x41 & ccc <= 0x5A)
                {
                    if (pos > 0)
                    {
                        bool isLastChar = (pos == (str_Chars.Length - 1));
                        bool prevCharIsUppercase =
                            (str_Chars[pos - 1] >= 0x41 & str_Chars[pos - 1] <= 0x5A);
                        bool nextCharIsUppercase =
                            ((!isLastChar) && (str_Chars[pos + 1] >= 0x41 & str_Chars[pos + 1] <= 0x5A));
                        if (isLastChar)
                        {
                            if (!prevCharIsUppercase)
                                newStr_Chars.Add((char)0x20);
                        }
                        else if (!(prevCharIsUppercase & nextCharIsUppercase))
                        {
                            newStr_Chars.Add((char)0x20);
                        }
                    }
                    newStr_Chars.Add(ccc);
                }
                else
                    newStr_Chars.Add(ccc);
                pos += 1;
            }
            return new string(newStr_Chars.ToArray());
        }

        //Based off of https://docs.microsoft.com/en-us/dotnet/standard/io/how-to-copy-directories
        public static void CopyDirectory(string sourceDirName, string destDirName)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();

            // If the destination directory doesn't exist, create it.       
            Directory.CreateDirectory(destDirName);

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string tempPath = Path.Combine(destDirName, file.Name);
                file.CopyTo(tempPath, false);
            }

            foreach (DirectoryInfo subdir in dirs)
            {
                string tempPath = Path.Combine(destDirName, subdir.Name);
                CopyDirectory(subdir.FullName, tempPath);
            }
        }

        public static void ShowErrorMessage(string precedingText, string caption, Exception ex)
        {
            System.Windows.Forms.MessageBox.Show(
                ((precedingText != "") ? (precedingText + "\r\n") : "") + ex.Message,
                caption,
                System.Windows.Forms.MessageBoxButtons.OK,
                System.Windows.Forms.MessageBoxIcon.Error);
        }
        public static bool TryToFindRCT3PalFolders(
            string executableDirectory,
            string saveDirectory,
            string customFolderPrefix,
            string originalFolderPrefix,
            out List<string> dirs_Exe_NotRCT3Pal, //Are not affiliated with RCT3Pal
            out List<string> dirs_Sav_NotRCT3Pal, //Are not affiliated with RCT3Pal
            out List<string> dirs_Exe_Custom,
            out List<string> dirs_Sav_Custom,
            out List<string> dirs_Exe_Original,
            out List<string> dirs_Sav_Original
            )
        {
            dirs_Exe_NotRCT3Pal = new List<string>();
            dirs_Sav_NotRCT3Pal = new List<string>();
            dirs_Exe_Custom = new List<string>();
            dirs_Sav_Custom = new List<string>();
            dirs_Exe_Original = new List<string>();
            dirs_Sav_Original = new List<string>();
            string[] dirs_Exe;
            try { dirs_Exe = Directory.GetDirectories(executableDirectory); }
            catch (Exception ex)
            {
                ShowErrorMessage(
                    "Could not search for directories in executable directory",
                    "Error Searching Directory",
                    ex);
                return false;
            }
            string[] dirs_Sav;
            try { dirs_Sav = Directory.GetDirectories(saveDirectory); }
            catch (Exception ex)
            {
                ShowErrorMessage(
                    "Could not search for directories in save directory",
                    "Error Searching Directory",
                    ex);
                return false;
            }
            foreach (string dir in dirs_Exe)
            {
                string dirName = Path.GetFileName(dir);
                if (dirName.IndexOf(customFolderPrefix) == 0)
                    dirs_Exe_Custom.Add(dir);
                else if (dirName.IndexOf(originalFolderPrefix) == 0)
                    dirs_Exe_Original.Add(dir);
                else
                    dirs_Exe_NotRCT3Pal.Add(dir);
            }
            foreach (string dir in dirs_Sav)
            {
                string dirName = Path.GetFileName(dir);
                if (dirName.IndexOf(customFolderPrefix) == 0)
                    dirs_Sav_Custom.Add(dir);
                else if (dirName.IndexOf(originalFolderPrefix) == 0)
                    dirs_Sav_Original.Add(dir);
                else
                    dirs_Sav_NotRCT3Pal.Add(dir);
            }

            return true;
        }
    }
}
