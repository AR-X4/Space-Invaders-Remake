using System.Diagnostics;

namespace SpaceInvaders
{
    public class Sound : DLink
    {
        public enum Name
        {
            Default,
            Invader1,
            Invader2,
            Invader3,
            Invader4,

            Explosion,
            Shoot,
            DeadAlien,

            UFO,
            UFOExplosion,

            Uninitialized
        }

        // Data
        public Name name;
        private IrrKlang.ISoundSource pSource;

        public Sound()
            : base()
        {
            this.ClearNode();
        }

        //methods
        public void Set(Name name, string pSoundName, ref IrrKlang.ISoundEngine pSoundEngine)
        {
            this.name = name;
            Debug.Assert(pSoundName != null);
            if (System.IO.File.Exists(pSoundName))
            {
                this.pSource = pSoundEngine.AddSoundSourceFromFile(pSoundName);
            }
        }
        public void Wash()
        {
            this.ClearNode();
        }
        private void ClearNode()
        {
            this.name = Name.Default;
            if (this.pSource != null)
            {
                this.pSource.Dispose();
            }
            this.pSource = null;
        }

        public IrrKlang.ISoundSource GetSoundSource() {
            Debug.Assert(this.pSource != null);
            
            return this.pSource;
        }
        public void PlaySound() {
            Debug.Assert(this.pSource != null);
            SoundManager.PlaySound(this);
        }

        public void Dump()
        {
            //Debug.WriteLine("    ID:     " + this.GetHashCode());
            //Debug.WriteLine("    Name:   " + this.name);

            //if (this. != null)
            //{
            //    Debug.WriteLine("    Texture: " + this.pAzulTexture.GetHashCode());
            //}
            //else
            //{
            //    Debug.WriteLine("    Texture: null ");
            //}

            //if (this.pPrev == null)
            //{
            //    Debug.WriteLine("    pPrev: null");
            //}
            //else
            //{
            //    Texture pTmp = (Texture)this.pPrev;
            //    Debug.WriteLine("    pPrev: {0} ({1})", pTmp.name, pTmp.GetHashCode());
            //}

            //if (this.pNext == null)
            //{
            //    Debug.WriteLine("    PNext: null");
            //}
            //else
            //{
            //    Texture pTmp = (Texture)this.pNext;
            //    Debug.WriteLine("    pNext: {0} ({1})", pTmp.name, pTmp.GetHashCode());
            //}
        }
    }
}
