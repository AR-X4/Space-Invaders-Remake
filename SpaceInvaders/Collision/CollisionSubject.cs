using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class CollisionSubject
    {
        // Data: ------------------------
        private CollisionObserver pHead;
        public GameObject pObjA;
        public GameObject pObjB;

        public CollisionSubject()
        {
            this.pObjB = null;
            this.pObjA = null;
            this.pHead = null;
        }

        ~CollisionSubject()
        {
            this.pObjB = null;
            this.pObjA = null;
            // ToDo
            // Need to walk and nuke the list
            this.pHead = null;
        }

        public void Attach(CollisionObserver observer)
        {
            // protection
            Debug.Assert(observer != null);

            observer.pSubject = this;

            // add to front
            if (pHead == null)
            {
                pHead = observer;
                observer.pNext = null;
                observer.pPrev = null;
            }
            else
            {
                observer.pNext = pHead;
                pHead.pPrev = observer;
                pHead = observer;
            }
        }

        public void Notify()
        {
            CollisionObserver pNode = this.pHead;

            while (pNode != null)
            {
                // Fire off listener
                pNode.Notify();

                pNode = (CollisionObserver)pNode.pNext;
            }
        }

        public void Detach()
        {
        }
    }
}
