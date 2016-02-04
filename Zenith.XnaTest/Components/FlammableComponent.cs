﻿namespace Zenith.XnaTest.Components
{
    public class FlammableComponent
    {
        public int Damage { get; set; }
        public float Duration { get; set; }
        public float ElapsedTime { get; set; }
        public float Cooldown { get; set; }
        public float Frequency { get; set; }
        public bool OnFire => Duration > 0 && ElapsedTime < Duration;
        public bool Ready => OnFire && Cooldown <= 0;

        public FlammableComponent()
        {
            Cooldown = 0;
        }
    }
}
