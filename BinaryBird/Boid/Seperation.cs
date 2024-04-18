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
            List<Bird> Local = _getlocal(Bird, Boid, 4);

            Seperation = _getaway(Bird, Local) * BoidData.f_seperate;

            return Seperation;
        }

        private List<Bird> _getlocal(Bird Bird, List<Bird> Boid, int n)
        {
            return Boid
                .Where(b => !b.Equals(Bird)) // 현재의 bird를 리스트에서 제외
                .OrderBy(b => _dist(Bird, b)) // 현재 bird로부터의 거리에 따라 정렬
                .Take(n) // 가장 가까운 n개의 bird 선택
                .ToList(); // 결과를 List<Bird>로 반환
        }

        private double _dist(Bird bird1, Bird bird2)
        {
            double dist = new double();

            dist = bird1.Location.DistanceTo(bird2.Location);

            return dist;
        }

        private Vector3d _getaway(Bird Bird, List<Bird> Local)
        {
            Vector3d Sum = new Vector3d(0, 0, 0);
            for (int a=0; a<Local.Count(); a++)
            {
                double dist = _dist(Bird, Local[a]);
                Sum += (Bird.Location - Local[a].Location) * (1/(dist*dist));
            }

            return Sum;
        }
    }
}
