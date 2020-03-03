using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ShipReadyObserver : CollisionObserver
    {
        public override void Notify()
        {
            Ship pShip = ShipManager.GetShip();

            // Correction... only method that changes state is Handle
            // So correct this....
            // pShip.SetState(ShipMan.State.Ready);
            pShip.Handle();

        }
    }
}
