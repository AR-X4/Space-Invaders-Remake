﻿using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class Component : CollisionVisitor
    {
        public enum Container
        {
            LEAF,
            COMPOSITE,
            Unknown
        }
        //data
        static public float delta = 1.0f;
        public Component pParent = null;
        public Component pReverse = null;
        public Container holder = Container.Unknown;
        
        public abstract void Add(Component c);
        public abstract void Remove(Component c);
        public abstract void Print();
        public abstract void Move();
        public abstract Component GetFirstChild();
        //public abstract void DumpNode();
    }
}


