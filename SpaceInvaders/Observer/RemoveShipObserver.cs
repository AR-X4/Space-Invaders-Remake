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
            this.pShip.pProxySprite.Set(GameSprite.Name.ShipExplosion);
            Ship pShipCopy = (Ship)this.pShip;
          
            pShipCopy.SetState(ShipManager.State.Dead);

            SwapShipExplosionEvent pSwapEvent = new SwapShipExplosionEvent(Image.Name.ShipExplosionA);//move to ship manager?
            SwapShipExplosionEvent pSwapEvent2 = new SwapShipExplosionEvent(Image.Name.ShipExplosionB);//move to ship manager?


            TimerManager.Add(TimeEvent.Name.SwapShipExplosion, pSwapEvent2, 0.2f);
            TimerManager.Add(TimeEvent.Name.SwapShipExplosion, pSwapEvent, 0.4f);
            TimerManager.Add(TimeEvent.Name.SwapShipExplosion, pSwapEvent2, 0.6f);
            TimerManager.Add(TimeEvent.Name.SwapShipExplosion, pSwapEvent, 0.8f);
            TimerManager.Add(TimeEvent.Name.SwapShipExplosion, pSwapEvent2, 1.0f);
            TimerManager.Add(TimeEvent.Name.SwapShipExplosion, pSwapEvent, 1.2f);



            ResetShipEvent pEvent = new ResetShipEvent();//move to ship manager?
            TimerManager.Add(TimeEvent.Name.RemoveShip, pEvent, 1.4f);


        }


    }
}
