using System;
using System.Collections.Generic;
using Grasshopper.Kernel;
using Rhino.Geometry;

using BinaryBird.Data;

namespace BinaryBird.Behavior
{
    public class Force : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the MyComponent1 class.
        /// </summary>
        public Force()
          : base("Force", "F",
              "Set Force Strength and Threshold Distance of Force",
              "BinaryNature", "BinaryBird")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_InputParamManager pManager)
        {
            pManager.AddPointParameter("Target", "T", "Center of Force", GH_ParamAccess.item);
            pManager.AddNumberParameter("Strength", "S", "Strength of Force", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Threshold", "Th", "Impact Diameter of Force", GH_ParamAccess.item);
            pManager.AddBooleanParameter("Direction", "D", "True : Attract | False : Repel", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("TargetProperty", "TP", "Target Property", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            #region ///Set Param
            Point3d Target = new Point3d();
            double Strength = new double();
            int Threshold = new int();
            bool Direction = new bool();

            if (!DA.GetData(0, ref Target)) { return; }
            if (!DA.GetData(1, ref Strength)) { return; }
            if (!DA.GetData(2, ref Threshold)) { return; }
            if (DA.GetData(3, ref Direction)) { return; }
            #endregion

            if (Direction) { AttractForceData TargetProperty = new AttractForceData(Target, Strength, Threshold);  }
            else { RepelForceData TargetProperty = new RepelForceData(Target, Strength, Threshold); }
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        /*
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                //You can add image files to your project resources and access them like this:
                // return Resources.IconForThisComponent;
                return null;
            }
        }
        */

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("D7121D1F-7974-47E4-B5C3-90136B599C66"); }
        }
    }
}