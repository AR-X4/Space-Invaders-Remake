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

            UFORoot,
            AlienColumn,
            AlienGrid,

            WallGroup,
            WallRight,
            WallLeft,
            WallTop,
            WallBottom,

            Ship,
            ShipRoot,
            Missile,
            MissileGroup,

            Bomb,
            BombRoot,

            ShieldBrick,
            ShieldBrick_LeftTop0,
            ShieldBrick_LeftTop1,
            ShieldBrick_LeftBottom,
            ShieldBrick_RightTop0,
            ShieldBrick_RightTop1,
            ShieldBrick_RightBottom,
            ShieldColumn,
            ShieldGrid,
            ShieldRoot,


            Null_Object,
            Uninitialized
        }

        // Data: ---------------
        public GameObject.Name name;
        public float x;
        public float y;
        private readonly float xStartCopy;
        private readonly float yStartCopy;
        public ProxySprite pProxySprite;
        public CollisionObject poColObj;
        public GameSprite.Name pSpriteName;
        public bool bMarkForDeath;
        private char pad0;
        private char pad1;
        private char pad2;
        private int  pad3;


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

        protected GameObject(GameObject.Name gameName, GameSprite.Name spriteName, float posX = 0.0f, float posY = 0.0f)
        {
            this.name = gameName;
            this.x = posX;
            this.y = posY;

            this.xStartCopy = posX;
            this.yStartCopy = posY;

            this.bMarkForDeath = false;
            this.pProxySprite = new ProxySprite(spriteName, posX, posY);

            this.pSpriteName = spriteName;
            

            this.poColObj = new CollisionObject(this.pProxySprite);
            Debug.Assert(this.poColObj != null);
        }

        ~GameObject()
        {

        }

        public virtual void Remove()
        {
            // Keenan(delete.A)
            // -----------------------------------------------------------------
            // Very difficult at first... if you are messy, you will pay here!
            // Given a game object....
            // -----------------------------------------------------------------

            // Remove proxy sprite node from SpriteBatch manager
            Debug.Assert(this.pProxySprite != null);
            SpriteNode pSpriteNode = this.pProxySprite.GetSpriteNode();
            
            Debug.Assert(pSpriteNode != null);
            SpriteBatchManager.Remove(pSpriteNode);

            // Remove collision sprite node from spriteBatch manager
            Debug.Assert(this.poColObj != null);
            Debug.Assert(this.poColObj.pColSprite != null);
            pSpriteNode = this.poColObj.pColSprite.GetSpriteNode();

            Debug.Assert(pSpriteNode != null);
            SpriteBatchManager.Remove(pSpriteNode);

            // Remove game object node from GameObjectMan
            //Remove from parent composite in derived class remove() instead
            //never need to remove composites from GameObject Manager...right?
            
            //GameObjectManager.Remove(this);
            

        }

        protected void BaseUpdateBoundingBox(Component pStart)
        {
            GameObject pNode = (GameObject)pStart;

            // point to ColTotal
            CollisionRect ColTotal = this.poColObj.poColRect;

            // Get the first child
            pNode = (GameObject)Iterator.GetChild(pNode);

            if (pNode != null)
            {
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

        public void SetCollisionColor(float red, float green, float blue)
        {
            Debug.Assert(this.poColObj != null);
            Debug.Assert(this.poColObj.pColSprite != null);

            this.poColObj.pColSprite.SetLineColor(red, green, blue);
        }

        public CollisionObject GetColObject()
        {
            Debug.Assert(this.poColObj != null);
            return this.poColObj;
        }

        public GameObject.Name GetName()
        {
            return this.name;
        }

        public void ResetLocation() {
            this.x = this.xStartCopy;
            this.y = this.yStartCopy;
        }

        //public void SetGameObjectNode(GameObjectNode pGameObjectNode)
        //{
        //    Debug.Assert(pGameObjectNode != null);
        //    this.pBackGameObjectNode = pGameObjectNode;
        //}
    }
}
