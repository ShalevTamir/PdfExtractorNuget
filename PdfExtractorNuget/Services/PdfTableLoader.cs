using PdfExtractor.Extensions;
using Spire.Pdf;
using Spire.Pdf.Utilities;
using System;
using System.Data;

namespace PdfExtractor.Services
{
    internal class PdfTableLoader
    {
        private PdfDocument _pdfDocument;
      
        public PdfTableLoader(string documentPath) 
        {
            _pdfDocument = new PdfDocument(documentPath);           
        }

        internal static readonly char[] CHARS_TO_FILTER = { '\n', ' ' };
        internal static ReadOnlySpan<char> FilterText(ReadOnlySpan<char> textToFilter)
        {
            return textToFilter.Trim(new ReadOnlySpan<char>(CHARS_TO_FILTER))
                                .Replace('\n', ' ')
                                .RemoveDuplicateChar(' ');
        }

        internal DataSet LoadDocumentTables()
        {           
            PdfTableExtractor tableExtractor = new PdfTableExtractor(_pdfDocument);
            DataSet dataSet = new DataSet();
            for(int pageIndex = 0; pageIndex < _pdfDocument.Pages.Count; pageIndex++)
            {
                PdfTable[] tablesInPage = tableExtractor.ExtractTable(pageIndex);
                if (tablesInPage == null) continue;
                foreach(PdfTable pdfTable in tablesInPage)
                {
                    DataTable dataTable = CreateDataTable(pdfTable.GetColumnCount());
                    
                    for(int rowIndex = 0; rowIndex < pdfTable.GetRowCount(); rowIndex++)
                    {
                        DataRow newRow = dataTable.NewRow();
                        for(int columnIndex = 0; columnIndex < pdfTable.GetColumnCount(); columnIndex++)
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

      
        private DataTable CreateDataTable(int columnNumber)
        {
            DataTable dataTable = new DataTable();

            for(int columnIndex =0; columnIndex < columnNumber; columnIndex++)
                dataTable.Columns.Add($"Column {columnIndex}");
            
            return dataTable;
        }

    }
}
