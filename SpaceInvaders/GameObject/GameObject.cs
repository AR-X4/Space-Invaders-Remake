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

            Null_Object,
            Uninitialized
        }

        // Data: ---------------
        public GameObject.Name name;
        public float x;
        public float y;
        public ProxySprite pProxySprite;

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
        }

        ~GameObject()
        {

        }

        public virtual void Update()
        {
            Debug.Assert(this.pProxySprite != null);
            this.pProxySprite.x = this.x;
            this.pProxySprite.y = this.y;
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
