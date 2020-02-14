using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class SpaceInvaders : Azul.Game
    {
        GameSprite pPurpleOctopus;
        GameSprite pPurpleOctopus2;
        GameSprite pPurpleOctopus3;
        GameSprite pPurpleOctopus4;
        
        GameSprite pBlueCrab;
        GameSprite pBlueCrab2;
        GameSprite pBlueCrab3;
        GameSprite pBlueCrab4;
        
        GameSprite pGreenSquid;
        GameSprite pGreenSquid2;
        GameSprite pGreenSquid3;
        GameSprite pGreenSquid4;
        
        GameSprite pOrangeSaucer;
        GameSprite pOrangeSaucer2;
        GameSprite pOrangeSaucer3;
        GameSprite pOrangeSaucer4;

        float redSpeedx = 2.0f;
        float redSpeedx2 = 2.0f;
        float redSpeedx3 = 2.0f;
        float redSpeedx4 = 2.0f;

        float yellowSpeedY = 2.0f;
        float yellowSpeedY2 = 2.0f;
        float yellowSpeedY3 = 2.0f;
        float yellowSpeedY4 = 2.0f;

        float whiteBirdSpeed = 0.02f;

        

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
            this.SetClearColor(0.4f, 0.4f, 0.8f, 1.0f);
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

            //---------------------------------------------------------------------------------------------------------
            // Load the Textures
            //---------------------------------------------------------------------------------------------------------

            TextureManager.Add(Texture.Name.Aliens, "SpaceInvaders.tga");

            //---------------------------------------------------------------------------------------------------------
            // Create Images
            //---------------------------------------------------------------------------------------------------------

            // --- angry birds ---

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

            SpriteBatch pPurpleOctopusBatch = SpriteBatchManager.Add(SpriteBatch.Name.Aliens, 50);
            SpriteBatch pBlueCrabBatch = SpriteBatchManager.Add(SpriteBatch.Name.Aliens, 10);
            SpriteBatch pGreenSquidBatch = SpriteBatchManager.Add(SpriteBatch.Name.Aliens, 25);
            SpriteBatch pOrangeSaucerBatch = SpriteBatchManager.Add(SpriteBatch.Name.Aliens, 90);
            SpriteBatch pSB_Boxes = SpriteBatchManager.Add(SpriteBatch.Name.Boxes, 95);

            //---------------------------------------------------------------------------------------------------------
            // Create Sprites
            //---------------------------------------------------------------------------------------------------------

            // --- BoxSprites ---
            BoxSpriteManager.Add(BoxSprite.Name.Box2, 500.0f, 300.0f, 50.0f, 100.0f, new Azul.Color(1.0f, 0.0f, 0.0f, 1.0f));
            pSB_Boxes.Attach(BoxSprite.Name.Box2);

            // --- aliens ---
            //attach finds most recently added GameSprite with name and adds to batch

            pPurpleOctopus = GameSpriteManager.Add(GameSprite.Name.PurpleOctopus, Image.Name.OctopusA, 50, 300, 50, 50, new Azul.Color(1.0f, 0.0f, 1.0f, 1.0f));
            pPurpleOctopusBatch.Attach(GameSprite.Name.PurpleOctopus);
            //pPurpleOctopus2 = GameSpriteManager.Add(GameSprite.Name.PurpleOctopus, Image.Name.OctopusA, 150, 300, 50, 50, new Azul.Color(1.0f, 0.0f, 0.0f, 1.0f));
            //pPurpleOctopusBatch.Attach(GameSprite.Name.PurpleOctopus);
            //pPurpleOctopus3 = GameSpriteManager.Add(GameSprite.Name.PurpleOctopus, Image.Name.OctopusA, 250, 300, 50, 50, new Azul.Color(1.0f, 0.0f, 0.0f, 1.0f));
            //pPurpleOctopusBatch.Attach(GameSprite.Name.PurpleOctopus);
            //pPurpleOctopus4 = GameSpriteManager.Add(GameSprite.Name.PurpleOctopus, Image.Name.OctopusA, 350, 300, 50, 50, new Azul.Color(1.0f, 0.0f, 0.0f, 1.0f));
            //pPurpleOctopusBatch.Attach(GameSprite.Name.PurpleOctopus);

            pBlueCrab = GameSpriteManager.Add(GameSprite.Name.BlueCrab, Image.Name.AlienA, 200, 100, 30, 30, new Azul.Color(0.0f, 0.0f, 1.0f, 1.0f));
            pBlueCrabBatch.Attach(GameSprite.Name.BlueCrab);
            //pBlueCrab2 = GameSpriteManager.Add(GameSprite.Name.BlueCrab, Image.Name.AlienA, 250, 150, 30, 30);
            //pBlueCrabBatch.Attach(GameSprite.Name.BlueCrab);
            //pBlueCrab3 = GameSpriteManager.Add(GameSprite.Name.BlueCrab, Image.Name.AlienA, 500, 200, 30, 30);
            //pBlueCrabBatch.Attach(GameSprite.Name.BlueCrab);
            //pBlueCrab4 = GameSpriteManager.Add(GameSprite.Name.BlueCrab, Image.Name.AlienA, 550, 250, 30, 30);
            //pBlueCrabBatch.Attach(GameSprite.Name.BlueCrab);

            pGreenSquid = GameSpriteManager.Add(GameSprite.Name.GreenSquid, Image.Name.SquidA, 200, 300, 65, 65, new Azul.Color(0.0f, 1.0f, 0.0f, 1.0f));
            pGreenSquidBatch.Attach(GameSprite.Name.GreenSquid);
            //pGreenSquid2 = GameSpriteManager.Add(GameSprite.Name.GreenSquid, Image.Name.SquidA, 250, 300, 65, 65);
            //pGreenSquidBatch.Attach(GameSprite.Name.GreenSquid);
            //pGreenSquid3 = GameSpriteManager.Add(GameSprite.Name.GreenSquid, Image.Name.SquidA, 500, 300, 65, 65);
            //pGreenSquidBatch.Attach(GameSprite.Name.GreenSquid);
            //pGreenSquid4 = GameSpriteManager.Add(GameSprite.Name.GreenSquid, Image.Name.SquidA, 550, 300, 65, 65);
            //pGreenSquidBatch.Attach(GameSprite.Name.GreenSquid);

            pOrangeSaucer = GameSpriteManager.Add(GameSprite.Name.OrangeSaucer, Image.Name.Saucer, 50, 550, 10, 10, new Azul.Color(1.0f, 0.5f, 0.0f, 1.0f));
            pOrangeSaucerBatch.Attach(GameSprite.Name.OrangeSaucer);
            //pOrangeSaucer2 = GameSpriteManager.Add(GameSprite.Name.OrangeSaucer, Image.Name.Saucer, 50, 50, 10, 10);
            //pOrangeSaucerBatch.Attach(GameSprite.Name.OrangeSaucer);
            //pOrangeSaucer3 = GameSpriteManager.Add(GameSprite.Name.OrangeSaucer, Image.Name.Saucer, 750, 50, 10, 10);
            //pOrangeSaucerBatch.Attach(GameSprite.Name.OrangeSaucer);
            //pOrangeSaucer4 = GameSpriteManager.Add(GameSprite.Name.OrangeSaucer, Image.Name.Saucer, 750, 550, 10, 10);
            //pOrangeSaucerBatch.Attach(GameSprite.Name.OrangeSaucer);

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
            // Dumps
            //---------------------------------------------------------------------------------------------------------

            //TextureManager.Dump();
            //ImageManager.Dump();
            // GameSpriteManager.Dump();
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

            //--------------------------------------------------------
            // Purple Octopus
            //--------------------------------------------------------
            if (pPurpleOctopus.x > this.GetScreenWidth() || pPurpleOctopus.x < 0.0f)
            {
                redSpeedx *= -1.0f;
            }
            pPurpleOctopus.x += redSpeedx;
            pPurpleOctopus.Update();

            //if (pRedBird2.x > this.GetScreenWidth() || pRedBird2.x < 0.0f)
            //{
            //    redSpeedx2 *= -1.0f;
            //}
            //pRedBird2.x += redSpeedx2;
            //pRedBird2.Update();

            //if (pRedBird3.x > this.GetScreenWidth() || pRedBird3.x < 0.0f)
            //{
            //    redSpeedx3 *= -1.0f;
            //}
            //pRedBird3.x += redSpeedx3;
            //pRedBird3.Update();

            //if (pRedBird4.x > this.GetScreenWidth() || pRedBird4.x < 0.0f)
            //{
            //    redSpeedx4 *= -1.0f;
            //}
            //pRedBird4.x += redSpeedx4;
            //pRedBird4.Update();

            //--------------------------------------------------------
            // Blue Crab
            //--------------------------------------------------------

            if (pBlueCrab.y > this.GetScreenHeight() || pBlueCrab.y < 0.0f)
            {
                yellowSpeedY *= -1;
            }

            pBlueCrab.y += yellowSpeedY;
            pBlueCrab.Update();

            //if (pYellowBird2.y > this.GetScreenHeight() || pYellowBird2.y < 0.0f)
            //{
            //    yellowSpeedY2 *= -1;
            //}
            //pYellowBird2.y += yellowSpeedY2;
            //pYellowBird2.Update();

            //if (pYellowBird3.y > this.GetScreenHeight() || pYellowBird3.y < 0.0f)
            //{
            //    yellowSpeedY3 *= -1;
            //}
            //pYellowBird3.y += yellowSpeedY3;
            //pYellowBird3.Update();

            //if (pYellowBird4.y > this.GetScreenHeight() || pYellowBird4.y < 0.0f)
            //{
            //    yellowSpeedY4 *= -1;
            //}
            //pYellowBird4.y += yellowSpeedY4;
            //pYellowBird4.Update();

            //--------------------------------------------------------
            // Green Squid
            //--------------------------------------------------------

            pGreenSquid.angle += 0.025f;
            //pGreenBird2.angle += 0.025f;
            //pGreenBird3.angle += 0.025f;
            //pGreenBird4.angle += 0.025f;

            pGreenSquid.Update();
            //pGreenBird2.Update();
            //pGreenBird3.Update();
            //pGreenBird4.Update();

            //--------------------------------------------------------
            // White Bird
            //--------------------------------------------------------
            if (pOrangeSaucer.sx > 5.0f || pOrangeSaucer.sy < 1.0f)
            {
                whiteBirdSpeed *= -1.0f;
            }
            pOrangeSaucer.sx += whiteBirdSpeed;
            pOrangeSaucer.sy += whiteBirdSpeed;
            pOrangeSaucer.Update();

            //pWhiteBird2.sx += whiteBirdSpeed;
            //pWhiteBird2.sy += whiteBirdSpeed;
            //pWhiteBird2.Update();

            //pWhiteBird3.sx += whiteBirdSpeed;
            //pWhiteBird3.sy += whiteBirdSpeed;
            //pWhiteBird3.Update();

            //pWhiteBird4.sx += whiteBirdSpeed;
            //pWhiteBird4.sy += whiteBirdSpeed;
            //pWhiteBird4.Update();

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

