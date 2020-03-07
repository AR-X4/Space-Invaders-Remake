using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ToggleCollisionBoxObserver : InputObserver
    {
        public override void Notify()
        {
            CollisionStateManager.pCurrentState.ToggleVisibility();
        }
    }
}
