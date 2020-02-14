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
            // Setup Managers
            //---------------------------------------------------------------------------------------------------------

            TextureManager.Create(1, 1);
            ImageManager.Create(5, 2);
            GameSpriteManager.Create(4, 2);
            SpriteBatchManager.Create(3, 1);
            BoxSpriteManager.Create(3, 1);
            TimerManager.Create(3, 1);
            ProxySpriteManager.Create(10, 1);

            //---------------------------------------------------------------------------------------------------------
            // Load the Textures
            //---------------------------------------------------------------------------------------------------------

            TextureManager.Add(Texture.Name.Aliens, "SpaceInvaders.tga");

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


            //---------------------------------------------------------------------------------------------------------
            // Create SpriteBatch
            //---------------------------------------------------------------------------------------------------------

            //SpriteBatch pPurpleOctopusBatch = SpriteBatchManager.Add(SpriteBatch.Name.Aliens, 50);
            //SpriteBatch pBlueCrabBatch = SpriteBatchManager.Add(SpriteBatch.Name.Aliens, 10);
            //SpriteBatch pGreenSquidBatch = SpriteBatchManager.Add(SpriteBatch.Name.Aliens, 25);
            //SpriteBatch pOrangeSaucerBatch = SpriteBatchManager.Add(SpriteBatch.Name.Aliens, 90);
            SpriteBatch pAliensBatch = SpriteBatchManager.Add(SpriteBatch.Name.Aliens, 50);
            SpriteBatch pBoxBatch = SpriteBatchManager.Add(SpriteBatch.Name.Boxes, 95);

            //---------------------------------------------------------------------------------------------------------
            // Create Sprites
            //---------------------------------------------------------------------------------------------------------

            // --- BoxSprites ---
            BoxSpriteManager.Add(BoxSprite.Name.Box2, 500.0f, 300.0f, 50.0f, 100.0f, new Azul.Color(1.0f, 0.0f, 0.0f, 1.0f));
            pBoxBatch.Attach(BoxSprite.Name.Box2);

            // --- aliens ---
            //attach finds most recently added GameSprite with name and adds to batch

            GameSpriteManager.Add(GameSprite.Name.PurpleOctopus, Image.Name.OctopusA, 50, 300, 50, 50, new Azul.Color(1.0f, 0.0f, 1.0f, 1.0f));
            GameSpriteManager.Add(GameSprite.Name.BlueCrab, Image.Name.AlienA, 200, 100, 50, 50, new Azul.Color(0.0f, 0.0f, 1.0f, 1.0f));
            GameSpriteManager.Add(GameSprite.Name.GreenSquid, Image.Name.SquidA, 200, 300, 50, 50, new Azul.Color(0.0f, 1.0f, 0.0f, 1.0f));
            GameSpriteManager.Add(GameSprite.Name.OrangeSaucer, Image.Name.Saucer, 50, 550, 50, 50, new Azul.Color(1.0f, 0.5f, 0.0f, 1.0f));

            pAliensBatch.Attach(GameSprite.Name.PurpleOctopus);
            pAliensBatch.Attach(GameSprite.Name.BlueCrab);
            pAliensBatch.Attach(GameSprite.Name.GreenSquid);
            pAliensBatch.Attach(GameSprite.Name.OrangeSaucer);

            //---------------------------------------------------------------------------------------------------------
            // Timer
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
            // Proxy
            //---------------------------------------------------------------------------------------------------------

            // create 10 proxies
            for (int i = 0; i < 5; i++)
            {
                ProxySprite pOctopusProxy = ProxySpriteManager.Add(GameSprite.Name.PurpleOctopus);
                pOctopusProxy.x = 50.0f + 70 * i;
                pOctopusProxy.y = 100.0f;
                pAliensBatch.Attach(pOctopusProxy);

                ProxySprite pCrabProxy = ProxySpriteManager.Add(GameSprite.Name.BlueCrab);
                pCrabProxy.x = 50.0f + 70 * i;
                pCrabProxy.y = 250.0f;
                pAliensBatch.Attach(pCrabProxy);

                ProxySprite pSquidProxy = ProxySpriteManager.Add(GameSprite.Name.GreenSquid);
                pSquidProxy.x = 50.0f + 70 * i;
                pSquidProxy.y = 400.0f;
                pAliensBatch.Attach(pSquidProxy);

                ProxySprite pProxy = ProxySpriteManager.Add(GameSprite.Name.OrangeSaucer);
                pProxy.x = 50.0f + 70 * i;
                pProxy.y = 550.0f;
                pAliensBatch.Attach(pProxy);
            }

            //---------------------------------------------------------------------------------------------------------
            // Dumps
            //---------------------------------------------------------------------------------------------------------

            //TextureManager.Dump();
            //ImageManager.Dump();
            //GameSpriteManager.Dump();
            //SpriteBatchManager.Dump();

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
            //--------------------------------------------------------
            // Boxes
            //--------------------------------------------------------

            BoxSprite pSpriteBox2 = BoxSpriteManager.Find(BoxSprite.Name.Box2);
            pSpriteBox2.Update();

            GameSprite pSprite;

            pSprite = GameSpriteManager.Find(GameSprite.Name.PurpleOctopus);
            Debug.Assert(pSprite != null);
            pSprite.Update();

            pSprite = GameSpriteManager.Find(GameSprite.Name.BlueCrab);
            Debug.Assert(pSprite != null);
            pSprite.Update();

            pSprite = GameSpriteManager.Find(GameSprite.Name.GreenSquid);
            Debug.Assert(pSprite != null);
            pSprite.Update();

            pSprite = GameSpriteManager.Find(GameSprite.Name.OrangeSaucer);
            Debug.Assert(pSprite != null);
            pSprite.Update();

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

