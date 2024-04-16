using Rhino.Geometry;

namespace BinaryBird.Data
{
    public struct AttractForceData : IForce
    {
        public Point3d Target { get; set; }
        public double Force { get; set; }
        public int Threshold { get; set; }
        public bool LinearRepel { get; set; }
        private double MaxForce;
        public AttractForceData(Point3d Target, double Force, int Threshold, bool LinearRepel)
        {
            this. Target = Target;
            this. Force = Force;
            this. Threshold = Threshold;
            this. LinearRepel = LinearRepel;
            this. MaxForce = Force / (Threshold * Threshold);
        }
    }

    public struct RepelForceData : IForce
    {
        public Point3d Target { get; set; }
        public double Force { get; set; }
        public int Threshold { get; set; }
        public bool LinearRepel { get; set; }
        private double MaxForce;
        public RepelForceData(Point3d Target, double Force, int Threshold, bool LinearRepel)
        {
            this.Target = Target;
            this.Force = (-1) * Force;
            this.Threshold = Threshold;
            this.LinearRepel = LinearRepel;
            this.MaxForce = Force / (Threshold * Threshold);
        }
    }
}
