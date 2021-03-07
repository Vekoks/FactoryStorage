using FactoryStorage.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FactoryStorage.Service
{
    public class FileProcessing
    {
        public static List<StorageModel> GetInfomation()
        {
            string filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);

            var filePathSplit = filePath.Split('\\');

            var myFilePath = "";

            for (int i = 0; i < filePathSplit.Length - 3; i++)
            {
                myFilePath += filePathSplit[i] + "\\";
            }

            myFilePath += "Resources\\Resources.txt";

            var informationFromFile = File.ReadAllLines(myFilePath);

            var listOrders = new List<StorageModel>();

            for (int i = 0; i < informationFromFile.Count(); i++)
            {
                var recordAllInfo = informationFromFile[i].Split('-');

                var currentOrder = new StorageModel();

                currentOrder.Name = recordAllInfo[0];
                currentOrder.Number = int.Parse(recordAllInfo[1]);

                listOrders.Add(currentOrder);
            }

            return listOrders;
        }

        public static void SaveInfomation(List<StorageModel> list)
        {
            string filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);

            var filePathSplit = filePath.Split('\\');

            var myFilePath = "";

            for (int i = 0; i < filePathSplit.Length - 3; i++)
            {
                myFilePath += filePathSplit[i] + "\\";
            }

            myFilePath += "Resources\\Resources.txt";

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

            MessageBox.Show("Успешено добавяне");
        }
    }
}
