using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryBird.Boid
{
    public interface IBoid
    {
        Point3d Location { get; set; }
        Vector3d Velocity { get; set; }
    }
}
