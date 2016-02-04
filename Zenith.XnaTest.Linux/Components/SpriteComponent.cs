using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zenith.XnaTest.Components
{
    public class SpriteComponent
    {
        public Texture2D Texture { get; set; }
        public float Scale { get; set; }
        public float Rotation { get; set; }
        public Color Tint { get; set; }

        public SpriteComponent()
        {
            Scale = 1.0f;
            Rotation = 0f;
            Tint = Color.White;
        }
    }
}
