using System;
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
            DeleteFiles();
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