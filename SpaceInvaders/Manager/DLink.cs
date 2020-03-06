using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class DLink
    {
        public DLink pNext;
        public DLink pPrev;
        public uint Priority;

        protected DLink()
        {
            this.Clear();
        }

        public void Clear()
        {
            this.pNext = null;
            this.pPrev = null;
            this.Priority = 0;
        }

        public static void PushFront(ref DLink pHead, DLink pNode)
        {
            Debug.Assert(pNode != null);
            // add node
            if (pHead == null)
            {
                // push to the front
                pHead = pNode;
                pNode.pNext = null;
                pNode.pPrev = null;
            }
            else
            {
                // push to front
                pNode.pPrev = null;
                pNode.pNext = pHead;
                // update head
                pHead.pPrev = pNode;
                pHead = pNode;
            }
            Debug.Assert(pHead != null);
        }

        public static void PriorityInsert(ref DLink pHead, DLink pNode, uint priority) 
        {
            Debug.Assert(pNode != null);
            pNode.Priority = priority;

            if (pHead == null)
            {
                // push to the front
                pHead = pNode;
                pNode.pNext = null;
                pNode.pPrev = null;
            }
            else {
                DLink current = pHead;
                while (current != null) {
                    //Debug.Assert(current.Priority != pNode.Priority);
                    if (current.Priority <= pNode.Priority)
                    {
                        pNode.pPrev = current.pPrev;
                        pNode.pNext = current;

                        if (current.pPrev == null)
                        {
                            pHead = pNode;
                        }
                        else
                        {
                            current.pPrev.pNext = pNode;
                        }
                        current.pPrev = pNode;
                        break;
                    }
                    else if (current.pNext == null) {
                        pNode.pPrev = current;
                        current.pNext = pNode;
                        break;
                    }
                    current = current.pNext;
                }
            }
            Debug.Assert(pHead != null);
        }
        public static DLink PullFront(ref DLink pHead)
        {
            Debug.Assert(pHead != null);
            DLink pNode = pHead;
            pHead = pHead.pNext;
            if (pHead != null)
            {
                pHead.pPrev = null;
            }
            // remove any lingering links
            pNode.Clear();
            return pNode;
        }
        public static void RemoveNode(ref DLink pHead, DLink pNode)
        {
            Debug.Assert(pHead != null);
            Debug.Assert(pNode != null);

            if (pNode.pPrev != null)
            {
                pNode.pPrev.pNext = pNode.pNext;
            }
            else
            {
                pHead = pNode.pNext;
                
            }
            if (pHead != null) {
                pHead.pPrev = null;
            }

            if (pNode.pNext != null)
            {
                pNode.pNext.pPrev = pNode.pPrev;
            }
            // remove any lingering links
            pNode.Clear();
        }
        //------------------------------------------
        //---------Dealing with last node-----------
        //------------------------------------------
        public static void RemoveNode(ref DLink pHead, ref DLink pEnd, DLink pNode)
        {
            // protection
            Debug.Assert(pHead != null);
            Debug.Assert(pEnd != null);
            Debug.Assert(pNode != null);

            if (pNode.pPrev != null)
            {	// middle part 1/2
                pNode.pPrev.pNext = pNode.pNext;

                // last node
                if (pNode == pEnd)
                {
                    pEnd = pNode.pPrev;
                }
            }
            else
            {  // first
                pHead = pNode.pNext;

                if (pNode == pEnd)
                {
                    // Only one node
                    pEnd = pNode.pNext;
                }
                else
                {
                    // Only first not the last
                    // do nothing more
                }
            }

            if (pNode.pNext != null)
            {	// middle node part 2/2
                pNode.pNext.pPrev = pNode.pPrev;
            }

            pNode.Clear();
        }
        public static void PushFront(ref DLink pHead, ref DLink pEnd, DLink pNode)
        {
            // add to front
            Debug.Assert(pNode != null);

            // add node
            if (pHead == null)
            {
                // push to the front
                pHead = pNode;
                pEnd = pNode;
                pNode.pNext = null;
                pNode.pPrev = null;
            }
            else
            {
                // push to front
                pNode.pPrev = null;
                pNode.pNext = pHead;

                // update head
                pHead.pPrev = pNode;
                pHead = pNode;

                // update end
                // Adding to front --> end doesn't change
            }

            // worst case, pHead, pEnd was null initially, now we added a node so... this is true
            Debug.Assert(pHead != null);
            Debug.Assert(pEnd != null);
        }
        public static void PushBack(ref DLink pHead, ref DLink pEnd, DLink pNode)
        {
            // add to front
            Debug.Assert(pNode != null);

            // add node
            if (pEnd == null)
            {
                // no nodes on list
                pHead = pNode;
                pEnd = pNode;
                pNode.pNext = null;
                pNode.pPrev = null;
            }
            else
            {
                // add to end
                pEnd.pNext = pNode;
                pNode.pPrev = pEnd;
                pNode.pNext = null;
                pEnd = pNode;

                // update front
                // Adding to end --> front doesn't change
            }

            // worst case, pHead,pEnd was null initially, now we added a node so... this is true
            Debug.Assert(pHead != null);
            Debug.Assert(pEnd != null);
        }
    }
}
