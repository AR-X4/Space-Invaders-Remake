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
            pShip = new Ship(GameObject.Name.Ship, GameSprite.Name.Ship, 200, 65);
            pMissile = new Missile(GameObject.Name.Missile, GameSprite.Name.Missile, pShip);

            SpriteBatch pSB_Aliens = SpriteBatchManager.Find(SpriteBatch.Name.Aliens);
            SpriteBatch pSB_Boxes = SpriteBatchManager.Find(SpriteBatch.Name.Boxes);

            pMissile.ActivateCollisionSprite(pSB_Boxes);
            pMissile.ActivateGameSprite(pSB_Aliens);

            GameObject pMissileGroup = GameObjectManager.Find(GameObject.Name.MissileGroup);
            Debug.Assert(pMissileGroup != null);

            // Add to GameObject Tree - {update and collisions}
            pMissileGroup.Add(pMissile);
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
            pShip = ActivateShip();
            pShip.SetState(ShipManager.State.Ready);

        }

        private static ShipManager PrivInstance()
        {
            Debug.Assert(instance != null);

            return instance;
        }

        public static Ship GetShip()
        {
            //ShipManager pShipMan = ShipManager.PrivInstance();

            //Debug.Assert(pShipMan != null);
            //Debug.Assert(pShipMan.pShip != null);

            return pShip;
        }

        public static Missile GetMissile()
        {
            //ShipManager pShipMan = ShipManager.PrivInstance();

            //Debug.Assert(pShipMan != null);
            //Debug.Assert(pShipMan.pShip != null);

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

       

        public static void ActivateMissile()
        {
            //ShipManager pShipMan = ShipManager.PrivInstance();
            //Debug.Assert(pShipMan != null);

            //pMissile.ResetMissile();
            pMissile.delta = 5.0f;
            pMissile.bMarkForDeath = false;
            pMissile.pProxySprite.Set(GameSprite.Name.Missile);
            pMissile.poColObj.poColRect.Set(pMissile.pProxySprite.pSprite.GetScreenRect());

            
        }


        private static Ship ActivateShip()
        {
            ShipManager pShipMan = ShipManager.PrivInstance();
            Debug.Assert(pShipMan != null);

            

            // Attached to SpriteBatches
            SpriteBatch pSB_Aliens = SpriteBatchManager.Find(SpriteBatch.Name.Aliens);
            SpriteBatch pSB_Boxes = SpriteBatchManager.Find(SpriteBatch.Name.Boxes);

            pShip.ActivateCollisionSprite(pSB_Boxes);
            pShip.ActivateGameSprite(pSB_Aliens);

            // Attach the missile to the missile root???
            GameObject pShipRoot = GameObjectManager.Find(GameObject.Name.ShipRoot);
            Debug.Assert(pShipRoot != null);

            // Add to GameObject Tree - {update and collisions}
            pShipRoot.Add(pShip);

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
