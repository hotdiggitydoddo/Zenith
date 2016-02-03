using Zenith.Core;
using Zenith.Test.Components;
using Zenith.Test.Subsystems;

namespace Zenith.Test
{
    public class GameWorld : World
    {
        public HealthComponent[] HealthComponents { get; }
        public SpatialComponent[] SpatialComponents { get; }
        public RegenerationComponent[] RegenerationComponents { get; }
        public ConsoleRendererComponent[] ConsoleRendererComponents { get; }

        public HealthSubsystem HealthSubsystem { get; }
        public ConsoleDisplaySubsystem ConsoleDisplaySubsystem { get; }

        public GameWorld(uint maxEntities) : base(maxEntities) 
        {
            HealthComponents = new HealthComponent[maxEntities];
            SpatialComponents = new SpatialComponent[maxEntities];
            RegenerationComponents = new RegenerationComponent[maxEntities];
            ConsoleRendererComponents = new ConsoleRendererComponent[maxEntities];

            for (int i = 0; i < maxEntities; i++)
            {
                HealthComponents[i] = new HealthComponent();
                SpatialComponents[i] = new SpatialComponent();
                RegenerationComponents[i] = new RegenerationComponent();
                ConsoleRendererComponents[i] = new ConsoleRendererComponent();
            }

            HealthSubsystem = new HealthSubsystem(this);
            ConsoleDisplaySubsystem = new ConsoleDisplaySubsystem(this);

        }

        public override void Update(float dt)
        {
            HealthSubsystem.Update(dt);
            ConsoleDisplaySubsystem.Update(dt);
        }

        public override void Render()
        {
            //throw new NotImplementedException();
        }
    }
}
