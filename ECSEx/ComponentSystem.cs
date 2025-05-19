using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSEx
{
    public abstract class ComponentSystem
    {
        public abstract string[] RequiredComponents { get; }
        internal LinkedListNode<ComponentSystem>[] Locations;
        public bool IsRegistered { get; internal set; }

        public LinkedList<SystemEntity> Entities { get; internal set; } = new();
        
        public virtual void Register() { }
        public virtual void Unregister() { }

        /// <summary>
        /// Used for looping through the entities. like this:
        /// <code>while (<see cref="Next(out Entity entity)"/>)
        /// {
        ///   ...
        /// }
        /// </code>
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected bool Next(out Entity entity)
        {
            // TODOD : Do!
            entity = null;
            return false;
        }
        protected T Get<T>(LinkedListNode<Entity> entityNode, int componentIndex) where T : ComponentSystem
        {
            // TODO : Get component
            return default;
        }
    }
}
