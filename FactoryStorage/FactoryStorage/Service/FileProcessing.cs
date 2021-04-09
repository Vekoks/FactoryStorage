using FactoryStorage.Models;
using FactoryStorage.Models.Context;
using FactoryStorage.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FactoryStorage.Service
{
    public class FileProcessing
    {
        public static List<IModels> GetResources()
        {
            var informationFromFile = DataRepository.GetInfomationFromFile("Resources");

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

        public static void SaveInfomationInFile(List<IModels> list, string fileName)
        {
            DataRepository.SaveInfomation(list, fileName);
        }

        public static void SaveInfomationInFile(StorageModel element,char sign, string fileName)
        {
            var collectionStorageDataModels = this.GetResources();

            var find = false;

            foreach (var current in collectionStorageDataModels)
            {
                if (string.Equals(current.Name, element.Name))
                {
                    if (char.Equals(sign, '+'))
                    {
                        current.Number += element.Number;

                        this.SaveTransaction("Добавяне на " + element.Number + "бр " + DateTime.Now);
                    }
                    else
                    {
                        current.Number -= element.Number;

                        this.SaveTransaction("Вземане на " + element.Number + "бр "+ DateTime.Now);
                    }

                    find = true;
                }
            }

            if (!find)
            {
                this.SaveTransaction("Добавяне на " + element.Number + "бр " + DateTime.Now);

                collectionStorageDataModels.Add(element);
            }

           DataRepository.SaveInfomation(collectionStorageDataModels, fileName);
        }

        public static void SaveSchemeInFile(ISchemeModel scheme)
        {
            DataRepository.SaveScheme(scheme);
        }

        public static List<string> LoadSchemeNames()
        {
            return DataRepository.GetAllScheme();
        }

        public static SchemeModel LoadScheme(string nameScheme)
        {
            var fullNameScheme = "Schemes\\" + nameScheme;

            var listInfomation = DataRepository.GetInfomationFromFile(fullNameScheme);

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

        public static void SavePdfFile(string PathFile, string Topic, List<string> InformationContext)
        {
            DataRepository.SaveInfoToPdfFile(PathFile, Topic, InformationContext);
        }

        public static void SaveTransaction(string currentTransation)
        {
            var listElementFromFile = DataRepository.GetInfomationFromFile("Transaction").ToList();
            
            listElementFromFile.Add(currentTransation);
            
            DataRepository.SaveTransactions(listElementFromFile, "Transaction");
        }

        public static List<string> GetTransaction(string nameElement)
        {
            var listElementFromFile = DataRepository.GetInfomationFromFile("Transaction");

            var listFindElement = new List<string>();

            var listForRefreshTranzation = new List<string>();

            foreach (var element in listElementFromFile)
            {
                var elementFromFile = element.Split('%');

                var elementNameFromFile = elementFromFile[0];

                var elementInformation = elementFromFile[1];

                if (string.Equals(elementNameFromFile, nameElement))
                {
                    listFindElement.Add(elementInformation);
                }
                else
                {
                    listForRefreshTranzation.Add(element);
                }
            }

            if (listFindElement.Count > 5)
            {
                var numberForDelete = listFindElement.Count - 5;

                for (int i = 0; i < numberForDelete; i++)
                {
                    listFindElement.RemoveAt(i);
                }
            }

            foreach (var element in listFindElement)
            {
                listForRefreshTranzation.Add(nameElement+ "%" + element);
            }

            DataRepository.SaveTransactions(listForRefreshTranzation, "Transaction");

            return listFindElement;
        }

    }
}
