namespace PdfExtractor.Models.Requirement
{
    public class RequirementRange : RequirementParam
    {
        public double EndValue { get; set; }

        internal RequirementRange(double start, double end) : base(start)
        {
            EndValue = end;
        }

    }
}
