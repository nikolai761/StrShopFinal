using Microsoft.AspNetCore.Mvc;
using StrShop.Data.Interfaces;
using StrShop.Data.Models;
using StrShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml;
using Microsoft.AspNetCore.Hosting;
using DocumentFormat.OpenXml.Drawing;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using Text = DocumentFormat.OpenXml.Spreadsheet.Text;

namespace StrShop.Controllers
{
    public class FileGenController : Controller
    {
        private readonly IAllItems _items;

        private readonly IWebHostEnvironment hostEnvironment;
        public FileGenController(IAllItems _items, IWebHostEnvironment hostEnvironment)
        {
            this._items = _items;
            this.hostEnvironment = hostEnvironment;
        }
        private string GetLetterOfColumn(int numberOfColumn)
        {
            switch (numberOfColumn)
            {
                case 1: return "A";
                case 2: return "B";
                case 3: return "C";
                case 4: return "D";
                case 5: return "E";
                default: return "F";
            }
        }

        private void CreateXlxFileUsingTemplate(string path)
        {
            string wwwRootPath = hostEnvironment.WebRootPath;
            string fileTemplateName = "ItemsTemplate.xlsx";
            string templatePath = System.IO.Path.Combine(wwwRootPath + "/files/", fileTemplateName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                using (var fileTemplateStream = new FileStream(templatePath, FileMode.Open))
                {
                    fileTemplateStream.CopyTo(fileStream);
                }
            }
        }
        private string GeneratePathForXLSX()
        {
            string wwwRootPath = hostEnvironment.WebRootPath;
            string fileName = "Items";
            string extension = ".xlsx";
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            return System.IO.Path.Combine(wwwRootPath + "/files/", fileName);
        }
        public FileResult GenerateXLSX()
        {
            string path = GeneratePathForXLSX();
            CreateXlxFileUsingTemplate(path);

            // Open the copied template workbook. 
            using (SpreadsheetDocument myWorkbook = SpreadsheetDocument.Open(path, true))
            {
                // Access the main Workbook part, which contains all references.
                WorkbookPart workbookPart = myWorkbook.WorkbookPart;

                // Get the first worksheet. 
                WorksheetPart worksheetPart = workbookPart.WorksheetParts.ElementAt(0);

                // The SheetData object will contain all the data.
                SheetData sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();

                // Begining Row pointer                       
                int index = 2;

                // Database results
                List<Data.Models.Item> items = _items.Items.ToList();

                // For each item in the database, add a Row to SheetData.
                foreach (var item in items)
                {
                    // Cell related variable
                    string name = item.ItemName;
                    string price = item.price.ToString();
                    string category = item.Category.CategoryName;
                    string producer = item.Producer.ProducerName;


                    // New Row
                    Row row = new Row();
                    row.RowIndex = (UInt32)index;

                    for (int columnNumber = 1; columnNumber < 5; columnNumber++)
                    {
                        string letter = GetLetterOfColumn(columnNumber);
                        // New Cell
                        Cell cell = new Cell();
                        cell.DataType = CellValues.InlineString;


                        // Column A1, 2, 3 ... and so on
                        cell.CellReference = letter + index;

                        // Create Text object
                        Text text = new Text();
                        switch (columnNumber)
                        {
                            case 1: text.Text = name; break;
                            case 2: text.Text = price; break;
                            case 3: text.Text = category; break;
                            case 4: text.Text = producer; break;
                            default: text.Text = "ERROR"; break;
                        }

                        // Append Text to InlineString object
                        InlineString inlineString = new InlineString();
                        inlineString.AppendChild(text);

                        // Append InlineString to Cell
                        cell.AppendChild(inlineString);

                        // Append Cell to Row
                        row.AppendChild(cell);
                    }
                    // Append Row to SheetData
                    sheetData.AppendChild(row);

                    // increase row pointer
                    index++;

                }
                // save
                worksheetPart.Worksheet.Save();
                return PhysicalFile(path, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Items.xlsx");
            }
        }
        
        public FileResult GenerateXML()
        {
            List<Data.Models.Item> items = _items.Items.ToList();
            string wwwRootPath = hostEnvironment.WebRootPath;
            string fileName = "ItemsBack";
            string extension = ".xml";
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            string path = System.IO.Path.Combine(wwwRootPath + "/files/", fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                XmlTextWriter xmlWriter = new XmlTextWriter(stream, System.Text.Encoding.Default);
                xmlWriter.Formatting = Formatting.Indented;
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("Items");

                foreach (var item in items)
                {
                    xmlWriter.WriteStartElement("Item");
                    xmlWriter.WriteElementString("ItemName", item.ItemName);
                    xmlWriter.WriteStartElement("Category");
                    xmlWriter.WriteElementString("Category", item.Category.CategoryName);
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteStartElement("Producer");
                    xmlWriter.WriteElementString("Producer", item.Producer.ProducerName);
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndElement();
                }
                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();
                xmlWriter.Flush();
                xmlWriter.Close();

                return PhysicalFile(path, "text/xml", "ItemsBack.xml");
            }
        }
       

    }
}


