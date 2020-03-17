using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ShipStateReady : ShipState
    {
        public override void Handle(Ship pShip)
        {
            pShip.SetState(ShipManager.State.MissileFlying);
        }

        public override void MoveRight(Ship pShip)
        {
            pShip.x += pShip.shipSpeed;
        }

        public override void MoveLeft(Ship pShip)
        {
            pShip.x -= pShip.shipSpeed;
        }
        public override void PlayShootSound()
        {
            Sound pSound = SoundManager.Find(Sound.Name.Shoot);
            pSound.PlaySound();
        }

        public override void ShootMissile(Ship pShip)
        {
            ShipManager.LaunchMissile();

            // switch states
            this.Handle(pShip);
        }
    }
}
