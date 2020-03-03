using System.Diagnostics;

namespace SpaceInvaders
{
    public class SpriteNode : DLink
    {
        //SpriteNodes hold references to Sprites.  They are referenced in SpriteBatch collections.
        // Data: ----------------------------------------------
        private SpriteBase pSpriteBase;
        private SpriteNodeManager pBackSpriteNodeMan;

        public SpriteNode()
        : base()
        {
            this.pSpriteBase = null;
        }

        //public void Set(GameSprite.Name name)
        //{
        //    // Go find it
        //    this.pSpriteBase = GameSpriteManager.Find(name);
        //    Debug.Assert(this.pSpriteBase != null);
        //}

        //public void Set(BoxSprite.Name name)
        //{
        //    // Go find it
        //    this.pSpriteBase = BoxSpriteManager.Find(name);
        //    Debug.Assert(this.pSpriteBase != null);
        //}
        //public void Set(ProxySprite pNode)
        //{
        //    // associate it
        //    Debug.Assert(pNode != null);
        //    this.pSpriteBase = pNode;
        //}
        public void Set(SpriteBase pNode, SpriteNodeManager _pSpriteNodeMan)
        {
            Debug.Assert(pNode != null);
            this.pSpriteBase = pNode;

            // Set the back pointer
            // Allows easier deletion in the future
            Debug.Assert(pSpriteBase != null);
            this.pSpriteBase.SetSpriteNode(this);

            Debug.Assert(_pSpriteNodeMan != null);
            this.pBackSpriteNodeMan = _pSpriteNodeMan;
        }
        public SpriteBase GetSpriteBase()
        {
            return this.pSpriteBase;
        }
        public SpriteNodeManager GetSBNodeManager()
        {
            Debug.Assert(this.pBackSpriteNodeMan != null);
            return this.pBackSpriteNodeMan;
        }
        public SpriteBatch GetSpriteBatch()
        {
            Debug.Assert(this.pBackSpriteNodeMan != null);
            return this.pBackSpriteNodeMan.GetSpriteBatch();
        }
        public void Wash()
        {
            this.pSpriteBase = null;
        }
    }
}
