using FactoryStorage.Data.Data;
using FactoryStorage.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FactoryStorage.Data
{
    public class DbRepository
    {
        public static string[] GetInfomationFromStorage()
        {
            var context = new DataFactoryStorageContext();

            var collectionStorageDataModels = context.StorageDataModels.ToList();

            var stringArrResolt = new string[collectionStorageDataModels.Count];

            for (int i = 0; i < collectionStorageDataModels.Count; i++)
            {
                stringArrResolt[i] = collectionStorageDataModels[i].Name + "%" + collectionStorageDataModels[i].Number + "%" + collectionStorageDataModels[i].CriticalNumber;
            }

            return stringArrResolt;
        }

        public static void SaveInfomation(StorageDataModel storageDataModel, char sign)
        {
            var context = new DataFactoryStorageContext();

            var collectionStorageDataModels = context.StorageDataModels.ToList();

            var newTransation = new DataTransaction();

            var find = false;

            foreach (var current in collectionStorageDataModels)
            {
                if (string.Equals(current.Name, storageDataModel.Name))
                {
                    if (char.Equals(sign, '+'))
                    {
                        current.Number += storageDataModel.Number;

                        newTransation.Discription = "Добавяне на " + storageDataModel.Number + "бр";

                        newTransation.Date = DateTime.Now;
                    }
                    else
                    {
                        current.Number -= storageDataModel.Number;

                        newTransation.Discription = "Вземане на " + storageDataModel.Number + "бр";

                        newTransation.Date = DateTime.Now;
                    }

                    current.Transactions.Add(newTransation);

                    find = true;
                }
            }

            if (!find)
            {
                newTransation.Discription = "Добавяне на " + storageDataModel.Number + "бр";

                newTransation.Date = DateTime.Now;

                storageDataModel.Transactions.Add(newTransation);

                context.StorageDataModels.Add(storageDataModel);
            }

            context.SaveChanges();
        }

        public static void SaveScheme(SchemeDataModel scheme)
        {
            var context = new DataFactoryStorageContext();

            var findSchemes = context.SchemeDataModels.Where(x => x.Topic == scheme.Topic).FirstOrDefault();

            if (findSchemes != null)
            {
                if (findSchemes.DataSchemeElements.Count != scheme.DataSchemeElements.Count)
                {
                    var schemeElement = scheme.DataSchemeElements.ToList();

                    for (int i = 0; i < schemeElement.Count; i++)
                    {
                        var currnet = schemeElement[i];

                        if (!findSchemes.DataSchemeElements.Contains(currnet))
                        {
                            findSchemes.DataSchemeElements.Add(currnet);

                            break;
                        }
                    }
                }
                else
                {
                    var schemeElement = scheme.DataSchemeElements.ToList();

                    var existSchemeElement = findSchemes.DataSchemeElements.ToList();


                    for (int i = 0; i < schemeElement.Count; i++)
                    {
                        existSchemeElement[i].Number = schemeElement[i].Number;
                    }
                }

                context.SaveChanges();
            }
            else
            {
                context.SchemeDataModels.Add(scheme);

                context.SaveChanges();
            }

        }

        public static List<DataTransaction> GetTransactionOnElement(string nameElement)
        {
            var context = new DataFactoryStorageContext();

            var collectionStorageDataModels = context.StorageDataModels.Where(x => x.Name == nameElement).FirstOrDefault();

            var stringArrResolt = new string[collectionStorageDataModels.Transactions.Count];

            var transaction = collectionStorageDataModels.Transactions.ToList();

            return transaction;
        }

        public static void DleteTransactionOnElement(string nameElement)
        {
            var context = new DataFactoryStorageContext();

            var collectionStorageDataModels = context.StorageDataModels.Where(x => x.Name == nameElement).FirstOrDefault();

            if (collectionStorageDataModels.Transactions.Count > 5)
            {
                var endIndex = collectionStorageDataModels.Transactions.Count - 5;

                var transaction = collectionStorageDataModels.Transactions.ToList();

                for (int i = 0; i < endIndex; i++)
                {
                    collectionStorageDataModels.Transactions.Remove(transaction[i]);
                }
            }

            context.SaveChanges();
        }

        public static void ChangeCriticalNumber(string nameElement, int newNumber)
        {
            var context = new DataFactoryStorageContext();

            var element = context.StorageDataModels.Where(x => x.Name == nameElement).FirstOrDefault();

            element.CriticalNumber = newNumber;

            context.SaveChanges();
        }

        public static SchemeDataModel GetScheme(string nameScheme)
        {
            var context = new DataFactoryStorageContext();

            var element = context.SchemeDataModels.Where(x => x.Topic == nameScheme).FirstOrDefault();

            return element;
        }

        public static List<string> GetAllScheme()
        {
            var context = new DataFactoryStorageContext();

            var allScheme = context.SchemeDataModels.ToList();

            var resoltList = new List<string>();

            foreach (var scheme in allScheme)
            {
                resoltList.Add(scheme.Topic);
            }

            return resoltList;
        }
    }
}
