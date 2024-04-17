using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Rhino.Geometry;
using BinaryBird.Data;

namespace BinaryBird.Boid
{
    public class Bird
    {
        public Point3d Location;
        public Vector3d Velocity;
        private List<IBoid> Rule;
        private BoidData BoidData;
        private List<IForce> Force;

        public Bird(Point3d Location, Vector3d Velocity, List<IBoid> Rule, BoidData BoidData, List<IForce> Force)
        {
            this.Location = Location;
            this.Velocity = Velocity;
            this.Rule = Rule;
            this.BoidData = BoidData;
            this.Force = Force;
        }

        public void Update(List<Bird> Boid)
        {

        }
    }
}
