using System;
using System.Collections.Generic;
using Grasshopper.Kernel;
using Rhino.Geometry;

using BinaryBird.Data;

namespace BinaryBird.Behavior
{
    public class MergeDynamicForce : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the MyComponent1 class.
        /// </summary>
        public MergeDynamicForce()
          : base("MergeDynamicForce", "MDF",
              "Merge force Dynamically",
              "BinaryNature", "BinaryBird")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Force", "F", "Dynamic List of Force", GH_ParamAccess.list);
            pManager[0].DataMapping = GH_DataMapping.Flatten;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("MergedForce", "MF", "Merged Force List", GH_ParamAccess.list);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            List<IForce> Forces = new List<IForce>();
            List<IForce> temp = new List<IForce>();

            while(DA.GetDataList(0,temp))
            {
                Forces.AddRange(temp);
                temp.Clear();
            }

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
            get { return new Guid("13E0AD03-BFE2-44AF-9353-B44317830631"); }
        }
    }
}