using System;
using System.Collections.Generic;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Parameters;
using Grasshopper.Kernel.Types;
using Rhino.Geometry;
using BinaryBird.Utilites;
using Rhino.Render;

namespace BinaryBird.Field
{
    public class MergeDynamicForce : GH_Component, IGH_VariableParameterComponent
    {
        private GH_Document GrasshopperDocument;
        private IGH_Component Component;
        /// <summary>
        /// Initializes a new instance of the MyComponent1 class.
        /// </summary>
        public MergeDynamicForce()
          : base("MergeDynamicForce", "MDF",
              "Merge force Dynamically",
              "BinaryNature", "BinaryBird")
        {
        }
        public override GH_Exposure Exposure
        {
            get
            {
                return GH_Exposure.septenary;
            }
        }
        public override void CreateAttributes()
        {
            m_attributes = new CustomAttributes(this, 0);
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_InputParamManager pManager)
        {
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

            Component = this;
            int inputCount = Component.Params.Input.Count;
            int hitCounter = 0;

            for (int i = 0; i < inputCount; i++)
            {
                IGH_DocumentObject connectedComponent = Component.Params.Input[i].Sources[0].Attributes.GetTopLevel.DocObject;
                string name = connectedComponent.Name;
                Component.Params.Input[i].NickName = name;

                foreach (IGH_Goo a in Component.Params.Input[i].VolatileData.get_Branch(0))
                {
                    hitCounter++;
                    IForce fd;
                    bool worked = a.CastTo(out fd);
                    if (!worked) { return; }
                    Forces.Add(fd);
                }
            }
            if (hitCounter > 0)
            {
                DA.SetDataList(0, Forces);
            }
        }
        public bool CanInsertParameter(GH_ParameterSide side, int index)
        {
            if (side == GH_ParameterSide.Input)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool CanRemoveParameter(GH_ParameterSide side, int index)
        {
            if (side == GH_ParameterSide.Input && Params.Input.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public IGH_Param CreateParameter(GH_ParameterSide side, int index)
        {
            Param_GenericObject param = new Param_GenericObject();
            param.Name = GH_ComponentParamServer.InventUniqueNickname("NewComponentMagicalName", Params.Input);
            param.NickName = param.Name;
            param.Description = "Param" + (Params.Input.Count + 1);
            param.Access = GH_ParamAccess.item;

            return param;
        }
        public bool DestroyParameter(GH_ParameterSide side, int index)
        {
            return true;
        }
        public void VariableParameterMaintenance()
        {
        }
        private void ParamSourcesChanged(object sender, GH_ParamServerEventArgs e)
        {
            if (e.ParameterSide == 0 && e.ParameterIndex == Component.Params.Input.Count - 1 && e.Parameter.SourceCount > 0)
            {
                IGH_Param param = CreateParameter(0, Component.Params.Input.Count);
                Component.Params.RegisterInputParam(param);
                Component.Params.OnParametersChanged();
            }
        }

        /// <summary>
        /// Provides an Icon for the component.
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
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("13E0AD03-BFE2-44AF-9353-B44317830631"); }
        }
    }
}