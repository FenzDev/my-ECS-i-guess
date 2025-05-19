using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ECSEx
{
    public static class EntityManager
    {
        static internal Dictionary<string, LinkedList<Entity>> ComponentsEntities = new ();

        static public int EntitiesCount { get; private set; }

        /// <summary>
        /// This will store <paramref name="entity"/> into the collection.
        /// </summary>
        /// <param name="entity"></param>
        public static void Add(Entity entity)
        {
            foreach (var component in entity.GivenComponents)
            {
                entity.Add(component, false);
            }

            EntitiesCount++;
        }

        /// <summary>
        /// This will create named entity with components then store it into the collection
        /// </summary>
        /// <param name="name"></param>
        /// <param name="components"></param>
        public static void Create(string name, params EntityComponent[] components) { Add(new(name, components)); }

        /// <summary>
        /// This will remove the entity from the collection.
        /// </summary>
        /// <param name="entity"></param>
        public static void Remove(Entity entity)
        {
            // we remove entity from ComponentsEntities list
            foreach (var component in entity.Components)
            {
                component._Location.List.Remove(component._Location);
                component._Location = null;
            }

            // we remove entity from every system it was provided to
            var node = entity._SystemEntitiesLocations.First;
            while (node != null)
            {
                var entityNode = node.Value;
                entityNode.List.Remove(entityNode);

                // we try to remove each node of _InSystemLocations
                var next = node.Next;
                node.List.Remove(node);
                node = next;
            }

            EntitiesCount--;
        }
        
        
    }
}
