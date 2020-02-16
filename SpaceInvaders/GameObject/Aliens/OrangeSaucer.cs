using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class OrangeSaucer : GameObject
    {
        public OrangeSaucer(GameObject.Name name, GameSprite.Name spriteName, float posX, float posY)
            : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;
        }

        ~OrangeSaucer()
        {

        }
        public override void Update()
        {

            base.Update();
        }
        // this is just a placeholder, who knows what data will be stored here

    }
}
