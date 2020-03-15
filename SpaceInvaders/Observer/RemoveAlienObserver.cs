using System;
using System.Diagnostics;


namespace SpaceInvaders
{
    class RemoveAlienObserver : CollisionObserver
    {
        // data
        private GameObject pAlien;
        private RemoveAlienEvent pEvent;

        public RemoveAlienObserver()
        {
            this.pAlien = null;
            this.pEvent = null;
        }

        private RemoveAlienObserver(RemoveAlienObserver m)
        {
            Debug.Assert(m.pAlien != null);
            this.pAlien = m.pAlien;

            this.pEvent = new RemoveAlienEvent();
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
            this.pAlien.pProxySprite.Set(GameSprite.Name.AlienExplosion);
            //add removal to timer event
            this.pEvent.SetAlien(this.pAlien);

            TimerManager.Add(TimeEvent.Name.RemoveAlien, this.pEvent, 0.5f);

            //this.pAlien.Remove();
        }
    }
}
