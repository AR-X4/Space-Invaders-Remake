using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ShootSoundObserver : InputObserver
    {

        public override void Notify()
        {

            Ship pShip = ShipManager.GetShip();
            pShip.PlayShootSound();

        }
    }
}
