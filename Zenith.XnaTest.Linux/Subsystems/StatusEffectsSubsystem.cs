using System;

namespace Zenith.XnaTest.Subsystems
{
	public class StatusEffectsSubsystem : GameSubsystem
	{
		public StatusEffectsSubsystem ()
		{
			ComponentMask.SetBit (XnaGameComponentType.CircleCollision);
			ComponentMask.SetBit (XnaGameComponentType.Flammable);
		}

		public override void Update (float dt)
		{
			foreach (var entity in RelevantEntities)
			{
				var collision = World.CollisionComponents [entity];
				var flammable = World.FlammableComponents [entity];

				if (collision.CollidedWith != 9999)
				{
					flammable.Duration = 10000;
					flammable.Damage = 7;
					flammable.Frequency = 1500;
				}

			}
			base.Update (dt);
		}
	}
}

