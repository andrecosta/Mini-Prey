﻿namespace KokoEngine
{
    public abstract class Collider : Component
    {
        public bool IsColliding { get; set; }

        public override void Update(float dt)
        {

        }
    }
}
