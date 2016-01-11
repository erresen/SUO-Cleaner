using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace SolutionsUserOptionsCleaner
{
    internal class Program
    {
        private static string[] _filesToDelete;

        private static void Main(string[] args)
        {
            if (!ArgsAreValid(args))
                return;

            FindSuoFiles(args);
            DisplayFoundFiles();
            if (FilesWereFound() && DeleteConfirmed())
                DeleteFiles();
            else
                Console.ReadKey();
        }

        private static bool FilesWereFound()
        {
            return _filesToDelete.Length> 0;
        }

        private static bool DeleteConfirmed()
        {
            bool? confirmed = null;
            while (confirmed == null)
            {
                Console.WriteLine("Delete files? (Y/n)");
                var key = Console.ReadKey(true);
                confirmed = Confirm(key);
            }

            return confirmed.Value;
        }

        private static bool? Confirm(ConsoleKeyInfo key)
        {
            var authorise = new List<ConsoleKey> {ConsoleKey.Enter, ConsoleKey.Y};
            var decline = new List<ConsoleKey> {ConsoleKey.N};
            if (authorise.Contains(key.Key))
                return true;
            if (decline.Contains(key.Key))
                return false;
            return null;
        }

        private static void DisplayFoundFiles()
        {
            Console.WriteLine($"Found {_filesToDelete.Length} files to delete");
            foreach (var file in _filesToDelete)
                Console.WriteLine($"-\t{file}");
        }

        private static void DeleteFiles()
        {
            foreach (var file in _filesToDelete)
                File.Delete(file);
        }

        private static void FindSuoFiles(string[] args)
        {
            var baseDir = args[0];
            _filesToDelete = Directory.GetFiles(baseDir, "*.suo", SearchOption.AllDirectories);
        }

        private static bool ArgsAreValid(string[] args)
        {
            return SetupValidator(args).Valid();
        }

        private static ArgumentValidator SetupValidator(string[] args)
        {
            var validator = new ArgumentValidator(args);
            validator.ProcessErrorsOnInvalid = true;
            AddValidatorRules(validator);
            return validator;
        }

        private static void AddValidatorRules(ArgumentValidator validator)
        {
            validator.Rules.Add(x => x.Any());
            validator.Rules.Add(x => Directory.Exists(x[0]));
        }
    }
}