using System;
using System.Diagnostics;


namespace SpaceInvaders
{
    class RemoveAlienObserver : CollisionObserver
    {
        // data
        private GameObject pPurpleOctopus;

        public RemoveAlienObserver()
        {
            this.pPurpleOctopus = null;
        }

        public RemoveAlienObserver(RemoveAlienObserver m)
        {
            Debug.Assert(m.pPurpleOctopus != null);
            this.pPurpleOctopus = m.pPurpleOctopus;
        }

        public override void Notify()
        {
            this.pPurpleOctopus = this.pSubject.pObjB;

            if (this.pPurpleOctopus.bMarkForDeath == false)
            {
                this.pPurpleOctopus.bMarkForDeath = true;

                // Delay - remove object later
                // TODO - reduce the new functions
                RemoveAlienObserver pObserver = new RemoveAlienObserver(this);
                DelayedObjectManager.Attach(pObserver);
            }
        }

        public override void Execute()
        {
            // Let the gameObject deal with this... 
            this.pPurpleOctopus.Remove();
        }
    }
}
