using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class UFORoot : Composite
    {
        public UFORoot(GameObject.Name name, GameSprite.Name spriteName, float posX, float posY)
            : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;

            this.poColObj.pColSprite.SetLineColor(1, 1, 1);
        }

        public override void Accept(CollisionVisitor other)
        {
            // Important: at this point we have an Alien
            // Call the appropriate collision reaction            
            other.VisitUFORoot(this);
        }
        public override void Update()
        {
            // Go to first child
            base.BaseUpdateBoundingBox(this);
            base.Update();
        }
        public override void VisitMissileGroup(MissileGroup m)
        {
            
            GameObject pGameObj = (GameObject)Iterator.GetChild(this);
            CollisionPair.Collide(m, pGameObj);
        }
    }
}
