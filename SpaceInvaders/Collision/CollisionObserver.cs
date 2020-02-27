using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class CollisionObserver : DLink
    {
        public CollisionSubject pSubject;

        public abstract void Notify();
    }
}
