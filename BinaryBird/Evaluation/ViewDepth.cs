using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace BinaryBird.Evaluation
{
    public class ViewDepthComponent : GH_Component
    {
        public ViewDepthComponent()
          : base("ViewDepth", "VD",
              "Calculates the average view depth along a polyline path",
              "BinaryNature", "BinaryBird")
        {
        }

        protected override void RegisterInputParams(GH_InputParamManager pManager)
        {
            pManager.AddCurveParameter("Path", "P", "Polyline path representing the movement path", GH_ParamAccess.item);
            pManager.AddNumberParameter("MaxRayLength", "MaxL", "Maximum length of rays", GH_ParamAccess.item, 100.0);
            pManager.AddNumberParameter("ViewAngle", "VA", "View angle in degrees", GH_ParamAccess.item, 120.0);
            pManager.AddBrepParameter("Geometry", "G", "Geometry to intersect with rays", GH_ParamAccess.list);
        }

        protected override void RegisterOutputParams(GH_OutputParamManager pManager)
        {
            pManager.AddNumberParameter("AverageDepth", "AD", "Average view depth along the path", GH_ParamAccess.item);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Curve pathCurve = null;
            double maxRayLength = 100.0;
            double viewAngle = 120.0;
            List<Brep> geometries = new List<Brep>();

            if (!DA.GetData(0, ref pathCurve)) return;
            if (!DA.GetData(1, ref maxRayLength)) return;
            if (!DA.GetData(2, ref viewAngle)) return;
            if (!DA.GetDataList(3, geometries)) return;

            Polyline polylinePath;
            if (!pathCurve.TryGetPolyline(out polylinePath))
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "Path is not a valid polyline");
                return;
            }

            List<double> viewDepths = CalculateViewDepths(polylinePath, viewAngle, maxRayLength, geometries);
            double averageDepth = CalculateAverageDepth(viewDepths);

            DA.SetData(0, averageDepth);
        }

        private List<double> CalculateViewDepths(Polyline path, double viewAngle, double maxRayLength, List<Brep> geometries)
        {
            List<double> depths = new List<double>();

            foreach (var point in path)
            {
                double halfViewAngle = viewAngle / 2;
                int numRays = 100;
                double angleIncrement = viewAngle / (numRays - 1);

                for (int i = 0; i < numRays; i++)
                {
                    double angle = -halfViewAngle + i * angleIncrement;
                    Vector3d direction = new Vector3d(Math.Cos(angle), Math.Sin(angle), 0);

                    Ray3d ray = new Ray3d(point, direction);
                    double depth = maxRayLength;

                    var intersections = Rhino.Geometry.Intersect.Intersection.RayShoot(ray, geometries, 1);

                    if (intersections != null && intersections.Length > 0)
                    {
                        depth = ray.Position.DistanceTo(intersections[0]);
                    }

                    depths.Add(Math.Min(depth, maxRayLength));
                }
            }

            return depths;
        }

        private double CalculateAverageDepth(List<double> depths)
        {
            if (depths.Count == 0)
                return 0.0;

            double totalDepth = 0.0;
            foreach (double depth in depths)
            {
                totalDepth += depth;
            }

            return totalDepth / depths.Count;
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("2F68994A-E622-49EA-9D10-AD11FF7E64B4"); }
        }
    }
}