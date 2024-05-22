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
    public class Seperation : IBoidProperty
    {
        public Vector3d CalcForce(IBoid self, List<IBoid> boid, IBoidData BoidData)
        {
            Vector3d Seperation = new Vector3d();
            List<IBoid> Local = _getlocal(self, boid, 4);

            Seperation = _getaway(self, Local) * BoidData.f_seperate;

            return Seperation;
        }

        private List<IBoid> _getlocal(IBoid self, List<IBoid> boid, int n)
        {
            return boid
                .Where(b => !b.Equals(self)) // 현재의 bird를 리스트에서 제외
                .OrderBy(b => _dist(self, b)) // 현재 bird로부터의 거리에 따라 정렬
                .Take(n) // 가장 가까운 n개의 bird 선택
                .ToList(); // 결과를 List<Bird>로 반환
        }

        private double _dist(IBoid self, IBoid other)
        {
            double dist = new double();

            dist = self.Location.DistanceTo(other.Location);

            return dist;
        }

        private Vector3d _getaway(IBoid self, List<IBoid> Local)
        {
            Vector3d Sum = new Vector3d(0, 0, 0);
            for (int a = 0; a < Local.Count(); a++)
            {
                double dist = _dist(self, Local[a]);
                Sum += (self.Location - Local[a].Location) * (1 / (dist * dist));
            }

            return Sum;
        }
    }
}
