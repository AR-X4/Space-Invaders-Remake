using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    //SpriteBatch is a custom linked list that holds SpriteNodes
    public class SpriteBatch : DLink
    {
        public enum Name
        {
            Aliens,
            Shields,
            Boxes,
            Texts,
            NullObjects,
            Uninitialized
        }

        // Data -------------------------------
        public SpriteBatch.Name name;
        private readonly SpriteNodeManager pSpriteNodeManager;
        private bool bDraw;
        private char pad0;
        private char pad1;
        private char pad2;
        private int  pad3;


        public SpriteBatch()
            : base()
        {
            this.name = SpriteBatch.Name.Uninitialized;
            this.pSpriteNodeManager = new SpriteNodeManager();
            Debug.Assert(this.pSpriteNodeManager != null);

            this.bDraw = true;
        }

        public void Set(SpriteBatch.Name name, int reserveNum = 3, int reserveGrow = 1)
        {
            this.name = name;
            this.pSpriteNodeManager.Set(name, reserveNum, reserveGrow);
        }

        public void SetDrawBool(bool bDraw) {
            this.bDraw = bDraw;
        }
        public bool GetDrawBool() {
            return this.bDraw;
        }

        public void Attach(SpriteBase pNode)
        {
            Debug.Assert(pNode != null);

            // Go to Man, get a node from reserve, add to active, return it
            SpriteNode pSpriteNode = (SpriteNode)this.pSpriteNodeManager.Attach(pNode);
            Debug.Assert(pSpriteNode != null);

            // Initialize SpriteBatchNode
            pSpriteNode.Set(pNode, this.pSpriteNodeManager);

            // Back pointer
            this.pSpriteNodeManager.SetSpriteBatch(this);
        }

        public void Wash()
        {
        }

        public void Dump()
        {
            // Dump - Print contents to the debug output window
            //        Using HASH code as its unique identifier 
            Debug.WriteLine("   Name: {0} ({1})", this.name, this.GetHashCode());
            Debug.WriteLine("             Priority: " + this.Priority);
            
            if (this.pNext == null)
            {
                Debug.WriteLine("              next: null");
            }
            else
            {
                SpriteBatch pTmp = (SpriteBatch)this.pNext;
                Debug.WriteLine("              next: {0} ({1})", pTmp.name, pTmp.GetHashCode());
            }

            if (this.pPrev == null)
            {
                Debug.WriteLine("              prev: null");
            }
            else
            {
                SpriteBatch pTmp = (SpriteBatch)this.pPrev;
                Debug.WriteLine("              prev: {0} ({1})", pTmp.name, pTmp.GetHashCode());
            }
        }
    

        public void SetName(SpriteBatch.Name inName)
        {
            this.name = inName;
        }

        public SpriteBatch.Name GetName()
        {
            return this.name;
        }

        public SpriteNodeManager GetSpriteNodeManager()
        {
            return this.pSpriteNodeManager;
        }

    }
}
