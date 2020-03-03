using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class CollisionPair : DLink
    {
        public enum Name
        {
            Alien_Missile,
            Alien_Wall,
            Missile_Wall,
            Ship_Wall,
            Bomb_Wall,

            NullObject,
            Not_Initialized
        }

        // Data: ---------------
        public CollisionPair.Name name;
        public GameObject treeA;
        public GameObject treeB;
        public CollisionSubject poSubject;

        public CollisionPair()
            : base()
        {
            this.treeA = null;
            this.treeB = null;
            this.name = CollisionPair.Name.Not_Initialized;

            this.poSubject = new CollisionSubject();
            Debug.Assert(this.poSubject != null);
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
                    CollisionRect rectA = pNodeA.GetColObject().poColRect;
                    CollisionRect rectB = pNodeB.GetColObject().poColRect;

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
        public void Attach(CollisionObserver observer)
        {
            this.poSubject.Attach(observer);
        }
        public void NotifyListeners()
        {
            this.poSubject.Notify();
        }
        public void SetCollision(GameObject pObjA, GameObject pObjB)
        {
            Debug.Assert(pObjA != null);
            Debug.Assert(pObjB != null);

            // GameObject pAlien = AlienCategory.GetAlien(objA, objB);
            this.poSubject.pObjA = pObjA;
            this.poSubject.pObjB = pObjB;
        }
        public void Dump()
        {
            // TO DO ...
        }      
    }
}
