using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class Manager
    {
        //-------8 Byte Aligned Data------------
        private DLink pActiveHead;
        private DLink pReserveHead;
        private int mNumActive;
        private int mNumReserved;
        private int mGrowthSize;
        private int mTotalNodes;

        protected Manager()
        {
            this.pActiveHead = null;
            this.pReserveHead = null;
            this.mNumActive = 0;
            this.mNumReserved = 0;
            this.mGrowthSize = 0;
            this.mTotalNodes = 0;
        }

        protected void BaseInitialize(int mNumReserved = 3, int mGrowthSize = 1)
        {
            // Check now or pay later
            Debug.Assert(mNumReserved >= 0);
            Debug.Assert(mGrowthSize > 0);

            this.mGrowthSize = mGrowthSize;

            // Preload the reserve
            this.PopulateReserve(mNumReserved);
        }

        //------Private Helper Methods----------
        private void PopulateReserve(int numNodes)
        {
            Debug.Assert(numNodes > 0);

            this.mTotalNodes += numNodes;
            this.mNumReserved += numNodes;
            // Create the reserve
            for (int i = 0; i < numNodes; i++)
            {
                DLink pNode = this.DerivedCreateNode();
                Debug.Assert(pNode != null);
                Manager.PushFront(ref this.pReserveHead, pNode);
            }
        }
        private static void PushFront(ref DLink pHead, DLink pNode)
        {
            Debug.Assert(pNode != null);
            DLink.PushFront(ref pHead, pNode);
            // worst case, pHead was null initially, now we added a node so... this is true
            Debug.Assert(pHead != null);
        }

        private static void PriorityInsert(ref DLink pHead, DLink pNode, uint priority) {
            Debug.Assert(pNode != null);
            DLink.PriorityInsert(ref pHead, pNode, priority);
            // worst case, pHead was null initially, now we added a node so... this is true
            Debug.Assert(pHead != null);
        }
        private static DLink PullFront(ref DLink pHead)
        {
            Debug.Assert(pHead != null);
            DLink pNode;
            pNode = DLink.PullFront(ref pHead);
            Debug.Assert(pNode != null);
            return pNode;
        }
        private static void RemoveNode(ref DLink pHead, DLink pNode)
        {
            Debug.Assert(pHead != null);
            Debug.Assert(pNode != null);
            DLink.RemoveNode(ref pHead, pNode);
        }

        //--------------Base Methods------------------
        protected void BaseSetReserve(int reserveNum, int reserveGrow)
        {
            this.mGrowthSize = reserveGrow;

            if (reserveNum > this.mNumReserved)
            {
                // Preload the reserve
                this.PopulateReserve(reserveNum - this.mNumReserved);
            }
        }
        protected DLink BaseAdd()
        {
            // Check if reserve list is empty
            if (this.pReserveHead == null)
            {
                this.PopulateReserve(this.mGrowthSize);
            }
            // Pull from the reserve list
            DLink pLink = Manager.PullFront(ref this.pReserveHead);
            Debug.Assert(pLink != null);

            // Wash and push to active list
            this.DerivedWashNode(pLink);
            Manager.PushFront(ref this.pActiveHead, pLink);
            // Update stats
            this.mNumActive++;
            this.mNumReserved--;

            return pLink;
        }

        protected DLink BaseSortedAdd(uint priority) 
        {
            // Check if reserve list is empty
            if (this.pReserveHead == null)
            {
                this.PopulateReserve(this.mGrowthSize);
            }
            // Pull from the reserve list
            DLink pLink = Manager.PullFront(ref this.pReserveHead);
            Debug.Assert(pLink != null);

            // Wash and push to active list
            this.DerivedWashNode(pLink);
            Manager.PriorityInsert(ref this.pActiveHead, pLink, priority);

            // Update stats
            this.mNumActive++;
            this.mNumReserved--;

            return pLink;
        }
        protected DLink BaseFind(DLink pNodeTarget)
        {
            DLink pLink = this.pActiveHead;
            // Walk active list
            while (pLink != null)
            {
                if (DerivedCompareNode(pLink, pNodeTarget))
                {
                    // found it
                    break;
                }
                pLink = pLink.pNext;
            }
            return pLink;
        }
        protected void BaseRemove(DLink pNode)
        {
            Debug.Assert(pNode != null);
            // Remove from Active... delegate it to DLink
            Manager.RemoveNode(ref this.pActiveHead, pNode);

            // wash it before returning to reserve list
            this.DerivedWashNode(pNode);

            // add it to the reserve list
            Manager.PushFront(ref this.pReserveHead, pNode);

            // stats update
            this.mNumActive--;
            this.mNumReserved++;
        }
        protected DLink BaseGetActive()
        {
            return this.pActiveHead;
        }
        protected void BaseDump()
        {
            Debug.WriteLine("");
            Debug.WriteLine("    Active Nodes:  " + this.mNumActive);
            Debug.WriteLine("    Reserve Nodes: " + this.mNumReserved);
            Debug.WriteLine("    Total Nodes:   " + this.mTotalNodes);

            DLink pNode = null;
            //-----------Print Active List--------------
            if (this.pActiveHead == null)
            {
                Debug.WriteLine("    Active Head: null");
            }
            else
            {
                pNode = this.pActiveHead;
                Debug.WriteLine("    Active Head: " + pNode.GetHashCode());
            }

            Debug.WriteLine("   ------ Active List: -----------\n");
            pNode = this.pActiveHead;
            int i = 0;
            while (pNode != null)
            {
                Debug.WriteLine("   {0}: -------------", i);
                this.DerivedDumpNode(pNode);
                i++;
                pNode = pNode.pNext;
            }

            //------------Print Reserve List--------------
            if (this.pReserveHead == null)
            {
                Debug.WriteLine("\n   Reserve Head: null");
            }
            else
            {
                pNode = this.pReserveHead;
                Debug.WriteLine("\n   Reserve Head: " + pNode.GetHashCode());
            }


            Debug.WriteLine("   ------ Reserve List: ----------\n");

            pNode = this.pReserveHead;
            i = 0;
            while (pNode != null)
            {
                Debug.WriteLine("   {0}: -------------", i);
                this.DerivedDumpNode(pNode);
                i++;
                pNode = pNode.pNext;
            }
            Debug.WriteLine("\n   ****** End ******\n");
        }

        //---------Derived Methods-----------------
        abstract protected DLink DerivedCreateNode();
        abstract protected bool DerivedCompareNode(DLink pLinkA, DLink pLinkB);
        abstract protected void DerivedWashNode(DLink pLink);
        abstract protected void DerivedDumpNode(DLink pLink);
    }
}
