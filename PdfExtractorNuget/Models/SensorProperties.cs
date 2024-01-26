using PdfExtractor.Models.Enums;
using PdfExtractor.Models.Requirement;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
namespace PdfExtractor.Models
{
    public class SensorProperties
    {
        public string TelemetryParamName;
        public IEnumerable<RequirementModel> Requirements;
        public string AdditionalRequirement;

        public SensorProperties(string telemetryParamName, RequirementParam validRange, RequirementParam normalRange, RequirementParam invalidRange, string additionalRequirement)
        {
            TelemetryParamName = telemetryParamName;
            Requirements = new RequirementModel[] {new RequirementModel(RequirementType.VALID,validRange),
                                                    new RequirementModel(RequirementType.NORMAL,normalRange),
                                                    new RequirementModel(RequirementType.INVALID,invalidRange)};
            AdditionalRequirement = additionalRequirement;
        }

        public SensorProperties(string telemetryParamName, string additionalRequirement)
        {
            TelemetryParamName = telemetryParamName;
            Requirements = Enumerable.Empty<RequirementModel>();
            AdditionalRequirement = additionalRequirement;
        }   
    }
}
