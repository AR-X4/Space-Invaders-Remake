using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class CollisionStateManager
    {
        public enum StateName
        {
            Visible,
            Invisible
        }

        // Data: ----------------------------------------------
        private static CollisionStateManager instance = null;

        public static CollisionState pCurrentState;
        private static CollisionVisibleState pVisibleState;
        private static CollisionInvisibleState pInvisibleState;


        private CollisionStateManager()

        {
            pVisibleState = new CollisionVisibleState();
            pInvisibleState = new CollisionInvisibleState();
            pCurrentState = pInvisibleState;
        }

        private static CollisionStateManager GetInstance()
        {
            Debug.Assert(instance != null);

            return instance;
        }

        public static void Create()
        {

            // make sure its the first time
            Debug.Assert(instance == null);

            // Do the initialization
            if (instance == null)
            {
                instance = new CollisionStateManager();
            }
            Debug.Assert(instance != null);

        }

        public static void SetState(StateName inState)
        {
            switch (inState)
            {
                case StateName.Visible:
                    pCurrentState = pVisibleState;
                    break;
                case StateName.Invisible:
                    pCurrentState = pInvisibleState;
                    break;
            }
        }
    }
}
