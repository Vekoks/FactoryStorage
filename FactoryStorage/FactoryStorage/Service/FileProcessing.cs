using FactoryStorage.Models;
using FactoryStorage.Models.Context;
using FactoryStorage.Resources;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                var recordAllInfo = informationFromFile[i].Split('%');

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
                var recordAllInfo = informationFromFile[i].Split('%');

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

        public static void SaveSchemeInFile(ISchemeModel scheme)
        {
            DataRepositorycs.SaveScheme(scheme);
        }

        public static List<string> LoadSchemeNames()
        {
            return DataRepositorycs.GetAllScheme();
        }

        public static SchemeModel LoadScheme(string nameScheme)
        {
            var fullNameScheme = "Schemes\\" + nameScheme;

            var listInfomation= DataRepositorycs.GetInfomationFromFile(fullNameScheme);

            var newScheme = new SchemeModel();

            newScheme.Topic = listInfomation[0];

            newScheme.Description = listInfomation[1];

            newScheme.Elements = new List<StorageModel>();

            for (int i = 3; i < listInfomation.Count(); i++)
            {
                var stringSplit = listInfomation[i].Trim().Split('%');

                var newElement = new StorageModel();

                newElement.Name = stringSplit[0];

                newElement.Number = int.Parse(stringSplit[1]);

                newScheme.Elements.Add(newElement);
            }

            return newScheme;
        }
    }
}
