
using System.Diagnostics;

namespace SpaceInvaders
{
    public class SceneSelect : SceneState
    {
        // ---------------------------------------------------
        // Data
        // ---------------------------------------------------
        public SpriteBatchManager poSpriteBatchManager;
        private GameObjectManager poGameObjectManager;
        private InputManager poInputManager;
        

        public SceneSelect()
        {
            this.Initialize();
            
        }
        public override void Handle()
        {
            //set state of scene context to Scene Play
            SpaceInvaders.pSceneContext.SetState(SceneContext.Scene.Play);
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
            // --------------------------------------------------------------------------------------------------------
            //---------------------------------------------------------------------------------------------------------
            this.poGameObjectManager = new GameObjectManager(3, 1);
            GameObjectManager.SetActive(this.poGameObjectManager);

            Texture pTexture = TextureManager.Add(Texture.Name.Consolas36pt, "Consolas20pt.tga");
            GlyphManager.AddXml(Glyph.Name.Consolas36pt, "Consolas20pt.xml", Texture.Name.Consolas36pt);

            Font pFont = FontManager.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "Press 'P' to Play", Glyph.Name.Consolas36pt, 100, 500);
            pFont.SetColor(1.0f, 0.0f, 0.0f);

            //---------------------------------------------------------------------------------------------------------
            // Input
            //---------------------------------------------------------------------------------------------------------
            this.poInputManager = new InputManager();
            InputManager.SetActive(this.poInputManager);
            InputSubject pInputSubject = InputManager.GetPKeySubject();
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
            // update SpriteBatchMan()
            SpriteBatchManager.SetActive(this.poSpriteBatchManager);
            GameObjectManager.SetActive(this.poGameObjectManager);
            InputManager.SetActive(this.poInputManager);
        }
    }
}
