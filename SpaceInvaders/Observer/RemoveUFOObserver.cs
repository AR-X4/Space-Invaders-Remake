using System;
using System.Diagnostics;


namespace SpaceInvaders
{
    class RemoveUFOObserver : CollisionObserver
    {
        // data
        private OrangeSaucer pAlien;
        private RemoveAlienEvent pEvent;

        public RemoveUFOObserver()
        {
            this.pAlien = null;
            this.pEvent = null;
        }

        private RemoveUFOObserver(RemoveUFOObserver m)
        {
            Debug.Assert(m.pAlien != null);
            this.pAlien = m.pAlien;

            this.pEvent = new RemoveAlienEvent();
        }

        public override void Notify()
        {
            this.pAlien = (OrangeSaucer)this.pSubject.pObjB;

            if (this.pAlien.bMarkForDeath == false)
            {
                this.pAlien.bMarkForDeath = true;

                // Delay - remove object later
                // TODO - reduce the new functions
                RemoveUFOObserver pObserver = new RemoveUFOObserver(this);
                DelayedObjectManager.Attach(pObserver);
            }
        }

        public override void Execute()
        {
            // Let the gameObject deal with this... 
            this.pAlien.pProxySprite.Set(GameSprite.Name.SaucerExplosion);
            this.pAlien.SetDelta(0f);
            //add removal to timer event
            this.pEvent.SetAlien(this.pAlien);

            TimerManager.Add(TimeEvent.Name.RemoveAlien, this.pEvent, 0.5f);

        }
    }
}
