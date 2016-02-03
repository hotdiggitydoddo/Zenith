namespace Zenith.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var world = new GameWorld(5);
            var entity = world.EntityManager.CreateEntity();

            var timer = new GameTimer();

            world.HealthComponents[entity].MaxHealth = 100;
            world.HealthComponents[entity].CurrentHealth = 100;
            world.HealthComponents[entity].Damage = 17;
            world.RegenerationComponents[entity].AmountToHeal = 5;
            world.RegenerationComponents[entity].Frequency = 2500;
            world.EntityManager.AddComponent(entity, GameComponentType.HealthComponent);
            world.EntityManager.AddComponent(entity, GameComponentType.RegenerationComponent);
            world.EntityManager.AddComponent(entity, GameComponentType.ConsoleRendererComponent);

            while (true)
            {
                timer.Reset();
                world.Update(166);
                while (timer.GetTicks() < 166) ;
            }

        }
    }
}
