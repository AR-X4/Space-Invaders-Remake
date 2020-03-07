
using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class AlienFactory
    {
        // Data: ---------------------
        private SpriteBatch pSpriteBatch;
        private readonly SpriteBatch pCollisionSpriteBatch;

        public AlienFactory(SpriteBatch.Name spriteBatchName, SpriteBatch.Name collisionSpriteBatch)
        {
            this.pSpriteBatch = SpriteBatchManager.Find(spriteBatchName);
            Debug.Assert(this.pSpriteBatch != null);

            this.pCollisionSpriteBatch = SpriteBatchManager.Find(collisionSpriteBatch);
            Debug.Assert(this.pCollisionSpriteBatch != null);
        }

        ~AlienFactory()
        {
            this.pSpriteBatch = null;
        }

        public GameObject Create(GameObject.Name name, float posX = 0.0f, float posY = 0.0f)
        {
            GameObject pGameObj = null;

            switch (name)
            {
                case GameObject.Name.PurpleOctopus:
                    pGameObj = new PurpleOctopus(GameObject.Name.PurpleOctopus, GameSprite.Name.PurpleOctopus, posX, posY);
                    break;

                case GameObject.Name.BlueCrab:
                    pGameObj = new BlueCrab(GameObject.Name.BlueCrab, GameSprite.Name.BlueCrab, posX, posY);
                    break;

                case GameObject.Name.GreenSquid:
                    pGameObj = new GreenSquid(GameObject.Name.GreenSquid, GameSprite.Name.GreenSquid, posX, posY);
                    break;

                case GameObject.Name.OrangeSaucer:
                    pGameObj = new OrangeSaucer(GameObject.Name.OrangeSaucer, GameSprite.Name.OrangeSaucer, posX, posY);
                    break;

                case GameObject.Name.AlienColumn:
                    pGameObj = new AlienColumn(GameObject.Name.AlienColumn, GameSprite.Name.NullObject, 0.0f, 0.0f);
                    break;

                case GameObject.Name.AlienGrid:
                    pGameObj = new AlienGrid(GameObject.Name.AlienGrid, GameSprite.Name.NullObject, 0.0f, 0.0f);
                    break;

                default:
                    // something is wrong
                    Debug.Assert(false);
                    break;
            }
            Debug.Assert(pGameObj != null);

            // Attached to Group
            pGameObj.ActivateGameSprite(this.pSpriteBatch);
            pGameObj.ActivateCollisionSprite(this.pCollisionSpriteBatch);

            return pGameObj;
        }
    }
}
