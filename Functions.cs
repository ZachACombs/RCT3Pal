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
        
        public static string PadString(string value, int charCount)
        {
            if (charCount < 0)
                throw new ArgumentException("Value is negative", nameof(charCount));

            if (value.Length > charCount)
                return value.Substring(0, charCount);

            while (value.Length < charCount)
            {
                value = value + " ";
            }

            return value;
        }
    }
}
