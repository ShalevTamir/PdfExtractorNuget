using PdfExtractor.Services.Sensor;
using PdfExtractorNuget.Models.Enums;
using PdfExtractorNuget.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PdfExtractorNuget.Services.PdfLoaders
{
    internal class TableLoaderFactory
    {
        private static TableLoaderFactory _instance;
        internal static TableLoaderFactory Instance
        {
            get => _instance ??= new TableLoaderFactory();
            set => _instance = value;
        }

        public ITableLoader CreateTableLoader(string documentPath)
        {
            return CreateTableLoaderLogic(DetectDocumentType(documentPath), documentPath);
        }

        public ITableLoader CreateTableLoader(string fileName, Stream documentStream)
        {
            return CreateTableLoaderLogic(DetectDocumentType(fileName), documentStream);
        }

        private ITableLoader CreateTableLoaderLogic(DocumentType documentType, params object[] loaderArgs)
        {
            Type LoaderType = DocumentTypeToLoader(documentType);
            return (ITableLoader)Activator.CreateInstance(LoaderType, loaderArgs);
        }

        private DocumentType DetectDocumentType(string documentPathOrName)
        {
            // Remove the first character since it's a dot
            string extention = Path.GetExtension(documentPathOrName).Remove(0, 1);
            if(Enum.TryParse(typeof(DocumentType), extention, true, out object documentType))
            {
                return (DocumentType) documentType;
            }
            else
            {
                throw new ArgumentException("Extention of type " + extention + " isn't supported");
            }
        }

        private Type DocumentTypeToLoader(DocumentType documentType)
        {
            switch (documentType)
            {
                case DocumentType.PDF:
                    return typeof(SpirePdfTableLoader);
                case DocumentType.DOCX:
                    return typeof(OpenXmlDocxLoader);
                default:
                    return null;
            }
        }

    }
}
