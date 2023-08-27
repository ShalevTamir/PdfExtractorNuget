using PdfExtractor.Models.Enums;

namespace PdfExtractor.Models.Requirement
{
    public class RequirementModel
    {
        public RequirementType Type;
        public RequirementParam RequirementParam;

        internal RequirementModel(RequirementType type, RequirementParam requirementParam)
        {
            Type = type;
            RequirementParam = requirementParam;
        }

       
    }
}
