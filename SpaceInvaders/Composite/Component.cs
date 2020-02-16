using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class Component : DLink
    {
        public abstract void Add(Component c);
        public abstract void Remove(Component c);
        public abstract void Print();
        public abstract void Move();

        static public float delta = 1.0f;
    }
}


