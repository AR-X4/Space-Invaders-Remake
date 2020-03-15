using System;

namespace SpaceInvaders
{
    public class SceneContext
    {
        public enum Scene
        {
            Select,
            PlayerSelect,
            Play1,
            Play2,
            Over
        }

        // ----------------------------------------------------
        // Data: 
        // -------------------------------------------o---------
        public SceneState pSceneState;
        SceneSelect poSceneSelect;
        ScenePlayerSelect poScenePlayerSelect;
        SceneOver poSceneOver;
        ScenePlay poScenePlay;

        public SceneContext()
        {
            // reserve the states
            this.poSceneSelect = new SceneSelect();
            this.poScenePlayerSelect = new ScenePlayerSelect();
            this.poScenePlay = new ScenePlay();
            this.poSceneOver = new SceneOver();

            // initialize to the select state
            this.pSceneState = this.poSceneSelect;
            this.pSceneState.Transition();
        }

        public SceneState GetState()
        {
            return this.pSceneState;
        }
        public void SetState(Scene eScene)
        {
            switch (eScene)
            {
                case Scene.Select:
                    this.pSceneState = this.poSceneSelect;
                    this.pSceneState.Transition();
                    break;

                case Scene.PlayerSelect:
                    this.pSceneState = this.poScenePlayerSelect;
                    this.pSceneState.Transition();
                    break;

                case Scene.Play1:
                    this.pSceneState = this.poScenePlay;
                    this.pSceneState.Transition();
                    break;

                case Scene.Over:
                    this.pSceneState = this.poSceneOver;
                    this.pSceneState.Transition();
                    break;

            }
        }
    }
}
