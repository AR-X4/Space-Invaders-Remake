using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class RemoveBrickObserver : CollisionObserver
    {
        // -------------------------------------------
        // data:
        // -------------------------------------------

        private GameObject pBrick;

        public RemoveBrickObserver()
        {
            this.pBrick = null;
        }
        public RemoveBrickObserver(RemoveBrickObserver b)
        {
            Debug.Assert(b != null);
            this.pBrick = b.pBrick;
        }

        public override void Notify()
        {
            // Delete missile
    

            this.pBrick = (ShieldBrick)this.pSubject.pObjB;
            Debug.Assert(this.pBrick != null);

            if (pBrick.bMarkForDeath == false)
            {
                 pBrick.bMarkForDeath = true;
                //   Delay
                RemoveBrickObserver pObserver = new RemoveBrickObserver(this);
                DelayedObjectManager.Attach(pObserver);
            }
            //else
           // {
                //pBrick.bMarkForDeath = true;
            //}
        }
        public override void Execute()
        {
            
            this.pBrick.Remove();
        }
        
    }
}

// End of File
