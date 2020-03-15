using System;
using System.Diagnostics;


namespace SpaceInvaders
{
    public class ResetShipEvent : Command
    {

        public override void Execute(float deltaTime)
        {
            //Ship pShip = ShipManager.GetShip();

            //pShip.pProxySprite.Set(GameSprite.Name.Ship);
            //pShip.ResetLocation();
            //pShip.bMarkForDeath = false;

            ShipManager.ResetShip();

        }
    }
}
