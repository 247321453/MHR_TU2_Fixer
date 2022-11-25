using System;
using System.IO;
using static MHR_TU2_Fixer.Helpers.FolderHelper;
using static MHR_TU2_Fixer.Helpers.MDFHelper;
using static MHR_TU2_Fixer.MDF.MDFEnums;

namespace MHR_TU2_Fixer
{
    public static class Program
    {
        public static string CurrentDirectory;
        private static void Main(string[] args)
        {
            CosturaUtility.Initialize();
            CurrentDirectory = Path.GetDirectoryName(typeof(Program).Assembly.Location);
            string baseFolder;
            if (args.Length == 0)
            {
                baseFolder = CurrentDirectory;
            } else
            {
                baseFolder = args[0];
            }
            Console.WriteLine("Current Folder:" + baseFolder);

            //Get conversion folders
            //var conversionBaseFolder = CreateFolder(Environment.CurrentDirectory, "Conversions");
            //var conversionFolder = conversionBaseFolder.CreateSubdirectory($"{DateTime.Now:yyyyMMdd_HHmmss}_{Path.GetFileName(baseFolder.FullName)}");

            //Generate the prefabs, and update them to TU1 and TU2
            //Copy over all files in same format to folder, and attempt conversion on the folder
            //CloneDirectory(baseFolder, conversionFolder
            //    ,
            //     "*.pfb.17"
            //    );
            //
            //PrefabFixer.GeneratePrefabs(null, Directory.CreateDirectory(baseFolder));

            //Convert the MDF Files
            //Copy over all files in same format to folder, and attempt conversion on the folder

            //CloneDirectory(baseFolder, conversionFolder
            //    ,
           //     "*.mdf2.23"
           //     );

            ConvertMDFFiles(GetFiles(baseFolder, "*.mdf2.23"), MDFConversion.MergeAndAddMissingProperties);

            //Open Folder Location with file explorer
            //OpenExplorerLocation(conversionFolder.FullName);
            Console.WriteLine("completed!");
            Console.ReadKey();
        }
    }
}