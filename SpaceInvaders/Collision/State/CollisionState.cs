using System;
using System.Diagnostics;


namespace SpaceInvaders
{
    abstract public class CollisionState
    {
        public abstract void Handle();

        public abstract void ToggleVisibility();

    }
}
