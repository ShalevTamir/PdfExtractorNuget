using PdfExtractor.Models.Enums;

namespace PdfExtractor.Models.Requirement
{
    public class RequirementModel
    {
        public RequirementType Type;
        public RequirementParam RequirementParam;

        public RequirementModel(RequirementType type, RequirementParam requirementParam)
        {
            Type = type;
            RequirementParam = requirementParam;
        }

       
    }
}
