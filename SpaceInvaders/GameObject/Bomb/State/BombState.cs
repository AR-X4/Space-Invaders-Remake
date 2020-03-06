using System;
using System.Diagnostics;


namespace SpaceInvaders
{
    abstract public class BombState
    {
        public abstract void Handle(AlienColumn pCol);

        public abstract void DropBomb(AlienColumn pCol);
    }
}
