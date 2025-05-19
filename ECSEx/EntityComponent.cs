using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSEx
{
    public abstract class EntityComponent
    {
        public abstract string Name { get; }
        internal LinkedListNode<Entity> _Location = new LinkedListNode<Entity>(Entity.dummyEntity);
    }
}
