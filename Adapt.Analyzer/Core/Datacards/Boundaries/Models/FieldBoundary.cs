namespace Adapt.Analyzer.Core.Datacards.Boundaries.Models
{
    public class FieldBoundary
    {
        public Boundary[] Boundaries { get; }
        public string Description { get; }

        public FieldBoundary(string description, Boundary[] boundaries)
        {
            Boundaries = boundaries;
            Description = description;
        }
    }
}
