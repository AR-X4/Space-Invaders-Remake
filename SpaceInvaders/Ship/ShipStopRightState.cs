using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ShipStopRightState : ShipState
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
            this.Handle(pShip);
        }
        public override void PlayShootSound()
        {
            Sound pSound = SoundManager.Find(Sound.Name.Shoot);
            pSound.PlaySound();
        }
        public override void ShootMissile(Ship pShip)
        {
            ShipManager.ActivateMissile();

            // switch states
            //this.Handle(pShip);
            pShip.SetState(ShipManager.State.StopRightMissileFlying);
        }
    }
}
