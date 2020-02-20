using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class GameObject : Component
    {
        public enum Name
        {
            PurpleOctopus,
            BlueCrab,
            GreenSquid,
            OrangeSaucer,

            AlienColumn,
            AlienGrid,

            Null_Object,
            Uninitialized
        }

        // Data: ---------------
        public GameObject.Name name;
        public float x;
        public float y;
        public ProxySprite pProxySprite;
        public CollisionObject poColObj;

        protected GameObject() 
        { 
        
        }
        protected GameObject(GameObject.Name gameName)
        {
            this.name = gameName;
            this.x = 0.0f;
            this.y = 0.0f;
            this.pProxySprite = null;
        }

        protected GameObject(GameObject.Name gameName, GameSprite.Name spriteName)
        {
            this.name = gameName;
            this.x = 0.0f;
            this.y = 0.0f;
            this.pProxySprite = new ProxySprite(spriteName);

            this.poColObj = new CollisionObject(this.pProxySprite);
            Debug.Assert(this.poColObj != null);
        }

        ~GameObject()
        {

        }

        protected void BaseUpdateBoundingBox(Component pStart)
        {
            GameObject pNode = (GameObject)pStart;

            // point to ColTotal
            CollisionRect ColTotal = this.poColObj.poColRect;

            // Get the first child
            pNode = (GameObject)Iterator.GetChild(pNode);

            // Initialized the union to the first block
            ColTotal.Set(pNode.poColObj.poColRect);

            // loop through sliblings
            while (pNode != null)
            {
                ColTotal.Union(pNode.poColObj.poColRect);

                // go to next sibling
                pNode = (GameObject)Iterator.GetSibling(pNode);
            }

            //this.poColObj.poColRect.Set(201, 201, 201, 201);
            this.x = this.poColObj.poColRect.x;
            this.y = this.poColObj.poColRect.y;

            //  Debug.WriteLine("x:{0} y:{1} w:{2} h:{3}", ColTotal.x, ColTotal.y, ColTotal.width, ColTotal.height);
        }

        public virtual void Update()
        {
            Debug.Assert(this.pProxySprite != null);
            this.pProxySprite.x = this.x;
            this.pProxySprite.y = this.y;

            Debug.Assert(this.poColObj != null);
            this.poColObj.UpdatePos(this.x, this.y);
            Debug.Assert(this.poColObj.pColSprite != null);
            this.poColObj.pColSprite.Update();
        }
        public void ActivateCollisionSprite(SpriteBatch pSpriteBatch)
        {
            Debug.Assert(pSpriteBatch != null);
            Debug.Assert(this.poColObj != null);
            pSpriteBatch.Attach(this.poColObj.pColSprite);
        }
        public void ActivateGameSprite(SpriteBatch pSpriteBatch)
        {
            Debug.Assert(pSpriteBatch != null);
            pSpriteBatch.Attach(this.pProxySprite);
        }

        public void Dump()
        {
            // Data:
            Debug.WriteLine("\t\t\t       name: {0} ({1})", this.name, this.GetHashCode());

            if (this.pProxySprite != null)
            {
                Debug.WriteLine("\t\t   pProxySprite: {0}", this.pProxySprite.name);
                Debug.WriteLine("\t\t    pRealSprite: {0}", this.pProxySprite.pSprite.GetName());
            }
            else
            {
                Debug.WriteLine("\t\t   pProxySprite: null");
                Debug.WriteLine("\t\t    pRealSprite: null");
            }
            Debug.WriteLine("\t\t\t      (x,y): {0}, {1}", this.x, this.y);

        }

        public GameObject.Name GetName()
        {
            return this.name;
        }
    }
}
