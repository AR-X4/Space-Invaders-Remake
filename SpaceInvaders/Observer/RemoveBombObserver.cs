using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class RemoveBombObserver : CollisionObserver
    {
        // data
        private Bomb pBomb;

        public RemoveBombObserver()
        {
            this.pBomb = null;
        }

        public RemoveBombObserver(RemoveBombObserver m)
        {
            Debug.Assert(m.pBomb != null);
            this.pBomb = m.pBomb;
        }

        public override void Notify()
        {
            // Delete missile
            //Debug.WriteLine("ShipRemoveMissileObserver: {0} {1}", this.pSubject.pObjA, this.pSubject.pObjB);

            // At this point we have two game objects
            // Actually we can control the objects in the visitor
            // Alphabetical ordering... A is missile,  B is wall

            // This cast will throw an exception if I'm wrong
            this.pBomb = (Bomb)this.pSubject.pObjA;

            //Debug.WriteLine("MissileRemoveObserver: --> delete missile {0}", pBomb);

            if (pBomb.bMarkForDeath == false)
            {
                pBomb.bMarkForDeath = true;

                // Delay - remove object later
                // TODO - reduce the new functions
                RemoveBombObserver pObserver = new RemoveBombObserver(this);
                DelayedObjectManager.Attach(pObserver);
            }

            this.pBomb.pOwner.Handle();
        }

        public override void Execute()
        {
            // Let the gameObject deal with this...


            BombManager.Remove(this.pBomb);
        }
    }

}

// End of File
