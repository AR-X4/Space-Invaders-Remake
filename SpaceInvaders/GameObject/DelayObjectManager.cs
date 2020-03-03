
using System.Diagnostics;

namespace SpaceInvaders
{
    class DelayedObjectManager
    {
        // -------------------------------------------
        // Data: 
        // -------------------------------------------
        private CollisionObserver head;
        private static DelayedObjectManager instance = null;

        static public void Attach(CollisionObserver observer)
        {
            // protection
            Debug.Assert(observer != null);

            DelayedObjectManager pDelayMan = DelayedObjectManager.GetInstance();

            // add to front
            if (pDelayMan.head == null)
            {
                pDelayMan.head = observer;
                observer.pNext = null;
                observer.pPrev = null;
            }
            else
            {
                observer.pNext = pDelayMan.head;
                observer.pPrev = null;
                pDelayMan.head.pPrev = observer;
                pDelayMan.head = observer;
            }
        }
        private void Detach(CollisionObserver node, ref CollisionObserver head)
        {
            // protection
            Debug.Assert(node != null);

            if (node.pPrev != null)
            {	// middle or last node
                node.pPrev.pNext = node.pNext;
            }
            else
            {  // first
                head = (CollisionObserver)node.pNext;
            }

            if (node.pNext != null)
            {	// middle node
                node.pNext.pPrev = node.pPrev;
            }
        }
        static public void Process()
        {
            DelayedObjectManager pDelayMan = DelayedObjectManager.GetInstance();

            CollisionObserver pNode = pDelayMan.head;

            while (pNode != null)
            {
                // Fire off listener
                pNode.Execute();

                pNode = (CollisionObserver)pNode.pNext;
            }


            // remove
            pNode = pDelayMan.head;
            CollisionObserver pTmp = null;

            while (pNode != null)
            {
                pTmp = pNode;
                pNode = (CollisionObserver)pNode.pNext;

                // remove
                pDelayMan.Detach(pTmp, ref pDelayMan.head);
            }
        }
        private DelayedObjectManager()
        {
            this.head = null;
        }
        private static DelayedObjectManager GetInstance()
        {
            // Do the initialization
            if (instance == null)
            {
                instance = new DelayedObjectManager();
            }

            // Safety - this forces users to call create first
            Debug.Assert(instance != null);

            return instance;
        }
    }
}

// End of File
