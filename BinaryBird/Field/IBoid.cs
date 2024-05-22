using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Geometry;
using BinaryBird.Data;
using BinaryBird.Boid;

namespace BinaryBird.Field
{
    public interface IBoidProperty
    {
        Vector3d CalcForce(Bird Bird, List<Bird> Boid, FlockData BoidData);
    }
}
