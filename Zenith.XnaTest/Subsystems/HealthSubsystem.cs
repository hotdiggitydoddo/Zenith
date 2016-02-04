using Microsoft.Xna.Framework;

namespace Zenith.XnaTest.Subsystems
{
    public class HealthSubsystem : GameSubsystem
    {
        public HealthSubsystem(GameWorld world) : base(world)
        {
            ComponentMask.SetBit(XnaGameComponentType.Health);
            ComponentMask.SetBit(XnaGameComponentType.Regeneration);
            ComponentMask.SetBit(XnaGameComponentType.Sprite);
        }

        public override void Update(float dt)
        {
            foreach (var entity in RelevantEntities)
            {
                var health = World.HealthComponents[entity];
                var regen = World.RegenerationComponents[entity];
                var sprite = World.SpriteComponents[entity];

                if (regen.Frequency > 0)
                {
                    if (regen.ElapsedTime >= regen.Frequency && health.CurrentHealth != health.MaxHealth)
                    {
                        health.CurrentHealth += regen.AmountToHeal;
                        regen.ElapsedTime = 0;
                        //renderer.Messages.Add(string.Format("healed for {0} points.  Health: {1}/{2} ({3})",
                        //  regen.AmountToHeal, health.CurrentHealth, health.MaxHealth, health.IsAlive ? "ALIVE" : "DEAD"));
                        sprite.Tint = new Color(Color.Lime, 1f);
                    }
                    else
                    {
                        regen.ElapsedTime += dt;
                        sprite.Tint = new Color(Color.Lerp(Color.Lime, Color.White, regen.ElapsedTime / 750), 1f);
                    }

                }

                if (health.Damage == 0) continue;

                health.CurrentHealth -= health.Damage;
                //renderer.Messages.Add(string.Format("damaged for {0} points.  Health: {1}/{2} ({3})",
                //            health.Damage, health.CurrentHealth, health.MaxHealth, health.IsAlive ? "ALIVE" : "DEAD"));
                health.Damage = 0;
                regen.ElapsedTime = 0f;
                sprite.Tint = new Color(Color.Red, 1f);
            }
            base.Update(dt);
        }
    }
}
