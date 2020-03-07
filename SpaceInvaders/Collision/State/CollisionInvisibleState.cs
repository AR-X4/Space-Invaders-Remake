using System;
using System.Diagnostics;


namespace SpaceInvaders
{
    public class CollisionInvisibleState : CollisionState
    {
        public override void Handle()
        {
            CollisionStateManager.SetState(CollisionStateManager.StateName.Visible);
        }

        public override void ToggleVisibility()
        {
            SpriteBatch pBoxBatch = SpriteBatchManager.Find(SpriteBatch.Name.Boxes);
            pBoxBatch.SetDrawBool(true);
            this.Handle();
        }


    }
}
