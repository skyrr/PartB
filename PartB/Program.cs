using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace PartB
{
    class Program
    {
        private const int FilesToWrite = 10;
        private const string FilePath = "\\FilesFolder\\";

        static void Main(string[] args)
        {
            Console.WriteLine("App started!");
            
            FileClass fileClass = new FileClass() {filePath = FilePath };

            FileWriter fileWriter = new FileWriter(fileClass);

            for (int i = 0; i < FilesToWrite; i++)
            {
                fileClass.fileName = "file" + i + ".txt";
                fileWriter.FileWritingWorker(fileClass);
            }

            Console.WriteLine("App completed!");
            Console.Read();
        }
    }

    public class FileClass
    {
        public string fileName { get; set; }
        public string filePath { get; set; }
    }
    public class FileWriter
    {
        private FileClass _fileClass;

        public FileWriter(FileClass fileClass)
        {
            _fileClass = fileClass;
        }
        public void FileWritingWorker(FileClass fileToWrite)
        {
            FolderService(_fileClass);
            
            if (WriteFile(_fileClass))
            {
                WritingReport();
            }
        }
        public bool WriteFile(FileClass fileToWrite)
        {
            using (StreamWriter writetext = new StreamWriter(_fileClass.filePath + _fileClass.fileName))
            {
                writetext.WriteLine("Text example");
            }
            return true;
        }

        public void FolderService(FileClass fileToWrite)
        {
            if (!Directory.Exists(_fileClass.filePath))
            {
                Directory.CreateDirectory(_fileClass.filePath);
                
            }
        }
        public void WritingReport()
        {
            Console.WriteLine("File {0} was created successfully!", _fileClass.fileName);
        }
    }
}
