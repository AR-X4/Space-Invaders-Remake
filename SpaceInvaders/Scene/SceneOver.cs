using System.Diagnostics;

namespace SpaceInvaders
{
    public class SceneOver : SceneState
    {
        // ---------------------------------------------------
        // Data
        // ---------------------------------------------------
        public SpriteBatchManager poSpriteBatchManager;
        private GameObjectManager poGameObjectManager;
        private InputManager poInputManager;


        public SceneOver()
        {
            this.Initialize();
            
        }
        public override void Handle()
        {
            //set state of scene context to Scene Select
            SpaceInvaders.pSceneContext.SetState(SceneContext.Scene.Select);
        }
        public override void Initialize()
        {
            this.poSpriteBatchManager = new SpriteBatchManager(3, 1);
            SpriteBatchManager.SetActive(this.poSpriteBatchManager);

            this.poGameObjectManager = new GameObjectManager(3, 1);
            GameObjectManager.SetActive(this.poGameObjectManager);

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
