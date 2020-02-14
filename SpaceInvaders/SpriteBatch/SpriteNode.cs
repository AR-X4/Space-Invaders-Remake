﻿using System.Diagnostics;

namespace SpaceInvaders
{
    public class SpriteNode : DLink
    {
        //SpriteNodes hold references to Sprites.  They are referenced in SpriteBatch collections.
        // Data: ----------------------------------------------
        public SpriteBase pSpriteBase;

        public SpriteNode()
        : base()
        {
            this.pSpriteBase = null;
        }

        public void Set(GameSprite.Name name)
        {
            // Go find it
            this.pSpriteBase = GameSpriteManager.Find(name);
            Debug.Assert(this.pSpriteBase != null);
        }

        public void Set(BoxSprite.Name name)
        {
            // Go find it
            this.pSpriteBase = BoxSpriteManager.Find(name);
            Debug.Assert(this.pSpriteBase != null);
        }

        public void Wash()
        {
            this.pSpriteBase = null;
        }
    }
}