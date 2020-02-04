using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class SpaceInvaders : Azul.Game
    {
        GameSprite pRedBird;
        GameSprite pRedBird2;
        GameSprite pRedBird3;
        GameSprite pRedBird4;
        
        GameSprite pWhiteBird;
        GameSprite pWhiteBird2;
        GameSprite pWhiteBird3;
        GameSprite pWhiteBird4;
        
        GameSprite pYellowBird;
        GameSprite pYellowBird2;
        GameSprite pYellowBird3;
        GameSprite pYellowBird4;
        
        GameSprite pGreenBird;
        GameSprite pGreenBird2;
        GameSprite pGreenBird3;
        GameSprite pGreenBird4;

        float redSpeedx = 2.0f;
        float redSpeedx2 = 2.0f;
        float redSpeedx3 = 2.0f;
        float redSpeedx4 = 2.0f;

        float yellowSpeedY = 2.0f;
        float yellowSpeedY2 = 2.0f;
        float yellowSpeedY3 = 2.0f;
        float yellowSpeedY4 = 2.0f;

        float whiteBirdSpeed = 0.02f;

        int count = 0;

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

            //---------------------------------------------------------------------------------------------------------
            // Load the Textures
            //---------------------------------------------------------------------------------------------------------

            TextureManager.Add(Texture.Name.Birds, "Birds.tga");
            //TextureMan.Add(Texture.Name.Birds, "Bifasdrds.tga");//for testing default pink texture

            //---------------------------------------------------------------------------------------------------------
            // Create Images
            //---------------------------------------------------------------------------------------------------------

            // --- angry birds ---

            ImageManager.Add(Image.Name.RedBird, Texture.Name.Birds, 47, 41, 48, 46);
            ImageManager.Add(Image.Name.YellowBird, Texture.Name.Birds, 124, 34, 60, 56);
            ImageManager.Add(Image.Name.GreenBird, Texture.Name.Birds, 246, 135, 99, 72);
            ImageManager.Add(Image.Name.WhiteBird, Texture.Name.Birds, 139, 131, 84, 97);

            //---------------------------------------------------------------------------------------------------------
            // Create SpriteBatch
            //---------------------------------------------------------------------------------------------------------

            SpriteBatch pRedBirdsBatch = SpriteBatchManager.Add(SpriteBatch.Name.AngryBirds, 50);
            SpriteBatch pYellowBirdsBatch = SpriteBatchManager.Add(SpriteBatch.Name.AngryBirds, 10);
            SpriteBatch pGreenBirdsBatch = SpriteBatchManager.Add(SpriteBatch.Name.AngryBirds, 25);
            SpriteBatch pWhiteBirdsBatch = SpriteBatchManager.Add(SpriteBatch.Name.AngryBirds, 90);
            SpriteBatch pSB_Boxes = SpriteBatchManager.Add(SpriteBatch.Name.Boxes, 95);

            //---------------------------------------------------------------------------------------------------------
            // Create Sprites
            //---------------------------------------------------------------------------------------------------------

            // --- BoxSprites ---
            BoxSpriteManager.Add(BoxSprite.Name.Box2, 500.0f, 300.0f, 50.0f, 100.0f, new Azul.Color(1.0f, 0.0f, 0.0f, 1.0f));
            pSB_Boxes.Attach(BoxSprite.Name.Box2);

            // --- angry birds ---
            //attah finds most recently added GameSprite with name and adds to batch

            pRedBird = GameSpriteManager.Add(GameSprite.Name.RedBird, Image.Name.RedBird, 50, 300, 50, 50);
            pRedBirdsBatch.Attach(GameSprite.Name.RedBird);
            pRedBird2 = GameSpriteManager.Add(GameSprite.Name.RedBird, Image.Name.RedBird, 150, 300, 50, 50);
            pRedBirdsBatch.Attach(GameSprite.Name.RedBird);
            pRedBird3 = GameSpriteManager.Add(GameSprite.Name.RedBird, Image.Name.RedBird, 250, 300, 50, 50);
            pRedBirdsBatch.Attach(GameSprite.Name.RedBird);
            pRedBird4 = GameSpriteManager.Add(GameSprite.Name.RedBird, Image.Name.RedBird, 350, 300, 50, 50);
            pRedBirdsBatch.Attach(GameSprite.Name.RedBird);

            pYellowBird = GameSpriteManager.Add(GameSprite.Name.YellowBird, Image.Name.YellowBird, 200, 100, 30, 30);
            pYellowBirdsBatch.Attach(GameSprite.Name.YellowBird);
            pYellowBird2 = GameSpriteManager.Add(GameSprite.Name.YellowBird, Image.Name.YellowBird, 250, 150, 30, 30);
            pYellowBirdsBatch.Attach(GameSprite.Name.YellowBird);
            pYellowBird3 = GameSpriteManager.Add(GameSprite.Name.YellowBird, Image.Name.YellowBird, 500, 200, 30, 30);
            pYellowBirdsBatch.Attach(GameSprite.Name.YellowBird);
            pYellowBird4 = GameSpriteManager.Add(GameSprite.Name.YellowBird, Image.Name.YellowBird, 550, 250, 30, 30);
            pYellowBirdsBatch.Attach(GameSprite.Name.YellowBird);

            pGreenBird = GameSpriteManager.Add(GameSprite.Name.GreenBird, Image.Name.GreenBird, 200, 300, 65, 65);
            pGreenBirdsBatch.Attach(GameSprite.Name.GreenBird);
            pGreenBird2 = GameSpriteManager.Add(GameSprite.Name.GreenBird, Image.Name.GreenBird, 250, 300, 65, 65);
            pGreenBirdsBatch.Attach(GameSprite.Name.GreenBird);
            pGreenBird3 = GameSpriteManager.Add(GameSprite.Name.GreenBird, Image.Name.GreenBird, 500, 300, 65, 65);
            pGreenBirdsBatch.Attach(GameSprite.Name.GreenBird);
            pGreenBird4 = GameSpriteManager.Add(GameSprite.Name.GreenBird, Image.Name.GreenBird, 550, 300, 65, 65);
            pGreenBirdsBatch.Attach(GameSprite.Name.GreenBird);

            pWhiteBird = GameSpriteManager.Add(GameSprite.Name.WhiteBird, Image.Name.WhiteBird, 50, 550, 10, 10);
            pWhiteBirdsBatch.Attach(GameSprite.Name.WhiteBird);
            pWhiteBird2 = GameSpriteManager.Add(GameSprite.Name.WhiteBird, Image.Name.WhiteBird, 50, 50, 10, 10);
            pWhiteBirdsBatch.Attach(GameSprite.Name.WhiteBird);
            pWhiteBird3 = GameSpriteManager.Add(GameSprite.Name.WhiteBird, Image.Name.WhiteBird, 750, 50, 10, 10);
            pWhiteBirdsBatch.Attach(GameSprite.Name.WhiteBird);
            pWhiteBird4 = GameSpriteManager.Add(GameSprite.Name.WhiteBird, Image.Name.WhiteBird, 750, 550, 10, 10);
            pWhiteBirdsBatch.Attach(GameSprite.Name.WhiteBird);

            //---------------------------------------------------------------------------------------------------------
            // Dumps
            //---------------------------------------------------------------------------------------------------------

            //TextureManager.Dump();
            //ImageManager.Dump();
            // GameSpriteManager.Dump();
            SpriteBatchManager.Dump();

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

            //--------------------------------------------------------
            // Boxes
            //--------------------------------------------------------

            BoxSprite pSpriteBox2 = BoxSpriteManager.Find(BoxSprite.Name.Box2);
            pSpriteBox2.Update();

            //--------------------------------------------------------
            // Red Bird
            //--------------------------------------------------------
            if (pRedBird.x > this.GetScreenWidth() || pRedBird.x < 0.0f)
            {
                redSpeedx *= -1.0f;
            }
            pRedBird.x += redSpeedx;
            pRedBird.Update();

            if (pRedBird2.x > this.GetScreenWidth() || pRedBird2.x < 0.0f)
            {
                redSpeedx2 *= -1.0f;
            }
            pRedBird2.x += redSpeedx2;
            pRedBird2.Update();

            if (pRedBird3.x > this.GetScreenWidth() || pRedBird3.x < 0.0f)
            {
                redSpeedx3 *= -1.0f;
            }
            pRedBird3.x += redSpeedx3;
            pRedBird3.Update();

            if (pRedBird4.x > this.GetScreenWidth() || pRedBird4.x < 0.0f)
            {
                redSpeedx4 *= -1.0f;
            }
            pRedBird4.x += redSpeedx4;
            pRedBird4.Update();

            //--------------------------------------------------------
            // Yellow Bird
            //--------------------------------------------------------

            if (pYellowBird.y > this.GetScreenHeight() || pYellowBird.y < 0.0f)
            {
                yellowSpeedY *= -1;
            }

            pYellowBird.y += yellowSpeedY;
            pYellowBird.Update();

            if (pYellowBird2.y > this.GetScreenHeight() || pYellowBird2.y < 0.0f)
            {
                yellowSpeedY2 *= -1;
            }
            pYellowBird2.y += yellowSpeedY2;
            pYellowBird2.Update();

            if (pYellowBird3.y > this.GetScreenHeight() || pYellowBird3.y < 0.0f)
            {
                yellowSpeedY3 *= -1;
            }
            pYellowBird3.y += yellowSpeedY3;
            pYellowBird3.Update();

            if (pYellowBird4.y > this.GetScreenHeight() || pYellowBird4.y < 0.0f)
            {
                yellowSpeedY4 *= -1;
            }
            pYellowBird4.y += yellowSpeedY4;
            pYellowBird4.Update();

            //--------------------------------------------------------
            // Green Bird
            //--------------------------------------------------------

            pGreenBird.angle += 0.025f;
            pGreenBird2.angle += 0.025f;
            pGreenBird3.angle += 0.025f;
            pGreenBird4.angle += 0.025f;

            pGreenBird.Update();
            pGreenBird2.Update();
            pGreenBird3.Update();
            pGreenBird4.Update();

            //--------------------------------------------------------
            // White Bird
            //--------------------------------------------------------
            if (pWhiteBird.sx > 5.0f || pWhiteBird.sy < 1.0f)
            {
                whiteBirdSpeed *= -1.0f;
            }
            pWhiteBird.sx += whiteBirdSpeed;
            pWhiteBird.sy += whiteBirdSpeed;
            pWhiteBird.Update();

            pWhiteBird2.sx += whiteBirdSpeed;
            pWhiteBird2.sy += whiteBirdSpeed;
            pWhiteBird2.Update();

            pWhiteBird3.sx += whiteBirdSpeed;
            pWhiteBird3.sy += whiteBirdSpeed;
            pWhiteBird3.Update();

            pWhiteBird4.sx += whiteBirdSpeed;
            pWhiteBird4.sy += whiteBirdSpeed;
            pWhiteBird4.Update();

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

