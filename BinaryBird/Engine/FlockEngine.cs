using Grasshopper;
using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

using BinaryBird.Data;
using BinaryBird.Behavior;
using BinaryBird.Boid;

namespace BinaryBird.Engine
{
    public class FlockEngine : GH_Component
    {
        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public FlockEngine()
          : base("BinaryBird", "BB",
            "Description",
            "BinaryNature", "BinaryBird")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddPointParameter("Birds", "B", "Bird Seeking Freedom", GH_ParamAccess.list);
            pManager.AddGenericParameter("Force", "F", "Birds are seeking freedom but captured by unknown", GH_ParamAccess.item);
            pManager.AddGenericParameter("Behavior", "BH", "How the birds will fly?", GH_ParamAccess.item);
            pManager.AddNumberParameter("Delta", "dt", "Time Step", GH_ParamAccess.item);
            pManager.AddBooleanParameter("reset", "R", "You don`t like the move they make?", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddPointParameter("Trace", "T", "The history of flock", GH_ParamAccess.tree);
        }



        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            #region ///Param set
            List<Point3d> pt_bird = new List<Point3d>();
            List<IForce> Forces = new List<IForce>();
            BoidData Behavior = new BoidData();
            double dt = new double();
            bool reset = new bool();

            if(!DA.GetDataList(0, pt_bird)) { return; }
            if(!DA.GetDataList(1, Forces)) { return; }
            if(!DA.GetData(2, ref Behavior)) { return; }
            if(!DA.GetData(3, ref dt)) { return; }
            if(!DA.GetData(4, ref reset)) { return; }
            #endregion
            
            if (!reset)
            {
                for (int a = 0; a < pt_bird.Count; a++)
                {

                }
            }
        }

        /// <summary>
        /// Provides an Icon for every component that will be visible in the User Interface.
        /// Icons need to be 24x24 pixels.
        /// You can add image files to your project resources and access them like this:
        /// return Resources.IconForThisComponent;
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
        /// Each component must have a unique Guid to identify it. 
        /// It is vital this Guid doesn't change otherwise old ghx files 
        /// that use the old ID will partially fail during loading.
        /// </summary>
        public override Guid ComponentGuid => new Guid("c6a8cccc-4fa2-4127-98f9-ecfd8a9d242f");
    }
}