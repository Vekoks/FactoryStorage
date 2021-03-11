using FactoryStorage.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryStorage.Resources
{
    public static class DataRepositorycs
    {
        public static string[] GetInfomationFromFile(string nameFile)
        {
            string filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);

            var filePathSplit = filePath.Split('\\');

            var myFilePath = "";

            for (int i = 0; i < filePathSplit.Length - 3; i++)
            {
                myFilePath += filePathSplit[i] + "\\";
            }

            myFilePath += "Resources\\" + nameFile + ".txt";

            return File.ReadAllLines(myFilePath);

        }

        public static void SaveInfomation<T>(List<T> list , string nameFile) where T : IModels
        {
            string filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);

            var filePathSplit = filePath.Split('\\');

            var myFilePath = "";

            for (int i = 0; i < filePathSplit.Length - 3; i++)
            {
                myFilePath += filePathSplit[i] + "\\";
            }

            myFilePath += "Resources\\" + nameFile + ".txt";

            try
            {
                StreamWriter sw = new StreamWriter(myFilePath);

                foreach (var item in list)
                {
                    sw.WriteLine(item.Name + "-" + item.Number);
                }

                sw.Close();
            }
            catch (Exception e)
            {
                var error = e.Message;
            }
        }
    }
}
