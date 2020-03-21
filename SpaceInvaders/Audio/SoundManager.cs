using System.Diagnostics;

namespace SpaceInvaders
{
    public class SoundManager : Manager
    {
       
        private readonly Sound pNodeCompare;
        private static SoundManager pInstance = null;//for singleton pattern
        private static IrrKlang.ISoundEngine pSoundEngine = null;

        private SoundManager(int reserveNum = 1, int reserveGrow = 1)
            : base()
        {
            base.BaseInitialize(reserveNum, reserveGrow);

            this.pNodeCompare = new Sound();
            //this.pInstance = null;
        }

        //Methods

        public static void Create(int reserveNum = 1, int reserveGrow = 1)
        {
            // make sure values are ressonable 
            Debug.Assert(reserveNum > 0);
            Debug.Assert(reserveGrow > 0);

            // initialize the singleton here
            Debug.Assert(pInstance == null);
            Debug.Assert(pSoundEngine == null);

            // Do the initialization
            if (pInstance == null && pSoundEngine == null)
            {
                pInstance = new SoundManager(reserveNum, reserveGrow);
                pSoundEngine = new IrrKlang.ISoundEngine();
            }
        }

        public static void Destroy()
        {
            SoundManager pMan = SoundManager.GetInstance();
            Debug.Assert(pMan != null);

            // Do something clever here
            // track peak number of active nodes
            // print stats on destroy
            // invalidate the singleton
        }

        public static Sound Add(Sound.Name name, string pSoundName)
        {
            SoundManager pMan = SoundManager.GetInstance();
            Debug.Assert(pMan != null);

            Sound pNode = (Sound)pMan.BaseAdd();
            Debug.Assert(pNode != null);

            // Initialize the data
            Debug.Assert(pSoundName != null);
            pNode.Set(name, pSoundName, ref pSoundEngine);

            return pNode;
        }
        public static Sound Find(Sound.Name name)
        {
            SoundManager pMan = SoundManager.GetInstance();
            Debug.Assert(pMan != null);

            pMan.pNodeCompare.name = name;
            Sound pData = (Sound)pMan.BaseFind(pMan.pNodeCompare);
            return pData;
        }
        public static void Remove(Sound pNode)
        {
            SoundManager pMan = SoundManager.GetInstance();
            Debug.Assert(pMan != null);

            Debug.Assert(pNode != null);
            pMan.BaseRemove(pNode);
        }
        public static void Update() {
            Debug.Assert(pSoundEngine != null);
            pSoundEngine.Update();
        }

        public static void PlaySound(Sound pSound) {
            Debug.Assert(pSound != null);
            pSoundEngine.Play2D(pSound.GetSoundSource(), false, false, false);
        }

        public static void Dump()
        {
            SoundManager pMan = SoundManager.GetInstance();
            Debug.Assert(pMan != null);

            Debug.WriteLine("***********Sound Lists************");
            pMan.BaseDump();
        }

        private static SoundManager GetInstance()
        {
            Debug.Assert(pInstance != null);
            return pInstance;
        }

        // -------Override abstract methods-----------
        override protected DLink DerivedCreateNode()
        {
            DLink pNode = new Sound();
            Debug.Assert(pNode != null);
            return pNode;
        }
        override protected bool DerivedCompareNode(DLink pLinkA, DLink pLinkB)
        {
            Debug.Assert(pLinkA != null);
            Debug.Assert(pLinkB != null);

            Sound pDataA = (Sound)pLinkA;
            Sound pDataB = (Sound)pLinkB;

            bool status = false;
            if (pDataA.name == pDataB.name)
            {
                status = true;
            }
            return status;
        }
        override protected void DerivedWashNode(DLink pLink)
        {
            Debug.Assert(pLink != null);
            Sound pNode = (Sound)pLink;
            pNode.Wash();
        }
        override protected void DerivedDumpNode(DLink pLink)
        {
            Debug.Assert(pLink != null);
            Sound pData = (Sound)pLink;
            pData.Dump();
        }
    }
}
