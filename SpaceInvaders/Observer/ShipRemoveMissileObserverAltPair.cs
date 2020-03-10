using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ShipRemoveMissileObserverAltPair : CollisionObserver
    {
        // data
        private GameObject pMissile;

        public ShipRemoveMissileObserverAltPair()
        {
            this.pMissile = null;
        }

        public ShipRemoveMissileObserverAltPair(ShipRemoveMissileObserverAltPair m)
        {
            Debug.Assert(m.pMissile != null);
            this.pMissile = m.pMissile;
        }

        public override void Notify()
        {

            // At this point we have two game objects
            // Actually we can control the objects in the visitor
            // Alphabetical ordering... A is missile,  B is wall

            // This cast will throw an exception if I'm wrong
            this.pMissile = (Missile)this.pSubject.pObjB;

            Debug.WriteLine("MissileRemoveObserver: --> delete missile {0}", pMissile);

            if (pMissile.bMarkForDeath == false)
            {
                pMissile.bMarkForDeath = true;

                // Delay - remove object later
                // TODO - reduce the new functions
                ShipRemoveMissileObserverAltPair pObserver = new ShipRemoveMissileObserverAltPair(this);
                DelayedObjectManager.Attach(pObserver);
            }
        }

        public override void Execute()
        {
            // Let the gameObject deal with this... 
            this.pMissile.Remove();
        }
    }
}
