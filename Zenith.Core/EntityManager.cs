using System;
using System.Collections.Generic;

namespace Zenith.Core
{
	public class EntityManager
	{
		private BitSet[] _entities;
		private Dictionary<BitSet, List<Subsystem>> _subsystems;

		public World World { get; private set; }
		public uint MaxEntities { get; private set; }


		public EntityManager (World world, uint maxEntities)
		{
			World = world;
			_entities = new BitSet[maxEntities];
			MaxEntities = maxEntities;

			_subsystems = new Dictionary<BitSet, List<Subsystem>> ();

			for (uint entity = 0; entity < MaxEntities; entity++) 
			{
				_entities[entity] = new BitSet();
				_entities[entity].SetBit(ComponentType.NoComponents);
			}
		}

		public void RegisterSubsystem(BitSet mask, Subsystem subsystem)
		{
			if (_subsystems.ContainsKey (mask))
				_subsystems [mask].Add (subsystem);
			else
				_subsystems.Add (mask, new List<Subsystem>{ subsystem });
		}

		public void AddComponent<T>(uint entity) where T : int
		{
			if (_entities [entity].IsSet (T)) return;

			_entities [entity].SetBit (T);

			foreach (var subsystemList in _subsystems)
				if (subsystemList.Key.IsSubsetOf (_entities [entity]))
					foreach (var subsystem in subsystemList.Value)
						subsystem.AddEntity (entity);
		}

		public void RemoveComponent<T>(uint entity) where T : int
		{
			if (!_entities [entity].IsSet (T)) return;

			_entities [entity].ClearBit (T);

			foreach (var subsystemList in _subsystems)
				var subsystemsWithEntity = subsystemList.Value.Find(x => x.HasEntity(entity));
				if (subsystemList.Value.FindAll(x => x.HasEntity(entity)))
				if (subsystemList.Key.IsSubsetOf (_entities [entity]))
					foreach (var subsystem in subsystemList.Value)
						subsystem.AddEntity (entity);

		}



	}
}

