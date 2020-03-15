using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ShieldGrid : Composite
    {
        public ShieldGrid(GameObject.Name name, GameSprite.Name spriteName, float posX, float posY)
            : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;
        }

        ~ShieldGrid()
        {
        }

        public override void Accept(CollisionVisitor other)
        {
            // Important: at this point we have an Alien
            // Call the appropriate collision reaction            
            other.VisitShieldGrid(this);
        }
        public override void VisitMissileGroup(MissileGroup m)
        {

            GameObject pGameObj = (GameObject)Iterator.GetChild(this);
            CollisionPair.Collide(m, pGameObj);
        }

        public override void VisitBombRoot(BombRoot b)
        {
            // BombRoot vs ShieldRoot
            CollisionPair.Collide((GameObject)Iterator.GetChild(b), this);
        }
        public override void VisitBomb(Bomb b)
        {
            // Missile vs ShieldRoot
            CollisionPair.Collide(b, (GameObject)Iterator.GetChild(this));
        }

        public override void Update()
        {
            // Go to first child
            base.BaseUpdateBoundingBox(this);
            base.Update();
        }
    }
}

// End of File
