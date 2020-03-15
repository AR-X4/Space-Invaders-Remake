using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class Missile : Leaf
    {

        // Data -------------------------------------
        
        public float delta;
        private readonly Ship pShip;

        public Missile(GameObject.Name name, GameSprite.Name spriteName, Ship pShip)
            : base(name, spriteName)
        {
            
            this.pShip = pShip;
            this.ResetMissile();

            //fix fix collision bug... change eventually
            this.bMarkForDeath = true;
        }

        public override void Update()
        {
            if (this.pProxySprite.pSprite.name == GameSprite.Name.NullObject)
            {
                base.Update();
                this.ResetMissile();
            }
            else
            {
                base.Update();
                this.y += delta;
            }
        }

        ~Missile()
        {

        }
        //public override void Remove()
        //{
        //    // Keenan(delete.E)
        //    // Since the Root object is being drawn
        //    // 1st set its size to zero
        //    this.poColObj.poColRect.Set(0, 0, 0, 0);
        //    base.Update();


        //    this.pProxySprite.Set(GameSprite.Name.NullObject);

        //    this.x = 0.0f;
        //    this.y = 0.0f;

        //    //// Update the parent (missile root)
        //    //GameObject pParent = (GameObject)this.pParent;
           
        //    //remove missile from composite... 
        //    //pParent.Remove(this);
        //    //pParent.Update();

            

        //    // Now remove it
        //    //base.Remove();
        //}

        public override void Accept(CollisionVisitor other)
        {
            // Important: at this point we have an Missile
            // Call the appropriate collision reaction            
            other.VisitMissile(this);
        }

        public void ResetMissile() {
            this.x = this.pShip.x;
            this.y = this.pShip.y + 20;

            this.delta = 0.0f;

            this.pProxySprite.Set(GameSprite.Name.NullObject);
          

        }

        public override void VisitBombRoot(BombRoot b)
        {
            GameObject pGameObj = (GameObject)Iterator.GetChild(b);
            CollisionPair.Collide(pGameObj, this);
        }
        public override void VisitBomb(Bomb b)
        {
            if (b.bMarkForDeath == false && this.bMarkForDeath == false)
            {
                //Debug.WriteLine(" ---> Done");
                CollisionPair pColPair = CollisionPairManager.GetActiveColPair();
                pColPair.SetCollision(b, this);
                pColPair.NotifyListeners();
            }
        }

    }
}