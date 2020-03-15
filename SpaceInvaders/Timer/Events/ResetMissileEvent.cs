using System;
using System.Diagnostics;


namespace SpaceInvaders
{
    public class ResetMissileEvent : Command
    {

        public override void Execute(float deltaTime)
        {
         
            Missile pMissile = ShipManager.GetMissile();
            pMissile.ResetMissile();
            Ship pShip = ShipManager.GetShip();
            if (pShip.CurrentStateName != ShipManager.State.Dead)
            {
                pShip.Handle();
            }

        }
    }
}
