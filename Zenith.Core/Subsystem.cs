namespace Zenith.Core
{
    public abstract class Subsystem
    {
        /// <summary>
        /// The world that the subsystem operates in.
        /// </summary>
        protected World World { get; private set; }

        /// <summary>
        /// Set of components on an entity that this system requires in order to perform its operations.
        /// </summary>
        protected BitSet ComponentMask { get; private set; }
        protected Subsystem(World theWorld)
        {
            World = theWorld;
            ComponentMask = new BitSet();
        }

        /// <summary>
        /// Update all entities in the world whose component masks include the component mask of this subsystem.
        /// </summary>
        /// <param name="dt"></param>
        public abstract void Update(float dt);
    }
}
