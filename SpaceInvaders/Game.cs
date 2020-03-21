using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class SpaceInvaders : Azul.Game
    {
        public static SceneContext pSceneContext = null;

        public static float ScreenWidth;
        public static float ScreenHeight;
        public static int pHiScore;
        public static int pPlayer1Score;
        public static int pPlayer2Score;
        public static bool Player1Mode;

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
            this.SetWidthHeight(896, 1024);
            this.SetClearColor(0.0f, 0.0f, 0.0f, 1.0f);

            
        }

        //-----------------------------------------------------------------------------
        // Game::LoadContent()
        //		Allows you to load all content needed for your engine,
        //	    such as objects, graphics, etc.
        //-----------------------------------------------------------------------------
        public override void LoadContent()
        {
            ScreenWidth = this.GetScreenWidth();
            ScreenHeight = this.GetScreenHeight();

            //---------------------------------------------------------------------------------------------------------
            // Init Managers
            //---------------------------------------------------------------------------------------------------------

            //Constant Managers
            TextureManager.Create(1, 1);
            ImageManager.Create(5, 2);
            SoundManager.Create(3, 1);
            GameSpriteManager.Create(4, 2);
            BoxSpriteManager.Create(3, 1);
            CollisionPairManager.Create(1, 1);
            CollisionStateManager.Create();
            GlyphManager.Create(3, 1);
            Simulation.Create();
            RandomManager.Create();
            BombManager.Create();
            ShipManager.Create();

            //State-unique Managers
            SpriteBatchManager.Create();
            GameObjectManager.Create();
            InputManager.Create();
            FontManager.Create();
            TimerManager.Create();



            //ProxySpriteManager.Create(10, 1);//not in use currently

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

            SoundManager.Add(Sound.Name.Shoot, "invaderkilled.wav");
            SoundManager.Add(Sound.Name.DeadAlien, "shoot.wav");
            SoundManager.Add(Sound.Name.UFO, "ufo_highpitch.wav");
            SoundManager.Add(Sound.Name.UFOExplosion, "ufo_lowpitch.wav");
            SoundManager.Add(Sound.Name.Explosion, "explosion.wav");

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

            ImageManager.Add(Image.Name.AlienExplosion, Texture.Name.SpaceInvaders, 83, 3, 13, 8);
            ImageManager.Add(Image.Name.SaucerExplosion, Texture.Name.SpaceInvaders, 118, 3, 21, 8);
            ImageManager.Add(Image.Name.ShipExplosionA, Texture.Name.SpaceInvaders, 19, 14, 16, 8);
            ImageManager.Add(Image.Name.ShipExplosionB, Texture.Name.SpaceInvaders, 38, 14, 16, 8);

            ImageManager.Add(Image.Name.MissileExplosion, Texture.Name.SpaceInvaders, 7, 25, 8, 8);
            ImageManager.Add(Image.Name.BombExplosion, Texture.Name.SpaceInvaders, 86, 25, 6, 8);

            ImageManager.Add(Image.Name.BombDagger, Texture.Name.SpaceInvaders, 42, 27, 3, 6);
            ImageManager.Add(Image.Name.BombZigZag, Texture.Name.SpaceInvaders, 18, 26, 3, 7);
            ImageManager.Add(Image.Name.BombStraight, Texture.Name.SpaceInvaders, 65, 26, 3, 7);
            
            ImageManager.Add(Image.Name.ShieldBrick, Texture.Name.SpaceInvaders, 120, 35, 4, 2);
            ImageManager.Add(Image.Name.ShieldBrick_LeftTop0, Texture.Name.SpaceInvaders, 115, 30, 4, 2);
            ImageManager.Add(Image.Name.ShieldBrick_LeftTop1, Texture.Name.SpaceInvaders, 116, 31, 4, 2);
            ImageManager.Add(Image.Name.ShieldBrick_LeftBottom, Texture.Name.SpaceInvaders, 119, 43, 4, 2);
            ImageManager.Add(Image.Name.ShieldBrick_RightTop0, Texture.Name.SpaceInvaders, 132, 31, 4, 2);
            ImageManager.Add(Image.Name.ShieldBrick_RightTop1, Texture.Name.SpaceInvaders, 130, 31, 4, 2);
            ImageManager.Add(Image.Name.ShieldBrick_RightBottom, Texture.Name.SpaceInvaders, 126, 43, 4, 2);

            //---------------------------------------------------------------------------------------------------------
            // Create Glyphs
            //---------------------------------------------------------------------------------------------------------
            GlyphManager.Add(Glyph.Name.Space, 32, Texture.Name.SpaceInvaders, 99, 56, 5, 7);

            GlyphManager.Add(Glyph.Name.Asterisk, 42, Texture.Name.SpaceInvaders, 115, 56, 5, 7);
            GlyphManager.Add(Glyph.Name.Hyphen, 45, Texture.Name.SpaceInvaders, 131, 56, 5, 7);

            GlyphManager.Add(Glyph.Name.Zero, 48, Texture.Name.SpaceInvaders, 3, 56, 5, 7);
            GlyphManager.Add(Glyph.Name.One, 49, Texture.Name.SpaceInvaders, 11, 56, 5, 7);
            GlyphManager.Add(Glyph.Name.Two, 50, Texture.Name.SpaceInvaders, 19, 56, 5, 7);
            GlyphManager.Add(Glyph.Name.Three, 51, Texture.Name.SpaceInvaders, 27, 56, 5, 7);
            GlyphManager.Add(Glyph.Name.Four, 52, Texture.Name.SpaceInvaders, 35, 56, 5, 7);
            GlyphManager.Add(Glyph.Name.Five, 53, Texture.Name.SpaceInvaders, 43, 56, 5, 7);
            GlyphManager.Add(Glyph.Name.Six, 54, Texture.Name.SpaceInvaders, 51, 56, 5, 7);
            GlyphManager.Add(Glyph.Name.Seven, 55, Texture.Name.SpaceInvaders, 59, 56, 5, 7);
            GlyphManager.Add(Glyph.Name.Eight, 56, Texture.Name.SpaceInvaders, 67, 56, 5, 7);
            GlyphManager.Add(Glyph.Name.Nine, 57, Texture.Name.SpaceInvaders, 75, 56, 5, 7);

            GlyphManager.Add(Glyph.Name.LessThan, 60, Texture.Name.SpaceInvaders, 83, 56, 5, 7);
            GlyphManager.Add(Glyph.Name.Equals, 61, Texture.Name.SpaceInvaders, 107, 56, 5, 7);
            GlyphManager.Add(Glyph.Name.GreaterThan, 62, Texture.Name.SpaceInvaders, 91, 56, 5, 7);
            GlyphManager.Add(Glyph.Name.Question, 63, Texture.Name.SpaceInvaders, 123, 56, 5, 7);

            GlyphManager.Add(Glyph.Name.A, 65, Texture.Name.SpaceInvaders, 3, 36, 5, 7);
            GlyphManager.Add(Glyph.Name.B, 66, Texture.Name.SpaceInvaders, 11, 36, 5, 7);
            GlyphManager.Add(Glyph.Name.C, 67, Texture.Name.SpaceInvaders, 19, 36, 5, 7);
            GlyphManager.Add(Glyph.Name.D, 68, Texture.Name.SpaceInvaders, 27, 36, 5, 7);
            GlyphManager.Add(Glyph.Name.E, 69, Texture.Name.SpaceInvaders, 35, 36, 5, 7);
            GlyphManager.Add(Glyph.Name.F, 70, Texture.Name.SpaceInvaders, 43, 36, 5, 7);
            GlyphManager.Add(Glyph.Name.G, 71, Texture.Name.SpaceInvaders, 51, 36, 5, 7);
            GlyphManager.Add(Glyph.Name.H, 72, Texture.Name.SpaceInvaders, 59, 36, 5, 7);
            GlyphManager.Add(Glyph.Name.I, 73, Texture.Name.SpaceInvaders, 67, 36, 5, 7);
            GlyphManager.Add(Glyph.Name.J, 74, Texture.Name.SpaceInvaders, 75, 36, 5, 7);
            GlyphManager.Add(Glyph.Name.K, 75, Texture.Name.SpaceInvaders, 83, 36, 5, 7);
            GlyphManager.Add(Glyph.Name.L, 76, Texture.Name.SpaceInvaders, 91, 36, 5, 7);
            GlyphManager.Add(Glyph.Name.M, 77, Texture.Name.SpaceInvaders, 99, 36, 5, 7);
            GlyphManager.Add(Glyph.Name.N, 78, Texture.Name.SpaceInvaders, 3, 46, 5, 7);
            GlyphManager.Add(Glyph.Name.O, 79, Texture.Name.SpaceInvaders, 11, 46, 5, 7);
            GlyphManager.Add(Glyph.Name.P, 80, Texture.Name.SpaceInvaders, 19, 46, 5, 7);
            GlyphManager.Add(Glyph.Name.Q, 81, Texture.Name.SpaceInvaders, 27, 46, 5, 7);
            GlyphManager.Add(Glyph.Name.R, 82, Texture.Name.SpaceInvaders, 35, 46, 5, 7);
            GlyphManager.Add(Glyph.Name.S, 83, Texture.Name.SpaceInvaders, 43, 46, 5, 7);
            GlyphManager.Add(Glyph.Name.T, 84, Texture.Name.SpaceInvaders, 51, 46, 5, 7);
            GlyphManager.Add(Glyph.Name.U, 85, Texture.Name.SpaceInvaders, 59, 46, 5, 7);
            GlyphManager.Add(Glyph.Name.V, 86, Texture.Name.SpaceInvaders, 67, 46, 5, 7);
            GlyphManager.Add(Glyph.Name.W, 87, Texture.Name.SpaceInvaders, 75, 46, 5, 7);
            GlyphManager.Add(Glyph.Name.X, 88, Texture.Name.SpaceInvaders, 83, 46, 5, 7);
            GlyphManager.Add(Glyph.Name.Y, 89, Texture.Name.SpaceInvaders, 91, 46, 5, 7);
            GlyphManager.Add(Glyph.Name.Z, 90, Texture.Name.SpaceInvaders, 99, 46, 5, 7);
                                                                           

            //---------------------------------------------------------------------------------------------------------
            // Create Sprites
            //---------------------------------------------------------------------------------------------------------

            // --- aliens ---

            GameSpriteManager.Add(GameSprite.Name.PurpleOctopus, Image.Name.OctopusA, 50, 300, 49, 33);
            GameSpriteManager.Add(GameSprite.Name.BlueCrab, Image.Name.AlienB, 200, 100, 45, 33);
            GameSpriteManager.Add(GameSprite.Name.GreenSquid, Image.Name.SquidA, 200, 300, 33, 33);
            GameSpriteManager.Add(GameSprite.Name.OrangeSaucer, Image.Name.Saucer, 50, 550, 59, 33, new Azul.Color(1.0f, 0.0f, 0.0f, 1.0f));

            //-----Missile----
            GameSpriteManager.Add(GameSprite.Name.Missile, Image.Name.Missile, 50, 50, 3, 15);
            //----Player Ship----
            GameSpriteManager.Add(GameSprite.Name.Ship, Image.Name.Ship, 500, 100, 50, 30, new Azul.Color(0.0f, 1.0f, 0.0f, 1.0f));

            //---Explosions---
            GameSpriteManager.Add(GameSprite.Name.AlienExplosion, Image.Name.AlienExplosion, 50, 50, 33, 33);
            GameSpriteManager.Add(GameSprite.Name.ShipExplosion, Image.Name.ShipExplosionA, 50, 50, 55, 35);
            GameSpriteManager.Add(GameSprite.Name.SaucerExplosion, Image.Name.SaucerExplosion, 50, 50, 45, 35, new Azul.Color(1.0f, 0.0f, 0.0f, 1.0f));
            GameSpriteManager.Add(GameSprite.Name.BombExplosion, Image.Name.BombExplosion, 50, 50, 25, 25);
            GameSpriteManager.Add(GameSprite.Name.MissileExplosionRed, Image.Name.MissileExplosion, 50, 50, 20, 20, new Azul.Color(1.0f, 0.0f, 0.0f, 1.0f));
            GameSpriteManager.Add(GameSprite.Name.MissileExplosionGreen, Image.Name.MissileExplosion, 50, 50, 20, 20, new Azul.Color(0.0f, 1.0f, 0.0f, 1.0f));
            GameSpriteManager.Add(GameSprite.Name.MissileExplosionWhite, Image.Name.MissileExplosion, 50, 50, 20, 20);

            //----Bombs----
            GameSpriteManager.Add(GameSprite.Name.BombDagger, Image.Name.BombDagger, 50, 50, 10, 25);
            GameSpriteManager.Add(GameSprite.Name.BombZigZag, Image.Name.BombZigZag, 50, 50, 10, 25);
            GameSpriteManager.Add(GameSprite.Name.BombStraight, Image.Name.BombStraight, 50, 50, 10, 25);

            //----Shield----
            GameSpriteManager.Add(GameSprite.Name.ShieldBrick, Image.Name.ShieldBrick, 50, 25, 12, 7, new Azul.Color(0.0f, 1.0f, 0.0f, 1.0f));
            GameSpriteManager.Add(GameSprite.Name.ShieldBrick_LeftTop0, Image.Name.ShieldBrick_LeftTop0, 50, 25, 12, 7, new Azul.Color(0.0f, 1.0f, 0.0f, 1.0f));
            GameSpriteManager.Add(GameSprite.Name.ShieldBrick_LeftTop1, Image.Name.ShieldBrick_LeftTop1, 50, 25, 12, 7, new Azul.Color(0.0f, 1.0f, 0.0f, 1.0f));
            GameSpriteManager.Add(GameSprite.Name.ShieldBrick_LeftBottom, Image.Name.ShieldBrick_LeftBottom, 50, 25, 12, 7, new Azul.Color(0.0f, 1.0f, 0.0f, 1.0f));
            GameSpriteManager.Add(GameSprite.Name.ShieldBrick_RightTop0, Image.Name.ShieldBrick_RightTop0, 50, 25, 12, 7, new Azul.Color(0.0f, 1.0f, 0.0f, 1.0f));
            GameSpriteManager.Add(GameSprite.Name.ShieldBrick_RightTop1, Image.Name.ShieldBrick_RightTop1, 50, 25, 12, 7, new Azul.Color(0.0f, 1.0f, 0.0f, 1.0f));
            GameSpriteManager.Add(GameSprite.Name.ShieldBrick_RightBottom, Image.Name.ShieldBrick_RightBottom, 50, 25, 12, 7, new Azul.Color(0.0f, 1.0f, 0.0f, 1.0f));

            //----Ground----
            GameSpriteManager.Add(GameSprite.Name.Ground, Image.Name.ShieldBrick, 50, 50, SpaceInvaders.ScreenWidth - 10, 5, new Azul.Color(0.0f, 1.0f, 0.0f, 1.0f));

            //--------------------------------------------------------------------------
            //Create Scenes
            //--------------------------------------------------------------------------
            pHiScore = 0;
            pPlayer1Score = 0;
            pPlayer2Score = 0;

            pSceneContext = new SceneContext();
        }

        //-----------------------------------------------------------------------------
        // Game::Update()
        //      Called once per frame, update data, tranformations, etc
        //      Use this function to control process order
        //      Input, AI, Physics, Animation, and Graphics
        //-----------------------------------------------------------------------------

        public override void Update()
        {
            SoundManager.Update();
            // Update the scene
            Simulation.Update(this.GetTime());
            pSceneContext.GetState().Update(this.GetTime());

        }

        //-----------------------------------------------------------------------------
        // Game::Draw()
        //		This function is called once per frame
        //	    Use this for draw graphics to the screen.
        //      Only do rendering here
        //-----------------------------------------------------------------------------
        public override void Draw()
        {
            pSceneContext.GetState().Draw();
        }

        //-----------------------------------------------------------------------------
        // Game::UnLoadContent()
        //       unload content (resources loaded above)
        //       unload all content that was loaded before the Engine Loop started
        //-----------------------------------------------------------------------------
        public override void UnLoadContent()
        {

        }

        public static void UpdateHiScore() {
            if (SpaceInvaders.pPlayer1Score > SpaceInvaders.pHiScore)
            {
                SpaceInvaders.pHiScore = SpaceInvaders.pPlayer1Score;
            }
            if (SpaceInvaders.pPlayer2Score > SpaceInvaders.pHiScore)
            {
                SpaceInvaders.pHiScore = SpaceInvaders.pPlayer2Score;
            }
        }
    }


}

