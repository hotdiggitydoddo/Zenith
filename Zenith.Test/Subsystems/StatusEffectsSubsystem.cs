namespace Zenith.Test.Subsystems
{
    //public class StatusEffectsSubsystem : Subsystem
    //{
    //    public StatusEffectsSubsystem(World theWorld) : base(theWorld)
    //    {
    //        ComponentMask.SetBit(ComponentType.HealthComponent);
    //        ComponentMask.SetBit(ComponentType.FlammableComponent);
    //        ComponentMask.SetBit(ComponentType.ConsoleRendererComponent);

    //    }

    //    public override void Update(float dt)
    //    {
    //        uint entity;

    //        for (entity = 0; entity <= World.MaxEntityId; entity++)
    //        {
    //            if (!ComponentMask.IsSubsetOf(World.EntityMasks[entity])) continue;

    //            var health = World.HealthComponents[entity];
    //            var flammable = World.FlammableComponents[entity];
    //            var renderer = World.ConsoleRendererComponents[entity];

    //            if (!flammable.OnFire) continue;

    //            if (!flammable.Ready)
    //            {
    //                flammable.Cooldown -= dt;
    //                flammable.ElapsedTime += dt;
    //                continue;
    //            }

    //            health.Damage += flammable.Damage;
    //            flammable.Cooldown = flammable.Frequency;
    //            flammable.ElapsedTime += dt;
    //            renderer.Messages.Add(string.Format("Entity #{0} is on fire! Ouch!!", entity));

    //        }
    //    }

    //}
}
