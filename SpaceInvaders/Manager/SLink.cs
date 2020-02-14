using System.Diagnostics;

namespace SpaceInvaders
{
    abstract class SLink
    {
        // Data: ---------------
        public SLink pSNext;
        public SLink pSPrev;

        protected SLink()
        {
            this.pSNext = null;
            this.pSPrev = null;
        }

        public static void PushFront(ref SLink pHead, SLink pNode)
        {
            Debug.Assert(pNode != null);
            if (pHead == null)
            {
                // push to the front
                pHead = pNode;
                pNode.pSNext = null;
                pNode.pSPrev = null;
            }
            else
            {
                // push to front
                pNode.pSPrev = null;
                pNode.pSNext = pHead;
                // update head
                pHead.pSPrev = pNode;
                pHead = pNode;
            }
            Debug.Assert(pHead != null);
        }
    }
}
