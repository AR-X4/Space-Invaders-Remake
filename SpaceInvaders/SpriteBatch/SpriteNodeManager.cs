using System.Diagnostics;

namespace SpaceInvaders
{

    //---------------------------------------------------------------------------------------------------------
    // Design Notes:
    //---------------------------------------------------------------------------------------------------------
    public class SpriteNodeManager : Manager
    {

        //----------------------------------------------------------------------
        // Data - unique data for this manager 
        //----------------------------------------------------------------------
        private SpriteBatch.Name name;
        private readonly SpriteNode poNodeCompare;


        //----------------------------------------------------------------------
        // Constructor
        //----------------------------------------------------------------------
        public SpriteNodeManager(int reserveNum = 3, int reserveGrow = 1)
        : base() // <--- Kick the can (delegate)
        {
            // At this point SBMan is created, now initialize the reserve
            this.BaseInitialize(reserveNum, reserveGrow);

            // initialize derived data here
            this.poNodeCompare = new SpriteNode();
        }

        //----------------------------------------------------------------------
        // Public Methods
        //----------------------------------------------------------------------
        public void Set(SpriteBatch.Name name, int reserveNum, int reserveGrow)
        {
            this.name = name;

            Debug.Assert(reserveNum > 0);
            Debug.Assert(reserveGrow > 0);

            this.BaseSetReserve(reserveNum, reserveGrow);
        }
        public SpriteNode Attach(GameSprite.Name name)
        {
            SpriteNode pNode = (SpriteNode)this.BaseAdd();
            Debug.Assert(pNode != null);

            // Initialize SpriteBatchNode
            pNode.Set(name);

            return pNode;
        }
        public SpriteNode Attach(BoxSprite.Name name)
        {
            SpriteNode pNode = (SpriteNode)this.BaseAdd();
            Debug.Assert(pNode != null);

            // Initialize SpriteBatchNode
            pNode.Set(name);

            return pNode;
        }
        public SpriteNode Attach(ProxySprite pNode)
        {
            // Go to Man, get a node from reserve, add to active, return it
            SpriteNode pSpriteNode = (SpriteNode)this.BaseAdd();
            Debug.Assert(pSpriteNode != null);

            // Initialize SpriteBatchNode
            pSpriteNode.Set(pNode);

            return pSpriteNode;
        }

        public void Draw()
        {
            // walk through the list and render
            SpriteNode pNode = (SpriteNode)this.BaseGetActive();

            while (pNode != null)
            {
                // Assumes someone before here called update() on each sprite
                // Draw me.
                pNode.GetSpriteBase().Render();

                pNode = (SpriteNode)pNode.pNext;
            }
        }
        public void Remove(SpriteNode pNode)
        {
            Debug.Assert(pNode != null);
            this.BaseRemove(pNode);
        }
        public void Dump()
        {
            this.BaseDump();
        }

        //----------------------------------------------------------------------
        // Override Abstract methods
        //----------------------------------------------------------------------
        override protected DLink DerivedCreateNode()
        {
            DLink pNode = new SpriteNode();
            Debug.Assert(pNode != null);

            return pNode;
        }
        override protected bool DerivedCompareNode(DLink pLinkA, DLink pLinkB)
        {
            // This is used in baseFind() 
            Debug.Assert(pLinkA != null);
            Debug.Assert(pLinkB != null);

            SpriteNode pDataA = (SpriteNode)pLinkA;
            SpriteNode pDataB = (SpriteNode)pLinkB;

            bool status = false;

            // Stubbed this function out
            if (pLinkB == pLinkA)
            {
                status = false;
            }
            else
            {
                status = false;
            }

            return status;
        }
        override protected void DerivedWashNode(DLink pLink)
        {
            Debug.Assert(pLink != null);
            SpriteNode pNode = (SpriteNode)pLink;
            pNode.Wash();
        }
        override protected void DerivedDumpNode(DLink pLink)
        {
            Debug.Assert(pLink != null);
            SpriteNode pData = (SpriteNode)pLink;
            // pData.Dump();
        }
    }
}
