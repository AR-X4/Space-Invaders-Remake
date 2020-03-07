using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class SpaceInvaders : Azul.Game
    {

        //-----------------------------------------------------------------------------
        // Game::Initialize()
        //		Allows the engine to perform any initialization it needs to before 
        //      starting to run.  This is where it can query for any required services 
        //      and load any non-graphic related content. 
        //-----------------------------------------------------------------------------
        public override void Initialize()
        {
            // Game Window Device setup
            this.SetWindowName("Sprites");
            this.SetWidthHeight(800, 600);
            this.SetClearColor(0.0f, 0.0f, 0.0f, 1.0f);
        }

        //-----------------------------------------------------------------------------
        // Game::LoadContent()
        //		Allows you to load all content needed for your engine,
        //	    such as objects, graphics, etc.
        //-----------------------------------------------------------------------------
        public override void LoadContent()
        {
            //---------------------------------------------------------------------------------------------------------
            // Init Managers
            //---------------------------------------------------------------------------------------------------------

            TextureManager.Create(1, 1);
            ImageManager.Create(5, 2);
            GameSpriteManager.Create(4, 2);
            SpriteBatchManager.Create(3, 1);
            BoxSpriteManager.Create(3, 1);
            TimerManager.Create(3, 1);
            ProxySpriteManager.Create(10, 1);
            GameObjectManager.Create(3, 1);
            CollisionPairManager.Create(1, 1);
            SoundManager.Create(3, 1);

           

            //---------------------------------------------------------------------------------------------------------
            // Load the Textures
            //---------------------------------------------------------------------------------------------------------

            TextureManager.Add(Texture.Name.SpaceInvaders, "SpaceInvaders.tga");

            //---------------------------------------------------------------------------------------------------------
            // Load Sounds
            //---------------------------------------------------------------------------------------------------------

            SoundManager.Add(Sound.Name.Invader1, "fastinvader1.wav");
            SoundManager.Add(Sound.Name.Invader2, "fastinvader2.wav");
            SoundManager.Add(Sound.Name.Invader3, "fastinvader3.wav");
            SoundManager.Add(Sound.Name.Invader4, "fastinvader4.wav");

            //---------------------------------------------------------------------------------------------------------
            // Create Images
            //---------------------------------------------------------------------------------------------------------

            // --- aliens ---

            ImageManager.Add(Image.Name.OctopusA, Texture.Name.SpaceInvaders, 3, 3, 12, 8);
            ImageManager.Add(Image.Name.OctopusB, Texture.Name.SpaceInvaders, 18, 3, 12, 8);
            ImageManager.Add(Image.Name.AlienA, Texture.Name.SpaceInvaders, 33, 3, 11, 8);
            ImageManager.Add(Image.Name.AlienB, Texture.Name.SpaceInvaders, 47, 3, 11, 8);
            ImageManager.Add(Image.Name.SquidA, Texture.Name.SpaceInvaders, 61, 3, 8, 8);
            ImageManager.Add(Image.Name.SquidB, Texture.Name.SpaceInvaders, 72, 3, 8, 8);
            ImageManager.Add(Image.Name.Saucer, Texture.Name.SpaceInvaders, 99, 3, 16, 8);

            ImageManager.Add(Image.Name.Missile, Texture.Name.SpaceInvaders, 3, 29, 1, 4);
            ImageManager.Add(Image.Name.Ship, Texture.Name.SpaceInvaders, 3, 14, 13, 8);

            ImageManager.Add(Image.Name.BombDagger, Texture.Name.SpaceInvaders, 42, 27, 3, 6);
            ImageManager.Add(Image.Name.BombZigZag, Texture.Name.SpaceInvaders, 18, 26, 3, 7);

            

            ImageManager.Add(Image.Name.ShieldBrick, Texture.Name.SpaceInvaders, 120, 35, 4, 2);
            ImageManager.Add(Image.Name.ShieldBrick_LeftTop0, Texture.Name.SpaceInvaders, 115, 30, 4, 2);
            ImageManager.Add(Image.Name.ShieldBrick_LeftTop1, Texture.Name.SpaceInvaders, 116, 31, 4, 2);
            ImageManager.Add(Image.Name.ShieldBrick_LeftBottom, Texture.Name.SpaceInvaders, 119, 43, 4, 2);
            ImageManager.Add(Image.Name.ShieldBrick_RightTop0, Texture.Name.SpaceInvaders, 132, 31, 4, 2);
            ImageManager.Add(Image.Name.ShieldBrick_RightTop1, Texture.Name.SpaceInvaders, 130, 31, 4, 2);
            ImageManager.Add(Image.Name.ShieldBrick_RightBottom, Texture.Name.SpaceInvaders, 126, 43, 4, 2);


            //---------------------------------------------------------------------------------------------------------
            // Create SpriteBatch
            //---------------------------------------------------------------------------------------------------------

            SpriteBatch pAliensBatch = SpriteBatchManager.Add(SpriteBatch.Name.Aliens, 1);
            SpriteBatch pBoxBatch = SpriteBatchManager.Add(SpriteBatch.Name.Boxes, 2);
            SpriteBatch pShieldsBatch = SpriteBatchManager.Add(SpriteBatch.Name.Shields, 3);

            //---------------------------------------------------------------------------------------------------------
            // Create Sprites
            //---------------------------------------------------------------------------------------------------------


            // --- aliens ---
            //attach finds most recently added GameSprite with name and adds to batch

            GameSpriteManager.Add(GameSprite.Name.PurpleOctopus, Image.Name.OctopusA, 50, 300, 25, 25, new Azul.Color(1.0f, 0.0f, 1.0f, 1.0f));
            GameSpriteManager.Add(GameSprite.Name.BlueCrab, Image.Name.AlienA, 200, 100, 25, 25, new Azul.Color(0.0f, 1.0f, 1.0f, 1.0f));
            GameSpriteManager.Add(GameSprite.Name.GreenSquid, Image.Name.SquidA, 200, 300, 25, 25, new Azul.Color(0.0f, 1.0f, 0.0f, 1.0f));
            GameSpriteManager.Add(GameSprite.Name.OrangeSaucer, Image.Name.Saucer, 50, 550, 25, 25, new Azul.Color(1.0f, 0.5f, 0.0f, 1.0f));

            //-----Missile----
            GameSpriteManager.Add(GameSprite.Name.Missile, Image.Name.Missile, 50, 50, 5, 25, new Azul.Color(1.0f, 0.5f, 0.0f, 1.0f));
            //----Player Ship----
            GameSpriteManager.Add(GameSprite.Name.Ship, Image.Name.Ship, 500, 100, 55, 25, new Azul.Color(0.0f, 1.0f, 1.0f, 1.0f));
            //----Bombs----
            GameSpriteManager.Add(GameSprite.Name.BombDagger, Image.Name.BombDagger, 50, 50, 10, 25, new Azul.Color(1.0f, 0.5f, 0.0f, 1.0f));
            GameSpriteManager.Add(GameSprite.Name.BombZigZag, Image.Name.BombZigZag, 50, 50, 10, 25, new Azul.Color(1.0f, 0.5f, 0.0f, 1.0f));
            //----Shield----
            GameSpriteManager.Add(GameSprite.Name.ShieldBrick, Image.Name.ShieldBrick, 50, 25, 14, 7, new Azul.Color(1.0f, 0.5f, 0.0f, 1.0f));
            GameSpriteManager.Add(GameSprite.Name.ShieldBrick_LeftTop0, Image.Name.ShieldBrick_LeftTop0, 50, 25, 14, 7, new Azul.Color(1.0f, 0.5f, 0.0f, 1.0f));
            GameSpriteManager.Add(GameSprite.Name.ShieldBrick_LeftTop1, Image.Name.ShieldBrick_LeftTop1, 50, 25, 14, 7, new Azul.Color(1.0f, 0.5f, 0.0f, 1.0f));
            GameSpriteManager.Add(GameSprite.Name.ShieldBrick_LeftBottom, Image.Name.ShieldBrick_LeftBottom, 50, 25, 14, 7, new Azul.Color(1.0f, 0.5f, 0.0f, 1.0f));
            GameSpriteManager.Add(GameSprite.Name.ShieldBrick_RightTop0, Image.Name.ShieldBrick_RightTop0, 50, 25, 14, 7, new Azul.Color(1.0f, 0.5f, 0.0f, 1.0f));
            GameSpriteManager.Add(GameSprite.Name.ShieldBrick_RightTop1, Image.Name.ShieldBrick_RightTop1, 50, 25, 14, 7, new Azul.Color(1.0f, 0.5f, 0.0f, 1.0f));
            GameSpriteManager.Add(GameSprite.Name.ShieldBrick_RightBottom, Image.Name.ShieldBrick_RightBottom, 50, 25, 14, 7, new Azul.Color(1.0f, 0.5f, 0.0f, 1.0f));

            //---------------------------------------------------------------------------------------------------------
            // Input
            //---------------------------------------------------------------------------------------------------------

            InputSubject pInputSubject;
            pInputSubject = InputManager.GetArrowRightSubject();
            pInputSubject.Attach(new MoveRightObserver());

            pInputSubject = InputManager.GetArrowLeftSubject();
            pInputSubject.Attach(new MoveLeftObserver());

            pInputSubject = InputManager.GetSpaceSubject();
            pInputSubject.Attach(new ShootObserver());

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

            GameObjectManager.Attach(pShipRoot);

            ShipManager.Create();

            //---------------------------------------------------------------------------------------------------------
            // Create Aliens
            //---------------------------------------------------------------------------------------------------------

            AlienFactory AF = new AlienFactory(SpriteBatch.Name.Aliens, SpriteBatch.Name.Boxes);
            GameObject pAlienGrid = AF.Create(GameObject.Name.AlienGrid);
           

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

            AlienGridMoveEvent pGridMoveEvent = new AlienGridMoveEvent();
            TimerManager.Add(TimeEvent.Name.MoveAlienGrid,  pGridMoveEvent, 1.0f);

            //---------------------------------------------------------------------------------------------------------
            // Shield 
            //---------------------------------------------------------------------------------------------------------

            // Create the factory 
            ShieldFactory SF = new ShieldFactory(SpriteBatch.Name.Shields, SpriteBatch.Name.Boxes);
            
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

                GameObjectManager.Attach(pShieldGrid);
            }
            //---------------------------------------------------------------------------------------------------------
            // Create CollisionPairs 
            //---------------------------------------------------------------------------------------------------------

            //Why does the order that left/right wall are added matter??????? reverse order breaks game
            CollisionPair pShipWallRightPair = CollisionPairManager.Add(CollisionPair.Name.Ship_Wall_Right, pShipRoot, pWallRight);
            Debug.Assert(pShipWallRightPair != null);

            CollisionPair pShipWallLeftPair = CollisionPairManager.Add(CollisionPair.Name.Ship_Wall_Left, pShipRoot, pWallLeft);
            Debug.Assert(pShipWallLeftPair != null);

            CollisionPair pAlienMissilePair  = CollisionPairManager.Add(CollisionPair.Name.Alien_Missile, pMissileGroup, pAlienGrid);
            Debug.Assert(pAlienMissilePair != null);

            CollisionPair pMissileWallPair = CollisionPairManager.Add(CollisionPair.Name.Missile_Wall_Top, pMissileGroup, pWallTop);
            Debug.Assert(pMissileWallPair != null);

            CollisionPair pAlienWallPair = CollisionPairManager.Add(CollisionPair.Name.Alien_Wall, pAlienGrid, pWallGroup);
            Debug.Assert(pAlienWallPair != null);

            CollisionPair pBombWallPair = CollisionPairManager.Add(CollisionPair.Name.Bomb_Wall_Bottom, pBombRoot, pWallBottom);
            Debug.Assert(pBombWallPair != null);


            pShipWallLeftPair.Attach(new ShipStopLeftObserver());
            pShipWallRightPair.Attach(new ShipStopRightObserver());

            pMissileWallPair.Attach(new ShipReadyObserver());//This is called when Missile is done flying
            pMissileWallPair.Attach(new ShipRemoveMissileObserver());

            pAlienMissilePair.Attach(new ShipReadyObserver());
            pAlienMissilePair.Attach(new ShipRemoveMissileObserver());
            pAlienMissilePair.Attach(new RemoveAlienObserver());

            pAlienWallPair.Attach(new GridObserver());

            pBombWallPair.Attach(new RemoveBombObserver());

            //---------------------------------------------------------------------------------------------------------
            // Dumps
            //---------------------------------------------------------------------------------------------------------

            //TextureManager.Dump();
            //ImageManager.Dump();
            //GameSpriteManager.Dump();
            //SpriteBatchManager.Dump();
            //GameObjectManager.Dump();//broken
        }

        //-----------------------------------------------------------------------------
        // Game::Update()
        //      Called once per frame, update data, tranformations, etc
        //      Use this function to control process order
        //      Input, AI, Physics, Animation, and Graphics
        //-----------------------------------------------------------------------------

        public override void Update()
        {
            // Add your update below this line: ----------------------------
            TimerManager.Update(this.GetTime());

            
            InputManager.Update();
            SoundManager.Update();
            GameObjectManager.Update();//<---

            //Collision Checks
            CollisionPairManager.Process();

            // Delete any objects here...
            DelayedObjectManager.Process();

        }

        //-----------------------------------------------------------------------------
        // Game::Draw()
        //		This function is called once per frame
        //	    Use this for draw graphics to the screen.
        //      Only do rendering here
        //-----------------------------------------------------------------------------
        public override void Draw()
        {
            SpriteBatchManager.Draw();
        }

        //-----------------------------------------------------------------------------
        // Game::UnLoadContent()
        //       unload content (resources loaded above)
        //       unload all content that was loaded before the Engine Loop started
        //-----------------------------------------------------------------------------
        public override void UnLoadContent()
        {

        }
    }
}

