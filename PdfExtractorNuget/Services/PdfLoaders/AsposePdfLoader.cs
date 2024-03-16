using Aspose.Pdf;
using Aspose.Pdf.Text;
using PdfExtractorNuget.Extensions;
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
    internal class AsposePdfLoader : IPdfTableLoader
    {
        private Document _pdfDocument;
        public AsposePdfLoader(string documentPath)
        {
            _pdfDocument = new Document(documentPath);
        }

        public AsposePdfLoader(Stream documentStream) 
        {
            _pdfDocument = new Document(documentStream);
        }
        public DataSet LoadDocumentTables()
        {
            var dataSet = new DataSet();
            foreach(var page in _pdfDocument.Pages)
            {
                var tableAbsorber = new TableAbsorber();
                tableAbsorber.Visit(page);
                foreach(AbsorbedTable table in tableAbsorber.TableList)
                {
                    var dataTable = new DataTable();
                    bool firstRow = true;
                    foreach(AbsorbedRow row in table.RowList)
                    {
                        if (firstRow)
                        {
                            dataTable.DefineColumns(row.CellList.Count);
                            firstRow = false;
                        }
                        DataRow dataRow = dataTable.NewRow();
                        for(int columnIndex = 0; columnIndex < row.CellList.Count; columnIndex++)
                        {
                            AbsorbedCell cell = row.CellList[columnIndex];
                            TextFragmentCollection textFragmentColleciton = cell.TextFragments;
                            string textInCell = string.Join("", textFragmentColleciton.AsEnumerable().Select(textFragment => textFragment.Text).ToArray());
                            dataRow[columnIndex] = textInCell;
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
