using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Intrinsics;
using System.Text;
using System.Threading.Tasks;

namespace ECSEx.Components
{
    public static partial class CompID
    {
        public const string mask = "mask";
    }

    public class Mask : EntityComponent
    {
        public override string Name => CompID.mask;

        public RectangleF Dimensions;

        public RectangleF GetMask(Position pos) => new RectangleF(new (pos.X, pos.Y), Dimensions.Size );
    }
}
