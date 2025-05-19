using ECSEx.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSEx.Systems
{
    public class MovementSystem : ComponentSystem
    {
        public override string[] RequiredComponents { get; } = [CompID.position, CompID.gravity];

        public override void Register()
        {
            Events.Update += Update;
            base.Register();
        }

        public static float GlobalGravity = 0.2f;

        void Update()
        {
            foreach (var entity in Entities)
            {
                entity.Get( out Position pos, out Gravity gravity);

                pos.Y += gravity.Multiplier * GlobalGravity + gravity.Additional;
                
                if (entity.ToString() == "player") Console.WriteLine("{0}: pos: {1}", entity, pos.Y);

            }
        }
    }
}
