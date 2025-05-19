using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSEx.Components
{
    public static partial class CompID
    {
        public const string gravity = "gravity";
    }

    public class Gravity : EntityComponent
    {
        public Gravity(float multiplier = 1f, float additional = 0f)
        {
            Multiplier = multiplier;
            Additional = additional;
        }

        public override string Name => CompID.gravity;

        public float Multiplier;
        public float Additional;
    }
}
