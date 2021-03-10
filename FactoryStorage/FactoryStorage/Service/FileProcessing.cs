using FactoryStorage.Models;
using FactoryStorage.Resources;
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
        public static List<IModels> GetResources()
        {
            var informationFromFile = DataRepositorycs.GetInfomationFromFile("Resources");

            var listOrders = new List<IModels>();

            for (int i = 0; i < informationFromFile.Count(); i++)
            {
                var recordAllInfo = informationFromFile[i].Split('-');

                var currentOrder = new StorageModel
                {
                    Name = recordAllInfo[0],
                    Number = int.Parse(recordAllInfo[1])
                };

                listOrders.Add(currentOrder);
            }

            return listOrders;
        }

        public static List<IModels> GetCriticalNumber()
        {
            var informationFromFile = DataRepositorycs.GetInfomationFromFile("CriticalNumber");

            var listCriticalElelements = new List<IModels>();

            for (int i = 0; i < informationFromFile.Count(); i++)
            {
                var recordAllInfo = informationFromFile[i].Split('-');

                var currentElelement = new CriticaNumbers
                {
                    Name = recordAllInfo[0],
                    Number = int.Parse(recordAllInfo[1])
                };

                listCriticalElelements.Add(currentElelement);
            }

            return listCriticalElelements;
        }

        public static void SaveInfomationInFile(List<IModels> list, string fileName)
        {
            DataRepositorycs.SaveInfomation(list, fileName);
        }
    }
}
