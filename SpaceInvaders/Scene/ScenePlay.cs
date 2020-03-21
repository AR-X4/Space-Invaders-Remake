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
        private FontManager poFontManager;
        private TimerManager poTimerManager;
        private CollisionPairManager poCollisionPairManager;

        public static int ShipLives;
        public static float SwitchTime = 0f;
        public static float StartTimeDelta = 0f;
        private float RunTime = 0f;

        public ScenePlay()
        {
            this.Initialize();
           
        }
        public override void Handle()
        {
            //set state of scene context to Scene Over
            if (SpaceInvaders.Player1Mode == true || ScenePlay2.ShipLives == 0)
            {
                //Debug.WriteLine("-------P1 " + this.RunTime);

                SpaceInvaders.pSceneContext.SetState(SceneContext.Scene.Over);
            }
          
            else {
                //Debug.WriteLine("-------P1 " + this.RunTime);

                SpaceInvaders.pSceneContext.SetState(SceneContext.Scene.Play2);
            }
            ScenePlay2.StartTimeDelta += (Simulation.GetTotalTime() - ScenePlay.SwitchTime);
        }


        public override void Initialize()
        {
         

            this.poGameObjectManager = new GameObjectManager(3, 1);
            GameObjectManager.SetActive(this.poGameObjectManager);

            this.poTimerManager = new TimerManager(3, 1);
            TimerManager.SetActive(this.poTimerManager);

            this.poCollisionPairManager = new CollisionPairManager(3, 1);
            CollisionPairManager.SetActive(this.poCollisionPairManager);

            //---------------------------------------------------------------------------------------------------------
            // Create SpriteBatch
            //---------------------------------------------------------------------------------------------------------
            this.poSpriteBatchManager = new SpriteBatchManager(3, 1);
            SpriteBatchManager.SetActive(this.poSpriteBatchManager);

            SpriteBatch pAliensBatch = SpriteBatchManager.Add(SpriteBatch.Name.Aliens, 1);
            SpriteBatch pBoxBatch = SpriteBatchManager.Add(SpriteBatch.Name.Boxes, 2);
            SpriteBatch pShieldsBatch = SpriteBatchManager.Add(SpriteBatch.Name.Shields, 3);
            SpriteBatch pNullBatch = SpriteBatchManager.Add(SpriteBatch.Name.NullObjects, 4);
            SpriteBatch pTexts = SpriteBatchManager.Add(SpriteBatch.Name.Texts, 5);

            pBoxBatch.SetDrawBool(false);

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
            // Create Texts
            //---------------------------------------------------------------------------------------------------------
            
            this.poFontManager = new FontManager(3, 1);
            FontManager.SetActive(this.poFontManager);

            FontManager.Add(Font.Name.Header, SpriteBatch.Name.Texts, "SCORE<1>       HI-SCORE       SCORE<2>", 20f, SpaceInvaders.ScreenHeight - 20f, 15f, 25f);
            FontManager.Add(Font.Name.Player1Score, SpriteBatch.Name.Texts, "0000", 65f, SpaceInvaders.ScreenHeight - 70f, 15f, 25f);
            FontManager.Add(Font.Name.Player2Score, SpriteBatch.Name.Texts, "0000", SpaceInvaders.ScreenWidth - 156f, SpaceInvaders.ScreenHeight - 70f, 15f, 25f);
            FontManager.Add(Font.Name.HiScore, SpriteBatch.Name.Texts, "0000", 380f, SpaceInvaders.ScreenHeight - 70f, 15f, 25f);
            FontManager.Add(Font.Name.Lives, SpriteBatch.Name.Texts, "3", 40f, 40f, 15f, 25f);


            //---------------------------------------------------------------------------------------------------------
            // Create Walls
            //---------------------------------------------------------------------------------------------------------

            // Wall Root
            WallGroup pWallGroup = new WallGroup(GameObject.Name.WallGroup, GameSprite.Name.NullObject, 0.0f, 0.0f);
            pWallGroup.ActivateGameSprite(pAliensBatch);//even need this?
            pWallGroup.ActivateCollisionSprite(pBoxBatch);

            WallRight pWallRight = new WallRight(GameObject.Name.WallRight, GameSprite.Name.NullObject, SpaceInvaders.ScreenWidth - 15, SpaceInvaders.ScreenHeight/2, 20, SpaceInvaders.ScreenHeight - 110);
            pWallRight.ActivateCollisionSprite(pBoxBatch);

            WallLeft pWallLeft = new WallLeft(GameObject.Name.WallLeft, GameSprite.Name.NullObject, 20, SpaceInvaders.ScreenHeight / 2, 20, SpaceInvaders.ScreenHeight - 110);
            pWallLeft.ActivateCollisionSprite(pBoxBatch);

            WallTop pWallTop = new WallTop(GameObject.Name.WallTop, GameSprite.Name.NullObject, 450, SpaceInvaders.ScreenHeight - 70, SpaceInvaders.ScreenWidth-10, 30);
            pWallTop.ActivateCollisionSprite(pBoxBatch);

            WallBottom pWallBottom = new WallBottom(GameObject.Name.WallBottom, GameSprite.Name.Ground, 450, 60, SpaceInvaders.ScreenWidth - 10, 5);
            pWallBottom.ActivateGameSprite(pAliensBatch);
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

            DropBombEvent pBombEvent = new DropBombEvent();
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

            ShipManager.ActivateShip();
            ShipManager.ActivateMissile();
            
            //---------------------------------------------------------------------------------------------------------
            // Create UFO and UFO Root
            //---------------------------------------------------------------------------------------------------------
            UFORoot pUFORoot = new UFORoot(GameObject.Name.UFORoot, GameSprite.Name.NullObject, 0.0f, 0.0f);
            pUFORoot.ActivateGameSprite(pAliensBatch);//change? even need this?
            pUFORoot.ActivateCollisionSprite(pBoxBatch);

            GameObjectManager.Attach(pUFORoot);

            OrangeSaucer pUFO = new OrangeSaucer(GameObject.Name.OrangeSaucer, GameSprite.Name.OrangeSaucer, 20f, SpaceInvaders.ScreenHeight - 110f);
            pUFO.ActivateGameSprite(pAliensBatch);//change? even need this?
            pUFO.ActivateCollisionSprite(pBoxBatch);

            pUFORoot.Add(pUFO);
            pUFO.Remove();


            SpawnUFOEvent pUFOEvent = new SpawnUFOEvent();
            TimerManager.Add(TimeEvent.Name.UFOSpawn, pUFOEvent, RandomManager.RandomInt(15, 45));

            //---------------------------------------------------------------------------------------------------------
            // Create Aliens
            //---------------------------------------------------------------------------------------------------------

            AlienFactory AF = new AlienFactory(SpriteBatch.Name.Aliens, SpriteBatch.Name.Boxes);
            AlienGrid pAlienGrid = (AlienGrid)AF.Create(GameObject.Name.AlienGrid);


            for (int i = 0; i < 11; i++)
            {
                GameObject pCol = AF.Create(GameObject.Name.AlienColumn);
                GameObject pGameObject;
                pGameObject = AF.Create(GameObject.Name.PurpleOctopus, 50.0f + 66 * i, SpaceInvaders.ScreenHeight - 364f);
                pCol.Add(pGameObject);

                pGameObject = AF.Create(GameObject.Name.PurpleOctopus, 50.0f + 66 * i, SpaceInvaders.ScreenHeight - 298f);
                pCol.Add(pGameObject);

                pGameObject = AF.Create(GameObject.Name.BlueCrab, 50.0f + 66 * i, SpaceInvaders.ScreenHeight - 232f);
                pCol.Add(pGameObject);

                pGameObject = AF.Create(GameObject.Name.BlueCrab, 50.0f + 66 * i, SpaceInvaders.ScreenHeight - 166f);
                pCol.Add(pGameObject);

                pGameObject = AF.Create(GameObject.Name.GreenSquid, 50.0f + 66 * i, SpaceInvaders.ScreenHeight-100f);
                pCol.Add(pGameObject);

                pAlienGrid.Add(pCol);
            }
            GameObjectManager.Attach(pAlienGrid);

            pAlienGrid.Attach(new MoveAlienSoundObserver());
            pAlienGrid.Attach(new MoveAlienGridObserver());

            AlienGridMoveEvent pGridMoveEvent = new AlienGridMoveEvent();
            TimerManager.Add(TimeEvent.Name.MoveAlienGrid, pGridMoveEvent, pAlienGrid.GetMoveRate());

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
            TimerManager.Add(TimeEvent.Name.SpriteAnimation, pAnimOctopus, pAlienGrid.GetMoveRate());
            TimerManager.Add(TimeEvent.Name.SpriteAnimation, pAnimCrab, pAlienGrid.GetMoveRate());
            TimerManager.Add(TimeEvent.Name.SpriteAnimation, pAnimSquid, pAlienGrid.GetMoveRate());

            //---------------------------------------------------------------------------------------------------------
            // Shield 
            //---------------------------------------------------------------------------------------------------------

            // Create the factory 
            Composite pShieldRoot = (Composite)new ShieldRoot(GameObject.Name.ShieldRoot, GameSprite.Name.NullObject, 0.0f, 0.0f);
            GameObjectManager.Attach(pShieldRoot);
            ShieldFactory SF = new ShieldFactory(SpriteBatch.Name.Shields, SpriteBatch.Name.Boxes, pShieldRoot);

            float start_x = 160.0f;
            float start_y = 200.0f;
            float off_x;
            float brickWidth = 12.0f;
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

                start_x += 170;
            }


            //---------------------------------------------------------------------------------------------------------
            // Create Null Ship Lives 
            //---------------------------------------------------------------------------------------------------------

            ScenePlay.ShipLives = 3;

            PlayerLivesComposite pNullObjs = new PlayerLivesComposite();

            Ship pNullShip1 = new Ship(GameObject.Name.Null_Object, GameSprite.Name.Ship, 120.0f, 40.0f);
            pNullShip1.ActivateGameSprite(pNullBatch);
            pNullObjs.Add(pNullShip1);

            Ship pNullShip2 = new Ship(GameObject.Name.Null_Object, GameSprite.Name.Ship, 180.0f, 40.0f);
            pNullShip2.ActivateGameSprite(pNullBatch);
            pNullObjs.Add(pNullShip2);

            GameObjectManager.Attach(pNullObjs);

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

            CollisionPair pAlienWallBottomPair = CollisionPairManager.Add(CollisionPair.Name.Alien_WallBottom, pAlienGrid, pWallBottom);
            Debug.Assert(pAlienWallBottomPair != null);

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

            CollisionPair pAlienShieldPair = CollisionPairManager.Add(CollisionPair.Name.Alien_Shield, pShieldRoot, pAlienGrid);
            Debug.Assert(pAlienShieldPair != null);

            CollisionPair pUFOMissilePair = CollisionPairManager.Add(CollisionPair.Name.UFO_Missile, pMissileGroup, pUFORoot);
            Debug.Assert(pUFOMissilePair != null);

            //TODO consolidate these news
            pShipWallLeftPair.Attach(new ShipStopLeftObserver());
            pShipWallRightPair.Attach(new ShipStopRightObserver());

            pMissileWallPair.Attach(new ShipRemoveMissileObserver());

            pAlienMissilePair.Attach(new ShipRemoveMissileObserver());
            pAlienMissilePair.Attach(new RemoveAlienObserver());
            pAlienMissilePair.Attach(new DeadAlienSoundObserver());
            pAlienMissilePair.Attach(new AddP1PointsObserver());
            pAlienMissilePair.Attach(new IncreaseAlienSpeedObserver());

            pAlienWallPair.Attach(new GridObserver());

            pBombWallPair.Attach(new RemoveBombObserver());

            pBombShieldPair.Attach(new RemoveBombObserver());
            pBombShieldPair.Attach(new RemoveBrickObserver());

            pMissileShieldPair.Attach(new ShipRemoveMissileObserver());
            pMissileShieldPair.Attach(new RemoveBrickObserver());

            pBombShipPair.Attach(new RemoveBombObserver());
            pBombShipPair.Attach(new RemoveShipObserver());
            pBombShipPair.Attach(new DeadShipSoundObserver());
            pBombShipPair.Attach(new RemoveLifeObserver());
            pBombShipPair.Attach(new ChangeStateObserver());

            pBombMissilePair.Attach(new RemoveBombObserver());
            pBombMissilePair.Attach(new ShipRemoveMissileObserverAltPair());

            pAlienShieldPair.Attach(new RemoveBrickObserver());

            pUFOMissilePair.Attach(new RemoveUFOObserver());
            pUFOMissilePair.Attach(new ShipRemoveMissileObserver());
            pUFOMissilePair.Attach(new AddP1PointsObserver());
            pUFOMissilePair.Attach(new DeadUFOSoundObserver());

            pAlienWallBottomPair.Attach(new DeadShipSoundObserver());
            pAlienWallBottomPair.Attach(new RemoveAllP1LivesObserver());

        }

        public override void Update(float systemTime)
        {
            InputManager.Update();

            FontManager.Update(Font.Name.Player1Score, SpaceInvaders.pPlayer1Score);
            FontManager.Update(Font.Name.Player2Score, SpaceInvaders.pPlayer2Score);
            FontManager.Update(Font.Name.HiScore, SpaceInvaders.pHiScore);
            FontManager.Update(Font.Name.Lives, ScenePlay.ShipLives.ToString());

            this.RunTime = Simulation.GetTotalTime() - ScenePlay.StartTimeDelta;
            //Debug.WriteLine(this.RunTime);

            TimerManager.Update(this.RunTime);
            GameObjectManager.Update();
            //Collision Checks
            CollisionPairManager.Process();
            // Delete any objects here...
            DelayedObjectManager.Process();

            //check if all alien killed
            AlienGrid pGrid = (AlienGrid)GameObjectManager.Find(GameObject.Name.AlienGrid);
            if (pGrid.GetAlienCount() < 1) {
                pGrid.IncreaseStartRate();
                this.ResetAll();
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
            FontManager.SetActive(this.poFontManager);
            TimerManager.SetActive(this.poTimerManager);
            CollisionPairManager.SetActive(this.poCollisionPairManager);

            ScenePlay.SwitchTime = Simulation.GetTotalTime(); 

            if (ScenePlay.ShipLives < 1) {
                AlienGrid pGrid = (AlienGrid)GameObjectManager.Find(GameObject.Name.AlienGrid);
                pGrid.ResetStartRate();
                this.ResetAll();
                SpaceInvaders.pPlayer1Score = 0;
                SpaceInvaders.pPlayer2Score = 0;
                ScenePlay.ShipLives = 3;
                ScenePlay2.ShipLives = 3;
            }
        }

        private void ResetAll() {
            //-----Reset Everything-----
            PlayerLivesComposite pNullObjs = (PlayerLivesComposite)GameObjectManager.Find(GameObject.Name.Null_Object);
            pNullObjs.ResetLives();

            AlienGrid pGrid = (AlienGrid)GameObjectManager.Find(GameObject.Name.AlienGrid);
            pGrid.ResetAliens();

            ShieldRoot pSRoot = (ShieldRoot)GameObjectManager.Find(GameObject.Name.ShieldRoot);
            pSRoot.ResetShields();

            UFORoot pUFORoot = (UFORoot)GameObjectManager.Find(GameObject.Name.UFORoot);
            OrangeSaucer pUFO = (OrangeSaucer)pUFORoot.GetFirstChild();
            pUFO.Remove();

            TimerManager.Reset();
        }
    }
}
