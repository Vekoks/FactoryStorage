using FactoryStorage.Data;
using FactoryStorage.Data.Models;
using FactoryStorage.Models;
using FactoryStorage.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FactoryStorage.Service
{
    public class DataProcessing
    {
        public static List<IModels> GetResources()
        {
            var informationFromFile = DbRepository.GetInfomationFromStorage();

            var listOrders = new List<IModels>();

            for (int i = 0; i < informationFromFile.Count(); i++)
            {
                var recordAllInfo = informationFromFile[i].Split('%');

                var currentOrder = new StorageModel
                {
                    Name = recordAllInfo[0],
                    Number = int.Parse(recordAllInfo[1]),
                    CriticalNumber = int.Parse(recordAllInfo[2]),
                };

                listOrders.Add(currentOrder);
            }

            return listOrders;
        }

        public static void SaveInfomation(IModels element,char sign)
        {
            var newElement = new StorageDataModel();

            newElement.Name = element.Name;

            newElement.Number = element.Number;

            newElement.CriticalNumber = element.CriticalNumber;

            DbRepository.SaveInfomation(newElement, sign);
        }

        public static void SaveScheme(ISchemeModel scheme)
        {
            var newScheme = new SchemeDataModel();

            newScheme.Topic = scheme.Topic;

            newScheme.Description = scheme.Description;

            foreach (var element in scheme.Elements)
            {
                var newElement = new DataSchemeElement()
                {
                    Name = element.Name,
                    Number = element.Number
                };

                newScheme.DataSchemeElements.Add(newElement);
            }

            DbRepository.SaveScheme(newScheme);
        }

        public static List<string> GetTransaction(string nameElement)
        {
            var listElementFromFile = DbRepository.GetTransactionOnElement(nameElement);

            var listFindElement = new List<string>();

            if (listElementFromFile.Count > 5)
            {
                var startIndex = listElementFromFile.Count - 5;

                for (int i = startIndex; i < listElementFromFile.Count; i++)
                {
                    listFindElement.Add(listElementFromFile[i].Discription + " " + listElementFromFile[i].Date);
                }

                //DbRepository.DleteTransactionOnElement(nameElement);

            }
            else
            {
                for (int i = 0; i < listElementFromFile.Count; i++)
                {
                    listFindElement.Add(listElementFromFile[i].Discription + " " + listElementFromFile[i].Date);
                }
            }

            return listFindElement;
        }

        public static void ChangeCriticalNumber(string nameElement, int newNumber)
        {
            DbRepository.ChangeCriticalNumber(nameElement, newNumber);
        }

        public static List<string> LoadSchemeNames()
        {
            return DbRepository.GetAllScheme();
        }

        public static SchemeModel LoadScheme(string nameScheme)
        {
            var schemeInfomation = DbRepository.GetScheme(nameScheme);

            var newScheme = new SchemeModel();

            newScheme.Topic = schemeInfomation.Topic;

            newScheme.Description = schemeInfomation.Description;

            newScheme.Elements = new List<StorageModel>();

            foreach (var element in schemeInfomation.DataSchemeElements)
            {
                var newElement = new StorageModel();

                newElement.Name = element.Name;

                newElement.Number = element.Number;

                newScheme.Elements.Add(newElement);
            }

            return newScheme;
        }
    }
}
