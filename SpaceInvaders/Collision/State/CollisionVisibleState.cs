using System;
using System.Diagnostics;


namespace SpaceInvaders
{
    public class CollisionVisibleState : CollisionState
    {
        public override void Handle()
        {
            CollisionStateManager.SetState(CollisionStateManager.StateName.Invisible);
        }

        public override void ToggleVisibility()
        {
            SpriteBatch pBoxBatch = SpriteBatchManager.Find(SpriteBatch.Name.Boxes);
            pBoxBatch.SetDrawBool(false);

            this.Handle();
        }


    }
}
