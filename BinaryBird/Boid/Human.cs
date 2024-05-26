using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BinaryBird.Data;
using BinaryBird.Field.ForceProperty;
using BinaryBird.Field.BoidProperty;
using BinaryBird.Field.Force;

namespace BinaryBird.Boid
{
    public class Human : IBoid
    {
        public Point3d Location { get; set; }
        public Vector3d Velocity { get; set; }
        private List<IBoidProperty> Rule { get; set; }
        private WalkData WalkBehavior { get; set; }
        private List<IForce> Force { get; set; }
        private double delta { get; set; }
        private double max_slope { get; set; }
        private double duration;
        private int max_speed = 5;
        private int min_speed = 1;
        private double rpe;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Location"></param>
        /// <param name="Velocity"></param>
        /// <param name="WalkBehavior"></param>
        /// <param name="Force"></param>
        /// <param name="delta"></param>
        public Human(Point3d Location, Vector3d Velocity, WalkData WalkBehavior, List<IForce> Force, double delta)
        {
            ///public 
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

            ///private
            this.duration = 0;
        }

        #region ///Method
        public void BehaviorUpdate(List<IBoid> Boid)
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
        /// <summary>
        /// Calculate the effect of Force
        /// </summary>
        /// <param name="Forces"></param>
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
        /// <summary>
        /// Check if the Speed is faster or slower than setting
        /// </summary>
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
        /// <summary>
        /// Check if the Slope is steeper than max_slope that human can handle
        /// </summary>
        public void CheckSlope()
        {
            if(this._CalcSlope() > this.max_slope)
            {
                this.Velocity = new Vector3d(this.Velocity.X, this.Velocity.Y,
                    Math.Sqrt(Math.Pow(this.Velocity.X, 2) + Math.Pow(this.Velocity.Y, 2)) * this.max_slope);
            }
        }
        public void CheckExertion()
        {
            _UpdateRPE();

        }
        public void Move()
        {
            this.Location += this.Velocity * this.delta;
        }
        #endregion

        #region ///Calculate Fatigue 
        private double _CalculateEnergyConsumption()
        {
            double G = this._CalcSlope();
            double V = this.Velocity.Length;

            double energyConsumption = (155.4 * Math.Pow(G, 5)
                                       - 30.4 * Math.Pow(G, 4)
                                       - 43.3 * Math.Pow(G, 3)
                                       + 46.3 * Math.Pow(G, 2)
                                       + 19.5 * G
                                       + 3.6) * V;
            return energyConsumption;
        }
        private double _ConvertEnergyToMET(double energyConsumption)
        {
            // Convert energy consumption from Watt/kg to MET
            double met = (energyConsumption * 3600) / 4184;
            return met;
        }
        private double _CalculateRPE(double met)
        {
            // METs를 RPE로 변환하는 공식 (수정된 Borg 척도)
            double rpe = (met * this.duration) / 60.0; // 단위 시간당 피로도 증가
            return rpe;
        }
        private void _UpdateRPE()
        {
            double EnergyConsumption = _CalculateEnergyConsumption();
            double MET = _ConvertEnergyToMET(EnergyConsumption);
            double RPE = _CalculateRPE(MET);
            this.rpe = RPE;
        }

        private double _CalcSlope()
        {
            double Slope = new double();
            Slope = this.Velocity.Z / Math.Sqrt(Math.Pow(this.Velocity.X, 2) + Math.Pow(this.Velocity.Y, 2));
            return Slope;
        }
        #endregion
    }
}
