using Rhino.Geometry;

namespace BinaryBird.Data
{
    public struct WalkData : IBoidData
    {
        public double f_seperate { get; set; }
        public double f_cohesion { get; set; }
        public double f_align { get; set; }
        public int energy { get; set; }
        public double max_slope { get; set; }

        public WalkData(double f_seperate = 0.1, double f_cohesion = 0.1, double f_align = 0.1, int energy = 100, double max_slope = 0.05)
        {
            this.f_seperate = f_seperate;
            this.f_cohesion = f_cohesion;
            this.f_align = f_align;
            this.energy = energy;
            this.max_slope = max_slope;
        }
    }
}
