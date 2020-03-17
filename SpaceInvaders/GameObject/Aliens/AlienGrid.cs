using System.Diagnostics;


namespace SpaceInvaders
{
    //Specific type of composite
    public class AlienGrid : Composite
    {
        // Data: ---------------
        private float delta;
        private float Y_Delta;
        private float MoveRate;
        private float RateChange;

        private AlienSubject poSubject;

        public AlienGrid(GameObject.Name name, GameSprite.Name spriteName, float posX, float posY)
        : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;

            this.Y_Delta = 0.0f;
            this.delta = 10.0f;

            this.MoveRate = 1.3f;

            this.RateChange = 0.025f;

            this.poColObj.pColSprite.SetLineColor(1, 0, 0);

            this.poSubject = new AlienSubject();
            Debug.Assert(this.poSubject != null);
        }
        public override void Accept(CollisionVisitor other)
        {
            // Important: at this point we have an BirdGroup
            // Call the appropriate collision reaction            
            other.VisitGroup(this);
        }
        public override void VisitMissileGroup(MissileGroup m)
        {
            // BirdGroup vs MissileGroup
            //Debug.WriteLine("         collide:  {0} <-> {1}", m.name, this.name);

            // MissileGroup vs Columns
            GameObject pGameObj = (GameObject)Iterator.GetChild(this);
            CollisionPair.Collide(m, pGameObj);
        }
        public override void VisitShieldRoot(ShieldRoot m)
        {
            // BirdGroup vs MissileGroup
            //Debug.WriteLine("         collide:  {0} <-> {1}", m.name, this.name);

            // MissileGroup vs Columns
            GameObject pGameObj = (GameObject)Iterator.GetChild(this);
            CollisionPair.Collide(m, pGameObj);
        }

        public override void Update()
        {
            base.BaseUpdateBoundingBox(this);

            base.Update();
        }
        public void MoveGrid()
        {
            //iterates through composite and moves by delta
            ForwardIterator pFor = new ForwardIterator(this);

            Component pNode = pFor.First();
            while (!pFor.IsDone())
            {
                GameObject pGameObj = (GameObject)pNode;

                pGameObj.x += this.delta;
                pGameObj.y += this.Y_Delta;
                
                pNode = pFor.Next();
            }

            this.SetYDelta(0.0f);
        }
        
        //public float GetDelta()
        //{
        //    return this.delta;
        //}

        public void SetDelta(float inDelta)
        {
            this.delta = inDelta;
        }
        public void SetYDelta(float inDelta)
        {
            this.Y_Delta = inDelta;
        }

        public void Attach(AlienObserver observer)
        {
            this.poSubject.Attach(observer);
        }
        public void NotifyListeners()
        {
            this.poSubject.Notify();
        }

        public void ResetAliens() {

            this.MoveRate = 1.3f;

            ForwardIterator pFor = new ForwardIterator(this);

            GameObject pGameObj;
            GameObject pParent;
            Component pNode = pFor.First();

            while (!pFor.IsDone())
            {
                pGameObj = (GameObject)pNode;

                pGameObj.pProxySprite.Set(pGameObj.pSpriteName);
                pGameObj.poColObj.poColRect.Set(pGameObj.pProxySprite.pSprite.GetScreenRect());
                pGameObj.ResetLocation();
                pGameObj.bMarkForDeath = false;

                pParent = (GameObject)pGameObj.pParent;
                if (pParent != null)
                {
                    pParent.Update();
                }
                pNode = pFor.Next();
            }

        }

        public float GetMoveRate() {
            return this.MoveRate;
        }
        public void SetMoveRate(float inRate) {
            this.MoveRate = inRate;
        }

        public float GetRateChange()
        {
            return this.RateChange;
        }
    }
}
