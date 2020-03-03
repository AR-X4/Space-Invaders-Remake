using System;
using System.Diagnostics;

namespace SpaceInvaders
{

    public class SpriteBatchManager : Manager
    {


        //----------------------------------------------------------------------
        // Data - unique data for this manager 
        //----------------------------------------------------------------------
        private static SpriteBatchManager pInstance = null;
        private SpriteBatch poNodeCompare;

       
        private SpriteBatchManager(int reserveNum = 3, int reserveGrow = 1)
        : base() // <--- Kick the can (delegate)
        {
            // At this point SpriteBatchMan is created, now initialize the reserve
            this.BaseInitialize(reserveNum, reserveGrow);

            // initialize derived data here
            this.poNodeCompare = new SpriteBatch();
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
                pInstance = new SpriteBatchManager(reserveNum, reserveGrow);
            }
        }
        public static void Destroy()
        {
            SpriteBatchManager pMan = SpriteBatchManager.GetInstance();
            Debug.Assert(pMan != null);

            // Do something clever here
            // track peak number of active nodes
            // print stats on destroy
            // invalidate the singleton

        }

        public static SpriteBatch Add(SpriteBatch.Name name, uint priority, int reserveNum = 3, int reserveGrow = 1)
        {
            SpriteBatchManager pMan = SpriteBatchManager.GetInstance();
            Debug.Assert(pMan != null);

            SpriteBatch pNode = (SpriteBatch)pMan.BaseSortedAdd(priority);
            Debug.Assert(pNode != null);

            pNode.Set(name, reserveNum, reserveGrow);
            return pNode;
        }

        public static void Draw()
        {
            SpriteBatchManager pMan = SpriteBatchManager.GetInstance();
            Debug.Assert(pMan != null);

            // walk through the list and render
            SpriteBatch pSpriteBatch = (SpriteBatch)pMan.BaseGetActive();

            while (pSpriteBatch != null)
            {
                SpriteNodeManager pSBNodeMan = pSpriteBatch.GetSpriteNodeManager();
                Debug.Assert(pSBNodeMan != null);

                // Kick the can
                pSBNodeMan.Draw();

                pSpriteBatch = (SpriteBatch)pSpriteBatch.pNext;
            }

        }

        public static SpriteBatch Find(SpriteBatch.Name name)
        {
            SpriteBatchManager pMan = SpriteBatchManager.GetInstance();
            Debug.Assert(pMan != null);

            // Compare functions only compares two Nodes

            // So:  Use the Compare Node - as a reference
            //      use in the Compare() function
            pMan.poNodeCompare.name = name;

            SpriteBatch pData = (SpriteBatch)pMan.BaseFind(pMan.poNodeCompare);
            return pData;
        }
        public static void Remove(SpriteBatch pNode)
        {
            SpriteBatchManager pMan = SpriteBatchManager.GetInstance();
            Debug.Assert(pMan != null);

            Debug.Assert(pNode != null);
            pMan.BaseRemove(pNode);
        }

        public static void Remove(SpriteNode pSpriteBatchNode)
        {
            Debug.Assert(pSpriteBatchNode != null);
            SpriteNodeManager pSpriteNodeMan = pSpriteBatchNode.GetSBNodeManager();

            Debug.Assert(pSpriteNodeMan != null);
            pSpriteNodeMan.Remove(pSpriteBatchNode);
        }
        public static void Dump()
        {
            SpriteBatchManager pMan = SpriteBatchManager.GetInstance();
            Debug.Assert(pMan != null);

            pMan.BaseDump();
        }

        //----------------------------------------------------------------------
        // Override Abstract methods
        //----------------------------------------------------------------------
        override protected DLink DerivedCreateNode()
        {
            DLink pNode = new SpriteBatch();
            Debug.Assert(pNode != null);

            return pNode;
        }
        override protected Boolean DerivedCompareNode(DLink pLinkA, DLink pLinkB)
        {
            // This is used in baseFind() 
            Debug.Assert(pLinkA != null);
            Debug.Assert(pLinkB != null);

            SpriteBatch pDataA = (SpriteBatch)pLinkA;
            SpriteBatch pDataB = (SpriteBatch)pLinkB;

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
            SpriteBatch pNode = (SpriteBatch)pLink;
            pNode.Wash();
        }
        override protected void DerivedDumpNode(DLink pLink)
        {
            Debug.Assert(pLink != null);
            SpriteBatch pData = (SpriteBatch)pLink;
            pData.Dump();
        }

        //----------------------------------------------------------------------
        // Private methods
        //----------------------------------------------------------------------
        private static SpriteBatchManager GetInstance()
        {
            // Safety - this forces users to call Create() first before using class
            Debug.Assert(pInstance != null);

            return pInstance;
        }

        
    }
}
