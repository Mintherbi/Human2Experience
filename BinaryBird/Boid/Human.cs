using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BinaryBird.Data;
using BinaryBird.Field.ForceProperty;
using BinaryBird.Field;

namespace BinaryBird.Boid
{
    public class Human : IBoid
    {
        public Point3d Location { get; set; }
        public Vector3d Velocity { get; set; }
        private List<IBoidProperty> Rule;
        private WalkData WalkBehavior;
        private List<IForce> Force;
        private double delta;
        private int max_speed = 5;
        private int min_speed = 1;

        public Human(Point3d Location, Vector3d Velocity, WalkData WalkBehavior, List<IForce> Force, double delta)
        {
            this.Location = Location;
            this.Velocity = Velocity;

            IBoidProperty alignRule = new Align();
            IBoidProperty cohesionRule = new Cohesion();
            IBoidProperty separationRule = new Seperation();
            List<IBoidProperty> rules = new List<IBoidProperty> { alignRule, cohesionRule, separationRule };
            this.Rule = rules;

            this.WalkBehavior = WalkBehavior;
            this.Force = Force;
            this.delta = delta;
        }

        public void Update(List<IBoid> Boid)
        {
            Vector3d Sum = new Vector3d(0, 0, 0);

            // 각 규칙에 따른 힘 계산
            foreach (var rule in Rule)
            {
                Vector3d force = rule.CalcForce(this, Boid, WalkBehavior);
                Sum += force;
            }

            // 속도 업데이트
            this.Velocity += Sum * this.delta;

            // 위치 업데이트

        }
        public void ForceUpdate(List<IForce> Forces)
        {
            Vector3d Sum = new Vector3d(0, 0, 0);

            for (int a = 0; a < Force.Count; a++)
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

            if (this.Velocity.Length < this.min_speed)
            {
                this.Velocity.Unitize();
                this.Velocity = this.Velocity * this.min_speed;
            }
        }
        public void CheckSlope()
        {

        }
        public void CheckEnergy()
        {

        }
        public void Move()
        {
            this.Location += this.Velocity * this.delta;
        }
    }
}
