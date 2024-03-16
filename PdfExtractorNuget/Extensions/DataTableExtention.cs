using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace PdfExtractorNuget.Extensions
{
    internal static class DataTableExtention
    {
        public static void DefineColumns(this DataTable table, int columnsAmount)
        {
            for (int columnIndex = 0; columnIndex < columnsAmount; columnIndex++)
                table.Columns.Add($"Column {columnIndex}");
        }
    }
}
