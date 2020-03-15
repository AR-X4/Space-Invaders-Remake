using System;
using System.Diagnostics;


namespace SpaceInvaders
{
    public class Ship : Leaf
    {
        // Data: --------------------
        public float shipSpeed;
        private ShipState state;
        public ShipManager.State CurrentStateName;

        public Ship(GameObject.Name name, GameSprite.Name spriteName, float posX, float posY)
         : base(name, spriteName, posX, posY)
        {
            this.x = posX;
            this.y = posY;

            this.shipSpeed = 3.0f;
            this.state = null;
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Accept(CollisionVisitor other)
        {
            // Important: at this point we have an Bomb
            // Call the appropriate collision reaction
            other.VisitShip(this);
        }

        public void MoveRight()
        {
            this.state.MoveRight(this);
        }

        public void MoveLeft()
        {
            this.state.MoveLeft(this);
        }

        public void ShootMissile()
        {
            this.state.ShootMissile(this);
        }
        public void PlayShootSound()
        {
            this.state.PlayShootSound();
        }

        public void SetState(ShipManager.State inState)
        {
            this.state = ShipManager.GetState(inState);
            this.CurrentStateName = inState;
            Debug.WriteLine(this.state);
        }
        public void Handle()
        {
            this.state.Handle(this);
        }
        public ShipState GetState()
        {
            return this.state;
        }

        public override void VisitBombRoot(BombRoot b)//this necessary?
        {
            GameObject pGameObj = (GameObject)Iterator.GetChild(b);
            CollisionPair.Collide(pGameObj, this);
        }
        public override void VisitBomb(Bomb b)
        {
            if (b.bMarkForDeath == false)
            {
                //Debug.WriteLine(" ---> Done");
                CollisionPair pColPair = CollisionPairManager.GetActiveColPair();
                pColPair.SetCollision(b, this);
                pColPair.NotifyListeners();
            }
        }

        //public override void Remove()
        //{
        //    // Since the Root object is being drawn
        //    // 1st set its size to zero
        //    this.poColObj.poColRect.Set(0, 0, 0, 0);
        //    //base.Update();

        //    this.pProxySprite.Set(GameSprite.Name.NullObject);

        //    // Update the parent (bomb root)
        //    //GameObject pParent = (GameObject)this.pParent;

        //   // pParent.Remove(this);
        //    //pParent.Update();

        //    // Now remove from sprite batches
        //    //base.Remove();
        //}

        
    }
}
