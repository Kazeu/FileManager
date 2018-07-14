using System;
using static System.Console;
using System.Collections.Generic;
using System.IO;

namespace FileManager
{
    class Program
    {
        static void Main(string[] args)
        {
            string src;
            string dest;
            int opt = 0;
            string name;

            Write("Enter a path name to copy files from: ");
            src = ReadLine();

            Write("Enter a path name to copy the files to: ");
            dest = ReadLine();

            if (!dest.EndsWith("\\"))
            {
                dest += "\\";
            }

            Write("Enter the type of files you would like to enter|\n1| {0,-8}\n2| {1,-8}\n3| {2,-8}\n4| {3,-8}\n", "Text", "Image", "Audio", "All");

            switch (ReadLine().ToLower())
            {
                case "1":
                case "text":
                    opt = FileType.TXT;
                    break;
                case "2":
                case "image":
                    opt = FileType.IMG;
                    break;
                case "3":
                case "audio":
                    opt = FileType.AUD;
                    break;
                case "4":
                case "all":
                default:
                    opt = FileType.ALL;
                    break;
            }

            Write("Enter the name you would like to correspond to each file with an ascending number: ");

            name = ReadLine();

            Dir.DirCopy(src, dest, name, opt);
        }
    }

    public class Dir
    {
        public static void DirCopy(string src, string dest, string name = "", int fileType = FileType.ALL)
        {
            string[] files = Directory.GetFiles(src);
            List<string> moveFiles = new List<string>();

            string fileExtension;

            switch (fileType)
            {
                case FileType.TXT:
                    foreach (string file in files)
                    {
                        fileExtension = Path.GetExtension(file).ToLower();
                        switch (fileExtension)
                        {
                            case ".txt":
                            case ".doc":
                            case ".docx":
                            case ".cs":
                                moveFiles.Add(file);
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case FileType.IMG:
                    foreach (string file in files)
                    {
                        fileExtension = Path.GetExtension(file).ToLower();

                        switch (fileExtension)
                        {
                            case ".png":
                            case ".jpg":
                            case ".jpeg":
                            case ".bmp":
                            case ".gif":
                                moveFiles.Add(file);
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case FileType.AUD:
                    foreach (string file in files)
                    {
                        fileExtension = Path.GetExtension(file).ToLower();

                        switch (fileExtension)
                        {
                            case ".mp3":
                            case ".wav":
                            case ".flac":
                                moveFiles.Add(file);
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case FileType.ALL:
                    foreach (string file in files)
                    {
                        moveFiles.Add(file);
                    }
                    break;
            }

            if (name != "")
            {
                int i = 0;

                foreach (string file in moveFiles)
                {
                    File.Copy(file, dest + name + i + Path.GetExtension(file));
                    i++;
                }
            }
            else
            {
                foreach (string file in moveFiles)
                {
                    File.Copy(file, dest + Path.GetFileName(file));
                }
            }
        }
    }

    public class FileType
    {
        public const int ALL = 0;
        public const int TXT = 1;
        public const int IMG = 2;
        public const int AUD = 3;
    }
}
