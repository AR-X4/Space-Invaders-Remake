using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ShipRemoveMissileObserver : CollisionObserver
    {
        // data
        private Missile pMissile;
        private ResetMissileEvent pEvent;
        private GameObject pSubB;

        public ShipRemoveMissileObserver()
        {
            this.pMissile = null;
            this.pEvent = null;
            this.pSubB = null;
        }

        public ShipRemoveMissileObserver(ShipRemoveMissileObserver m)
        {
            Debug.Assert(m.pMissile != null);
            this.pMissile = m.pMissile;
            this.pEvent = new ResetMissileEvent();
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
            this.pMissile = (Missile)this.pSubject.pObjA;
            this.pSubB = this.pSubject.pObjB;


            if (pMissile.bMarkForDeath == false)
            {
                pMissile.bMarkForDeath = true;

                // Delay - remove object later
                // TODO - reduce the new functions
                ShipRemoveMissileObserver pObserver = new ShipRemoveMissileObserver(this);
                DelayedObjectManager.Attach(pObserver);
            }
        }

        public override void Execute()
        {
            this.pMissile.delta = 0.0f;

            switch (this.pSubB.name) {
                case GameObject.Name.WallTop:
                    this.pMissile.pProxySprite.Set(GameSprite.Name.MissileExplosionRed);
                    TimerManager.Add(TimeEvent.Name.RemoveMissile, this.pEvent, 0.25f);
                    break;

                case GameObject.Name.ShieldBrick:
                    this.pMissile.pProxySprite.Set(GameSprite.Name.MissileExplosionGreen);
                    TimerManager.Add(TimeEvent.Name.RemoveMissile, this.pEvent, 0.25f);
                    break;

                case GameObject.Name.Missile:
                    this.pMissile.pProxySprite.Set(GameSprite.Name.MissileExplosionWhite);
                    TimerManager.Add(TimeEvent.Name.RemoveMissile, this.pEvent, 0.25f);
                    break;
                default:

                    this.pMissile.ResetMissile();
                    Ship pShip = ShipManager.GetShip();
                    if (pShip.CurrentStateName != ShipManager.State.Dead)
                    {
                        pShip.Handle();
                    }
                    break;
            }

            //if (this.pSubB.name != GameObject.Name.BlueCrab && this.pSubB.name != GameObject.Name.PurpleOctopus && 
            //    this.pSubB.name != GameObject.Name.GreenSquid && this.pSubB.name != GameObject.Name.OrangeSaucer) {

            //    this.pMissile.delta = 0.0f;
            //    this.pMissile.pProxySprite.Set(GameSprite.Name.MissileExplosion);
            //    TimerManager.Add(TimeEvent.Name.RemoveMissile, this.pEvent, 0.25f);
            //}
            //else {
            //    this.pMissile.ResetMissile();
            //    Ship pShip = ShipManager.GetShip();
            //    if (pShip.CurrentStateName != ShipManager.State.Dead)
            //    {
            //        pShip.Handle();
            //    }
            //}
        }
    }
}
