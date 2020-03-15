using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class SwapShipExplosionEvent : Command
    {
        //data
        Image.Name pName;


        public SwapShipExplosionEvent(Image.Name name) {
            this.pName = name;
        }

        public override void Execute(float deltaTime)
        {
            Ship pShip = ShipManager.GetShip();
            pShip.pProxySprite.pSprite.SwapImage(ImageManager.Find(this.pName));

        }
    }
}
