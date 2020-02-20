using System.Diagnostics;

namespace SpaceInvaders
{
    class TextureManager : Manager
    {
        private readonly Texture pNodeCompare;
        private static TextureManager pInstance = null;//for singleton pattern

        private TextureManager(int reserveNum = 1, int reserveGrow = 1)
            : base()
        {
            base.BaseInitialize(reserveNum, reserveGrow);

            this.pNodeCompare = new Texture();
            //this.pInstance = null;
        }

        //Methods

        public static void Create(int reserveNum = 1, int reserveGrow = 1)
        {
            // make sure values are ressonable 
            Debug.Assert(reserveNum > 0);
            Debug.Assert(reserveGrow > 0);

            // initialize the singleton here
            Debug.Assert(pInstance == null);

            // Do the initialization
            if (pInstance == null)
            {
                pInstance = new TextureManager(reserveNum, reserveGrow);
            }

            // NullObject texture
            Texture pTexture = TextureManager.Add(Texture.Name.NullObject, "HotPink.tga");
            Debug.Assert(pTexture != null);
            // Default texture
            pTexture = TextureManager.Add(Texture.Name.Default, "HotPink.tga");
            Debug.Assert(pTexture != null);
        }

        public static void Destroy()
        {
            TextureManager pMan = TextureManager.GetInstance();
            Debug.Assert(pMan != null);

            // Do something clever here
            // track peak number of active nodes
            // print stats on destroy
            // invalidate the singleton
        }

        public static Texture Add(Texture.Name name, string pTextureName)
        {

            TextureManager pMan = TextureManager.GetInstance();
            Debug.Assert(pMan != null);

            Texture pNode = (Texture)pMan.BaseAdd();
            Debug.Assert(pNode != null);

            // Initialize the data
            Debug.Assert(pTextureName != null);
            pNode.Set(name, pTextureName);

            return pNode;
        }
        public static Texture Find(Texture.Name name)
        {
            TextureManager pMan = TextureManager.GetInstance();
            Debug.Assert(pMan != null);

            pMan.pNodeCompare.name = name;
            Texture pData = (Texture)pMan.BaseFind(pMan.pNodeCompare);
            return pData;
        }
        public static void Remove(Texture pNode)
        {
            TextureManager pMan = TextureManager.GetInstance();
            Debug.Assert(pMan != null);

            Debug.Assert(pNode != null);
            pMan.BaseRemove(pNode);
        }
        public static void Dump()
        {
            TextureManager pMan = TextureManager.GetInstance();
            Debug.Assert(pMan != null);

            Debug.WriteLine("***********Texture Lists************");
            pMan.BaseDump();
        }

        private static TextureManager GetInstance()
        {
            Debug.Assert(pInstance != null);
            return pInstance;
        }

        // Override abstract methods
        override protected DLink DerivedCreateNode()
        {
            DLink pNode = new Texture();
            Debug.Assert(pNode != null);
            return pNode;
        }
        override protected bool DerivedCompareNode(DLink pLinkA, DLink pLinkB)
        {
            Debug.Assert(pLinkA != null);
            Debug.Assert(pLinkB != null);

            Texture pDataA = (Texture)pLinkA;
            Texture pDataB = (Texture)pLinkB;

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
            Texture pNode = (Texture)pLink;
            pNode.Wash();
        }
        override protected void DerivedDumpNode(DLink pLink)
        {
            Debug.Assert(pLink != null);
            Texture pData = (Texture)pLink;
            pData.Dump();
        }
    }
}
