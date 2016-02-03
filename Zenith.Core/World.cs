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
		private EntityManager _entityManager;

		public World (uint maxEntities)
		{
			
			_entityManager = new EntityManager(this, maxEntities);
		}

		public void RegisterSubsystem(BitSet mask, Subsystem subsystem)
		{
			if (_subsystemComponentMasks.ContainsKey (mask))
				_subsystemComponentMasks [mask].Add (subsystem);
			else
				_subsystemComponentMasks.Add (mask, new List<Subsystem>{ subsystem });
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
