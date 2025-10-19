using Grasshopper.Kernel;
using Grasshopper.Kernel.Data;
using Grasshopper.Kernel.Types;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

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
            pManager.AddIntegerParameter("HorizontalViewRes", "HVR", "View angle in degrees", GH_ParamAccess.item, 12);
            pManager.AddIntegerParameter("VerticalViewRes", "VVR", "Vertical view angle in degrees", GH_ParamAccess.item, 15);
            pManager.AddBrepParameter("Geometry", "G", "Geometry to intersect with rays", GH_ParamAccess.list);
        }

        protected override void RegisterOutputParams(GH_OutputParamManager pManager)
        {
            pManager.AddNumberParameter("AverageDepth", "AD", "Average view depth along the path", GH_ParamAccess.tree);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Curve pathCurve = null;
            double maxRayLength = 100;
            int horizontalViewRes = 12;
            int verticalViewRes = 15;
            List<Brep> geometries = new List<Brep>();

            if (!DA.GetData(0, ref pathCurve)) return;
            if (!DA.GetData(1, ref maxRayLength)) return;
            if (!DA.GetData(2, ref horizontalViewRes)) return;
            if (!DA.GetData(3, ref verticalViewRes)) return;
            if (!DA.GetDataList(4, geometries)) return;

            Polyline polylinePath;
            if (!pathCurve.TryGetPolyline(out polylinePath))
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "Path is not a valid polyline");
                return;
            }

            GH_Structure<GH_Number> viewDepths = CalculateViewDepths(polylinePath, horizontalViewRes, verticalViewRes, maxRayLength, geometries);

            DA.SetDataTree(0, viewDepths);
        }

        private GH_Structure<GH_Number> CalculateViewDepths(Polyline path, int horizontalViewRes, int verticalViewRes, double maxRayLength, List<Brep> geometries)
        {
            GH_Structure<GH_Number> allPointDepthsTree = new GH_Structure<GH_Number>();
            int branchId = 0;

            int pointCount = path.Count;

            for (int p = 0; p < pointCount; p++)
            {
                Point3d point = path[p];

                Vector3d tangent;
                if (p < pointCount - 1)
                {
                    tangent = path[p + 1] - point;
                }
                else
                {
                    if (path.IsClosed)
                    {
                        tangent = path[0] - point;
                    }
                    else
                    {
                        tangent = point - path[p - 1];
                    }
                }
                tangent.Unitize();

                Vector3d initialForwardDirection = Vector3d.XAxis;

                Transform rotationXform = Transform.Rotation(initialForwardDirection, tangent, Point3d.Origin);

                double horizontalViewAngle = 120 * (Math.PI / 180.0);
                double verticalViewAngle = 150 * (Math.PI / 180.0);
                double halfHorizontalViewAngle = horizontalViewAngle / 2.0;
                double halfVerticalViewAngle = verticalViewAngle / 2.0;

                int numHorizontalRays = horizontalViewRes;
                int numVerticalRays = verticalViewRes;

                double horizontalAngleIncrement = (numHorizontalRays > 1) ? horizontalViewAngle / (numHorizontalRays - 1) : 0;
                double verticalAngleIncrement = (numVerticalRays > 1) ? verticalViewAngle / (numVerticalRays - 1) : 0;

                GH_Path currentBranchPath = new GH_Path(branchId);

                for (int i = 0; i < numHorizontalRays; i++)
                {
                    double hAngle = -halfHorizontalViewAngle + i * horizontalAngleIncrement;

                    for (int j = 0; j < numVerticalRays; j++)
                    {
                        double vAngle = -halfVerticalViewAngle + j * verticalAngleIncrement;

                        double x = Math.Cos(hAngle) * Math.Cos(vAngle);
                        double y = Math.Sin(hAngle) * Math.Cos(vAngle);
                        double z = Math.Sin(vAngle);
                        Vector3d localDirection = new Vector3d(x, y, z);
                        localDirection.Unitize();

                        localDirection.Transform(rotationXform);

                        Ray3d ray = new Ray3d(point, localDirection);
                        double depth = maxRayLength;

                        var intersections = Rhino.Geometry.Intersect.Intersection.RayShoot(ray, geometries, 1);

                        if (intersections != null && intersections.Length > 0)
                        {
                            depth = ray.Position.DistanceTo(intersections[0]);
                        }

                        allPointDepthsTree.Append(new GH_Number(Math.Min(depth, maxRayLength)), currentBranchPath);
                    }
                }
                branchId++;
            }

            return allPointDepthsTree;
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