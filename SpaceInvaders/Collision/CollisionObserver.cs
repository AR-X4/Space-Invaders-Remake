using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class CollisionObserver : DLink
    {
        public CollisionSubject pSubject;

        public abstract void Notify();

        // WHY not add a state pattern into our Observer!
        public virtual void Execute()
        {
            // default implementation
        }
    }
}
