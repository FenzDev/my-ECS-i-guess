using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ECSEx
{
    public class SystemEntity
    {
        public SystemEntity(ComponentSystem system)
        {
            System = system;
        }

        internal EntityComponent[] components; 
        public Entity Actual { get; internal set; }
        public ComponentSystem System { get; internal set; }

        public T Get<T>(int componentIndex) where T : EntityComponent => components[componentIndex] as T;

        public void Get<T0> (out T0 c0) where T0 : EntityComponent 
        { 
            c0 = Get<T0>(0);
        }

        public void Get<T0,T1> (out T0 c0, out T1 c1) 
            where T0 : EntityComponent 
            where T1 : EntityComponent
        {
            c0 = Get<T0>(0);
            c1 = Get<T1>(1);
        }
        
        public void Get<T0,T1,T2> (out T0 c0, out T1 c1, out T2 c2) 
            where T0 : EntityComponent 
            where T1 : EntityComponent
            where T2 : EntityComponent
        {
            c0 = Get<T0>(0);
            c1 = Get<T1>(1);
            c2 = Get<T2>(2);
        }

        public void Get<T0,T1,T2,T3> (out T0 c0, out T1 c1, out T2 c2, out T3 c3) 
            where T0 : EntityComponent 
            where T1 : EntityComponent
            where T2 : EntityComponent
            where T3 : EntityComponent
        {
            c0 = Get<T0>(0);
            c1 = Get<T1>(1);
            c2 = Get<T2>(2);
            c3 = Get<T3>(3);
        }

        public override string ToString() => (Actual is null)? "< No Entity >": Actual.Name;
    }
}
