using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class RemoveBombObserver : CollisionObserver
    {
        // data
        private Bomb pBomb;
        private RemoveBombEvent pEvent;
        private GameObject pSubB;

        public RemoveBombObserver()
        {
            this.pBomb = null;
            this.pEvent = null;
            this.pSubB = null;
        }

        public RemoveBombObserver(RemoveBombObserver m)
        {
            Debug.Assert(m.pBomb != null);
            Debug.Assert(m.pSubB != null);
            this.pBomb = m.pBomb;
            this.pEvent = new RemoveBombEvent();
            this.pSubB = m.pSubB;
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
            this.pSubB = this.pSubject.pObjB;

            //Debug.WriteLine("MissileRemoveObserver: --> delete missile {0}", pBomb);

            if (pBomb.bMarkForDeath == false)
            {
                pBomb.bMarkForDeath = true;

                // Delay - remove object later
                // TODO - reduce the new functions
                RemoveBombObserver pObserver = new RemoveBombObserver(this);
                DelayedObjectManager.Attach(pObserver);
            }

            //this.pBomb.pOwner.Handle();
        }

        public override void Execute()
        {
            // Let the gameObject deal with this...
            if (this.pSubB.name != GameObject.Name.Ship)
            {
                this.pBomb.delta = 0.0f;
                this.pBomb.pProxySprite.Set(GameSprite.Name.BombExplosion);
                this.pEvent.SetBomb(this.pBomb);
                TimerManager.Add(TimeEvent.Name.RemoveBomb, this.pEvent, 0.5f);
            }
            else
            {
                BombManager.Reset(this.pBomb);
            }
            this.pBomb.pOwner.Handle();
        }
    }

}

// End of File
