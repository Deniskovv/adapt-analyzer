namespace Adapt.Analyzer.Core.Datacards.Boundaries.Models
{
    public class Boundary
    {
        public Ring[] Exteriors { get; }
        public Ring[] Interiors { get; }

        public Boundary(Ring[] exteriors, Ring[] interiors)
        {
            Exteriors = exteriors;
            Interiors = interiors;
        }
    }
}