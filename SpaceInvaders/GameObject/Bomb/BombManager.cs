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

        //Active
       

        //Reference
        private static BombReadyState pReadyState;
        private static BombFallingState pFallingState;

        private Random pRandom;

        private BombManager()
           
        {
            
            this.pRandom = new Random();


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

     

        public static Bomb CreateBomb()
        {
            // each column owns one instance of a bomb that is reused.
            // create a null bomb
            Bomb pBomb = new Bomb();

            return pBomb;
        }

        public static void Remove(Bomb pNode)
        {
            Debug.WriteLine("Remove Bomb!!!!");
          
            Debug.Assert(pNode != null);

            pNode.Remove();
            
            
        }

        public static void ActivateBomb(AlienColumn pBombOwner, float posX, float posY) {

            pBombOwner.pBomb.SetBomb(pBombOwner, posX, posY);

            // Attached to SpriteBatches
            SpriteBatch pSB_Aliens = SpriteBatchManager.Find(SpriteBatch.Name.Aliens);
            SpriteBatch pSB_Boxes = SpriteBatchManager.Find(SpriteBatch.Name.Boxes);

            pBombOwner.pBomb.ActivateCollisionSprite(pSB_Boxes);
            pBombOwner.pBomb.ActivateGameSprite(pSB_Aliens);

            // Attach the missile to the missile root
            GameObject pBombRoot = GameObjectManager.Find(GameObject.Name.BombRoot);
            Debug.Assert(pBombRoot != null);

            // Add to GameObject Tree - {update and collisions}
            pBombRoot.Add(pBombOwner.pBomb);

        }


        public static void RandomizeBombType(Bomb pBomb)
        {
            BombManager pBombMan = BombManager.GetInstance();
            float rand = pBombMan.pRandom.Next(1, 3);

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
            }
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
