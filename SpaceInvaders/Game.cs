using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class SpaceInvaders : Azul.Game
    {
        
        Missile pMissile;

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
            // Setup Managers
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

            TextureManager.Add(Texture.Name.Aliens, "SpaceInvaders.tga");

            //---------------------------------------------------------------------------------------------------------
            // Load Sounds
            //---------------------------------------------------------------------------------------------------------

            SoundManager.Add(Sound.Name.Invader1, "fastinvader1.wav");

            //---------------------------------------------------------------------------------------------------------
            // Create Images
            //---------------------------------------------------------------------------------------------------------

            // --- aliens ---

            ImageManager.Add(Image.Name.OctopusA, Texture.Name.Aliens, 3, 3, 12, 8);
            ImageManager.Add(Image.Name.OctopusB, Texture.Name.Aliens, 18, 3, 12, 8);
            ImageManager.Add(Image.Name.AlienA, Texture.Name.Aliens, 33, 3, 11, 8);
            ImageManager.Add(Image.Name.AlienB, Texture.Name.Aliens, 47, 3, 11, 8);
            ImageManager.Add(Image.Name.SquidA, Texture.Name.Aliens, 61, 3, 8, 8);
            ImageManager.Add(Image.Name.SquidB, Texture.Name.Aliens, 72, 3, 8, 8);
            ImageManager.Add(Image.Name.Saucer, Texture.Name.Aliens, 99, 3, 16, 8);

            ImageManager.Add(Image.Name.Missile, Texture.Name.Aliens, 3, 29, 1, 4);


            //---------------------------------------------------------------------------------------------------------
            // Create SpriteBatch
            //---------------------------------------------------------------------------------------------------------

            SpriteBatch pAliensBatch = SpriteBatchManager.Add(SpriteBatch.Name.Aliens, 50);
            SpriteBatch pBoxBatch = SpriteBatchManager.Add(SpriteBatch.Name.Boxes, 95);

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

            pMissile = new Missile(GameObject.Name.Missile, GameSprite.Name.Missile, 405, 100);
            pMissile.ActivateGameSprite(pAliensBatch);
            pMissile.ActivateCollisionSprite(pAliensBatch);
            //GameObjectManager.Attach(pMissile);

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
            TimerManager.Add(TimeEvent.Name.SpriteAnimation, pAnimOctopus, 1, 0.5f);
            TimerManager.Add(TimeEvent.Name.SpriteAnimation, pAnimCrab, 2, 0.5f);
            TimerManager.Add(TimeEvent.Name.SpriteAnimation, pAnimSquid, 3, 0.5f);

            //---------------------------------------------------------------------------------------------------------
            // Create Walls
            //---------------------------------------------------------------------------------------------------------

            // Wall Root
            WallGroup pWallGroup = new WallGroup(GameObject.Name.WallGroup, GameSprite.Name.NullObject, 0.0f, 0.0f);
            //pWallGroup.ActivateGameSprite(pSB_Birds);
            pWallGroup.ActivateCollisionSprite(pBoxBatch);

            WallRight pWallRight = new WallRight(GameObject.Name.WallRight, GameSprite.Name.NullObject, 700, 300, 50, 500);
            pWallRight.ActivateCollisionSprite(pBoxBatch);

            WallLeft pWallLeft = new WallLeft(GameObject.Name.WallLeft, GameSprite.Name.NullObject, 50, 300, 50, 500);
            pWallLeft.ActivateCollisionSprite(pBoxBatch);

            // Add to the composite the children
            pWallGroup.Add(pWallRight);
            pWallGroup.Add(pWallLeft);

            GameObjectManager.Attach(pWallGroup);

            //---------------------------------------------------------------------------------------------------------
            // Create Aliens
            //---------------------------------------------------------------------------------------------------------

            AlienFactory AF = new AlienFactory(SpriteBatch.Name.Aliens);
            GameObject pGrid = AF.Create(GameObject.Name.AlienGrid);
           

            for (int i = 0; i < 10; i++)
            {
                GameObject pCol = AF.Create(GameObject.Name.AlienColumn);
                GameObject pGameObject;
                pGameObject = AF.Create(GameObject.Name.PurpleOctopus, 50.0f + 40 * i, 250.0f);
                pCol.Add(pGameObject);

                pGameObject = AF.Create(GameObject.Name.PurpleOctopus, 50.0f + 40 * i, 300.0f);
                pCol.Add(pGameObject);

                pGameObject = AF.Create(GameObject.Name.BlueCrab, 50.0f + 40 * i, 350.0f);
                pCol.Add(pGameObject);

                pGameObject = AF.Create(GameObject.Name.BlueCrab, 50.0f + 40 * i, 400.0f);
                pCol.Add(pGameObject);

                pGameObject = AF.Create(GameObject.Name.GreenSquid, 50.0f + 40 * i, 450.0f);
                pCol.Add(pGameObject);

                pGrid.Add(pCol);
            }
            GameObjectManager.Attach(pGrid);


            //---------------------------------------------------------------------------------------------------------
            // CollisionPair 
            //---------------------------------------------------------------------------------------------------------

            CollisionPairManager.Add(CollisionPair.Name.Alien_Missile, pMissile, pGrid);
            CollisionPair pAlienWallPair = CollisionPairManager.Add(CollisionPair.Name.Alien_Wall, pGrid, pWallGroup);

            pAlienWallPair.Attach(new GridObserver());

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
        uint i = 0;

        public override void Update()
        {
            // Add your update below this line: ----------------------------
            TimerManager.Update(this.GetTime());

            //this.pGrid.Move();

            SoundManager.Update();
            GameObjectManager.Update();

            // Move the grid
            AlienGrid pGrid = (AlienGrid)GameObjectManager.Find(GameObject.Name.AlienGrid);
            pGrid.MoveGrid();

            //Collision Checks
            CollisionPairManager.Process();

            i++;
            if (i == 150) {
                Sound test = SoundManager.Find(Sound.Name.Invader1);
                test.PlaySound();
                i = 0;
            }
       
        }

        //-----------------------------------------------------------------------------
        // Game::Draw()
        //		This function is called once per frame
        //	    Use this for draw graphics to the screen.
        //      Only do rendering here
        //-----------------------------------------------------------------------------
        public override void Draw()
        {
            // --- angry birds ---

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

