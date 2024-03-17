using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using PdfExtractorNuget.Services.Helpers;
using PdfExtractorNuget.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace PdfExtractorNuget.Services.PdfLoaders
{
    internal class OpenXmlDocxLoader : ITableLoader
    {
        WordprocessingDocument _document;
        public OpenXmlDocxLoader(string documentPath)
        {
            _document = WordprocessingDocument.Open(documentPath, false);
        }

        public OpenXmlDocxLoader(Stream documentStream) 
        {
            _document = WordprocessingDocument.Open(documentStream, false);
        }

        public DataSet LoadDocumentTables()
        {
            var tables = _document.MainDocumentPart.Document.Body.Elements<Table>();
            DataSet dataSet = new DataSet();
            foreach(Table table in tables)
            {
                if (table.Elements<TableRow>().Any())
                {
                    int columnsAmount = table.GetFirstChild<TableRow>().Count();
                    DataTable dataTable = DataTableHelper.CreateDataTable(columnsAmount);
                    foreach(TableRow row in table.Elements<TableRow>())
                    {
                        DataRow dataRow = dataTable.NewRow();
                        foreach(var (cell, columnIndex) in row.Descendants<TableCell>().Select((cell, index) => (cell, index)))
                        {
                            dataRow[columnIndex] = cell.InnerText;
                        }
                        dataTable.Rows.Add(dataRow);
                    }
                    dataSet.Tables.Add(dataTable); 
                }
            }
            return dataSet;
        }
    }
}
