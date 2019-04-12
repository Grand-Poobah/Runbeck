using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeExercise
{
   class Program
   {

      static void Main(string[] args)
      {
         Console.WriteLine("\n\nWelcome to the Runbeck Code Exercise.");
         Console.WriteLine("Press the Enter Key at any time to Exit..\n");

         while (true)
         {

            string fileSpec = GetFilename();
            if (String.IsNullOrWhiteSpace(fileSpec)) return;

            FileTypes? filetype = GetFileType();
            if (filetype == null) return;

            int? fieldCount = GetFieldCount();
            if (fieldCount == null) return;

            try
            {
               Console.WriteLine("\nProcessing File\n");
               FileProcessor fileProcessor = new FileProcessor(fileSpec, (FileTypes)filetype, (int)fieldCount);
               fileProcessor.ProcessFile();
               Console.WriteLine("File Processed\n\n");
            }
            catch (InvalidOperationException ioe)
            {
               Console.WriteLine(ioe.Message + "\n");
            }
            catch
            {
               Console.WriteLine("An error has occurred processing the file.\n");
            }
            Console.WriteLine("Process another file (y/n) ? ");
            if (Console.ReadLine().ToLower() != "y")
            {
               break;
            }
         }
      }

      private static string GetFilename()
      {
         while (true)
         {
            Console.WriteLine("\nPlease enter the path to the file: ");
            string fileSpec = Console.ReadLine();
            if (String.IsNullOrWhiteSpace(fileSpec))
            {
               return null;
            }
            if (File.Exists(fileSpec))
            {
               return fileSpec;
            }
            Console.WriteLine("\nThe file specified does not exist.\n");
         }
      }

      private static FileTypes? GetFileType()
      {
         while (true)
         {
            Console.WriteLine("\nPlease enter the file type (CSV/TSV): ");
            string input = Console.ReadLine().ToLower();
            if (String.IsNullOrWhiteSpace(input))
            {
               return null;
            }

            if (!int.TryParse(input, out int n))
            {
               FileTypes fileType;
               if (Enum.TryParse(input, out fileType))
               {
                  return fileType;
               }
            }

            Console.WriteLine($"\nPlease enter a valid file type. ({String.Join("/", Enum.GetNames(typeof(FileTypes)))})\n");
         }
      }

      private static int? GetFieldCount()
      {
         while (true)
         {
            Console.WriteLine("\nPlease enter the number of fields per record: ");
            string input = Console.ReadLine();
            if (String.IsNullOrWhiteSpace(input))
            {
               return null;
            }

            int fieldCount;
            if (int.TryParse(input, out fieldCount))
            {
               return fieldCount;
            }
            Console.WriteLine("The value entered is not a valid number.");
         }
      }
   }
}