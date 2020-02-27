using System.Diagnostics;


namespace SpaceInvaders
{
    //Specific type of composite
    public class AlienGrid : Composite
    {
        // Data: ---------------
        private float delta;

        public AlienGrid(GameObject.Name name, GameSprite.Name spriteName, float posX, float posY)
        : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;

            this.poColObj.pColSprite.SetLineColor(1, 0, 0);
        }
        public override void Accept(CollisionVisitor other)
        {
            // Important: at this point we have an BirdGroup
            // Call the appropriate collision reaction            
            other.VisitGroup(this);
        }
        public override void VisitMissile(Missile m)
        {
            
            Debug.WriteLine("         collide:  {0} <-> {1}", m.name, this.name);

            // Missile vs Alien
            Debug.WriteLine("-------> Done  <--------");


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

                pNode = pFor.Next();
            }
        }
        public float GetDelta()
        {
            return this.delta;
        }

        public void SetDelta(float inDelta)
        {
            this.delta = inDelta;
        }
    }
}
