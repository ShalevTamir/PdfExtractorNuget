using PdfExtractor.Models.Enums;
using PdfExtractor.Models.Requirement;
namespace PdfExtractor.Models
{
    public class SensorProperties
    {
        public string TelemetryParamName;
        public RequirementModel[] Requirements;
        public string AdditionalRequirement;

        internal SensorProperties(string telemetryParamName, RequirementParam validRange, RequirementParam normalRange, RequirementParam invalidRange, string additionalRequirement)
        {
            TelemetryParamName = telemetryParamName;
            Requirements = new RequirementModel[] {new RequirementModel(RequirementType.VALID,validRange),
                                                    new RequirementModel(RequirementType.NORMAL,normalRange),
                                                    new RequirementModel(RequirementType.INVALID,invalidRange)};
            AdditionalRequirement = additionalRequirement;
        }
    }
}
