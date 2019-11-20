using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

namespace PartB
{
    internal class Program
    {
        // 
        private static void Main(string[] args)
        {
            var app = new ApplicationController();

            Console.WriteLine("App started!");
            app.PopulateQueue();
            app.DequeueQueue();
            Console.WriteLine("App completed!");
            Console.Read();
        }
    }

    public class ApplicationController
    {
        private const int FilesToWrite = 3;
        private const string FilePath = "\\FilesFolder\\";
        private FileModelClass _fileEntity;
        private FileWriter _fileWriter;

        public QueueService QueueService;

        private void InitializeComponents()
        {
            _fileEntity = new FileModelClass() { FilePath = FilePath };
            _fileWriter = new FileWriter(_fileEntity);
            QueueService = new QueueService();
        }

        public void PopulateQueue()
        {
            InitializeComponents();

            for (var i = 0; i < FilesToWrite; i++)
            {
                Console.WriteLine("Please, enter file name: ");
                var fileName = Console.ReadLine();
                var fileEntityNewElement = new FileModelClass {FilePath = FilePath, FileName = fileName + ".txt"};
                QueueService.EnqueueEntity(fileEntityNewElement);
            }
        }

        public void DequeueQueue()
        {
            while (QueueService.EntityQueue.Count > 0)
            {
                var val = QueueService.DequeueEntity();
                _fileWriter.FileWritingWorker(val);
            }
        }
    }
    public class FileModelClass
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
    }

    public class FileWriter
    {
        private readonly FileModelClass _fileModelClass;
        public FileWriter(FileModelClass fileModelClass)
        {
            _fileModelClass = fileModelClass;
        }
        public void FileWritingWorker(FileModelClass fileModelToWrite)
        {
            FolderService(fileModelToWrite);
            
            if (WriteFile(fileModelToWrite))
            {
                WritingReport(fileModelToWrite);
            }
        }
        public bool WriteFile(FileModelClass fileModelToWrite)
        {
            using (var writeText = new StreamWriter(fileModelToWrite.FilePath + fileModelToWrite.FileName))
            {
                writeText.WriteLine("Text example");
            }
            return true;
        }

        public void FolderService(FileModelClass fileModelToWrite)
        {
            if (!Directory.Exists(fileModelToWrite.FilePath))
            {
                Directory.CreateDirectory(fileModelToWrite.FilePath);
                
            }
        }
        public void WritingReport(FileModelClass fileModelToWrite)
        {
            Console.WriteLine("File {0} was created successfully!", fileModelToWrite.FileName);
        }
    }

    public class QueueService
    {
        public Queue<FileModelClass> EntityQueue = new Queue<FileModelClass>();

       public void EnqueueEntity(FileModelClass fileEntity)
        {
            EntityQueue.Enqueue(fileEntity);
        }
       public FileModelClass DequeueEntity()
        {
            return EntityQueue.Dequeue() as FileModelClass;
        }
    }
}