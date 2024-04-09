using Rhino.Geometry;

namespace BinaryBird.Data
{
    public struct FlockData
    {
        public double f_seperate {  get; set; }
        public double f_avoid { get; set; }
        public double f_align { get; set; }

        public FlockData (double f_seperate=0.1, double f_avoid=0.1, double f_align = 0.1)
        {
            this.f_seperate = f_seperate;
            this.f_avoid = f_avoid;
            this.f_align = f_align;
        }
    }
}
