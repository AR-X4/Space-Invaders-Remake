using System;
using System.Diagnostics;


namespace SpaceInvaders
{
    public class ResetShipEvent : Command
    {

        public override void Execute(float deltaTime)
        {

            ShipManager.ResetShip();

        }
    }
}
