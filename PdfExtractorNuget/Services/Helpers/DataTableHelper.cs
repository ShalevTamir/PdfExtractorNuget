using PdfExtractorNuget.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace PdfExtractorNuget.Services.Helpers
{
    internal class DataTableHelper
    {
        public static DataTable CreateDataTable(int columnNumber)
        {
            DataTable dataTable = new DataTable();
            dataTable.DefineColumns(columnNumber); 
            return dataTable;
        }
    }
}
