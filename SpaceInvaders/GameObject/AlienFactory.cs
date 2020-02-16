
using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class AlienFactory
    {
        // Data: ---------------------
        SpriteBatch pSpriteBatch;

        public AlienFactory(SpriteBatch.Name spriteBatchName)
        {
            this.pSpriteBatch = SpriteBatchManager.Find(spriteBatchName);
            Debug.Assert(this.pSpriteBatch != null);
        }

        ~AlienFactory()
        {

        }

        public void Create(GameObject.Name type, float posX, float posY)
        {
            GameObject pGameObj = null;

            switch (type)
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

                default:
                    // something is wrong
                    Debug.Assert(false);
                    break;
            }

            // add it to the gameObjectManager
            Debug.Assert(pGameObj != null);
            GameObjectManager.Attach(pGameObj);

            // Attached to Group
            this.pSpriteBatch.Attach(pGameObj.pProxySprite);
        }
    }
}
