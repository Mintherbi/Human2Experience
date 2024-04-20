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
        private int max_speed=5;
        private int min_speed = 1;

        public Bird(Point3d Location, Vector3d Velocity, BoidData BoidData, List<IForce> Force, double delta)
        {
            this.Location = Location;
            this.Velocity = Velocity;

            IBoid alignRule = new Align();
            IBoid cohesionRule = new Cohesion();
            IBoid separationRule = new Seperation();
            List<IBoid> rules = new List<IBoid> { alignRule, cohesionRule, separationRule };
            this.Rule = rules;

            this.BoidData = BoidData;
            this.Force = Force;
            this.delta = delta;
        }

        public void Update(List<Bird> Boid)
        {
            Vector3d Sum = new Vector3d(0, 0, 0);

            // 각 규칙에 따른 힘 계산
            foreach (var rule in Rule)
            {
                Vector3d force = rule.CalcForce(this, Boid, BoidData);
                Sum += force;
            }

            // 속도 업데이트
            this.Velocity += Sum * this.delta;

            // 위치 업데이트
            
        }
        public void ForceUpdate(List<IForce> Forces)
        {
            Vector3d Sum = new Vector3d(0, 0, 0);

            for(int a = 0; a < Force.Count; a++)
            {
                double dist = Force[a].Target.DistanceTo(this.Location);
                if (dist > Forces[a].Threshold)
                {
                    Sum += (Forces[a].Force / dist) * (Forces[a].Target - this.Location);
                }
            }

            this.Velocity += Sum * this.delta;
        }
        public void CheckSpeed()
        {
            if (this.Velocity.Length > this.max_speed)
            {
                this.Velocity.Unitize();
                this.Velocity = this.Velocity * this.max_speed;
            }

            if(this.Velocity.Length < this.min_speed)
            {
                this.Velocity.Unitize();
                this.Velocity = this.Velocity * this.min_speed;
            }
        }
        public void Move()
        {
            this.Location += this.Velocity * this.delta;
        }
    }
}
