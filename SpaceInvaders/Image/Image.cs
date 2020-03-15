using System.Diagnostics;

namespace SpaceInvaders
{
    public class Image : DLink
    {
        public enum Name
        {
            Default,
            OctopusA,
            OctopusB,
            AlienA,
            AlienB,
            SquidA,
            SquidB,
            Saucer,

            Ship,
            Missile,

            AlienExplosion,
            SaucerExplosion,
            ShipExplosionA,
            ShipExplosionB,
            MissileExplosion,
            BombExplosion,

            BombDagger,
            BombZigZag,

            ShieldBrick,
            ShieldBrick_LeftTop0,
            ShieldBrick_LeftTop1,
            ShieldBrick_LeftBottom,
            ShieldBrick_RightTop0,
            ShieldBrick_RightTop1,
            ShieldBrick_RightBottom,

            NullObject,
            Uninitialized
        }

        // Data
        public Name name;
        private readonly Azul.Rect pRect;
        private Texture pTexture;

        public Image()
            : base()
        {

            this.pRect = new Azul.Rect();
            Debug.Assert(this.pRect != null);

            this.ClearNode();
        }

        //methods
        public void Set(Image.Name name, Texture pTexture, float x, float y, float width, float height)
        {
            this.name = name;
            Debug.Assert(pTexture != null);
            this.pTexture = pTexture;
            this.pRect.Set(x, y, width, height);
        }
        public void Wash()
        {
            this.ClearNode();
        }
        private void ClearNode()
        {
            this.name = Name.Uninitialized;
            this.pRect.Clear();
            this.pTexture = null;
        }

        public Azul.Rect GetAzulRect()
        {
            Debug.Assert(this.pRect != null);
            return this.pRect;
        }
        public Azul.Texture GetAzulTexture()
        {
            return this.pTexture.GetAzulTexture();
        }

        public void Dump()
        {
            Debug.WriteLine("      ID:     " + this.GetHashCode());
            Debug.WriteLine("      Name:   " + this.name);
            Debug.WriteLine("      Rect: [{0} {1} {2} {3}] ", this.pRect.x, this.pRect.y, this.pRect.width, this.pRect.height);

            if (this.pPrev == null)
            {
                Debug.WriteLine("      pPrev: null");
            }
            else
            {
                Image pTmp = (Image)this.pPrev;
                Debug.WriteLine("      pPrev: {0} ({1})", pTmp.name, pTmp.GetHashCode());
            }
            if (this.pNext == null)
            {
                Debug.WriteLine("      PNext: null");
            }
            else
            {
                Image pTmp = (Image)this.pNext;
                Debug.WriteLine("      pNext: {0} ({1})", pTmp.name, pTmp.GetHashCode());
            }
        }
    }
}

