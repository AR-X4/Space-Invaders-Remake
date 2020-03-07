using System;
using System.Diagnostics;


namespace SpaceInvaders
{
    public class AlienSubject
    {

        // Data: ------------------------
        private AlienObserver head;

        public void Attach(AlienObserver observer)
        {
            // protection
            Debug.Assert(observer != null);

            observer.pSubject = this;

            // add to front
            if (head == null)
            {
                head = observer;
                observer.pNext = null;
                observer.pPrev = null;
            }
            else
            {
                observer.pNext = head;
                observer.pPrev = null;
                head.pPrev = observer;
                head = observer;
            }
        }


        public void Notify()
        {
            AlienObserver pNode = this.head;

            while (pNode != null)
            {
                // Fire off listener
                pNode.Notify();

                pNode = (AlienObserver)pNode.pNext;
            }
        }

        public void Detach()
        {
        }
    }
}
