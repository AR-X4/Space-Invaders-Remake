using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ShieldFactory
    {

        // Data: ---------------------
        private SpriteBatch pSpriteBatch;
        private readonly SpriteBatch pCollisionSpriteBatch;
        private Composite pTree;

        public ShieldFactory(SpriteBatch.Name spriteBatchName, SpriteBatch.Name collisionSpriteBatch, Composite pRoot)
        {
            this.pSpriteBatch = SpriteBatchManager.Find(spriteBatchName);
            Debug.Assert(this.pSpriteBatch != null);

            this.pCollisionSpriteBatch = SpriteBatchManager.Find(collisionSpriteBatch);
            Debug.Assert(this.pCollisionSpriteBatch != null);
            
            this.pTree = pRoot;
        }
        public void SetParent(GameObject pParentNode)
        {
            // OK being null
            Debug.Assert(pParentNode != null);
            this.pTree = (Composite)pParentNode;
        }
        ~ShieldFactory()
        {
            this.pSpriteBatch = null;
        }
        public GameObject Create(GameObject.Name gameName, float posX = 0.0f, float posY = 0.0f)
        {
            GameObject pShield = null;

            switch (gameName)
            {
                case GameObject.Name.ShieldBrick:
                    pShield = new ShieldBrick(gameName, GameSprite.Name.ShieldBrick, posX, posY);
                    break;

                case GameObject.Name.ShieldBrick_LeftTop1:
                    pShield = new ShieldBrick(gameName, GameSprite.Name.ShieldBrick_LeftTop1, posX, posY);
                    break;

                case GameObject.Name.ShieldBrick_LeftTop0:
                    pShield = new ShieldBrick(gameName, GameSprite.Name.ShieldBrick_LeftTop0, posX, posY);
                    break;

                case GameObject.Name.ShieldBrick_LeftBottom:
                    pShield = new ShieldBrick(gameName, GameSprite.Name.ShieldBrick_LeftBottom, posX, posY);
                    break;

                case GameObject.Name.ShieldBrick_RightTop1:
                    pShield = new ShieldBrick(gameName, GameSprite.Name.ShieldBrick_RightTop1, posX, posY);
                    break;

                case GameObject.Name.ShieldBrick_RightTop0:
                    pShield = new ShieldBrick(gameName, GameSprite.Name.ShieldBrick_RightTop0, posX, posY);
                    break;

                case GameObject.Name.ShieldBrick_RightBottom:
                    pShield = new ShieldBrick(gameName, GameSprite.Name.ShieldBrick_RightBottom, posX, posY);
                    break;

                case GameObject.Name.ShieldColumn:
                    pShield = new ShieldColumn(gameName, GameSprite.Name.NullObject, posX, posY);
                    pShield.SetCollisionColor(1.0f, 0.0f, 0.0f);
                    break;

                case GameObject.Name.ShieldGrid:
                    pShield = new ShieldGrid(gameName, GameSprite.Name.NullObject, posX, posY);
                    pShield.SetCollisionColor(0.0f, 0.0f, 1.0f);
                    break;



                default:
                    // something is wrong
                    Debug.Assert(false);
                    break;
            }

            // add to the tree
         
            Debug.Assert(this.pTree != null);
            this.pTree.Add(pShield);
            
            // Attached to Group
            pShield.ActivateGameSprite(this.pSpriteBatch);
            pShield.ActivateCollisionSprite(this.pCollisionSpriteBatch);

            return pShield;
        }
    }
}

// End of File
