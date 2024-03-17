using PdfExtractor.Models;
using PdfExtractor.Services.Sensor;
using PdfExtractorNuget.Services.Interfaces;
using PdfExtractorNuget.Services.PdfLoaders;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace PdfExtractor.Services
{
    public class TableProccessor
    {
        private static TableProccessor _instance;
        private SensorFactory _sensorUtils;
        private SensorParamsParser _sensorParser;
        private TableLoaderFactory _tableLoaderFactory;

        private const int PARAM_NAME_INDEX = 0;
        private const int VALID_RANGE_INDEX = 1;
        private const int NORMAL_RANGE_INDEX = 2;
        private const int INVALID_RANGE_INDEX = 3;
        private const int ADDITIONAL_INDEX = 4;

        public static TableProccessor Instance
        {
            get => _instance ??= new TableProccessor();
            set => _instance = value;
        }
        private TableProccessor() 
        {
            _sensorUtils = SensorFactory.Instance;
            _sensorParser = SensorParamsParser.Instance;
            _tableLoaderFactory = TableLoaderFactory.Instance;
        }

        public IEnumerable<SensorProperties> ProccessTable(string fileName, Stream documentStream)
        {
            return ProcessTableLogic(_tableLoaderFactory.CreateTableLoader(fileName, documentStream));
        }
        public IEnumerable<SensorProperties> ProccessTable(string tablePath)
        {
            return ProcessTableLogic(_tableLoaderFactory.CreateTableLoader(tablePath));
        }
        private IEnumerable<SensorProperties> ProcessTableLogic(ITableLoader pdfLoader)
        {
            DataSet tablesDataSet = pdfLoader.LoadDocumentTables();
            for (int tableIndex = 0; tableIndex < tablesDataSet.Tables.Count; tableIndex++)
            {
                DataTable dataTable = tablesDataSet.Tables[tableIndex];
                for (int rowIndex = 0; rowIndex < dataTable.Rows.Count; rowIndex++)
                {
                    SensorProperties sensorInCurRow = null;
                    try
                    {
                        DataRow tableRow = dataTable.Rows[rowIndex];
                        ReadOnlySpan<char> telemetryParamName = _sensorParser.ParseParameterName(tableRow[PARAM_NAME_INDEX].ToString());
                        double[] validRange = _sensorParser.ParseRequirement(tableRow[VALID_RANGE_INDEX].ToString());
                        double[] normalRange = _sensorParser.ParseRequirement(tableRow[NORMAL_RANGE_INDEX].ToString());
                        double[] invalidRange = _sensorParser.ParseRequirement(tableRow[INVALID_RANGE_INDEX].ToString());
                        ReadOnlySpan<char> additionalRequirement = _sensorParser.ParseAdditional(tableRow[ADDITIONAL_INDEX].ToString());
                        sensorInCurRow = _sensorUtils.BuildSensor(telemetryParamName.ToString(),
                                                            validRange,
                                                            normalRange,
                                                            invalidRange,
                                                            additionalRequirement.ToString());
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine($"Invalid format at row {rowIndex} at table number {tableIndex}. This row could be a header");
                    }
                    if (sensorInCurRow != null)
                    {
                        yield return sensorInCurRow;
                    }
                }
            }
        }


       
    }
}
