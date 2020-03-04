using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ShipStopLeftObserver : CollisionObserver
    {
        public override void Notify()
        {
            Ship pShip = ShipManager.GetShip();

            // Correction... only method that changes state is Handle
            // So correct this....

            //pShip.Handle();

            if (pShip.CurrentStateName == ShipManager.State.Ready)
            {
                pShip.SetState(ShipManager.State.StopLeft);
            }
            else if (pShip.CurrentStateName == ShipManager.State.MissileFlying)
            {
                pShip.SetState(ShipManager.State.StopLeftMissileFlying);
            }

        }
    }
}
