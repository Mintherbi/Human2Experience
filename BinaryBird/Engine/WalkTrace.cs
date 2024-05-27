using Grasshopper;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Data;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;

using BinaryBird.Data;
using BinaryBird.Behavior;
using BinaryBird.Boid;
using BinaryBird.Field.Force;

namespace BinaryBird.Engine
{
    public class WalkTrace : GH_Component
    {
        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public WalkTrace()
          : base("WalkTrace", "WT",
            "The Trace of the Human",
            "BinaryNature", "BinaryBird")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddPointParameter("Birds", "B", "Initial Location of Human", GH_ParamAccess.list);
            pManager.AddGenericParameter("Force", "F", "Control Points of Human Trace", GH_ParamAccess.list);
            pManager.AddGenericParameter("Behavior", "BH", "How the humans will walk", GH_ParamAccess.item);
            pManager.AddNumberParameter("Delta", "R", "Time Step", GH_ParamAccess.item);
            pManager.AddNumberParameter("Goal", "G", "Desired height", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Max Calculation Step", "MxC", "Max Calculation Step", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddPointParameter("Trace", "T", "The history of flock", GH_ParamAccess.tree);
            pManager.AddNumberParameter("Exertion", "E", "The history of Exertion", GH_ParamAccess.tree);
            pManager.AddBooleanParameter("Reach2Goal", "R2G", "Reached to the Goal", GH_ParamAccess.list);
        }


        List<Human> Boid;
        int delta;
        DataTree<Point3d> Trace;
        DataTree<double> Exertion;
        List<IForce> PreviousValue;
        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            #region ///Param set
            List<Point3d> pt_human = new List<Point3d>();
            List<IForce> Forces = new List<IForce>();
            WalkData Behavior = new WalkData();
            double Delta = new double();
            double goal = new double();
            int max_calc = new int();

            if(!DA.GetDataList(0, pt_human)) { return; }
            if(!DA.GetDataList(1, Forces)) { return; }
            if(!DA.GetData(2, ref Behavior)) { return; }
            if(!DA.GetData(3, ref Delta)) { return; }
            if(!DA.GetData(4, ref goal)) { return; }
            if(!DA.GetData(5, ref max_calc)) { return; }
            #endregion

            

            #region ///reset parameter
            if (!(Forces == PreviousValue))
            {
                Trace = new DataTree<Point3d>();
                Exertion = new DataTree<double>();
                Boid = new List<Human>();
                PreviousValue = new List<IForce>();
                PreviousValue = Forces;

                delta = 0;
                for (int a = 0; a < pt_human.Count; a++)
                {
                    GH_Path path = new GH_Path(a);
                    List<Point3d> subtree = new List<Point3d>();
                    subtree.Add(pt_human[a]);

                    Trace.AddRange(subtree, path);

                    Boid.Add(new Human(pt_human[a], new Vector3d(0, 0, 1), Behavior, Forces, Delta));
                }
            }
            #endregion

            List<bool> R2G = new List<bool>(Enumerable.Repeat(false, Boid.Count));

            while (max_calc > 0)
            {
                for (int b = 0; b < Boid.Count; b++)
                {
                    if (!R2G[b]) 
                    { 
                        Boid[b].BehaviorUpdate(Boid.Cast<IBoid>().ToList());
                        Boid[b].ForceUpdate(Forces);
                        Boid[b].CheckSpeed();
                        //Boid[b].CheckSlope();
                        Boid[b].ConstantSlope();
                        Boid[b].CheckExertion();
                        Boid[b].Move();
                        Trace.Add(Boid[b].Location, new GH_Path(b));
                        Exertion.Add(Boid[b].rpe, new GH_Path(b));

                        if (Boid[b].Location.Z > goal) { R2G[b] = true; }
                    }
                }

                if(Boid.All(a => a.Location.Z > goal))
                {
                    break;
                }
                max_calc--;
            }

            DA.SetDataTree(0, Trace);
            DA.SetDataTree(1, Exertion);
            DA.SetDataList(2, R2G);
        }

        /// <summary>
        /// Provides an Icon for every component that will be visible in the User Interface.
        /// Icons need to be 24x24 pixels.
        /// You can add image files to your project resources and access them like this:
        /// return Resources.IconForThisComponent;
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                //You can add image files to your project resources and access them like this:
                // return Resources.IconForThisComponent;
                return null;
            }
        }

        /// <summary>
        /// Each component must have a unique Guid to identify it. 
        /// It is vital this Guid doesn't change otherwise old ghx files 
        /// that use the old ID will partially fail during loading.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("FD8B8B03-5DBA-41ED-99D0-9F73CE6037EB"); }
        }
    }
}