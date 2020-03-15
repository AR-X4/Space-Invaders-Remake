using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ShipReadyObserver : CollisionObserver
    {
        //This is called when Missile is done flying
        public override void Notify()
        {
            Ship pShip = ShipManager.GetShip();

            // Correction... only method that changes state is Handle
            // So correct this....
            // pShip.SetState(ShipMan.State.Ready);
            if (pShip.CurrentStateName != ShipManager.State.Dead)
            {
                pShip.Handle();
            }

        }
    }
}
