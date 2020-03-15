using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ShieldBrick : Leaf
    {
        public ShieldBrick(GameObject.Name name, GameSprite.Name spriteName, float posX, float posY)
            : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;

            this.SetCollisionColor(1.0f, 0.0f, 0.0f);
        }
        ~ShieldBrick()
        {

        }
        public override void Accept(CollisionVisitor other)
        {
            // Important: at this point we have an Alien
            // Call the appropriate collision reaction            
            other.VisitShieldBrick(this);
        }
        public override void VisitMissileGroup(MissileGroup m)
        {
            
            //if (this.bMarkForDeath == false && m.bMarkForDeath == false)// to fix bug with collision with null objs
            //{
                GameObject pGameObj = (GameObject)Iterator.GetChild(m);
                CollisionPair.Collide(pGameObj, this);
            //}
        }
        public override void VisitMissile(Missile m)
        {
            if (this.bMarkForDeath == false && m.bMarkForDeath == false)// to fix bug with collision with null objs
            {
                CollisionPair pColPair = CollisionPairManager.GetActiveColPair();
                pColPair.SetCollision(m, this);
                pColPair.NotifyListeners();
            }

        }
        public override void VisitBombRoot(BombRoot b)
        {
          
                // BombRoot vs ShieldRoot
                CollisionPair.Collide((GameObject)Iterator.GetChild(b), this);
            
        }
        public override void VisitBomb(Bomb b)
        {
            if (this.bMarkForDeath == false)// to fix bug with collision with null objs
            {
                // Bomb vs ShieldBrick
                //Debug.WriteLine(" ---> Done");
                CollisionPair pColPair = CollisionPairManager.GetActiveColPair();
                pColPair.SetCollision(b, this);
                pColPair.NotifyListeners();
            }
        }
        public override void Update()
        {
            base.Update();
        }
        public override void Remove()
        {
            // Since the Root object is being drawn
            // 1st set its size to zero
            this.poColObj.poColRect.Set(0, 0, 0, 0);
            //base.Update();

           

            this.pProxySprite.Set(GameSprite.Name.NullObject);

            // Update the parent (bomb root)
            //GameObject pParent = (GameObject)this.pParent;

            //pParent.Remove(this);
            //pParent.Update();

            // Now remove from sprite batches
            //base.Remove();
        }
    }
}

// End of File
