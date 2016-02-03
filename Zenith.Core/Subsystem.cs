using System.Collections.Generic;
namespace Zenith.Core
{
    public abstract class Subsystem
    {
		private List<uint> _relevantEntities;

        /// <summary>
        /// The world that the subsystem operates in.
        /// </summary>
        protected World World { get; private set; }


        protected Subsystem(World theWorld, BitSet componentMask)
        {
            World = theWorld;
			World.RegisterSubsystem (componentMask, this);
			_relevantEntities = new List<uint> ();
        }

        /// <summary>
        /// Update all entities in the world whose component masks include the component mask of this subsystem.
        /// </summary>
        /// <param name="dt"></param>
        public abstract void Update(float dt);

		public void AddEntity(uint entity)
		{
			if (!_relevantEntities.Contains (entity))
				_relevantEntities.Add (entity);
		}

		public void RemoveEntity(uint entity)
		{
			if (_relevantEntities.Contains (entity))
				_relevantEntities.Remove (entity);
		}

		public bool HasEntity(uint entity)
		{
			return _relevantEntities.IndexOf(entity) != -1;
		}

    }
}
