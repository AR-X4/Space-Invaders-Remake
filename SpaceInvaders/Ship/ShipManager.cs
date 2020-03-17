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
            StopLeft,
            StopRight,
            StopLeftMissileFlying,
            StopRightMissileFlying,
            Dead
        }

        // Data: ----------------------------------------------
        private static ShipManager instance = null;

        // Active
        private static Ship pShip;
        private static Missile pMissile;

        // Reference
        public State CurrentStateName;
        private ShipStateReady pStateReady;
        private ShipMissileFlyingState pStateMissileFlying;
        private ShipStopLeftState pStateStopLeft;
        private ShipStopRightState pStateStopRight;
        private ShipStopRightMissileFlyingState pStateStopRightMissileFlying;
        private ShipStopLeftMissileFlyingState pStateStopLeftMissileFlying;
        private readonly ShipDeadState pStateDead;

        private ShipManager()
        {
            // Store the states
            this.pStateReady = new ShipStateReady();
            this.pStateMissileFlying = new ShipMissileFlyingState();
            this.pStateDead = new ShipDeadState();
            this.pStateStopLeft = new ShipStopLeftState();
            this.pStateStopRight = new ShipStopRightState();
            this.pStateStopLeftMissileFlying = new ShipStopLeftMissileFlyingState();
            this.pStateStopRightMissileFlying = new ShipStopRightMissileFlyingState();


            // set active
            pShip = null;
            pMissile = null;

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
            pShip = CreateShip();
            pShip.SetState(ShipManager.State.Ready);

            pMissile = CreateMissile();

        }

        private static ShipManager PrivInstance()
        {
            Debug.Assert(instance != null);

            return instance;
        }

        public static Ship GetShip()
        {
            return pShip;
        }

        public static Missile GetMissile()
        {
            return pMissile;
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

                case ShipManager.State.StopLeft:
                    pShipState = pShipMan.pStateStopLeft;
                    break;

                case ShipManager.State.StopRight:
                    pShipState = pShipMan.pStateStopRight;
                    break;

                case ShipManager.State.StopRightMissileFlying:
                    pShipState = pShipMan.pStateStopRightMissileFlying;
                    break;

                case ShipManager.State.StopLeftMissileFlying:
                    pShipState = pShipMan.pStateStopLeftMissileFlying;
                    break;

                default:
                    Debug.Assert(false);
                    break;
            }
            
            return pShipState;
        }

        public static void ActivateShip() {
            SpriteBatch pAliensBatch = SpriteBatchManager.Find(SpriteBatch.Name.Aliens);
            SpriteBatch pBoxBatch = SpriteBatchManager.Find(SpriteBatch.Name.Boxes);

            GameObject pShipRoot = GameObjectManager.Find(GameObject.Name.ShipRoot);
            Debug.Assert(pShipRoot != null);

            pShip.pProxySprite.Set(GameSprite.Name.Ship);
            pShip.poColObj.poColRect.Set(pShip.pProxySprite.pSprite.GetScreenRect());

            pShip.ActivateCollisionSprite(pBoxBatch);
            pShip.ActivateGameSprite(pAliensBatch);

            pShipRoot.Add(pShip);
        }

        public static void ActivateMissile() {
            SpriteBatch pAliensBatch = SpriteBatchManager.Find(SpriteBatch.Name.Aliens);
            SpriteBatch pBoxBatch = SpriteBatchManager.Find(SpriteBatch.Name.Boxes);

            GameObject pMissileGroup = GameObjectManager.Find(GameObject.Name.MissileGroup);
            Debug.Assert(pMissileGroup != null);

            pMissile.ActivateCollisionSprite(pBoxBatch);
            pMissile.ActivateGameSprite(pAliensBatch);

            pMissileGroup.Add(pMissile);
        }

        public static void LaunchMissile()
        {
            Debug.Assert(pMissile != null);

            pMissile.delta = 5.0f;
            pMissile.bMarkForDeath = false;
            pMissile.pProxySprite.Set(GameSprite.Name.Missile);
            pMissile.poColObj.poColRect.Set(pMissile.pProxySprite.pSprite.GetScreenRect());
        }

        private static Missile CreateMissile() {
            if (pMissile == null)
            {
                pMissile = new Missile(GameObject.Name.Missile, GameSprite.Name.NullObject, pShip);
            }
            return pMissile;
        }


        private static Ship CreateShip()
        {
            if (pShip == null) {
                pShip = new Ship(GameObject.Name.Ship, GameSprite.Name.NullObject, 200, 140);
            }
            return pShip;
        }

        public static void ResetShip()
        {
            pShip.pProxySprite.Set(GameSprite.Name.Ship);
            pShip.ResetLocation();
            pShip.bMarkForDeath = false;
            
            pShip.Handle();
        }
    }
}
