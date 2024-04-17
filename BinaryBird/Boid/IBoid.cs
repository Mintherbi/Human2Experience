using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Geometry;
using BinaryBird.Data;

namespace BinaryBird.Boid
{
    public interface IBoid
    {
        Vector3d CalcForce(Bird Bird, List<Bird> Boid, BoidData BoidData);
    }
}
