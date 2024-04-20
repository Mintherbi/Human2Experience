using System;
using System.Collections.Generic;
using Grasshopper.Kernel;
using Rhino.Geometry;

using BinaryBird.Data;
using Grasshopper.Kernel.Types;

namespace BinaryBird.Behavior
{
    public class MergeForce : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the MyComponent1 class.
        /// </summary>
        public MergeForce()
          : base("MergeForce", "MF",
              "Input 3 Force to Merge",
              "BinaryNature", "BinaryBird")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Force1", "F1", "First Force", GH_ParamAccess.item);
            pManager.AddGenericParameter("Force2", "F2", "Second Force", GH_ParamAccess.item);
            pManager.AddGenericParameter("Force3", "F3", "Third Force", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Merged Force", "MF", "Merged Force", GH_ParamAccess.list);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            #region ///Set Param
            GH_ObjectWrapper Force1=null;
            GH_ObjectWrapper Force2 = null;
            GH_ObjectWrapper Force3 = null;

            if (!DA.GetData(0, ref Force1)) { return; }
            if (!DA.GetData(0, ref Force2)) { return; }
            if (!DA.GetData(0, ref Force3)) { return; }
            #endregion

            List<IForce> Forces = new List<IForce>();

            Forces.Add(Force1.Value as IForce);
            Forces.Add(Force2.Value as IForce);
            Forces.Add(Force3.Value as IForce);

            DA.SetDataList(0, Forces);
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
            get { return new Guid("C7D69656-3997-4986-90EA-0CC7CB54783C"); }
        }
    }
}