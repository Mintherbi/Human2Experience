using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Geometry;

namespace BinaryBird.Data
{
    public interface IForce
    {
        Point3d Target { get; set; }
        double Force { get; set; }
        int Threshold { get; set; }


    }
}
