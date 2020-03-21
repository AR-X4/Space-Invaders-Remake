
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ScenePlayerSelect : SceneState
    {
        // ---------------------------------------------------
        // Data
        // ---------------------------------------------------
        public SpriteBatchManager poSpriteBatchManager;
        private InputManager poInputManager;
        private FontManager poFontManager;


        public ScenePlayerSelect()
        {
            this.Initialize();

        }
        public override void Handle()
        {
            //set state of scene context to Scene Play

            ScenePlay.StartTimeDelta += (Simulation.GetTotalTime() - SceneOver.SwitchTime);
            ScenePlay2.StartTimeDelta += (Simulation.GetTotalTime() - SceneOver.SwitchTime);
            SpaceInvaders.pSceneContext.SetState(SceneContext.Scene.Play1);
        }
        public override void Initialize()
        {
            //---------------------------------------------------------------------------------------------------------
            // Create SpriteBatch
            //---------------------------------------------------------------------------------------------------------
            this.poSpriteBatchManager = new SpriteBatchManager(3, 1);
            SpriteBatchManager.SetActive(this.poSpriteBatchManager);

            SpriteBatch pSB_Texts = SpriteBatchManager.Add(SpriteBatch.Name.Texts, 4);


            //---------------------------------------------------------------------------------------------------------
            // Create Texts
            //---------------------------------------------------------------------------------------------------------
            this.poFontManager = new FontManager(3, 1);
            FontManager.SetActive(this.poFontManager);

            FontManager.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "PUSH", 380f, SpaceInvaders.ScreenHeight - 250f, 15f, 25f);

            FontManager.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "1 OR 2PLAYERS BUTTON", 225f, SpaceInvaders.ScreenHeight - 350f, 15f, 25f);



            FontManager.Add(Font.Name.Header, SpriteBatch.Name.Texts, "SCORE<1>       HI-SCORE       SCORE<2>", 20f, SpaceInvaders.ScreenHeight - 20f, 15f, 25f);
            FontManager.Add(Font.Name.Player1Score, SpriteBatch.Name.Texts, "0000", 65f, SpaceInvaders.ScreenHeight - 70f, 15f, 25f);
            FontManager.Add(Font.Name.Player2Score, SpriteBatch.Name.Texts, "0000", SpaceInvaders.ScreenWidth - 156f, SpaceInvaders.ScreenHeight - 70f, 15f, 25f);
            FontManager.Add(Font.Name.HiScore, SpriteBatch.Name.Texts, "0000", 380f, SpaceInvaders.ScreenHeight - 70f, 15f, 25f);

            //---------------------------------------------------------------------------------------------------------
            // Input
            //---------------------------------------------------------------------------------------------------------
            this.poInputManager = new InputManager();
            InputManager.SetActive(this.poInputManager);

            InputSubject pInputSubject = InputManager.Get1KeySubject();
            pInputSubject.Attach(new SelectPlayObserver());

            pInputSubject = InputManager.Get2KeySubject();
            pInputSubject.Attach(new SelectPlay2Observer());

        }
        public override void Update(float systemTime)
        {
            FontManager.Update(Font.Name.Player1Score, SpaceInvaders.pPlayer1Score);
            FontManager.Update(Font.Name.Player2Score, SpaceInvaders.pPlayer2Score);
            FontManager.Update(Font.Name.HiScore, SpaceInvaders.pHiScore);

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

            SpaceInvaders.Player1Mode = true;
            
        }
    }
}
