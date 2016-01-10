using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolutionsUserOptionsCleaner
{
    class Program
    {
        static void Main(string[] args)
        {
            ArgumentValidator validator = new ArgumentValidator(args);
            validator.Rules.Add(x => x.Any());
            validator.Rules.Add(x => Directory.Exists(x[0]));
            if(!validator.Valid())
                foreach (var error in validator.Errors)
                    Console.WriteLine(error);


        }
    }
}
