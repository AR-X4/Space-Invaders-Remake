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
    //  * Create one - NULL Object - Sprite Default
    //  * Dependency - ImageMan needs to be initialized before SpriteMan
    //
    //---------------------------------------------------------------------------------------------------------
    
    public class ProxySpriteManager : Manager
    {

        //----------------------------------------------------------------------
        // Data - unique data for this manager 
        //----------------------------------------------------------------------
        private static ProxySpriteManager pInstance = null;
        private ProxySprite poNodeCompare;

        //----------------------------------------------------------------------
        // Constructor
        //----------------------------------------------------------------------
        private ProxySpriteManager(int reserveNum = 3, int reserveGrow = 1)
        : base() // <--- Kick the can (delegate)
        {
            // At this point ImageMan is created, now initialize the reserve
            this.BaseInitialize(reserveNum, reserveGrow);

            // initialize derived data here
            this.poNodeCompare = new ProxySprite();
        }

        ~ProxySpriteManager()
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
                pInstance = new ProxySpriteManager(reserveNum, reserveGrow);
            }
        }

        public static void Destroy()
        {

        }

        public static ProxySprite Add(GameSprite.Name name)
        {
            ProxySpriteManager pMan = ProxySpriteManager.GetInstance();
            Debug.Assert(pMan != null);

            ProxySprite pNode = (ProxySprite)pMan.BaseAdd();
            Debug.Assert(pNode != null);

            pNode.Set(name);

            return pNode;
        }
        public static ProxySprite Find(ProxySprite.Name name)
        {
            ProxySpriteManager pMan = ProxySpriteManager.GetInstance();
            Debug.Assert(pMan != null);

            // Compare functions only compares two Nodes

            // So:  Use the Compare Node - as a reference
            //      use in the Compare() function
            pMan.poNodeCompare.SetName(name);

            ProxySprite pData = (ProxySprite)pMan.BaseFind(pMan.poNodeCompare);
            return pData;
        }
        public static void Remove(GameSprite pNode)
        {
            ProxySpriteManager pMan = ProxySpriteManager.GetInstance();
            Debug.Assert(pMan != null);

            Debug.Assert(pNode != null);
            pMan.BaseRemove(pNode);
        }
        public static void Dump()
        {
            ProxySpriteManager pMan = ProxySpriteManager.GetInstance();
            Debug.Assert(pMan != null);

            pMan.BaseDump();
        }

        //----------------------------------------------------------------------
        // Override Abstract methods
        //----------------------------------------------------------------------
        override protected DLink DerivedCreateNode()
        {
            DLink pNode = new ProxySprite();
            Debug.Assert(pNode != null);

            return pNode;
        }
        override protected Boolean DerivedCompareNode(DLink pLinkA, DLink pLinkB)
        {
            // This is used in baseFind() 
            Debug.Assert(pLinkA != null);
            Debug.Assert(pLinkB != null);

            ProxySprite pDataA = (ProxySprite)pLinkA;
            ProxySprite pDataB = (ProxySprite)pLinkB;

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
            ProxySprite pNode = (ProxySprite)pLink;
            pNode.Wash();
        }
        override protected void DerivedDumpNode(DLink pLink)
        {
            Debug.Assert(pLink != null);
            ProxySprite pData = (ProxySprite)pLink;
            pData.Dump();
        }

        //----------------------------------------------------------------------
        // Private methods
        //----------------------------------------------------------------------
        private static ProxySpriteManager GetInstance()
        {
            // Safety - this forces users to call Create() first before using class
            Debug.Assert(pInstance != null);

            return pInstance;
        }
    }
}

