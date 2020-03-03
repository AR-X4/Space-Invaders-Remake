using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ShipManager
    {
        public enum State
        {
            Ready,
            MissileFlying,
            Dead
        }

        // Data: ----------------------------------------------
        private static ShipManager instance = null;

        // Active
        private Ship pShip;
        private Missile pMissile;

        // Reference
        private ShipStateReady pStateReady;
        private ShipMissileFlyingState pStateMissileFlying;
        private readonly ShipDeadState pStateDead;

        private ShipManager()
        {
            // Store the states
            this.pStateReady = new ShipStateReady();
            this.pStateMissileFlying = new ShipMissileFlyingState();
            this.pStateDead = new ShipDeadState();

            // set active
            this.pShip = null;
            this.pMissile = null;
        }

        public static void Create()
        {
            // make sure its the first time
            Debug.Assert(instance == null);

            // Do the initialization
            if (instance == null)
            {
                instance = new ShipManager();
            }

            Debug.Assert(instance != null);

            // Stuff to initialize after the instance was created
            instance.pShip = ActivateShip();
            instance.pShip.SetState(ShipManager.State.Ready);

        }

        private static ShipManager PrivInstance()
        {
            Debug.Assert(instance != null);

            return instance;
        }

        public static Ship GetShip()
        {
            ShipManager pShipMan = ShipManager.PrivInstance();

            Debug.Assert(pShipMan != null);
            Debug.Assert(pShipMan.pShip != null);

            return pShipMan.pShip;
        }

        public static ShipState GetState(State state)
        {
            ShipManager pShipMan = ShipManager.PrivInstance();
            Debug.Assert(pShipMan != null);

            ShipState pShipState = null;

            switch (state)
            {
                case ShipManager.State.Ready:
                    pShipState = pShipMan.pStateReady;
                    break;

                case ShipManager.State.MissileFlying:
                    pShipState = pShipMan.pStateMissileFlying;
                    break;

                case ShipManager.State.Dead:
                    pShipState = pShipMan.pStateDead;
                    break;

                default:
                    Debug.Assert(false);
                    break;
            }

            return pShipState;
        }

        public static Missile GetMissile()
        {
            ShipManager pShipMan = ShipManager.PrivInstance();

            Debug.Assert(pShipMan != null);
            Debug.Assert(pShipMan.pMissile != null);

            return pShipMan.pMissile;
        }

        public static Missile ActivateMissile()
        {
            ShipManager pShipManager = ShipManager.PrivInstance();
            Debug.Assert(pShipManager != null);

            // copy over safe copy
            // TODO: This can be cleaned up more... no need to re-calling new()
            Missile pMissile = new Missile(GameObject.Name.Missile, GameSprite.Name.Missile, 400, 100);
            pShipManager.pMissile = pMissile;

            // Attached to SpriteBatches
            SpriteBatch pSB_Aliens = SpriteBatchManager.Find(SpriteBatch.Name.Aliens);
            SpriteBatch pSB_Boxes = SpriteBatchManager.Find(SpriteBatch.Name.Boxes);

            pMissile.ActivateCollisionSprite(pSB_Boxes);
            pMissile.ActivateGameSprite(pSB_Aliens);

            //FIX THIS

            // Attach the missile to the missile root
            GameObject pMissileGroup = GameObjectManager.Find(GameObject.Name.MissileGroup);
            Debug.Assert(pMissileGroup != null);

            // Add to GameObject Tree - {update and collisions}
            pMissileGroup.Add(pShipManager.pMissile);

            return pShipManager.pMissile;
        }


        private static Ship ActivateShip()
        {
            ShipManager pShipMan = ShipManager.PrivInstance();
            Debug.Assert(pShipMan != null);

            // copy over safe copy
            Ship pShip = new Ship(GameObject.Name.Ship, GameSprite.Name.Ship, 200, 100);
            pShipMan.pShip = pShip;

            // Attach the sprite to the correct sprite batch???? change to ship batch?
            SpriteBatch pSB_Aliens = SpriteBatchManager.Find(SpriteBatch.Name.Aliens);
            pSB_Aliens.Attach(pShip.pProxySprite);

            // Attach the missile to the missile root???
            GameObject pShipRoot = GameObjectManager.Find(GameObject.Name.ShipRoot);
            Debug.Assert(pShipRoot != null);

            // Add to GameObject Tree - {update and collisions}
            pShipRoot.Add(pShipMan.pShip);

            return pShipMan.pShip;
        }

        

    }
}
