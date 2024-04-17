using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Rhino.Geometry;
using BinaryBird.Data;

namespace BinaryBird.Boid
{
    public class Cohesion : IBoid
    {
        public Vector3d CalcForce(Bird Bird, List<Bird> Boid, BoidData BoidData)
        {
            Vector3d Cohesion = new Vector3d();

            Point3d Sum = new Point3d(0, 0, 0);

            for (int a = 0; a < Boid.Count(); a++) { Sum += Boid[a].Location; }

            Cohesion = ((Sum / Boid.Count()) - Bird.Location) * BoidData.f_cohesion;

            return Cohesion;
        }
    }
}
