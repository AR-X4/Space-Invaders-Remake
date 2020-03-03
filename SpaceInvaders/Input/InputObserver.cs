using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract class InputObserver : DLink
    {
        public InputSubject pSubject;

        // define this in concrete
        abstract public void Notify();
    }
}
