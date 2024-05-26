using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Rhino.Geometry;
using BinaryBird.Data;
using BinaryBird.Boid;
using BinaryBird.Field.BoidProperty;

namespace BinaryBird.Field.ForceProperty
{
    public class Cohesion : IBoidProperty
    {
        public Vector3d CalcForce(IBoid self, List<IBoid> boid, IBoidData BoidData)
        {
            Vector3d Cohesion = new Vector3d();

            Point3d Sum = new Point3d(0, 0, 0);

            for (int a = 0; a < boid.Count(); a++) { Sum += boid[a].Location; }

            Cohesion = (Sum / boid.Count() - self.Location) * BoidData.f_cohesion;

            return Cohesion;
        }
    }
}
