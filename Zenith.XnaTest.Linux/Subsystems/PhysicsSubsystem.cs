using Microsoft.Xna.Framework;

namespace Zenith.XnaTest.Subsystems
{
    public class PhysicsSubsystem : GameSubsystem
    {
        public PhysicsSubsystem(GameWorld world) : base(world)
        {
            ComponentMask.SetBit(XnaGameComponentType.Spatial);
            ComponentMask.SetBit(XnaGameComponentType.Physics);
        }

        public override void Update(float dt)
        {
            foreach (var entity in RelevantEntities)
            {
                var spatial = World.SpatialComponents[entity];
                var physics = World.PhysicsComponents[entity];

                spatial.Position += new Vector2(physics.VelX, physics.VelY);
            }
            base.Update(dt);
        }
    }
}
