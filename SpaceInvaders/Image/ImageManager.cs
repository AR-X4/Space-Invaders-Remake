using System.Diagnostics;

namespace SpaceInvaders
{
    class ImageManager : Manager
    {
        private readonly Image pNodeCompare;
        private static ImageManager pInstance = null;

        private ImageManager(int reserveNum = 3, int reserveGrow = 1)
            : base()
        {
            base.BaseInitialize(reserveNum, reserveGrow);
            this.pNodeCompare = new Image();
        }

        //Methods

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
                pInstance = new ImageManager(reserveNum, reserveGrow);
            }
            Image pImage = ImageManager.Add(Image.Name.NullObject, Texture.Name.NullObject, 0, 0, 128, 128);
            Debug.Assert(pImage != null);
            // Default image manager
            ImageManager.Add(Image.Name.Default, Texture.Name.Default, 0, 0, 128, 128);
            Debug.Assert(pImage != null);
        }
        public static void Destroy()
        {
            ImageManager pMan = ImageManager.GetInstance();
            Debug.Assert(pMan != null);

            // Do something clever here
            // track peak number of active nodes
            // print stats on destroy
            // invalidate the singleton
        }

        public static Image Add(Image.Name name, Texture.Name TextureName, float x, float y, float width, float height)
        {
            ImageManager pMan = ImageManager.GetInstance();
            Debug.Assert(pMan != null);

            Image pNode = (Image)pMan.BaseAdd();
            Debug.Assert(pNode != null);

            // Initialize the data
            Texture pTexture = TextureManager.Find(TextureName);
            Debug.Assert(pTexture != null);
            pNode.Set(name, pTexture, x, y, width, height);

            return pNode;
        }
        public static Image Find(Image.Name name)
        {
            ImageManager pMan = ImageManager.GetInstance();
            Debug.Assert(pMan != null);

            pMan.pNodeCompare.name = name;
            Image pData = (Image)pMan.BaseFind(pMan.pNodeCompare);
            return pData;
        }
        public static void Remove(Image pNode)
        {
            ImageManager pMan = ImageManager.GetInstance();
            Debug.Assert(pMan != null);

            Debug.Assert(pNode != null);
            pMan.BaseRemove(pNode);
        }
        public static void Dump()
        {
            Debug.WriteLine("************Image Lists**************");
            ImageManager pMan = ImageManager.GetInstance();
            Debug.Assert(pMan != null);
            pMan.BaseDump();
        }

        private static ImageManager GetInstance()
        {
            Debug.Assert(pInstance != null);
            return pInstance;
        }

        // Override Abstract methods
        override protected DLink DerivedCreateNode()
        {
            DLink pNode = new Image();
            Debug.Assert(pNode != null);
            return pNode;
        }
        override protected bool DerivedCompareNode(DLink pLinkA, DLink pLinkB)
        {
            Debug.Assert(pLinkA != null);
            Debug.Assert(pLinkB != null);

            Image pDataA = (Image)pLinkA;
            Image pDataB = (Image)pLinkB;

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
            Image pNode = (Image)pLink;
            pNode.Wash();
        }
        override protected void DerivedDumpNode(DLink pLink)
        {
            Debug.Assert(pLink != null);
            Image pData = (Image)pLink;
            pData.Dump();
        }

    }
}

