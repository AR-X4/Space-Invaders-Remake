using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ShipDeadState : ShipState
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

        }
        public override void PlayShootSound()
        {

        }
        public override void ShootMissile(Ship pShip)
        {

        }
    }
}
