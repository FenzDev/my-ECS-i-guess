using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSEx
{
    public static class Events
    {
        static Events()
        {
            Update += () => { };
            Render += () => { };
        }

        internal static void InvokeUpdate() { Update(); }
        public static event Action Update;
        internal static void InvokeRender() { Render(); }
        public static event Action Render;
    }
}
