using Grasshopper;
using Grasshopper.Kernel;
using System;
using System.Drawing;

namespace BinaryBird
{
    public class BinaryBirdInfo : GH_AssemblyInfo
    {
        public override string Name => "BinaryBird";

        //Return a 24x24 pixel bitmap to represent this GHA library.
        //public override Bitmap Icon => null;

        //Return a short string describing the purpose of this GHA library.
        public override string Description => "";

        public override Guid Id => new Guid("5053f109-63f1-408c-b928-9ac282a361eb");

        //Return a string identifying you or your company.
        public override string AuthorName => "";

        //Return a string representing your preferred contact details.
        public override string AuthorContact => "";
    }
}