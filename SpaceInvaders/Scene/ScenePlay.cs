using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ScenePlay : SceneState
    {
        // ---------------------------------------------------
        // Data
        // ---------------------------------------------------
        private SpriteBatchManager poSpriteBatchManager;
        private GameObjectManager poGameObjectManager;
        private InputManager poInputManager;

        

        

        public ScenePlay()
        {
            this.Initialize();
           
        }
        public override void Handle()
        {
            //set state of scene context to Scene Over
            SpaceInvaders.pSceneContext.SetState(SceneContext.Scene.Over);
        }


        public override void Initialize()
        {
            this.poGameObjectManager = new GameObjectManager(3, 1);
            GameObjectManager.SetActive(this.poGameObjectManager);

            //---------------------------------------------------------------------------------------------------------
            // Create SpriteBatch
            //---------------------------------------------------------------------------------------------------------
            this.poSpriteBatchManager = new SpriteBatchManager(3, 1);
            SpriteBatchManager.SetActive(this.poSpriteBatchManager);

            SpriteBatch pAliensBatch = SpriteBatchManager.Add(SpriteBatch.Name.Aliens, 1);
            SpriteBatch pBoxBatch = SpriteBatchManager.Add(SpriteBatch.Name.Boxes, 2);
            SpriteBatch pShieldsBatch = SpriteBatchManager.Add(SpriteBatch.Name.Shields, 3);

            //---------------------------------------------------------------------------------------------------------
            // Input
            //---------------------------------------------------------------------------------------------------------
            this.poInputManager = new InputManager();
            InputManager.SetActive(this.poInputManager);

            InputSubject pInputSubject;
            pInputSubject = InputManager.GetArrowRightSubject();
            pInputSubject.Attach(new MoveRightObserver());

            pInputSubject = InputManager.GetArrowLeftSubject();
            pInputSubject.Attach(new MoveLeftObserver());

            pInputSubject = InputManager.GetSpaceSubject();
            pInputSubject.Attach(new ShootObserver());
            pInputSubject.Attach(new ShootSoundObserver());

            pInputSubject = InputManager.GetCKeySubject();
            pInputSubject.Attach(new ToggleCollisionBoxObserver());

            Simulation.SetState(Simulation.State.Realtime);

            //---------------------------------------------------------------------------------------------------------
            // Timer Animations
            //---------------------------------------------------------------------------------------------------------

            // Create an animation sprite
            AnimationSprite pAnimOctopus = new AnimationSprite(GameSprite.Name.PurpleOctopus);
            AnimationSprite pAnimCrab = new AnimationSprite(GameSprite.Name.BlueCrab);
            AnimationSprite pAnimSquid = new AnimationSprite(GameSprite.Name.GreenSquid);

            // attach several images to cycle

            pAnimOctopus.Attach(Image.Name.OctopusA);
            pAnimOctopus.Attach(Image.Name.OctopusB);

            pAnimCrab.Attach(Image.Name.AlienA);
            pAnimCrab.Attach(Image.Name.AlienB);

            pAnimSquid.Attach(Image.Name.SquidA);
            pAnimSquid.Attach(Image.Name.SquidB);

            // add AnimationSprite to timer
            TimerManager.Add(TimeEvent.Name.SpriteAnimation, pAnimOctopus, 1.0f);
            TimerManager.Add(TimeEvent.Name.SpriteAnimation, pAnimCrab, 1.0f);
            TimerManager.Add(TimeEvent.Name.SpriteAnimation, pAnimSquid, 1.0f);

            //---------------------------------------------------------------------------------------------------------
            // Create Walls
            //---------------------------------------------------------------------------------------------------------

            // Wall Root
            WallGroup pWallGroup = new WallGroup(GameObject.Name.WallGroup, GameSprite.Name.NullObject, 0.0f, 0.0f);
            pWallGroup.ActivateGameSprite(pAliensBatch);//even need this?
            pWallGroup.ActivateCollisionSprite(pBoxBatch);

            WallRight pWallRight = new WallRight(GameObject.Name.WallRight, GameSprite.Name.NullObject, 700, 300, 50, 500);
            pWallRight.ActivateCollisionSprite(pBoxBatch);

            WallLeft pWallLeft = new WallLeft(GameObject.Name.WallLeft, GameSprite.Name.NullObject, 50, 300, 50, 500);
            pWallLeft.ActivateCollisionSprite(pBoxBatch);

            WallTop pWallTop = new WallTop(GameObject.Name.WallTop, GameSprite.Name.NullObject, 375, 570, 700, 30);
            pWallTop.ActivateCollisionSprite(pBoxBatch);

            WallBottom pWallBottom = new WallBottom(GameObject.Name.WallBottom, GameSprite.Name.NullObject, 375, 30, 700, 30);
            pWallBottom.ActivateCollisionSprite(pBoxBatch);

            // Add to the composite the children
            pWallGroup.Add(pWallRight);
            pWallGroup.Add(pWallLeft);
            pWallGroup.Add(pWallTop);
            pWallGroup.Add(pWallBottom);

            GameObjectManager.Attach(pWallGroup);

            //---------------------------------------------------------------------------------------------------------
            // Create Bomb Root
            //---------------------------------------------------------------------------------------------------------

            BombRoot pBombRoot = new BombRoot(GameObject.Name.BombRoot, GameSprite.Name.NullObject, 0.0f, 0.0f);
            pBombRoot.ActivateGameSprite(pAliensBatch);//change? even need this?
            pBombRoot.ActivateCollisionSprite(pBoxBatch);

            GameObjectManager.Attach(pBombRoot);

            BombManager.Create();

            Random pRandom = new Random();
            DropBombEvent pBombEvent = new DropBombEvent(pRandom);
            TimerManager.Add(TimeEvent.Name.DropBomb, pBombEvent, 3.0f);

            //---------------------------------------------------------------------------------------------------------
            // Create Missile Root
            //---------------------------------------------------------------------------------------------------------

            // Missile Root
            MissileGroup pMissileGroup = new MissileGroup(GameObject.Name.MissileGroup, GameSprite.Name.NullObject, 0.0f, 0.0f);
            pMissileGroup.ActivateGameSprite(pAliensBatch);//change? even need this?
            pMissileGroup.ActivateCollisionSprite(pBoxBatch);

            GameObjectManager.Attach(pMissileGroup);

            //---------------------------------------------------------------------------------------------------------
            // Create Ship Root
            //---------------------------------------------------------------------------------------------------------

            ShipRoot pShipRoot = new ShipRoot(GameObject.Name.ShipRoot, GameSprite.Name.NullObject, 0.0f, 0.0f);
            pShipRoot.ActivateGameSprite(pAliensBatch);//change? even need this?
            pShipRoot.ActivateCollisionSprite(pBoxBatch);

            GameObjectManager.Attach(pShipRoot);

            ShipManager.Create();

            //---------------------------------------------------------------------------------------------------------
            // Create Aliens
            //---------------------------------------------------------------------------------------------------------

            AlienFactory AF = new AlienFactory(SpriteBatch.Name.Aliens, SpriteBatch.Name.Boxes);
            AlienGrid pAlienGrid = (AlienGrid)AF.Create(GameObject.Name.AlienGrid);


            for (int i = 0; i < 10; i++)
            {
                GameObject pCol = AF.Create(GameObject.Name.AlienColumn);
                GameObject pGameObject;
                pGameObject = AF.Create(GameObject.Name.PurpleOctopus, 50.0f + 40 * i, 370.0f);
                pCol.Add(pGameObject);

                pGameObject = AF.Create(GameObject.Name.PurpleOctopus, 50.0f + 40 * i, 410.0f);
                pCol.Add(pGameObject);

                pGameObject = AF.Create(GameObject.Name.BlueCrab, 50.0f + 40 * i, 450.0f);
                pCol.Add(pGameObject);

                pGameObject = AF.Create(GameObject.Name.BlueCrab, 50.0f + 40 * i, 490.0f);
                pCol.Add(pGameObject);

                pGameObject = AF.Create(GameObject.Name.GreenSquid, 50.0f + 40 * i, 530.0f);
                pCol.Add(pGameObject);

                pAlienGrid.Add(pCol);
            }
            GameObjectManager.Attach(pAlienGrid);

            pAlienGrid.Attach(new MoveAlienSoundObserver());
            pAlienGrid.Attach(new MoveAlienGridObserver());

            AlienGridMoveEvent pGridMoveEvent = new AlienGridMoveEvent();
            TimerManager.Add(TimeEvent.Name.MoveAlienGrid, pGridMoveEvent, 1.0f);

            //---------------------------------------------------------------------------------------------------------
            // Shield 
            //---------------------------------------------------------------------------------------------------------

            // Create the factory 
            Composite pShieldRoot = (Composite)new ShieldRoot(GameObject.Name.ShieldRoot, GameSprite.Name.NullObject, 0.0f, 0.0f);
            GameObjectManager.Attach(pShieldRoot);
            ShieldFactory SF = new ShieldFactory(SpriteBatch.Name.Shields, SpriteBatch.Name.Boxes, pShieldRoot);

            float start_x = 110.0f;
            float start_y = 130.0f;
            float off_x;
            float brickWidth = 14.0f;
            float brickHeight = 7.0f;

            GameObject pShieldGrid;
            GameObject pShieldCol;

            for (int i = 0; i < 4; i++)
            {
                off_x = 0;
                SF.SetParent(pShieldRoot);
                pShieldGrid = SF.Create(GameObject.Name.ShieldGrid);

                //------Col1
                SF.SetParent(pShieldGrid);
                pShieldCol = SF.Create(GameObject.Name.ShieldColumn);
                SF.SetParent(pShieldCol);

                SF.Create(GameObject.Name.ShieldBrick, start_x, start_y);
                SF.Create(GameObject.Name.ShieldBrick, start_x, start_y + brickHeight);
                SF.Create(GameObject.Name.ShieldBrick, start_x, start_y + 2 * brickHeight);
                SF.Create(GameObject.Name.ShieldBrick, start_x, start_y + 3 * brickHeight);
                SF.Create(GameObject.Name.ShieldBrick, start_x, start_y + 4 * brickHeight);
                SF.Create(GameObject.Name.ShieldBrick, start_x, start_y + 5 * brickHeight);
                SF.Create(GameObject.Name.ShieldBrick, start_x, start_y + 6 * brickHeight);
                SF.Create(GameObject.Name.ShieldBrick, start_x, start_y + 7 * brickHeight);
                SF.Create(GameObject.Name.ShieldBrick_LeftTop1, start_x, start_y + 8 * brickHeight);
                SF.Create(GameObject.Name.ShieldBrick_LeftTop0, start_x, start_y + 9 * brickHeight);

                //-------Col2
                SF.SetParent(pShieldGrid);
                pShieldCol = SF.Create(GameObject.Name.ShieldColumn);
                SF.SetParent(pShieldCol);

                off_x += brickWidth;
                SF.Create(GameObject.Name.ShieldBrick, start_x + off_x, start_y);
                SF.Create(GameObject.Name.ShieldBrick, start_x + off_x, start_y + brickHeight);
                SF.Create(GameObject.Name.ShieldBrick, start_x + off_x, start_y + 2 * brickHeight);
                SF.Create(GameObject.Name.ShieldBrick, start_x + off_x, start_y + 3 * brickHeight);
                SF.Create(GameObject.Name.ShieldBrick, start_x + off_x, start_y + 4 * brickHeight);
                SF.Create(GameObject.Name.ShieldBrick, start_x + off_x, start_y + 5 * brickHeight);
                SF.Create(GameObject.Name.ShieldBrick, start_x + off_x, start_y + 6 * brickHeight);
                SF.Create(GameObject.Name.ShieldBrick, start_x + off_x, start_y + 7 * brickHeight);
                SF.Create(GameObject.Name.ShieldBrick, start_x + off_x, start_y + 8 * brickHeight);
                SF.Create(GameObject.Name.ShieldBrick, start_x + off_x, start_y + 9 * brickHeight);

                //-------Col3
                SF.SetParent(pShieldGrid);
                pShieldCol = SF.Create(GameObject.Name.ShieldColumn);
                SF.SetParent(pShieldCol);

                off_x += brickWidth;
                SF.Create(GameObject.Name.ShieldBrick_LeftBottom, start_x + off_x, start_y + 2 * brickHeight);
                SF.Create(GameObject.Name.ShieldBrick, start_x + off_x, start_y + 3 * brickHeight);
                SF.Create(GameObject.Name.ShieldBrick, start_x + off_x, start_y + 4 * brickHeight);
                SF.Create(GameObject.Name.ShieldBrick, start_x + off_x, start_y + 5 * brickHeight);
                SF.Create(GameObject.Name.ShieldBrick, start_x + off_x, start_y + 6 * brickHeight);
                SF.Create(GameObject.Name.ShieldBrick, start_x + off_x, start_y + 7 * brickHeight);
                SF.Create(GameObject.Name.ShieldBrick, start_x + off_x, start_y + 8 * brickHeight);
                SF.Create(GameObject.Name.ShieldBrick, start_x + off_x, start_y + 9 * brickHeight);

                //-------Col4
                SF.SetParent(pShieldGrid);
                pShieldCol = SF.Create(GameObject.Name.ShieldColumn);
                SF.SetParent(pShieldCol);

                off_x += brickWidth;
                SF.Create(GameObject.Name.ShieldBrick, start_x + off_x, start_y + 3 * brickHeight);
                SF.Create(GameObject.Name.ShieldBrick, start_x + off_x, start_y + 4 * brickHeight);
                SF.Create(GameObject.Name.ShieldBrick, start_x + off_x, start_y + 5 * brickHeight);
                SF.Create(GameObject.Name.ShieldBrick, start_x + off_x, start_y + 6 * brickHeight);
                SF.Create(GameObject.Name.ShieldBrick, start_x + off_x, start_y + 7 * brickHeight);
                SF.Create(GameObject.Name.ShieldBrick, start_x + off_x, start_y + 8 * brickHeight);
                SF.Create(GameObject.Name.ShieldBrick, start_x + off_x, start_y + 9 * brickHeight);

                //-------Col5
                SF.SetParent(pShieldGrid);
                pShieldCol = SF.Create(GameObject.Name.ShieldColumn);
                SF.SetParent(pShieldCol);

                off_x += brickWidth;
                SF.Create(GameObject.Name.ShieldBrick_RightBottom, start_x + off_x, start_y + 2 * brickHeight);
                SF.Create(GameObject.Name.ShieldBrick, start_x + off_x, start_y + 3 * brickHeight);
                SF.Create(GameObject.Name.ShieldBrick, start_x + off_x, start_y + 4 * brickHeight);
                SF.Create(GameObject.Name.ShieldBrick, start_x + off_x, start_y + 5 * brickHeight);
                SF.Create(GameObject.Name.ShieldBrick, start_x + off_x, start_y + 6 * brickHeight);
                SF.Create(GameObject.Name.ShieldBrick, start_x + off_x, start_y + 7 * brickHeight);
                SF.Create(GameObject.Name.ShieldBrick, start_x + off_x, start_y + 8 * brickHeight);
                SF.Create(GameObject.Name.ShieldBrick, start_x + off_x, start_y + 9 * brickHeight);

                //-------Col6
                SF.SetParent(pShieldGrid);
                pShieldCol = SF.Create(GameObject.Name.ShieldColumn);
                SF.SetParent(pShieldCol);

                off_x += brickWidth;
                SF.Create(GameObject.Name.ShieldBrick, start_x + off_x, start_y + 0 * brickHeight);
                SF.Create(GameObject.Name.ShieldBrick, start_x + off_x, start_y + 1 * brickHeight);
                SF.Create(GameObject.Name.ShieldBrick, start_x + off_x, start_y + 2 * brickHeight);
                SF.Create(GameObject.Name.ShieldBrick, start_x + off_x, start_y + 3 * brickHeight);
                SF.Create(GameObject.Name.ShieldBrick, start_x + off_x, start_y + 4 * brickHeight);
                SF.Create(GameObject.Name.ShieldBrick, start_x + off_x, start_y + 5 * brickHeight);
                SF.Create(GameObject.Name.ShieldBrick, start_x + off_x, start_y + 6 * brickHeight);
                SF.Create(GameObject.Name.ShieldBrick, start_x + off_x, start_y + 7 * brickHeight);
                SF.Create(GameObject.Name.ShieldBrick, start_x + off_x, start_y + 8 * brickHeight);
                SF.Create(GameObject.Name.ShieldBrick, start_x + off_x, start_y + 9 * brickHeight);

                //-------Col7
                SF.SetParent(pShieldGrid);
                pShieldCol = SF.Create(GameObject.Name.ShieldColumn);
                SF.SetParent(pShieldCol);

                off_x += brickWidth;
                SF.Create(GameObject.Name.ShieldBrick, start_x + off_x, start_y + 0 * brickHeight);
                SF.Create(GameObject.Name.ShieldBrick, start_x + off_x, start_y + 1 * brickHeight);
                SF.Create(GameObject.Name.ShieldBrick, start_x + off_x, start_y + 2 * brickHeight);
                SF.Create(GameObject.Name.ShieldBrick, start_x + off_x, start_y + 3 * brickHeight);
                SF.Create(GameObject.Name.ShieldBrick, start_x + off_x, start_y + 4 * brickHeight);
                SF.Create(GameObject.Name.ShieldBrick, start_x + off_x, start_y + 5 * brickHeight);
                SF.Create(GameObject.Name.ShieldBrick, start_x + off_x, start_y + 6 * brickHeight);
                SF.Create(GameObject.Name.ShieldBrick, start_x + off_x, start_y + 7 * brickHeight);
                SF.Create(GameObject.Name.ShieldBrick_RightTop1, start_x + off_x, start_y + 8 * brickHeight);
                SF.Create(GameObject.Name.ShieldBrick_RightTop0, start_x + off_x, start_y + 9 * brickHeight);

                start_x += 150;
            }


            //---------------------------------------------------------------------------------------------------------
            // Create CollisionPairs 
            //---------------------------------------------------------------------------------------------------------

            //Why does the order that left/right wall are added matter??????? reverse order breaks game
            CollisionPair pShipWallRightPair = CollisionPairManager.Add(CollisionPair.Name.Ship_Wall_Right, pShipRoot, pWallRight);
            Debug.Assert(pShipWallRightPair != null);

            CollisionPair pShipWallLeftPair = CollisionPairManager.Add(CollisionPair.Name.Ship_Wall_Left, pShipRoot, pWallLeft);
            Debug.Assert(pShipWallLeftPair != null);

            CollisionPair pAlienMissilePair = CollisionPairManager.Add(CollisionPair.Name.Alien_Missile, pMissileGroup, pAlienGrid);
            Debug.Assert(pAlienMissilePair != null);

            CollisionPair pMissileWallPair = CollisionPairManager.Add(CollisionPair.Name.Missile_Wall_Top, pMissileGroup, pWallTop);
            Debug.Assert(pMissileWallPair != null);

            CollisionPair pAlienWallPair = CollisionPairManager.Add(CollisionPair.Name.Alien_Wall, pAlienGrid, pWallGroup);
            Debug.Assert(pAlienWallPair != null);

            CollisionPair pBombWallPair = CollisionPairManager.Add(CollisionPair.Name.Bomb_Wall_Bottom, pBombRoot, pWallBottom);
            Debug.Assert(pBombWallPair != null);

            CollisionPair pBombShieldPair = CollisionPairManager.Add(CollisionPair.Name.Bomb_Shield, pBombRoot, pShieldRoot);
            Debug.Assert(pBombShieldPair != null);

            CollisionPair pMissileShieldPair = CollisionPairManager.Add(CollisionPair.Name.Missile_Shield, pMissileGroup, pShieldRoot);
            Debug.Assert(pMissileShieldPair != null);

            CollisionPair pBombShipPair = CollisionPairManager.Add(CollisionPair.Name.Bomb_Ship, pBombRoot, ShipManager.GetShip());
            Debug.Assert(pBombShipPair != null);

            CollisionPair pBombMissilePair = CollisionPairManager.Add(CollisionPair.Name.Bomb_Missile, pBombRoot, ShipManager.GetMissile());
            Debug.Assert(pBombMissilePair != null);

            //TODO consolidate these news
            pShipWallLeftPair.Attach(new ShipStopLeftObserver());
            pShipWallRightPair.Attach(new ShipStopRightObserver());

            pMissileWallPair.Attach(new ShipReadyObserver());//This is called when Missile is done flying
            pMissileWallPair.Attach(new ShipRemoveMissileObserver());

            pAlienMissilePair.Attach(new ShipReadyObserver());
            pAlienMissilePair.Attach(new ShipRemoveMissileObserver());
            pAlienMissilePair.Attach(new RemoveAlienObserver());
            pAlienMissilePair.Attach(new DeadAlienSoundObserver());

            pAlienWallPair.Attach(new GridObserver());

            pBombWallPair.Attach(new RemoveBombObserver());

            pBombShieldPair.Attach(new RemoveBombObserver());
            pBombShieldPair.Attach(new RemoveBrickObserver());

            pMissileShieldPair.Attach(new ShipRemoveMissileObserver());
            pMissileShieldPair.Attach(new RemoveBrickObserver());
            pMissileShieldPair.Attach(new ShipReadyObserver());

            pBombShipPair.Attach(new RemoveBombObserver());
            pBombShipPair.Attach(new RemoveShipObserver());
            pBombShipPair.Attach(new DeadShipSoundObserver());

            pBombMissilePair.Attach(new RemoveBombObserver());
            pBombMissilePair.Attach(new ShipRemoveMissileObserverAltPair());
            pBombMissilePair.Attach(new ShipReadyObserver());


        }

        public override void Update(float systemTime)
        {
            // Single Step, Free running...
            Simulation.Update(systemTime);
            InputManager.Update();

            if (Simulation.GetTimeStep() > 0.0f)
            {
                TimerManager.Update(Simulation.GetTotalTime());
                GameObjectManager.Update();
                //Collision Checks
                CollisionPairManager.Process();
                // Delete any objects here...
                DelayedObjectManager.Process();
            }

        }
        public override void Draw()
        {
            // draw all objects
            SpriteBatchManager.Draw();
        }
        public override void Transition()
        {
            // update SpriteBatchMan()
            SpriteBatchManager.SetActive(this.poSpriteBatchManager);
            GameObjectManager.SetActive(this.poGameObjectManager);
            InputManager.SetActive(this.poInputManager);
        }
    }
}
