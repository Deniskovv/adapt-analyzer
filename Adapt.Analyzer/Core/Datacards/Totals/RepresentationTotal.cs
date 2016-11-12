namespace Adapt.Analyzer.Core.Datacards.Totals
{
    public class RepresentationTotal
    {
        public string Representation { get; }
        public string UnitOfMeasure { get; }
        public double Total { get; }

        public RepresentationTotal(string representation, string unitOfMeasure, double total)
        {
            Representation = representation;
            UnitOfMeasure = unitOfMeasure;
            Total = total;
        }
    }
}
