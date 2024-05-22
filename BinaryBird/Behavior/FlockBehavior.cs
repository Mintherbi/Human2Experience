using System;
using System.Collections.Generic;
using Grasshopper.Kernel;
using Rhino.Geometry;

using BinaryBird.Data;

namespace BinaryBird.Behavior
{
    public class FlockBehavior : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the MyComponent1 class.
        /// </summary>
        public FlockBehavior()
          : base("Flock Behavior", "FB",
              "Set how the birds will fly",
              "BinaryNature", "BinaryBird")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_InputParamManager pManager)
        {
            pManager.AddNumberParameter("Seperate Coefficient", "SC", "I don't want to fly along with others", GH_ParamAccess.item);
            pManager.AddNumberParameter("Cohesion Coefficient", "CC", "Lets go together!", GH_ParamAccess.item);
            pManager.AddNumberParameter("Alignment Coefficient", "AC", "Are we heading to same way?", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Flock Behavior", "FB", "How the Birds will fly", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            #region ///Set Parameter
            double f_seperate = new double();
            double f_cohesion = new double();
            double f_align = new double();

            if (!DA.GetData(0, ref f_seperate)) { return; }
            if (!DA.GetData(1, ref f_cohesion)) { return; }
            if (!DA.GetData(2, ref f_align)) { return; }
            #endregion

            FlockData FB = new FlockData(f_seperate, f_cohesion, f_align);

            DA.SetData(0, FB);
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
            get { return new Guid("5EF8CB3C-5C2D-4622-B4B7-7DD39CFB8E40"); }
        }
    }
}