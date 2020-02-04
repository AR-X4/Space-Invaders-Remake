using System.Diagnostics;

namespace SpaceInvaders
{
    public class GameSpriteManager : Manager
    {
     
        private static GameSpriteManager pInstance = null;
        private readonly GameSprite poNodeCompare;

        private GameSpriteManager(int reserveNum = 3, int reserveGrow = 1)
        : base() 
        {
            // At this point ImageMan is created, now initialize the reserve
            this.BaseInitialize(reserveNum, reserveGrow);

            // initialize derived data here
            this.poNodeCompare = new GameSprite();
        }

        //----------------------------------------------------------------------
        // Static Methods
        //----------------------------------------------------------------------
        public static void Create(int reserveNum = 3, int reserveGrow = 1)
        {
            // make sure values are ressonable 
            Debug.Assert(reserveNum > 0);
            Debug.Assert(reserveGrow > 0);

            // initialize the singleton here
            Debug.Assert(pInstance == null);

            // Do the initialization
            if (pInstance == null)
            {
                pInstance = new GameSpriteManager(reserveNum, reserveGrow);
            }
        }
        public static void Destroy()
        {
            GameSpriteManager pMan = GameSpriteManager.privGetInstance();
            Debug.Assert(pMan != null);

      
        }
        public static GameSprite Add(GameSprite.Name name, Image.Name ImageName, float x, float y, float width, float height, Azul.Color pColor = null)
        {
            GameSpriteManager pMan = GameSpriteManager.privGetInstance();
            Debug.Assert(pMan != null);

            GameSprite pNode = (GameSprite)pMan.BaseAdd();
            Debug.Assert(pNode != null);

            // Initialize the data
            Image pImage = ImageManager.Find(ImageName);
            Debug.Assert(pImage != null);

            pNode.Set(name, pImage, x, y, width, height, pColor);

            return pNode;
        }
        public static GameSprite Find(GameSprite.Name name)
        {
            GameSpriteManager pMan = GameSpriteManager.privGetInstance();
            Debug.Assert(pMan != null);

          
            pMan.poNodeCompare.name = name;

            GameSprite pData = (GameSprite)pMan.BaseFind(pMan.poNodeCompare);
            return pData;
        }
        public static void Remove(GameSprite pNode)
        {
            GameSpriteManager pMan = GameSpriteManager.privGetInstance();
            Debug.Assert(pMan != null);

            Debug.Assert(pNode != null);
            pMan.BaseRemove(pNode);
        }
        public static void Dump()
        {
            GameSpriteManager pMan = GameSpriteManager.privGetInstance();
            Debug.Assert(pMan != null);

            pMan.BaseDump();
        }

        //----------------------------------------------------------------------
        // Override Abstract methods
        //----------------------------------------------------------------------
        override protected DLink DerivedCreateNode()
        {
            DLink pNode = new GameSprite();
            Debug.Assert(pNode != null);

            return pNode;
        }
        override protected bool DerivedCompareNode(DLink pLinkA, DLink pLinkB)
        {
            // This is used in baseFind() 
            Debug.Assert(pLinkA != null);
            Debug.Assert(pLinkB != null);

            GameSprite pDataA = (GameSprite)pLinkA;
            GameSprite pDataB = (GameSprite)pLinkB;

            bool status = false;

            if (pDataA.name == pDataB.name)
            {
                status = true;
            }

            return status;
        }
        override protected void DerivedWashNode(DLink pLink)
        {
            Debug.Assert(pLink != null);
            GameSprite pNode = (GameSprite)pLink;
            pNode.Wash();
        }
        override protected void DerivedDumpNode(DLink pLink)
        {
            Debug.Assert(pLink != null);
            GameSprite pData = (GameSprite)pLink;
            pData.Dump();
        }

        //----------------------------------------------------------------------
        // Private methods
        //----------------------------------------------------------------------
        private static GameSpriteManager privGetInstance()
        {
            // Safety - this forces users to call Create() first before using class
            Debug.Assert(pInstance != null);

            return pInstance;
        }

        
    }
}
