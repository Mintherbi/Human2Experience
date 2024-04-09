using Rhino.Geometry;

namespace BinaryBird.Data
{
    public struct ForceData
    {
        public List<Point3d> Target { get; set; }
        public List<double> Threshold { get; set; }
        public double f_attract { get; set; }
        public double fMax_attract { get; set; }
        public double f_repel { get; set; }
        public double fMax_repel { get; set; }
        public ForceData(List<Point3d> Target, List<double> Threshold, double f_attract = 0.1, double fMax_attract = 10, double f_repel=0.1, double fMax_repel = 10)
        {

        }

    }
}
