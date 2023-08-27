using System;

namespace PdfExtractor.Services.Sensor
{
    internal class SensorParamsParser
    {
        private static SensorParamsParser _instance;
        internal static SensorParamsParser Instance
        {
            get => _instance ??= new SensorParamsParser();
            set => _instance = value;
        }
        private SensorParamsParser() 
        {
            _sensorFilter = SensorParamsFilter.Instance;
            _rangeHandler = RangeHandler.Instance;
        }
        private SensorParamsFilter _sensorFilter;
        private RangeHandler _rangeHandler;
        internal ReadOnlySpan<char> ParseAdditional(ReadOnlySpan<char> additionalRequirement)
        {
            return _sensorFilter.FilterAdditional(additionalRequirement);
        }
     
        internal double[] ParseRequirement(ReadOnlySpan<char> requirement)
        {
            ReadOnlySpan<char> cleanText = _sensorFilter.FilterRequirement(requirement);
            return _rangeHandler.TurnToRange(cleanText);
        }

        internal ReadOnlySpan<char> ParseParameterName(ReadOnlySpan<char> parameterName)
        {
            return _sensorFilter.FilterParameterName(parameterName);
        }
    }
}
