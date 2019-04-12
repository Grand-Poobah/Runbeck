using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeExercise
{
   public enum FileTypes
   {
      csv,
      tsv
   }

   public class FileProcessor
   {

      public string FileSpecification { get; set; }
      public FileTypes? FileType { get; set; } = null;
      public int? FieldCount { get; set; } = null;

      public string InputFilename
      {
         get
         {
            if (String.IsNullOrWhiteSpace(FileSpecification))
            {
               return null;
            }

            return Path.GetFileNameWithoutExtension(FileSpecification);
         }
      }

      public FileProcessor(string fileSpecification, FileTypes? fileType, int? fieldCount)
      {
         FileSpecification = fileSpecification;
         FileType = fileType;
         FieldCount = fieldCount;
      }

      public void ProcessFile()
      {
         if (String.IsNullOrWhiteSpace(FileSpecification) || (!File.Exists(FileSpecification)))
         {
            throw new InvalidOperationException("The file specified is invalid.");
         }
         if (!FieldCount.HasValue)
         {
            throw new InvalidOperationException("The field count is invalid");
         }
         if (!FileType.HasValue)
         {
            throw new InvalidOperationException("The file type is invalid");
         }

         try
         {
            StringBuilder validRecords = new StringBuilder();
            StringBuilder invalidRecords = new StringBuilder();

            char delimiter = '\0';
            switch (FileType)
            {
               case FileTypes.csv:
                  delimiter = ',';
                  break;
               case FileTypes.tsv:
                  delimiter = '\t';
                  break;
            }

            string record;
            bool header = true;
            using (StreamReader file = new StreamReader(FileSpecification))
            {
               while ((record = file.ReadLine()) != null)
               {
                  if (header)
                  {
                     header = false;
                     continue;
                  }
                  var fields = record.Split(delimiter);
                  if (!fields.Any() || fields.Length != FieldCount)
                  {
                     invalidRecords.AppendLine(record);
                     continue;
                  }
                  validRecords.AppendLine(record);
               }
            }

            string outputPath = Path.GetDirectoryName(FileSpecification);
            OutputRecords(validRecords, Path.Combine(outputPath, $"{InputFilename}-CorrectRecords.{FileType.ToString()}"));
            OutputRecords(invalidRecords, Path.Combine(outputPath, $"{InputFilename}-IncorrectRecords.{FileType.ToString()}"));
         }
         catch (Exception ex)
         {
            //Note: exception would be logged/bubbled in real world scenario
            Console.WriteLine("An error occured parsing the input file, please try again.");
         }
      }

      private static void OutputRecords(StringBuilder data,
                                        string outputFileSpec)
      {
         if (File.Exists(outputFileSpec))
         {
            File.Delete(outputFileSpec);
         }

         if ((data == null) || (data.Length == 0)) return;
         try
         {
            File.WriteAllText(outputFileSpec, data.ToString());
         }
         catch (Exception ex)
         {
            //Note: exception would be logged/bubbled in real world scenario
            Console.WriteLine($"An error occured writing to {outputFileSpec}, please try again.");
         }
      }

   }
}
