using System.Diagnostics;

namespace SpaceInvaders
{
    public class BoxSpriteManager : Manager
    {
        private static BoxSpriteManager pInstance = null;
        private readonly BoxSprite poNodeCompare;

        private BoxSpriteManager(int reserveNum = 3, int reserveGrow = 1)
        : base() 
        {
            this.BaseInitialize(reserveNum, reserveGrow);
            this.poNodeCompare = new BoxSprite();
        }

        //----------------------------------------------------------------------
        // Methods
        //----------------------------------------------------------------------
        public static void Create(int reserveNum = 3, int reserveGrow = 1)
        {
            // make sure values are ressonable 
            Debug.Assert(reserveNum > 0);
            Debug.Assert(reserveGrow > 0);

           
            Debug.Assert(pInstance == null);

            // Do the initialization
            if (pInstance == null)
            {
                pInstance = new BoxSpriteManager(reserveNum, reserveGrow);
            }
        }
        public static void Destroy()
        {
            BoxSpriteManager pMan = BoxSpriteManager.privGetInstance();
            Debug.Assert(pMan != null);

            // Do something clever here
            // track peak number of active nodes
            // print stats on destroy
            // invalidate the singleton
        }
        public static BoxSprite Add(BoxSprite.Name name, float x, float y, float width, float height, Azul.Color pColor = null)
        {
            BoxSpriteManager pMan = BoxSpriteManager.privGetInstance();
            Debug.Assert(pMan != null);

            BoxSprite pNode = (BoxSprite)pMan.BaseAdd();
            Debug.Assert(pNode != null);

            pNode.Set(name, x, y, width, height, pColor);

            return pNode;
        }
        public static BoxSprite Find(BoxSprite.Name name)
        {
            BoxSpriteManager pMan = BoxSpriteManager.privGetInstance();
            Debug.Assert(pMan != null);

            // Compare functions only compares two Nodes

            // So:  Use the Compare Node - as a reference
            //      use in the Compare() function
            pMan.poNodeCompare.name = name;

            BoxSprite pData = (BoxSprite)pMan.BaseFind(pMan.poNodeCompare);
            return pData;
        }
        public static void Remove(BoxSprite pNode)
        {
            BoxSpriteManager pMan = BoxSpriteManager.privGetInstance();
            Debug.Assert(pMan != null);

            Debug.Assert(pNode != null);
            pMan.BaseRemove(pNode);
        }
        public static void Dump()
        {
            BoxSpriteManager pMan = BoxSpriteManager.privGetInstance();
            Debug.Assert(pMan != null);

            pMan.BaseDump();
        }

        override protected DLink DerivedCreateNode()
        {
            DLink pNode = new BoxSprite();
            Debug.Assert(pNode != null);

            return pNode;
        }
        override protected bool DerivedCompareNode(DLink pLinkA, DLink pLinkB)
        {
            // This is used in baseFind() 
            Debug.Assert(pLinkA != null);
            Debug.Assert(pLinkB != null);

            BoxSprite pDataA = (BoxSprite)pLinkA;
            BoxSprite pDataB = (BoxSprite)pLinkB;

            bool status = false;

            if (pDataA.name == pDataB.name)
            {
                status = true;
            }

            return status;
        }
        override protected void DerivedWashNode(DLink pLink)
        {
            Debug.Assert(pLink != null);
            BoxSprite pNode = (BoxSprite)pLink;
            pNode.Wash();
        }
        override protected void DerivedDumpNode(DLink pLink)
        {
            Debug.Assert(pLink != null);
            BoxSprite pData = (BoxSprite)pLink;
            pData.Dump();
        }

        private static BoxSpriteManager privGetInstance()
        {
            // Safety - this forces users to call Create() first before using class
            Debug.Assert(pInstance != null);

            return pInstance;
        }

        
    }
}
