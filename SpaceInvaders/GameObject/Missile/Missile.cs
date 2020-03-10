using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class Missile : Leaf
    {

        // Data -------------------------------------
        
        public float delta;

        public Missile(GameObject.Name name, GameSprite.Name spriteName, float posX, float posY)
            : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;
            
            this.delta = 5.0f;
        }

        public override void Update()
        {
            base.Update();
            this.y += delta;
        }

        ~Missile()
        {

        }
        public override void Remove()
        {
            // Keenan(delete.E)
            // Since the Root object is being drawn
            // 1st set its size to zero
            //this.poColObj.poColRect.Set(0, 0, 0, 0);
            //base.Update();

            //// Update the parent (missile root)
            GameObject pParent = (GameObject)this.pParent;
           
            //remove missile from composite... 
            pParent.Remove(this);
            pParent.Update();

            

            // Now remove it
            base.Remove();
        }

        public override void Accept(CollisionVisitor other)
        {
            // Important: at this point we have an Missile
            // Call the appropriate collision reaction            
            other.VisitMissile(this);
        }
        //public void SetPos(float xPos, float yPos)
        //{
        //    this.x = xPos;
        //    this.y = yPos;
        //}
        //public void SetActive(bool state)
        //{
        //    this.enable = state;
        //}

        public void ResetMissile(float xPos, float yPos) {
            this.x = xPos;
            this.y = yPos;

            this.bMarkForDeath = false;
          
        }

        public override void VisitBombRoot(BombRoot b)
        {
            GameObject pGameObj = (GameObject)Iterator.GetChild(b);
            CollisionPair.Collide(pGameObj, this);
        }
        public override void VisitBomb(Bomb b)
        {
            //Debug.WriteLine(" ---> Done");
            CollisionPair pColPair = CollisionPairManager.GetActiveColPair();
            pColPair.SetCollision(b, this);
            pColPair.NotifyListeners();
        }

    }
}