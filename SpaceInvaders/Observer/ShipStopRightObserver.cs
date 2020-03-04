using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ShipStopRightObserver : CollisionObserver
    {
        public override void Notify()
        {
            Ship pShip = ShipManager.GetShip();

            // Correction... only method that changes state is Handle
            // So correct this....

            if (pShip.CurrentStateName == ShipManager.State.Ready ) {
                pShip.SetState(ShipManager.State.StopRight);
            }
            else if (pShip.CurrentStateName == ShipManager.State.MissileFlying) {
                pShip.SetState(ShipManager.State.StopRightMissileFlying);
            }
            //pShip.Handle();

        }
    }
}
