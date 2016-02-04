using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Zenith.XnaTest.Subsystems
{
    public class SpriteRendererSubsystem : GameSubsystem
    {
        private SpriteBatch _spriteBatch;

        public SpriteRendererSubsystem(SpriteBatch sb, GameWorld theWorld) : base(theWorld)
        {
            ComponentMask.SetBit(XnaGameComponentType.Sprite);
            ComponentMask.SetBit(XnaGameComponentType.Spatial);

            _spriteBatch = sb;
        }

        public override void Update(float dt)
        {
            base.Update(dt);
        }

        public void Render()
        {
            foreach (var entity in RelevantEntities)
            {
                var sprite = World.SpriteComponents[entity];
                var spatial = World.SpatialComponents[entity];

                _spriteBatch.Draw(sprite.Texture, spatial.Position, sprite.Tint);
            }
        }
    }
}
