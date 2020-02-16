using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class GreenSquid : Leaf
    {
        public GreenSquid(GameObject.Name name, GameSprite.Name spriteName, float posX, float posY)
            : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;
        }

        ~GreenSquid()
        {

        }
        override public void Move()
        {
            this.x += delta;
            if (this.x < 0.0f || this.x > 800.0f)
            {
                delta *= -1;
            }
        }
        public override void Update()
        {

            base.Update();
        }
        // this is just a placeholder, who knows what data will be stored here

    }
}
