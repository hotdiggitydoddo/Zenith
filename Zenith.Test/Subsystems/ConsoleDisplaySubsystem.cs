using System;

namespace Zenith.Test.Subsystems
{
    public class ConsoleDisplaySubsystem : GameSubsystem
    {
        public ConsoleDisplaySubsystem(GameWorld world) : base(world)
        {
            ComponentMask.SetBit(GameComponentType.ConsoleRendererComponent);
        }

        public override void Update(float dt)
        {
            foreach (var entity in RelevantEntities)
            {
                var renderer = World.ConsoleRendererComponents[entity];

                foreach (var message in renderer.Messages)
                    Console.WriteLine("Entity #{0}: {1}", entity, message);

                renderer.Messages.Clear();
            }
            base.Update(dt);
        }
    }
}
