using PdfExtractor.Extensions;
using PdfExtractor.Models.Requirement;
using PdfExtractor.Models;

namespace PdfExtractor.Services.Sensor
{
    internal class SensorFactory
    {
        private static SensorFactory _instance;
        internal static SensorFactory Instance
        {
            get => _instance ??= new SensorFactory();
            private set => _instance = value;
        }
        private SensorFactory() {}
        internal SensorProperties BuildSensor(string telemetryParamName, double[] validRange, double[] normalRange, double[] invalidRange, string additionalRequirement)
        {
            var ranges = new double[][] { validRange, normalRange, invalidRange };
            RequirementParam[] requirementParams = new RequirementParam[ranges.Length];
            for (int i = 0; i < ranges.Length; i++)
            {
                if (ranges[i].Length == 1)
                    requirementParams[i] = new RequirementParam(ranges[i][0]);
                else if (ranges[i].Length == 2)
                    requirementParams[i] = new RequirementRange(ranges[i][0],
                                                                ranges[i][1]);
            }
            return new SensorProperties(telemetryParamName,
                              requirementParams[0],
                              requirementParams[1],
                              requirementParams[2],
                              additionalRequirement);
        }

       
    }
}
