using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class RemoveShipObserver : CollisionObserver
    {
        // data
        private GameObject pShip;

        public RemoveShipObserver()
        {
            this.pShip = null;
        }

        public RemoveShipObserver(RemoveShipObserver m)
        {
            Debug.Assert(m.pShip != null);
            this.pShip = m.pShip;
        }

        public override void Notify()
        {
            this.pShip = this.pSubject.pObjB;

            if (this.pShip.bMarkForDeath == false)
            {
                this.pShip.bMarkForDeath = true;

                // Delay - remove object later
                // TODO - reduce the new functions
                RemoveShipObserver pObserver = new RemoveShipObserver(this);
                DelayedObjectManager.Attach(pObserver);
            }
        }

        //command pattern for delayed manager
        public override void Execute()
        {
            // Let the gameObject deal with this... 
            this.pShip.Remove();
        }


    }
}
