using PdfExtractor.Extensions;
using PdfExtractorNuget.Services.PdfLoaders;
using System;

namespace PdfExtractor.Services.Sensor
{
    internal class SensorParamsFilter
    {
        private static SensorParamsFilter _instance;

        private readonly char[] CHARS_TO_FILTER = { '\n', ' ' };
        internal static SensorParamsFilter Instance
        {
            get => _instance ??= new SensorParamsFilter();
            set => _instance = value;
        }
        private SensorParamsFilter() { }

        private ReadOnlySpan<char> FilterText(ReadOnlySpan<char> textToFilter)
        {
            return textToFilter.Trim(new ReadOnlySpan<char>(CHARS_TO_FILTER))
                                .Replace('\n', ' ')
                                .RemoveDuplicateChar(' ');
        }

        internal ReadOnlySpan<char> FilterAdditional(ReadOnlySpan<char> additionalText)
        {
            return FilterText(additionalText);
        } 

        internal ReadOnlySpan<char> FilterRequirement(ReadOnlySpan<char> requirementText)
        {
            return requirementText.Filter(CHARS_TO_FILTER);
        }

        internal ReadOnlySpan<char> FilterParameterName(ReadOnlySpan<char> parameterName) 
        {
            return FilterText(parameterName).Remove(' ');
        }

    }
}
