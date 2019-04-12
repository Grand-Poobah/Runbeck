using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeExercise.Tests
{
   [TestClass]
   public class FileProcessorTest
   {
      [TestMethod]
      public void FileProcessor_Test_Csv_Pass()
      {
         string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;


         string fileSpec = Path.Combine(baseDirectory, @"Data\input.csv");
         FileTypes? filetype = FileTypes.csv;
         int? fieldCount = 3;

         FileProcessor fileProcessor = new FileProcessor(fileSpec, (FileTypes)filetype, (int)fieldCount);
         fileProcessor.ProcessFile();

         string correctFileSpec = Path.Combine(baseDirectory, @"Data\input-CorrectRecords.csv");
         string incorrectFileSpec = Path.Combine(baseDirectory, @"Data\input-IncorrectRecords.csv");
         Assert.IsTrue(File.Exists(correctFileSpec));
         Assert.IsTrue(File.Exists(incorrectFileSpec));

         int lines = File.ReadLines(correctFileSpec).Count();
         Assert.AreEqual(lines, 2);
         lines = File.ReadLines(incorrectFileSpec).Count();
         Assert.AreEqual(lines, 1);

      }

      [TestMethod]
      [ExpectedException(typeof(InvalidOperationException), "A filetype of null was inappropriately allowed.")]
      public void FileProcessor_Test_Csv_InvalidFileType()
      {
         string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

         string fileSpec = Path.Combine(baseDirectory, @"c:\temp\input.csv");
         FileTypes? filetype = null;
         int? fieldCount = 3;

         FileProcessor fileProcessor = new FileProcessor(fileSpec, filetype, fieldCount);
         fileProcessor.ProcessFile();

      }

      [TestMethod]
      [ExpectedException(typeof(InvalidOperationException), "A fieldCount of null was inappropriately allowed.")]
      public void FileProcessor_Test_Csv_InvalidFieldCount()
      {
         string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

         string fileSpec = Path.Combine(baseDirectory, @"c:\temp\input.csv");
         FileTypes? filetype = FileTypes.csv;
         int? fieldCount = null;

         FileProcessor fileProcessor = new FileProcessor(fileSpec, filetype, fieldCount);
         fileProcessor.ProcessFile();

      }

      [TestMethod]
      [ExpectedException(typeof(InvalidOperationException), "A fileSpec of null was inappropriately allowed.")]
      public void FileProcessor_Test_Csv_InvalidFileSpec()
      {
         string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

         string fileSpec = Path.Combine(baseDirectory, @"c:\temp\nofile.csv");
         FileTypes? filetype = FileTypes.csv;
         int? fieldCount = 3;

         FileProcessor fileProcessor = new FileProcessor(fileSpec, filetype, fieldCount);
         fileProcessor.ProcessFile();

      }


      [TestMethod]
      public void FileProcessor_Test_Tsv_Pass()
      {
         string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;


         string fileSpec = Path.Combine(baseDirectory, @"Data\input.tsv");
         FileTypes? filetype = FileTypes.tsv;
         int? fieldCount = 3;

         FileProcessor fileProcessor = new FileProcessor(fileSpec, (FileTypes)filetype, (int)fieldCount);
         fileProcessor.ProcessFile();

         string correctFileSpec = Path.Combine(baseDirectory, @"Data\input-CorrectRecords.tsv");
         string incorrectFileSpec = Path.Combine(baseDirectory, @"Data\input-IncorrectRecords.tsv");
         Assert.IsTrue(File.Exists(correctFileSpec));
         Assert.IsTrue(File.Exists(incorrectFileSpec));

         int lines = File.ReadLines(correctFileSpec).Count();
         Assert.AreEqual(lines, 2);
         lines = File.ReadLines(incorrectFileSpec).Count();
         Assert.AreEqual(lines, 1);

      }

      [TestMethod]
      [ExpectedException(typeof(InvalidOperationException), "A fileSpec of null was inappropriately allowed.")]
      public void FileProcessor_Test_Tsv_InvalidFileSpec()
      {
         string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;


         string fileSpec = Path.Combine(baseDirectory, @"Data\input123.tsv");
         FileTypes? filetype = FileTypes.tsv;
         int? fieldCount = 3;

         FileProcessor fileProcessor = new FileProcessor(fileSpec, (FileTypes)filetype, (int)fieldCount);
         fileProcessor.ProcessFile();

      }

   }
}
