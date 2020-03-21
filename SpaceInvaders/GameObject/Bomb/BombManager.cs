using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class BombManager
    {
        public enum StateName
        {
            Ready,
            BombDropping
        }

        // Data: ----------------------------------------------
        private static BombManager instance = null;

        //Reference
        private static BombReadyState pReadyState;
        private static BombFallingState pFallingState;

        private BombManager()
           
        {

            pReadyState = new BombReadyState();
            pFallingState = new BombFallingState();
        }

        public static void Create()
        {

            // make sure its the first time
            Debug.Assert(instance == null);

            // Do the initialization
            if (instance == null)
            {
                instance = new BombManager();
            }
            Debug.Assert(instance != null);
            
        }
        public static void Destroy()
        {
            BombManager pMan = BombManager.GetInstance();
            Debug.Assert(pMan != null);

            // Do something clever here
            // track peak number of active nodes
            // print stats on destroy
            // invalidate the singleton
        }

        private static BombManager GetInstance()
        {
            Debug.Assert(instance != null);

            return instance;
        }

     

        public static Bomb CreateBomb(AlienColumn pOwner)
        {
            // each column owns one instance of a bomb that is reused.
            // create a null bomb
            Bomb pBomb = new Bomb(pOwner);

            SpriteBatch pSB_Aliens = SpriteBatchManager.Find(SpriteBatch.Name.Aliens);
            SpriteBatch pSB_Boxes = SpriteBatchManager.Find(SpriteBatch.Name.Boxes);

            pBomb.ActivateCollisionSprite(pSB_Boxes);
            pBomb.ActivateGameSprite(pSB_Aliens);

            GameObject pBombRoot = GameObjectManager.Find(GameObject.Name.BombRoot);
            Debug.Assert(pBombRoot != null);

            // Add to GameObject Tree - {update and collisions}
            pBombRoot.Add(pBomb);

            return pBomb;
        }

        public static void Reset(Bomb pNode)
        {
           // Debug.WriteLine("Remove Bomb!!!!");
          
            Debug.Assert(pNode != null);

            pNode.ResetBomb();
        }

        public static void ActivateBomb(AlienColumn pBombOwner) {

            
            pBombOwner.pBomb.delta = 1.5f;
            RandomizeBombType(pBombOwner.pBomb);
            pBombOwner.pBomb.bMarkForDeath = false;
            pBombOwner.pBomb.pStrategy.Reset(pBombOwner.pBomb.y);

        }


        public static void RandomizeBombType(Bomb pBomb)
        {
            BombManager pBombMan = BombManager.GetInstance();
            float rand = RandomManager.RandomInt(1, 4);

            switch (rand)
            {
                case 1:
                    pBomb.pStrategy = pBomb.pZigZag;
                    pBomb.pProxySprite.Set(GameSprite.Name.BombZigZag);
                    break;
                case 2:
                    pBomb.pStrategy = pBomb.pDaggers;
                    pBomb.pProxySprite.Set(GameSprite.Name.BombDagger);
                    break;
                case 3:
                    pBomb.pStrategy = pBomb.pStraight;
                    pBomb.pProxySprite.Set(GameSprite.Name.BombStraight);
                    break;
            }
            pBomb.poColObj.poColRect.Set(pBomb.pProxySprite.pSprite.GetScreenRect());
        }


        public static void SetState(StateName inState, AlienColumn pCol)
        {
            BombManager pBombMan = BombManager.GetInstance();
            Debug.Assert(pBombMan != null);

            switch (inState)
            {
                case StateName.Ready:
                    pCol.state = pReadyState;
                    break;
                case StateName.BombDropping:
                    pCol.state = pFallingState;
                    break;
            }
        }
    }
}
