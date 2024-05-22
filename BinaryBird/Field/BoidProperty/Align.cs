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
        public Vector3d CalcForce(Bird Bird, List<Bird> Boid, FlockData BoidData)
        {
            Vector3d Align = new Vector3d();

            Vector3d Sum = new Vector3d(0, 0, 0);

            for (int a = 0; a < Boid.Count(); a++) { Sum += Boid[a].Velocity; }

            Align = BoidData.f_align * Sum / Boid.Count();

            return Align;
        }
    }
}
