using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMC4ModelConverter;
using DMC4ModelConverter.MTFramework;
using DMC4ModelConverter.Wavefront;

namespace ModelConverterConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write(">");
            string input = Console.ReadLine();
            while (input != "exit")
            {
                if (input.StartsWith("export "))
                {
                    string[] parameters = input.Split(new char[] { ' ' });
                    if (parameters.Length == 3)
                    {
                        ExportRoutine(parameters[1], parameters[2]);
                    }
                }
                Console.Write(">");
                input = Console.ReadLine();
            }
        }

        static void ExportRoutine(string sourceFile,string targetFile)
        {
            //Load src file
            DMC4Model mdl = new DMC4Model();
            mdl.LoadModel(sourceFile);

            //Convert
            WavefrontModel convertedMdl = WavefrontModel.FromDMC4Model(mdl);
            convertedMdl.SaveModel(targetFile);
        }
    }
}
