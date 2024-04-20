using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Rhino.Geometry;
using BinaryBird.Data;
using System.Security.Cryptography;

namespace BinaryBird.Boid
{
    public class Bird
    {
        public Point3d Location;
        public Vector3d Velocity;
        private List<IBoid> Rule;
        private BoidData BoidData;
        private List<IForce> Force;
        private double delta;

        public Bird(Point3d Location, Vector3d Velocity, List<IBoid> Rule, BoidData BoidData, List<IForce> Force, double delta)
        {
            this.Location = Location;
            this.Velocity = Velocity;
            this.Rule = Rule;
            this.BoidData = BoidData;
            this.Force = Force;
            this.delta = delta;
        }

        public void Update(List<Bird> Boid)
        {
            Vector3d totalForce = new Vector3d(0, 0, 0);

            // 각 규칙에 따른 힘 계산
            foreach (var rule in Rule)
            {
                Vector3d force = rule.CalcForce(this, Boid, BoidData);
                totalForce += force;
            }

            // 속도 업데이트
            this.Velocity += totalForce * this.delta;

            // 위치 업데이트
            this.Location += this.Velocity * this.delta;
        }
    }
}
