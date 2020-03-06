
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

        public Bomb()
            : base(GameObject.Name.Bomb, GameSprite.Name.NullObject)
        {
            this.x = 0.0f;
            this.y = 0.0f;
            this.delta = 0.5f;
            //bombs are reused and these news are called once per bomb... must have strategy instances for each bomb
            this.pDaggers = new FallDagger();
            this.pZigZag = new FallZigZag();
            this.pStrategy = null;

        }

        public void SetBomb(AlienColumn pOwner, float posX, float posY) {
            this.x = posX;
            this.y = posY;

            this.pOwner = pOwner;
            this.bMarkForDeath = false;

            BombManager.RandomizeBombType(this);

            Debug.Assert(this.poColObj != null);
            this.poColObj.poColRect.Set(this.pProxySprite.pSprite.GetScreenRect());
            this.poColObj.pColSprite.SetLineColor(1, 1, 0);

            this.pStrategy.Reset(this.y);
            base.Update();
        }

        public override void Remove()
        {
            // Since the Root object is being drawn
            // 1st set its size to zero
            this.poColObj.poColRect.Set(0, 0, 0, 0);
            base.Update();

            // Update the parent (bomb root)
            GameObject pParent = (GameObject)this.pParent;
           
            pParent.Remove(this);
            pParent.Update();

            // Now remove from sprite batches
            base.Remove();
        }
        public override void Update()
        {
            base.Update();
            this.y -= delta;

            // Strategy
            this.pStrategy.Fall(this);
            
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
