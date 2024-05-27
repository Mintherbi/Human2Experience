using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Geometry;
using BinaryBird.Data;
using BinaryBird.Boid;

namespace BinaryBird.Field.BoidProperty
{
    public interface IBoidProperty
    {
        Vector3d CalcForce(IBoid self, List<IBoid> boid, IBoidData BoidData);
    }
}
