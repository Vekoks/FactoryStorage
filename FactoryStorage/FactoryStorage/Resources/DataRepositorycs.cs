using FactoryStorage.Models.Context;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
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

            if (!(File.Exists(myFilePath)))
            {
                using (FileStream fs = File.Create(myFilePath))
                {
                    fs.Close();
                }
            }

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

                foreach (var item in list)
                {
                    if (item.Number == 0)
                    {
                        continue;
                    }

                    sw.WriteLine(item.Name + "%" + item.Number + "%" + item.CriticalNumber);
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

        public static void SaveInfoToPdfFile(string pathFile, string topicFile, List<string> informationList)
        {
            PdfDocument pdf = new PdfDocument();
            PdfPage pdfPage = pdf.AddPage();
            XGraphics graph = XGraphics.FromPdfPage(pdfPage);
            XFont fontTitle = new XFont("Verdana", 20);
            XFont fontRow = new XFont("Verdana", 14);

            graph.DrawString(topicFile, fontTitle, XBrushes.Black, new XRect(0, 0, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopCenter);

            for (int i = 0, j = 2; i < informationList.Count; i++, j += 2)
            {
                var currentLine = "     " + informationList[i];

                var numberLine = j * 14;

                if (numberLine / 756 == 1)
                {
                    pdfPage = pdf.AddPage();
                    graph = XGraphics.FromPdfPage(pdfPage);

                    j = 2;
                    numberLine = j * 14;
                }

                graph.DrawString(currentLine, fontRow, XBrushes.Black, new XRect(0, numberLine, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
            }

            pdf.Save(pathFile);
        }

        public static void SaveTransaction(string nameFile, string currentTransation)
        {
            string filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);

            var filePathSplit = filePath.Split('\\');

            var myFilePath = "";

            for (int i = 0; i < filePathSplit.Length - 2; i++)
            {
                myFilePath += filePathSplit[i] + "\\";
            }

            myFilePath += "Resources\\" + nameFile + ".txt";

            if (!(File.Exists(myFilePath)))
            {
                using (FileStream fs = File.Create(myFilePath))
                {
                    fs.Close();
                }
            }

            var listTransaction = File.ReadAllLines(myFilePath).ToList(); ;

            listTransaction.Add(currentTransation);

            try
            {
                StreamWriter sw = new StreamWriter(myFilePath);

                foreach (var item in listTransaction)
                {
                    sw.WriteLine(item);
                }

                sw.Close();
            }
            catch (Exception e)
            {
                var error = e.Message;
            }
        }

        public static void SaveTransaction(List<string> list, string nameFile)
        {
            string filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);

            var filePathSplit = filePath.Split('\\');

            var myFilePath = "";

            for (int i = 0; i < filePathSplit.Length - 2; i++)
            {
                myFilePath += filePathSplit[i] + "\\";
            }

            myFilePath += "Resources\\" + nameFile + ".txt";

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

                foreach (var item in list)
                {
                    sw.WriteLine(item);
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
