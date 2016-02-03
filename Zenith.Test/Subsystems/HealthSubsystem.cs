namespace Zenith.Test.Subsystems
{
    public class HealthSubsystem : GameSubsystem
    {
        public HealthSubsystem(GameWorld world) : base(world)
        {
            ComponentMask.SetBit(GameComponentType.HealthComponent);
            ComponentMask.SetBit(GameComponentType.RegenerationComponent);
            ComponentMask.SetBit(GameComponentType.ConsoleRendererComponent);
        }

        public override void Update(float dt)
        {
            foreach (var entity in RelevantEntities)
            {
                var health = World.HealthComponents[entity];
                var regen = World.RegenerationComponents[entity];
                var renderer = World.ConsoleRendererComponents[entity];

                if (regen.Frequency > 0)
                {
                    if (regen.ElapsedTime >= regen.Frequency && health.CurrentHealth != health.MaxHealth)
                    {
                        health.CurrentHealth += regen.AmountToHeal;
                        regen.ElapsedTime = 0;
                        renderer.Messages.Add(string.Format("healed for {0} points.  Health: {1}/{2} ({3})",
                            regen.AmountToHeal, health.CurrentHealth, health.MaxHealth, health.IsAlive ? "ALIVE" : "DEAD"));
                    }
                    else
                        regen.ElapsedTime += dt;
                }

                if (health.Damage == 0) continue;

                health.CurrentHealth -= health.Damage;
                renderer.Messages.Add(string.Format("damaged for {0} points.  Health: {1}/{2} ({3})",
                            health.Damage, health.CurrentHealth, health.MaxHealth, health.IsAlive ? "ALIVE" : "DEAD"));
                health.Damage = 0;
            }
            base.Update(dt);
        }
    }
}
