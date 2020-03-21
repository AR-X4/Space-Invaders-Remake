
using System.Diagnostics;

namespace SpaceInvaders
{
    public class SceneSelect : SceneState
    {
        // ---------------------------------------------------
        // Data
        // ---------------------------------------------------
        public SpriteBatchManager poSpriteBatchManager;
        private InputManager poInputManager;
        private FontManager poFontManager;
        

        public SceneSelect()
        {
            this.Initialize();
            
        }
        public override void Handle()
        {
            //set state of scene context to Scene Play
            SpaceInvaders.pSceneContext.SetState(SceneContext.Scene.PlayerSelect);
        }
        public override void Initialize()
        {
            //---------------------------------------------------------------------------------------------------------
            // Create SpriteBatch
            //---------------------------------------------------------------------------------------------------------
            this.poSpriteBatchManager = new SpriteBatchManager(3, 1);
            SpriteBatchManager.SetActive(this.poSpriteBatchManager);

            SpriteBatch pSB_Texts = SpriteBatchManager.Add(SpriteBatch.Name.Texts, 4);
            SpriteBatch pSB_Aliens = SpriteBatchManager.Add(SpriteBatch.Name.Aliens, 1);

            //---------------------------------------------------------------------------------------------------------
            // Game Objects
            //---------------------------------------------------------------------------------------------------------

            OrangeSaucer pSaucer = new OrangeSaucer(GameObject.Name.Null_Object, GameSprite.Name.OrangeSaucer, 325, SpaceInvaders.ScreenHeight - 575f);
            pSaucer.ActivateGameSprite(pSB_Aliens);

            GreenSquid pSquid = new GreenSquid(GameObject.Name.Null_Object, GameSprite.Name.GreenSquid, 325, SpaceInvaders.ScreenHeight - 650f);
            pSquid.ActivateGameSprite(pSB_Aliens);

            BlueCrab pCrab = new BlueCrab(GameObject.Name.Null_Object, GameSprite.Name.BlueCrab, 325, SpaceInvaders.ScreenHeight - 725f);
            pCrab.ActivateGameSprite(pSB_Aliens);

            PurpleOctopus pOcto = new PurpleOctopus(GameObject.Name.Null_Object, GameSprite.Name.PurpleOctopus, 325, SpaceInvaders.ScreenHeight - 800f);
            pOcto.ActivateGameSprite(pSB_Aliens);



            //---------------------------------------------------------------------------------------------------------
            // Create Texts
            //---------------------------------------------------------------------------------------------------------
            this.poFontManager = new FontManager(3, 1);
            FontManager.SetActive(this.poFontManager);

            FontManager.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "PLAY", 380f, SpaceInvaders.ScreenHeight - 250f, 15f, 25f);
            
            FontManager.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "SPACE   INVADERS", 255f, SpaceInvaders.ScreenHeight - 350f, 15f, 25f);

            FontManager.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "*SCORE ADVANCE TABLE*", 200f, SpaceInvaders.ScreenHeight - 500f, 15f, 25f);

            FontManager.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "=? MYSTERY", 350f, SpaceInvaders.ScreenHeight - 575f, 15f, 25f);
            FontManager.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "=30 POINTS", 350f, SpaceInvaders.ScreenHeight - 650f, 15f, 25f);
            FontManager.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "=20 POINTS", 350f, SpaceInvaders.ScreenHeight - 725f, 15f, 25f);
            FontManager.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "=10 POINTS", 350f, SpaceInvaders.ScreenHeight - 800f, 15f, 25f);

            FontManager.Add(Font.Name.Header, SpriteBatch.Name.Texts, "SCORE<1>       HI-SCORE       SCORE<2>", 20f, SpaceInvaders.ScreenHeight - 20f, 15f, 25f);
            FontManager.Add(Font.Name.HiScore, SpriteBatch.Name.Texts, "0000          0000            0000", 65f, SpaceInvaders.ScreenHeight - 70f, 15f, 25f);
            

            //---------------------------------------------------------------------------------------------------------
            // Input
            //---------------------------------------------------------------------------------------------------------
            this.poInputManager = new InputManager();
            InputManager.SetActive(this.poInputManager);
            InputSubject pInputSubject = InputManager.GetSpaceSubject();
            pInputSubject.Attach(new SelectPlayObserver());

        }
        public override void Update(float systemTime)
        {
            InputManager.Update();
            
        }

        public override void Draw()
        {
            // draw all objects
            SpriteBatchManager.Draw();
        }

        public override void Transition()
        {
            
            SpriteBatchManager.SetActive(this.poSpriteBatchManager);
            InputManager.SetActive(this.poInputManager);
            FontManager.SetActive(this.poFontManager);
        }
    }
}
