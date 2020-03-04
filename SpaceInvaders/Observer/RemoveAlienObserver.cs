using System;
using System.Diagnostics;


namespace SpaceInvaders
{
    class RemoveAlienObserver : CollisionObserver
    {
        // data
        private GameObject pAlien;

        public RemoveAlienObserver()
        {
            this.pAlien = null;
        }

        public RemoveAlienObserver(RemoveAlienObserver m)
        {
            Debug.Assert(m.pAlien != null);
            this.pAlien = m.pAlien;
        }

        public override void Notify()
        {
            this.pAlien = this.pSubject.pObjB;

            if (this.pAlien.bMarkForDeath == false)
            {
                this.pAlien.bMarkForDeath = true;

                // Delay - remove object later
                // TODO - reduce the new functions
                RemoveAlienObserver pObserver = new RemoveAlienObserver(this);
                DelayedObjectManager.Attach(pObserver);
            }
        }

        public override void Execute()
        {
            // Let the gameObject deal with this... 
            this.pAlien.Remove();
        }
    }
}
