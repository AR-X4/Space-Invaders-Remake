using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    //---------------------------------------------------------------------------------------------------------
    // Design Notes:
    //
    //  Singleton class - use only public static methods for customers
    //
    //  * One single compare node is owned by this singleton - used for find, prevent extra news
    //  * Create one - NULL Object - Image Default
    //  * Dependency - TextureMan needs to be initialized before ImageMan
    //
    //---------------------------------------------------------------------------------------------------------
  
    public class TimerManager : Manager
    {
        //----------------------------------------------------------------------
        // Data - unique data for this manager 
        //----------------------------------------------------------------------
        private static TimerManager pInstance = null;
        private static TimerManager pActiveMan;
        private static TimeEvent poNodeCompare;
        protected float mCurrTime;
       

        //----------------------------------------------------------------------
        // Constructor
        //----------------------------------------------------------------------
        public TimerManager(int reserveNum = 3, int reserveGrow = 1)
        : base() // <--- Kick the can (delegate)
        {
            // At this point ImageMan is created, now initialize the reserve
            this.BaseInitialize(reserveNum, reserveGrow);

        }

        private TimerManager() 
            :base()
        {
            TimerManager.pActiveMan = null;
            TimerManager.poNodeCompare = new TimeEvent();
        }

        //----------------------------------------------------------------------
        // Static Methods
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
        //        pInstance = new TimerManager(reserveNum, reserveGrow);
        //    }
        //}

        public static void Create() {

            Debug.Assert(pInstance == null);

            // Do the initialization
            if (pInstance == null)
            {
                pInstance = new TimerManager();
            }
        }

        public static TimeEvent Add(TimeEvent.Name timeName, Command pCommand, float deltaTimeToTrigger)
        {
            TimerManager pMan = TimerManager.pActiveMan;
            Debug.Assert(pMan != null);

            //TimeEvent pNode = (TimeEvent)pMan.BaseAdd();
            TimeEvent pNode = (TimeEvent)pMan.BaseSortedAdd(deltaTimeToTrigger);
            Debug.Assert(pNode != null);

            Debug.Assert(pCommand != null);
            Debug.Assert(deltaTimeToTrigger >= 0.0f);

            pNode.Set(timeName, pCommand, deltaTimeToTrigger);
            return pNode;

        }
        public static void SetActive(TimerManager pTMan)
        {
            //TimerManager pMan = TimerManager.GetInstance();
            //Debug.Assert(pMan != null);

            Debug.Assert(pTMan != null);
            TimerManager.pActiveMan = pTMan;
        }
        public static TimeEvent Find(TimeEvent.Name name)
        {
            TimerManager pMan = TimerManager.pActiveMan;
            Debug.Assert(pMan != null);

            // Compare functions only compares two Nodes

            // So:  Use the Compare Node - as a reference
            //      use in the Compare() function
            //Debug.Assert(pMan.poNodeCompare != null);
            TimerManager.poNodeCompare.Wash();
            TimerManager.poNodeCompare.SetName(name);

            TimeEvent pData = (TimeEvent)pMan.BaseFind(TimerManager.poNodeCompare);
            return pData;
        }
        public static void Remove(TimeEvent pNode)
        {
            TimerManager pMan = TimerManager.pActiveMan;
            Debug.Assert(pMan != null);

            Debug.Assert(pNode != null);
            pMan.BaseRemove(pNode);
        }
        public static void Dump()
        {
            TimerManager pMan = TimerManager.GetInstance();
            Debug.Assert(pMan != null);

            pMan.BaseDump();
        }

        public static void Update(float totalTime)
        {
            // Get the instance
            TimerManager pMan = TimerManager.pActiveMan;
            Debug.Assert(pMan != null);

            // squirrel away
            pMan.mCurrTime = totalTime;

            // walk the list
            TimeEvent pEvent = (TimeEvent)pMan.BaseGetActive();
            TimeEvent pNextEvent = null;

            // Walk the list until there is no more list OR currTime is greater than timeEvent 
            // ToDo Fix: List needs to be sorted
            while (pEvent != null)// && (pMan.mCurrTime >= pEvent.triggerTime))
            {
                // Difficult to walk a list and remove itself from the list
                // so squirrel away the next event now, use it at bottom of while
                pNextEvent = (TimeEvent)pEvent.pNext;

                if (pMan.mCurrTime >= pEvent.triggerTime)
                {
                    pEvent.Process();
                    pMan.BaseRemove(pEvent);
                }
                pEvent = pNextEvent;
            }
        }
        public static float GetCurrTime()
        {
            // Get the instance
            TimerManager pTimerMan = TimerManager.pActiveMan;

            // return time
            return pTimerMan.mCurrTime;
        }
        //----------------------------------------------------------------------
        // Override Abstract methods
        //----------------------------------------------------------------------
        override protected DLink DerivedCreateNode()
        {
            DLink pNode = new TimeEvent();
            Debug.Assert(pNode != null);

            return pNode;
        }
        override protected Boolean DerivedCompareNode(DLink pLinkA, DLink pLinkB)
        {
            // This is used in baseFind() 
            Debug.Assert(pLinkA != null);
            Debug.Assert(pLinkB != null);

            TimeEvent pDataA = (TimeEvent)pLinkA;
            TimeEvent pDataB = (TimeEvent)pLinkB;

            Boolean status = false;

            if (pDataA.GetName() == pDataB.GetName())
            {
                status = true;
            }

            return status;
        }
        override protected void DerivedWashNode(DLink pLink)
        {
            Debug.Assert(pLink != null);
            TimeEvent pNode = (TimeEvent)pLink;
            pNode.Wash();
        }
        override protected void DerivedDumpNode(DLink pLink)
        {
            Debug.Assert(pLink != null);
            TimeEvent pData = (TimeEvent)pLink;
            pData.Dump();
        }

        //----------------------------------------------------------------------
        // Private methods
        //----------------------------------------------------------------------
        private static TimerManager GetInstance()
        {
            // Safety - this forces users to call Create() first before using class
            Debug.Assert(pInstance != null);

            return pInstance;
        }
    }
}


