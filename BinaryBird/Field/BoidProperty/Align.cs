using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Rhino.Geometry;
using BinaryBird.Data;
using BinaryBird.Boid;

namespace BinaryBird.Field.ForceProperty
{
    public class Align : IBoidProperty
    {
        public Vector3d CalcForce(IBoid self, List<IBoid> boid, IBoidData BoidData)
        {
            Vector3d Align = new Vector3d();

            Vector3d Sum = new Vector3d(0, 0, 0);

            for (int a = 0; a < boid.Count(); a++) { Sum += boid[a].Velocity; }

            Align = BoidData.f_align * Sum / boid.Count();

            return Align;
        }
    }
}
