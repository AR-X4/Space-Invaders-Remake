using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ShipStopRightMissileFlyingState : ShipState
    {
        public override void Handle(Ship pShip)
        {
            pShip.SetState(ShipManager.State.Ready);
        }

        public override void MoveRight(Ship pShip)
        {
            
        }

        public override void MoveLeft(Ship pShip)
        {
            pShip.x -= pShip.shipSpeed;
            //this.Handle(pShip);
            pShip.SetState(ShipManager.State.MissileFlying);
        }

        public override void ShootMissile(Ship pShip)
        {
           
        }
    }
}
