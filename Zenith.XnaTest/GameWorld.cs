using Microsoft.Xna.Framework.Graphics;
using Zenith.Core;
using Zenith.XnaTest.Components;
using Zenith.XnaTest.Subsystems;

namespace Zenith.XnaTest
{
    public class GameWorld : World
    {
        //private Game1 _game;

        //Components
        public HealthComponent[] HealthComponents { get; }
        public SpatialComponent[] SpatialComponents { get; }
        public RegenerationComponent[] RegenerationComponents { get; }
        public SpriteComponent[] SpriteComponents { get; }
        public PhysicsComponent[] PhysicsComponents { get; }

        //Subsystems
        public HealthSubsystem HealthSubsystem { get; }
        public SpriteRendererSubsystem SpriteRendererSubsystem { get; }
        public PhysicsSubsystem PhysicsSubsystem { get; }
        public GameWorld(uint maxEntities, SpriteBatch spriteBatch) : base(maxEntities) 
        {
            HealthComponents = new HealthComponent[maxEntities];
            SpatialComponents = new SpatialComponent[maxEntities];
            RegenerationComponents = new RegenerationComponent[maxEntities];
            SpriteComponents = new SpriteComponent[maxEntities];
            PhysicsComponents = new PhysicsComponent[maxEntities];

            for (int i = 0; i < maxEntities; i++)
            {
                HealthComponents[i] = new HealthComponent();
                SpatialComponents[i] = new SpatialComponent();
                RegenerationComponents[i] = new RegenerationComponent();
                SpriteComponents[i] = new SpriteComponent();
                PhysicsComponents[i] = new PhysicsComponent();
            }

            HealthSubsystem = new HealthSubsystem(this);
            PhysicsSubsystem = new PhysicsSubsystem(this);
            SpriteRendererSubsystem = new SpriteRendererSubsystem(spriteBatch, this);
        }

        public override void Update(float dt)
        {
            HealthSubsystem.Update(dt);
            PhysicsSubsystem.Update(dt);
            SpriteRendererSubsystem.Update(dt);
        }

        public override void Render()
        {
            SpriteRendererSubsystem.Render();
        }
    }
}
