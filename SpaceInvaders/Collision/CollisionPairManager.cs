using System;
using System.Diagnostics;

namespace SpaceInvaders
{

    //---------------------------------------------------------------------------------------------------------
    // Design Notes:
    //
    //  Singleton class - use only public static methods for customers
    //
    //  * One single compare node is owned by this singleton - used for find, prevent extra news
    //  * Create one - NULL Object - Image Default
    //  * Dependency - TextureMan needs to be initialized before ImageMan
    //
    //---------------------------------------------------------------------------------------------------------


    public class CollisionPairManager : Manager
    {
        //----------------------------------------------------------------------
        // Data - unique data for this manager 
        //----------------------------------------------------------------------
        private static CollisionPairManager pInstance = null;
        private CollisionPair poNodeCompare;

        //----------------------------------------------------------------------
        // Constructor
        //----------------------------------------------------------------------
        private CollisionPairManager(int reserveNum = 3, int reserveGrow = 1)
        : base() // <--- Kick the can (delegate)
        {
            // At this point ImageMan is created, now initialize the reserve
            this.BaseInitialize(reserveNum, reserveGrow);

            // initialize derived data here
            this.poNodeCompare = new CollisionPair();
        }

        ~CollisionPairManager()
        {

        }

        //----------------------------------------------------------------------
        // Static Methods
        //----------------------------------------------------------------------
        public static void Create(int reserveNum = 3, int reserveGrow = 1)
        {
            // make sure values are ressonable 
            Debug.Assert(reserveNum > 0);
            Debug.Assert(reserveGrow > 0);

            // initialize the singleton here
            Debug.Assert(pInstance == null);

            // Do the initialization
            if (pInstance == null)
            {
                pInstance = new CollisionPairManager(reserveNum, reserveGrow);
            }

        }



        public static void Destroy()
        {
            // Get the instance

        }

        public static CollisionPair Add(CollisionPair.Name colpairName, GameObject treeRootA, GameObject treeRootB)
        {
            // Get the instance
            CollisionPairManager pMan = CollisionPairManager.GetInstance();
            Debug.Assert(pMan != null);

            // Go to Man, get a node from reserve, add to active, return it
            CollisionPair pColPair = (CollisionPair)pMan.BaseAdd();
            Debug.Assert(pColPair != null);

            // Initialize Image
            pColPair.Set(colpairName, treeRootA, treeRootB);

            return pColPair;
        }

        public static void Process()
        {
            // get the singleton
            CollisionPairManager pColPairMan = CollisionPairManager.GetInstance();

            CollisionPair pColPair = (CollisionPair)pColPairMan.BaseGetActive();

            while (pColPair != null)
            {
                // do the check for a single pair
                pColPair.Process();

                // advance to next
                pColPair = (CollisionPair)pColPair.pNext;
            }
        }

        public static CollisionPair Find(CollisionPair.Name name)
        {
            CollisionPairManager pMan = CollisionPairManager.GetInstance();
            Debug.Assert(pMan != null);

            // Compare functions only compares two Nodes

            // So:  Use the Compare Node - as a reference
            //      use in the Compare() function
            pMan.poNodeCompare.SetName(name);

            CollisionPair pData = (CollisionPair)pMan.BaseFind(pMan.poNodeCompare);
            return pData;
        }
        public static void Remove(CollisionPair pNode)
        {
            CollisionPairManager pMan = CollisionPairManager.GetInstance();
            Debug.Assert(pMan != null);

            Debug.Assert(pNode != null);
            pMan.BaseRemove(pNode);
        }
        public static void Dump()
        {
            CollisionPairManager pMan = CollisionPairManager.GetInstance();
            Debug.Assert(pMan != null);

            pMan.BaseDump();
        }

        //----------------------------------------------------------------------
        // Override Abstract methods
        //----------------------------------------------------------------------
        override protected DLink DerivedCreateNode()
        {
            DLink pNode = new CollisionPair();
            Debug.Assert(pNode != null);

            return pNode;
        }
        override protected Boolean DerivedCompareNode(DLink pLinkA, DLink pLinkB)
        {
            // This is used in baseFind() 
            Debug.Assert(pLinkA != null);
            Debug.Assert(pLinkB != null);

            CollisionPair pDataA = (CollisionPair)pLinkA;
            CollisionPair pDataB = (CollisionPair)pLinkB;

            Boolean status = false;

            if (pDataA.GetName() == pDataB.GetName())
            {
                status = true;
            }

            return status;
        }
        override protected void DerivedWashNode(DLink pLink)
        {
            Debug.Assert(pLink != null);
            CollisionPair pNode = (CollisionPair)pLink;
            pNode.Wash();
        }
        override protected void DerivedDumpNode(DLink pLink)
        {
            Debug.Assert(pLink != null);
            CollisionPair pData = (CollisionPair)pLink;
            pData.Dump();
        }

        //----------------------------------------------------------------------
        // Private methods
        //----------------------------------------------------------------------
        private static CollisionPairManager GetInstance()
        {
            // Safety - this forces users to call Create() first before using class
            Debug.Assert(pInstance != null);

            return pInstance;
        }

       
    }
}