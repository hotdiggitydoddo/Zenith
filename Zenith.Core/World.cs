using System.Collections.Generic;

namespace Zenith.Core
{
    /// <summary>
    /// Base class that houses all entities.  Inherited objects should contain one array for each component type 
    /// initialized to the length of MaxEntities.  All subsystems live here as well and get called to update and draw.
    /// Includes base methods for creation and destruction of entities.
    /// 
    /// </summary>
    public abstract class World
    {
        private readonly Stack<uint> _freeEntities;
        private readonly List<uint> _entitiesInUse;

        public readonly uint MaxEntities;
        public uint MaxEntityId => _entitiesInUse[_entitiesInUse.Count - 1];
        public BitSet[] EntityMasks { get; }

        protected World(uint maxEntities)
        {
            MaxEntities = maxEntities;
            EntityMasks = new BitSet[MaxEntities];
            _entitiesInUse = new List<uint>((int)MaxEntities);
            _freeEntities = new Stack<uint>();
        }

        /// <summary>
        /// Creates a new bitset for entity, sets the bitset for no components, and pushes it onto the free entities stack.  
        /// This method should be called in a loop over all entities and should include the initialization of a new component 
        /// at the index of the entity in that component array.
        /// </summary>
        /// <param name="entity">Entity to initialize.</param>
        protected virtual void Initialize(uint entity)
        {
            EntityMasks[entity] = new BitSet();
            EntityMasks[entity].SetBit(ComponentType.NoComponents);

            _freeEntities.Push((uint)entity);
        }

        protected uint CreateEntity()
        {
            if (_freeEntities.Count == 0)
                return MaxEntities;

            var entity = _freeEntities.Pop();

            EntityMasks[entity].ClearBit(ComponentType.NoComponents);

            _entitiesInUse.Add(entity);
            _entitiesInUse.Sort();

            return entity;
        }

        protected void DestroyEntity(uint entity)
        {
            EntityMasks[entity].ClearAll();
            EntityMasks[entity].SetBit(ComponentType.NoComponents);
            _freeEntities.Push(entity);
            _entitiesInUse.Remove(entity);
            _entitiesInUse.Sort();
        }
        /// <summary>
        /// Update all subsystems.
        /// </summary>
        /// <param name="dt"></param>
        public abstract void Update(float dt);

        /// <summary>
        /// Render any subsystem output to the screen.
        /// </summary>
        public abstract void Render();

    }
}
