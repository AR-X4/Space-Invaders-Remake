
using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    //---------------------------------------------------------------------------------------------------------
    // Design Notes:
    //---------------------------------------------------------------------------------------------------------
    
    public class GameObjectManager : Manager
    {
        //----------------------------------------------------------------------
        // Data - unique data for this manager 
        //----------------------------------------------------------------------
        private static GameObjectManager pInstance = null;
        private GameObjectNode poNodeCompare;
        private readonly NullGameObject poNullGameObject;
        //----------------------------------------------------------------------
        // Constructor
        //----------------------------------------------------------------------
        private GameObjectManager(int reserveNum = 3, int reserveGrow = 1)
        : base() // <--- Kick the can (delegate)
        {
            // At this point ImageMan is created, now initialize the reserve
            this.BaseInitialize(reserveNum, reserveGrow);

            // initialize derived data here
            this.poNodeCompare = new GameObjectNode();
            this.poNullGameObject = new NullGameObject();

            this.poNodeCompare.pGameObj = this.poNullGameObject; 
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
                pInstance = new GameObjectManager(reserveNum, reserveGrow);
            }
        }

        public static void Destroy()
        {

        }

        public static GameObjectNode Attach(GameObject pGameObject)
        {
            GameObjectManager pMan = GameObjectManager.GetInstance();
            Debug.Assert(pMan != null);

            GameObjectNode pNode = (GameObjectNode)pMan.BaseAdd();
            Debug.Assert(pNode != null);

            pNode.Set(pGameObject);
            return pNode;
        }

        public static GameObject Find(GameObject.Name name)
        {
            GameObjectManager pMan = GameObjectManager.GetInstance();
            Debug.Assert(pMan != null);

            // Compare functions only compares two Nodes
            pMan.poNodeCompare.pGameObj.name = name;

            GameObjectNode pNode = (GameObjectNode)pMan.BaseFind(pMan.poNodeCompare);
            Debug.Assert(pNode != null);

            return pNode.pGameObj;
        }

        public static void Remove(GameObjectNode pNode)
        {
            GameObjectManager pMan = GameObjectManager.GetInstance();
            Debug.Assert(pMan != null);

            Debug.Assert(pNode != null);
            pMan.BaseRemove(pNode);
        }

        public static void Update()
        {
            GameObjectManager pMan = GameObjectManager.GetInstance();
            Debug.Assert(pMan != null);

            GameObjectNode pGameObjectNode = (GameObjectNode)pMan.BaseGetActive();

            while (pGameObjectNode != null)
            {
                ReverseIterator pRev = new ReverseIterator(pGameObjectNode.pGameObj);

                Component pNode = pRev.First();
                while (!pRev.IsDone())
                {
                    GameObject pGameObj = (GameObject)pNode;

                    //Debug.WriteLine("update: {0} ({1})", pGameObj, pGameObj.GetHashCode());
                    pGameObj.Update();

                    pNode = pRev.Next();
                }

                pGameObjectNode = (GameObjectNode)pGameObjectNode.pNext;

            }
        }

        public static void Dump()//BROKEN
        {
            GameObjectManager pMan = GameObjectManager.GetInstance();
            Debug.Assert(pMan != null);

            pMan.BaseDump();
        }

        //----------------------------------------------------------------------
        // Override Abstract methods
        //----------------------------------------------------------------------
        override protected DLink DerivedCreateNode()
        {
            DLink pNode = new GameObjectNode();
            Debug.Assert(pNode != null);

            return pNode;
        }
        override protected Boolean DerivedCompareNode(DLink pLinkA, DLink pLinkB)
        {
            // This is used in baseFind() 
            Debug.Assert(pLinkA != null);
            Debug.Assert(pLinkB != null);

            GameObjectNode pDataA = (GameObjectNode)pLinkA;
            GameObjectNode pDataB = (GameObjectNode)pLinkB;

            Boolean status = false;

            if (pDataA.pGameObj.GetName() == pDataB.pGameObj.GetName())
            {
                status = true;
            }

            return status;
        }
        override protected void DerivedWashNode(DLink pLink)
        {
            Debug.Assert(pLink != null);
            GameObjectNode pNode = (GameObjectNode)pLink;
            pNode.Wash();
        }
        override protected void DerivedDumpNode(DLink pLink)
        {
            Debug.Assert(pLink != null);
            GameObjectNode pData = (GameObjectNode)pLink;
            pData.Dump();
        }

        //----------------------------------------------------------------------
        // Private methods
        //----------------------------------------------------------------------
        private static GameObjectManager GetInstance()
        {
            // Safety - this forces users to call Create() first before using class
            Debug.Assert(pInstance != null);

            return pInstance;
        }
    }
}
