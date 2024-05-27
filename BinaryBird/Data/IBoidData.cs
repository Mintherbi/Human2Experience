using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryBird.Data
{
    public interface IBoidData
    {
        double f_seperate { get; set; }
        double f_cohesion { get; set; }
        double f_align { get; set; }
    }
}
