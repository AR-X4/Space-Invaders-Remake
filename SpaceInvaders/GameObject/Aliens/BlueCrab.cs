using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class BlueCrab : GameObject
    {
        public BlueCrab(GameObject.Name name, GameSprite.Name spriteName, float posX, float posY)
            : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;
        }

        ~BlueCrab()
        {

        }
        public override void Update()
        {
            
            base.Update();
        }
        // this is just a placeholder, who knows what data will be stored here

    }
}
