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
        private static CollisionPairManager pActiveMan = null;
        private static CollisionPair pActiveColPair;
        private static CollisionPair poNodeCompare;

        //----------------------------------------------------------------------
        // Constructor
        //----------------------------------------------------------------------
        public CollisionPairManager(int reserveNum = 3, int reserveGrow = 1)
        : base() // <--- Kick the can (delegate)
        {
            // At this point ImageMan is created, now initialize the reserve
            this.BaseInitialize(reserveNum, reserveGrow);

            // no link... used in Process
            //this.pActiveColPair = null;

            // initialize derived data here
            //this.poNodeCompare = new CollisionPair();
        }

        private CollisionPairManager() {
            CollisionPairManager.pActiveMan = null;
            CollisionPairManager.pActiveColPair = null;
            CollisionPairManager.poNodeCompare = new CollisionPair();
        }

        ~CollisionPairManager()
        {

        }

        //----------------------------------------------------------------------
        // Static Methods
        //----------------------------------------------------------------------
        

        public static void Create()
        {
            // initialize the singleton here
            Debug.Assert(pInstance == null);

            // Do the initialization
            if (pInstance == null)
            {
                pInstance = new CollisionPairManager();
            }
        }

        public static void Destroy()
        {
            // Get the instance

        }

        public static CollisionPair Add(CollisionPair.Name colpairName, GameObject treeRootA, GameObject treeRootB)
        {
            // Get the instance
            CollisionPairManager pMan = CollisionPairManager.pActiveMan;
            Debug.Assert(pMan != null);

            // Go to Man, get a node from reserve, add to active, return it
            CollisionPair pColPair = (CollisionPair)pMan.BaseAdd();
            Debug.Assert(pColPair != null);

            // Initialize Image
            pColPair.Set(colpairName, treeRootA, treeRootB);

            return pColPair;
        }

        public static void SetActive(CollisionPairManager pSBMan)
        {
            CollisionPairManager pMan = CollisionPairManager.GetInstance();
            Debug.Assert(pMan != null);

            Debug.Assert(pSBMan != null);
            CollisionPairManager.pActiveMan = pSBMan;
        }

        public static void Process()
        {
            // get the singleton
            CollisionPairManager pColPairMan = CollisionPairManager.pActiveMan;

            CollisionPair pColPair = (CollisionPair)pColPairMan.BaseGetActive();

            while (pColPair != null)
            {
                // set the current active  <--- Key concept: set this before
                CollisionPairManager.pActiveColPair = pColPair;

                // do the check for a single pair
                pColPair.Process();

                // advance to next
                pColPair = (CollisionPair)pColPair.pNext;
            }
        }

        public static CollisionPair Find(CollisionPair.Name name)
        {
            CollisionPairManager pMan = CollisionPairManager.pActiveMan;
            Debug.Assert(pMan != null);

            // Compare functions only compares two Nodes

            // So:  Use the Compare Node - as a reference
            //      use in the Compare() function
            CollisionPairManager.poNodeCompare.SetName(name);

            CollisionPair pData = (CollisionPair)pMan.BaseFind(CollisionPairManager.poNodeCompare);
            return pData;
        }
        public static void Remove(CollisionPair pNode)
        {
            CollisionPairManager pMan = CollisionPairManager.pActiveMan;
            Debug.Assert(pMan != null);

            Debug.Assert(pNode != null);
            pMan.BaseRemove(pNode);
        }
        public static void Dump()
        {
            CollisionPairManager pMan = CollisionPairManager.pActiveMan;
            Debug.Assert(pMan != null);

            pMan.BaseDump();
        }
        static public CollisionPair GetActiveColPair()
        {
            // get the singleton
            //CollisionPairManager pMan = CollisionPairManager.GetInstance();

            return CollisionPairManager.pActiveColPair;
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