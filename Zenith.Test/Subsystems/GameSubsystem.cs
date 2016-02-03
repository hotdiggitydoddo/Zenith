using Zenith.Core;

namespace Zenith.Test.Subsystems
{
    public class GameSubsystem : Subsystem
    {
        protected new GameWorld World;
        public GameSubsystem(GameWorld theWorld) : base(theWorld)
        {
            World = theWorld;
        }
    }
}
