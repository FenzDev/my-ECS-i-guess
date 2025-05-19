using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ECSEx
{
    public class SystemManager
    {
        static internal Dictionary<string, LinkedList<ComponentSystem>> ComponentsSystems = new();

        /// <summary>
        /// This will store <paramref name="system"/> into the collection.
        /// </summary>
        /// <param name="system"></param>
        public static void Register(ComponentSystem system)
        {
            // New list of locations
            system.Locations = new LinkedListNode<ComponentSystem>[system.RequiredComponents.Length];

            // Include it in ComponentsSystems
            for (int i = 0; i < system.RequiredComponents.Length; i++)
            {
                if (ComponentsSystems.TryGetValue(system.RequiredComponents[i], out LinkedList<ComponentSystem> list))
                {
                    system.Locations[i] = list.AddLast(system);
                }
                else
                {
                    var newList = new LinkedList<ComponentSystem>();
                    system.Locations[i] = newList.AddFirst(system);
                    ComponentsSystems.Add(system.RequiredComponents[i], newList);
                }

            }

            // Provide entities
            Locate(system);

            // Finalizing
            system.IsRegistered = true;
            system.Register();
        }

        /// <summary>
        /// This will remove <paramref name="system"/> from the collection. 
        /// </summary>
        /// <param name="system"></param>
        public static void Unregister(ComponentSystem system)
        {
            // Clear locations in system based on ComponentsSystems
            foreach (var location in system.Locations)
            {
                location.List.Remove(location);
            }
            system.Locations = new LinkedListNode<ComponentSystem>[system.RequiredComponents.Length];

            // Clear stored locations of provided entities
            var entityNode = system.Entities.First;
            while (entityNode != null)
            {
                entityNode.Value.Actual._SystemEntitiesLocations.Remove(entityNode);

                entityNode = entityNode.Next;
            }

            // Finalizing
            system.IsRegistered = false;
            system.Unregister();
        }

        internal static void Locate(ComponentSystem system)
        {
            system.Entities = new();

            if (system.RequiredComponents.Length == 0) return;
            if (EntityManager.ComponentsEntities.Count == 0) return;

            // we search for the list with the least amount of entities
            LinkedList<Entity> minList = EntityManager.ComponentsEntities[system.RequiredComponents[0]];
            for (int i = 1; i < system.RequiredComponents.Length; i++)
            {
                // If there isn't any list of entity with one of the required components we return an empty list
                if (!EntityManager.ComponentsEntities.TryGetValue(system.RequiredComponents[i], out var entities))
                    return;

                if (entities.Count >= minList.Count)
                    continue;
                minList = entities;
            }

            // we then search there for entities with all required components
            var node = minList.First;
            while (node != null)
            {
                _AddEntityIfVaild(system, node.Value);

                node = node.Next;
            }
        }

        internal static void _AddEntityIfVaild(ComponentSystem system, Entity entity)
        {

            if (entity.Name == "player")
                Debug.Assert(true);

            var systemEntity = new SystemEntity(system) { components = new EntityComponent[system.RequiredComponents.Length] };

            var entityComponents = entity.Components;
            var hasAll = true;
            
            for (int i = 0; i < system.RequiredComponents.Length; i++)
            {
                var component = entityComponents.Find(e => e.Name == system.RequiredComponents[i]);

                if (component is null)
                {
                    hasAll = false;
                    break;
                }

                systemEntity.components[i] = component;
            }

            // If this entity has all the required components
            if (hasAll)
            {
                systemEntity.Actual = entity;


                var location = system.Entities.AddLast(systemEntity);
                entity._SystemEntitiesLocations.AddLast(location);
            }

        }
    }
}
