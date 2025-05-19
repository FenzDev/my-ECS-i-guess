using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSEx.Components
{
    public static partial class CompID
    {
        public const string position = "position";
    }

    public class Position : EntityComponent
    {
        public Position(float x, float y)
        {
            X = x; Y = y;
        }

        public override string Name => CompID.position;

        public float X;
        public float Y;
    }
}
