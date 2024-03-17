using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace PdfExtractorNuget.Services.Interfaces
{
    internal interface ITableLoader
    {
        public DataSet LoadDocumentTables();
    }
}
