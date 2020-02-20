using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class CollisionPair : DLink
    {
        public enum Name
        {
            Alien_Missile,

            NullObject,
            Not_Initialized
        }

        // Data: ---------------
        public CollisionPair.Name name;
        public GameObject treeA;
        public GameObject treeB;

        public CollisionPair()
            : base()
        {
            this.treeA = null;
            this.treeB = null;
            this.name = CollisionPair.Name.Not_Initialized;
        }

        ~CollisionPair()
        {

        }
        public void Set(CollisionPair.Name colpairName, GameObject pTreeRootA, GameObject pTreeRootB)
        {
            Debug.Assert(pTreeRootA != null);
            Debug.Assert(pTreeRootB != null);

            this.treeA = pTreeRootA;
            this.treeB = pTreeRootB;
            this.name = colpairName;
        }
        public void Wash()
        {
            this.treeA = null;
            this.treeB = null;
            this.name = CollisionPair.Name.Not_Initialized;

        }

        public CollisionPair.Name GetName()
        {
            return this.name;
        }

        public void Process()
        {
            Collide(this.treeA, this.treeB);
        }

        static public void Collide(GameObject pSafeTreeA, GameObject pSafeTreeB)
        {
            // A vs B
            GameObject pNodeA = pSafeTreeA;
            GameObject pNodeB = pSafeTreeB;

            while (pNodeA != null)
            {
                // Restart compare
                pNodeB = pSafeTreeB;

                while (pNodeB != null)
                {
                    // who is being tested?
                    //Debug.WriteLine("ColPair:    test:  {0}, {1}", pNodeA.name, pNodeB.name);

                    // Get rectangles
                    CollisionRect rectA = pNodeA.poColObj.poColRect;
                    CollisionRect rectB = pNodeB.poColObj.poColRect;

                    // test them
                    if (CollisionRect.Intersect(rectA, rectB))
                    {
                        // Boom - it works (Visitor in Action)
                        pNodeA.Accept(pNodeB);
                        break;
                    }

                    pNodeB = (GameObject)Iterator.GetSibling(pNodeB);
                }

                pNodeA = (GameObject)Iterator.GetSibling(pNodeA);
            }
        }

        public void SetName(CollisionPair.Name inName)
        {
            this.name = inName;
        }

        public void Dump()
        {
            // TO DO ...
        }      
    }
}
