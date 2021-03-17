using FactoryStorage.Models.Context;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FactoryStorage.Resources
{
    public class DataRepositorycs
    {
        public static string[] GetInfomationFromFile(string nameFile)
        {
            string filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);

            var filePathSplit = filePath.Split('\\');

            var myFilePath = "";

            for (int i = 0; i < filePathSplit.Length - 2; i++)
            {
                myFilePath += filePathSplit[i] + "\\";
            }

            myFilePath += "Resources\\" + nameFile + ".txt";

            return File.ReadAllLines(myFilePath);

        }

        public static void SaveInfomation<T>(List<T> list, string nameFile) where T : IModels
        {
            string filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);

            var filePathSplit = filePath.Split('\\');

            var myFilePath = "";

            for (int i = 0; i < filePathSplit.Length - 2; i++)
            {
                myFilePath += filePathSplit[i] + "\\";
            }

            myFilePath += "Resources\\" + nameFile + ".txt";

            try
            {
                StreamWriter sw = new StreamWriter(myFilePath);

                foreach (var item in list)
                {
                    if (item.Number == 0)
                    {
                        continue;
                    }

                    sw.WriteLine(item.Name + "%" + item.Number + "%" + item.CriticalNmber);
                }

                sw.Close();
            }
            catch (Exception e)
            {
                var error = e.Message;
            }
        }

        public static void SaveScheme(ISchemeModel scheme)
        {
            string filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);

            var filePathSplit = filePath.Split('\\');

            var myFilePath = "";

            for (int i = 0; i < filePathSplit.Length - 2; i++)
            {
                myFilePath += filePathSplit[i] + "\\";
            }

            myFilePath += "Resources\\Schemes\\" + scheme.Topic + ".txt";

            if (!(File.Exists(myFilePath)))
            {
                using (FileStream fs = File.Create(myFilePath))
                {
                    fs.Close();
                }
            }

            try
            {
                StreamWriter sw = new StreamWriter(myFilePath);

                sw.WriteLine(scheme.Topic);

                sw.WriteLine(scheme.Description);

                sw.WriteLine("Елементи:");

                foreach (var element in scheme.Elements)
                {
                    sw.WriteLine(element.Name + "%" + element.Number);
                }

                sw.Close();
            }
            catch (Exception e)
            {
                var error = e.Message;
            }
        }

        public static List<string> GetAllScheme()
        {
            string filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);

            var filePathSplit = filePath.Split('\\');

            var myFilePath = "";

            for (int i = 0; i < filePathSplit.Length - 2; i++)
            {
                myFilePath += filePathSplit[i] + "\\";
            }

            myFilePath += "Resources\\Schemes";

            DirectoryInfo d = new DirectoryInfo(myFilePath);

            FileInfo[] Files = d.GetFiles("*.txt");

            var listNameFile = new List<string>();

            foreach (FileInfo file in Files)
            {
                listNameFile.Add(file.Name.Split('.')[0]);
            }

            return listNameFile;
        }
    }
}
