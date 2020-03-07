using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class AlienObserver : DLink
    {
        public AlienSubject pSubject;

        public abstract void Notify();

    }
}