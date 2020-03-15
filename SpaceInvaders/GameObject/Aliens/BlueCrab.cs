using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class BlueCrab : Leaf
    {
        public BlueCrab(GameObject.Name name, GameSprite.Name spriteName, float posX, float posY)
            : base(name, spriteName, posX, posY)
        {
            this.x = posX;
            this.y = posY;
        }

        ~BlueCrab()
        {

        }
        
        public override void Accept(CollisionVisitor other)
        {
            // Important: at this point we have an BirdGroup
            // Call the appropriate collision reaction            
            other.VisitBlueCrab(this);
        }
        public override void VisitMissileGroup(MissileGroup m)
        {
            if (this.bMarkForDeath == false)// to fix bug with collision with null objs
            {
                // MissileRoot vs WallRoot
                GameObject pGameObj = (GameObject)Iterator.GetChild(m);
                CollisionPair.Collide(pGameObj, this);
            }
        }

        public override void VisitMissile(Missile m)
        {
            CollisionPair pColPair = CollisionPairManager.GetActiveColPair();
            pColPair.SetCollision(m, this);
            pColPair.NotifyListeners();
        }

        public override void Update()
        {
            
            base.Update();
        }
        public override void Remove()
        {
            // Keenan(delete.E)
            // Since the Root object is being drawn
            // 1st set its size to zero
            this.poColObj.poColRect.Set(0, 0, 0, 0);
            //base.Update();

            this.pProxySprite.Set(GameSprite.Name.NullObject);

            //// Update the parent (missile root)
            AlienColumn pParent = (AlienColumn)this.pParent;

            this.y = pParent.GetTop();

            //pParent.Update();
            //remove missile from composite... missile only has one parent..need to find root for others? 
            //pParent.Remove(this);

            // Now remove it
            //base.Remove();
        }

    }
}
