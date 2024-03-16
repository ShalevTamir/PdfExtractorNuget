using PdfExtractor.Extensions;
using PdfExtractorNuget.Services.Helpers;
using PdfExtractorNuget.Services.Interfaces;
using Spire.Pdf;
using Spire.Pdf.Utilities;
using System;
using System.Data;
using System.IO;

namespace PdfExtractorNuget.Services.PdfLoaders
{
    internal class SpirePdfTableLoader : IPdfTableLoader
    {
        private PdfDocument _pdfDocument;

        public SpirePdfTableLoader(string documentPath)
        {
            _pdfDocument = new PdfDocument(documentPath);
        }

        public SpirePdfTableLoader(Stream documentStream)
        {
            _pdfDocument = new PdfDocument(documentStream);
        }        

        public DataSet LoadDocumentTables()
        {
            PdfTableExtractor tableExtractor = new PdfTableExtractor(_pdfDocument);
            DataSet dataSet = new DataSet();
            for (int pageIndex = 0; pageIndex < _pdfDocument.Pages.Count; pageIndex++)
            {
                PdfTable[] tablesInPage = tableExtractor.ExtractTable(pageIndex);
                if (tablesInPage == null) continue;
                foreach (PdfTable pdfTable in tablesInPage)
                {
                    DataTable dataTable = DataTableHelper.CreateDataTable(pdfTable.GetColumnCount());

                    for (int rowIndex = 0; rowIndex < pdfTable.GetRowCount(); rowIndex++)
                    {
                        DataRow newRow = dataTable.NewRow();
                        for (int columnIndex = 0; columnIndex < pdfTable.GetColumnCount(); columnIndex++)
                        {
                            newRow[columnIndex] = pdfTable.GetText(rowIndex, columnIndex);
                        }
                        dataTable.Rows.Add(newRow);
                    }
                    dataSet.Tables.Add(dataTable);
                }
            }
            return dataSet;
        }
    }
}
