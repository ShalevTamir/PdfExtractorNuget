using PdfExtractor.Extensions;
using System;

namespace PdfExtractor.Services.Sensor
{
    internal class SensorParamsFilter
    {
        private static SensorParamsFilter _instance;
        internal static SensorParamsFilter Instance
        {
            get => _instance ??= new SensorParamsFilter();
            set => _instance = value;
        }
        private SensorParamsFilter() { }

        
        internal ReadOnlySpan<char> FilterAdditional(ReadOnlySpan<char> additionalText)
        {
            return PdfTableLoader.FilterText(additionalText);
        } 

        internal ReadOnlySpan<char> FilterRequirement(ReadOnlySpan<char> requirementText)
        {
            return requirementText.Filter(PdfTableLoader.CHARS_TO_FILTER);
        }

        internal ReadOnlySpan<char> FilterParameterName(ReadOnlySpan<char> parameterName) 
        {
            return PdfTableLoader.FilterText(parameterName).Remove(' ');
        }

    }
}
