using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class SpaceInvaders : Azul.Game
    {
        public static SceneContext pSceneContext = null;



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

            //Constant Managers
            TextureManager.Create(1, 1);
            ImageManager.Create(5, 2);
            SoundManager.Create(3, 1);
            GameSpriteManager.Create(4, 2);
            BoxSpriteManager.Create(3, 1);//?? delete this? not in use currently
            TimerManager.Create(3, 1);
            CollisionPairManager.Create(1, 1);
            CollisionStateManager.Create();

            Simulation.Create();

            //State-unique Managers

            SpriteBatchManager.Create();
            GameObjectManager.Create();
            InputManager.Create();

            GlyphManager.Create(3, 1);
            FontManager.Create(1, 1);

            ProxySpriteManager.Create(10, 1);//not in use currently

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
            SoundManager.Add(Sound.Name.UFO, "ufo_lowpitch.wav");
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

            //InputSubject pInputSubject;
            //pInputSubject = InputManager.GetArrowRightSubject();
            //pInputSubject.Attach(new MoveRightObserver());

            //pInputSubject = InputManager.GetArrowLeftSubject();
            //pInputSubject.Attach(new MoveLeftObserver());

            //pInputSubject = InputManager.GetSpaceSubject();
            //pInputSubject.Attach(new ShootObserver());
            //pInputSubject.Attach(new ShootSoundObserver());

            //pInputSubject = InputManager.GetCKeySubject();
            //pInputSubject.Attach(new ToggleCollisionBoxObserver());



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
    }
}

