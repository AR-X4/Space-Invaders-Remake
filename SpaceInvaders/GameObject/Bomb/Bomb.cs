
using System.Diagnostics;

namespace SpaceInvaders
{
    public class Bomb : Leaf
    {

        // Data
        public float delta;
        public AlienColumn pOwner;
        public FallStrategy pStrategy;
        public FallStrategy pDaggers;
        public FallStrategy pZigZag;

        public Bomb(AlienColumn pOwner)
            : base(GameObject.Name.Bomb, GameSprite.Name.NullObject)
        {
            this.x = pOwner.x;
            this.y = pOwner.GetBottom();
            this.delta = 0.0f;

            this.pOwner = pOwner;

            this.pDaggers = new FallDagger();
            this.pZigZag = new FallZigZag();
            this.pStrategy = null;

            this.poColObj.pColSprite.SetLineColor(1, 1, 0);

        }

        public void ResetBomb() {
            this.x = this.pOwner.x;
            this.y = this.pOwner.GetBottom();

            this.delta = 0.0f;

            
            this.pProxySprite.Set(GameSprite.Name.NullObject);
            

            

            //this.pStrategy.Reset(this.y);
            //base.Update();
        }

        

        //public override void Remove()
        //{
        //    // Since the Root object is being drawn
        //    // 1st set its size to zero
        //    this.poColObj.poColRect.Set(0, 0, 0, 0);
        //    base.Update();

        //    // Update the parent (bomb root)
        //    GameObject pParent = (GameObject)this.pParent;

        //    pParent.Remove(this);
        //    pParent.Update();

        //    // Now remove from sprite batches
        //    base.Remove();
        //}
        public override void Update()
        {
            if (this.pProxySprite.pSprite.name == GameSprite.Name.NullObject)
            {
                base.Update();
                this.ResetBomb();

            }
            else
            {

                base.Update();
                this.y -= delta;

                // Strategy
                if (this.pStrategy != null)//????
                {
                    this.pStrategy.Fall(this);
                }
            }
        }
        public float GetBoundingBoxHeight()
        {
            return this.poColObj.poColRect.height;
        }
        ~Bomb()
        {
        }
        public override void Accept(CollisionVisitor other)
        {
            // Important: at this point we have an Alien
            // Call the appropriate collision reaction            
            other.VisitBomb(this);
        }
        public void MultiplyScale(float sx, float sy)
        {
            Debug.Assert(this.pProxySprite != null);

            this.pProxySprite.sx *= sx;
            this.pProxySprite.sy *= sy;
        }

    }
}

// End of File
