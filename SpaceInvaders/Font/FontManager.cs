using System;
using System.Xml;
using System.Diagnostics;

namespace SpaceInvaders
{
    class FontManager : Manager
    {
        //----------------------------------------------------------------------
        // Data
        //----------------------------------------------------------------------
        private static FontManager pInstance = null;
        private static FontManager pActiveMan = null;
        private static Font poNodeCompare;

        //----------------------------------------------------------------------
        // Constructor
        //----------------------------------------------------------------------
        public FontManager(int reserveNum = 3, int reserveGrow = 1)
            : base()
        {
            this.BaseInitialize(reserveNum, reserveGrow);
            
        }

        private FontManager() 
            : base()
        {
            FontManager.pActiveMan = null;
            FontManager.poNodeCompare = new Font();
        }
        ~FontManager()
        {

        }

        //----------------------------------------------------------------------
        // Static Manager methods can be implemented with base methods 
        // Can implement/specialize more or less methods your choice
        //----------------------------------------------------------------------
        //public static void Create(int reserveNum = 3, int reserveGrow = 1)
        //{
        //    // make sure values are ressonable 
        //    Debug.Assert(reserveNum > 0);
        //    Debug.Assert(reserveGrow > 0);

        //    // initialize the singleton here
        //    Debug.Assert(pInstance == null);

        //    // Do the initialization
        //    if (pInstance == null)
        //    {
        //        pInstance = new FontManager(reserveNum, reserveGrow);
        //    }
        //}

        public static void Create()
        {
            // initialize the singleton here
            Debug.Assert(pInstance == null);

            // Do the initialization
            if (pInstance == null)
            {
                pInstance = new FontManager();
            }
        }

        public static void Destroy()
        {

        }
        public static Font Add(Font.Name name, SpriteBatch.Name SB_Name, String pMessage, float xStart, float yStart, float width, float height)
        {
            FontManager pMan = FontManager.pActiveMan;

            Font pNode = (Font)pMan.BaseAdd();
            Debug.Assert(pNode != null);

            pNode.Set(name, pMessage, xStart, yStart, width, height);

            // Add to sprite batch
            SpriteBatch pSB = SpriteBatchManager.Find(SB_Name);
            Debug.Assert(pSB != null);
            Debug.Assert(pNode.pFontSprite != null);
            pSB.Attach(pNode.pFontSprite);

            return pNode;
        }
        public static void SetActive(FontManager pFMan)
        {
            FontManager pMan = FontManager.GetInstance();
            Debug.Assert(pMan != null);

            Debug.Assert(pFMan != null);
            FontManager.pActiveMan = pFMan;
        }

        //public static void AddXml(Glyph.Name glyphName, String assetName, Texture.Name textName)
        //{
        //    GlyphManager.AddXml(glyphName, assetName, textName);
        //}

        public static void Update(Font.Name name, String pNewMessage) {
            Font pFont = FontManager.Find(name);
            Debug.Assert(pFont != null);

            Debug.Assert(pNewMessage != null);
            pFont.UpdateMessage(pNewMessage);

        }
        public static void Update(Font.Name name, int pNewMessage)
        {
            Font pFont = FontManager.Find(name);
            Debug.Assert(pFont != null);

            
            String temp = pNewMessage.ToString();
            if (pNewMessage < 10)
            {
                temp = "000" + temp;
            }
            else if (pNewMessage < 100)
            {
                temp = "00" + temp;
            }
            else if (pNewMessage < 1000) {
                temp = "0" + temp;
            }

            pFont.UpdateMessage(temp);
        }

        public static void Remove(Glyph pNode)
        {
            Debug.Assert(pNode != null);
            FontManager pMan = FontManager.pActiveMan;
            pMan.BaseRemove(pNode);
        }
        public static Font Find(Font.Name name)
        {
            FontManager pMan = FontManager.pActiveMan;

            // Compare functions only compares two Nodes
            FontManager.poNodeCompare.name = name;

            Font pData = (Font)pMan.BaseFind(FontManager.poNodeCompare);
            return pData;
        }


        public static void Dump()
        {
            FontManager pMan = FontManager.GetInstance();
            Debug.Assert(pMan != null);

            Debug.WriteLine("------ Font Manager ------");
            pMan.BaseDump();
        }

        //----------------------------------------------------------------------
        // Override Abstract methods
        //----------------------------------------------------------------------
        override protected Boolean DerivedCompareNode(DLink pLinkA, DLink pLinkB)
        {
            // This is used in baseFind() 
            Debug.Assert(pLinkA != null);
            Debug.Assert(pLinkB != null);

            Font pDataA = (Font)pLinkA;
            Font pDataB = (Font)pLinkB;

            Boolean status = false;

            if (pDataA.name == pDataB.name)
            {
                status = true;
            }

            return status;
        }
        override protected DLink DerivedCreateNode()
        {
            DLink pNode = new Font();
            Debug.Assert(pNode != null);
            return pNode;
        }
        override protected void DerivedWashNode(DLink pLink)
        {
            Debug.Assert(pLink != null);
            Font pNode = (Font)pLink;
            pNode.Wash();
        }

        override protected void DerivedDumpNode(DLink pLink)
        {
            Debug.Assert(pLink != null);
            Font pNode = (Font)pLink;

            Debug.Assert(pNode != null);
            pNode.Dump();
        }

        //----------------------------------------------------------------------
        // Private methods
        //----------------------------------------------------------------------
        private static FontManager GetInstance()
        {
            // Safety - this forces users to call Create() first before using class
            Debug.Assert(pInstance != null);

            return pInstance;
        }
    }
}
