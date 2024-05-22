using Rhino.Geometry;

namespace BinaryBird.Data
{
    public struct FlockData : IBoidData
    {
        public double f_seperate {  get; set; }
        public double f_cohesion { get; set; }
        public double f_align { get; set; }

        public FlockData (double f_seperate=0.1, double f_cohesion=0.1, double f_align = 0.1)
        {
            this.f_seperate = f_seperate;
            this.f_cohesion = f_cohesion;
            this.f_align = f_align;
        }
    }
}
