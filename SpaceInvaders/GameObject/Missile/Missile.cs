using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class Missile : Leaf
    {
        public Missile(GameObject.Name name, GameSprite.Name spriteName, float posX, float posY)
            : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;
            this.bHit = false;
        }

        public override void Update()
        {
            base.Update();

            if (!bHit)
            {
                this.y += 1.0f;
            }
        }

        ~Missile()
        {

        }

        public void Hit()
        {
            this.bHit = true;
        }

        public override void Accept(CollisionVisitor other)
        {
            // Important: at this point we have an Missile
            // Call the appropriate collision reaction            
            other.VisitMissile(this);
        }
        // Data

        public bool bHit;
    }
}