using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ECSEx
{
    public sealed class Entity
    {
        public Entity() { }
        public Entity(string name, params EntityComponent[] components) : this(name, new List<EntityComponent>(components)) { }
        internal Entity(string name, List<EntityComponent> components)
        {
            Name = name; GivenComponents = components;
        }

        internal static readonly Entity dummyEntity = new();

        public string Name { get; set; } = string.Empty;
        internal List<EntityComponent> Components = [];
        internal List<EntityComponent> GivenComponents = [];
        internal LinkedList<LinkedListNode<SystemEntity>> _SystemEntitiesLocations = new();

        /// <summary>
        /// <c>Suggestion:</c> If you are in system do not use this. 
        /// If you want to get entity's component use <see cref="SystemEntity.Get{T}(int)"/> 
        /// by iterating through <see cref="ComponentSystem.Entities"/> 
        /// such that the <see cref="int"/> is the index of component based on <see cref="ComponentSystem.RequiredComponents"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Get<T>() where T : EntityComponent
        {
            foreach (var c in Components)
            {
                if (c is T cT) return cT;
            }
            return default;
        }


        public void Add<T>(T component) where T : EntityComponent => Add<T>(component,true);
        internal void Add<T>(T component, bool listAdd) where T : EntityComponent
        {
            // Add the component to entity's list 
            Components.Add(component);

            // We include entity to its collection in EntityManager
            if (EntityManager.ComponentsEntities.TryGetValue(component.Name, out var entities))
            {
                component._Location = entities.AddLast(this);
            }
            else
            {
                var newList = new LinkedList<Entity>();
                component._Location = newList.AddFirst(this);
                EntityManager.ComponentsEntities.Add(component.Name, newList);
            }

            // We add entity to its system when combination is completed
            if (SystemManager.ComponentsSystems.TryGetValue(component.Name, out var systems))
            {
                foreach (var system in systems)
                {

                    if (Name == "player")
                        Debug.Assert(true);

                    // TODO : redo this That's inefficient
                    SystemManager._AddEntityIfVaild(system, this);

                }
            }

        }

        public void Remove<T>() where T : EntityComponent
        {
            for (int i = 0; i < Components.Count; i++)
            {
                if (Components[i] is T)
                {
                    var component = Components[i];

                    // Remove the entity from ComponentsEntities List
                    EntityManager.ComponentsEntities[component.Name].Remove(component._Location);

                    // Remove the entity from systems
                    foreach (var location in _SystemEntitiesLocations)
                    {
                        location.Value.System.Entities.Remove(location);
                    }

                    // Remove it from entity components list
                    Components.RemoveAt(i);

                    return;
                }
            }
        }

        public override string ToString() => Name;

        #nullable enable
        public EntityComponent? this[string name] => Components.Find(c=>c.Name == name);
    }
}
