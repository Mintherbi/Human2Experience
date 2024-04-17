using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Rhino.Geometry;
using BinaryBird.Data;

namespace BinaryBird.Boid
{
    public class Seperation : IBoid
    {
        public Vector3d CalcForce(Bird Bird, List<Bird> Boid, BoidData BoidData)
        {
            Vector3d Seperation = new Vector3d();

            return Seperation;
        }

        private List<Point3d> _getlocal(Bird Bird, List<Bird> Boid)
        {
            List<Point3d> localbird = new List<Point3d>();

            var distance = Boid.Select((Boid, index) => new{ Index = index, Distance = _dist})


            return localbird;
        }

        private double _dist(Bird bird1, Bird bird2)
        {
            double dist = new double();

            dist = bird1.Location.DistanceTo(bird2.Location);

            return dist;
        }
    }
}
