using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSEx.Components
{
    public static partial class CompID
    {
        public const string solid = "solid";
    }

    public class Solid : EntityComponent
    {
        public override string Name => CompID.mask;

        // used for example in temporarily purposes
        public bool IsSolid;
    }
}
