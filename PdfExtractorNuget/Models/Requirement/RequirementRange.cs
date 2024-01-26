namespace PdfExtractor.Models.Requirement
{
    public class RequirementRange : RequirementParam
    {
        public double EndValue { get; set; }

        public RequirementRange(double start, double end) : base(start)
        {
            EndValue = end;
        }

    }
}
