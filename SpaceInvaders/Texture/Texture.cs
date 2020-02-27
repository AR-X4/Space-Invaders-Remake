using System.Diagnostics;

namespace SpaceInvaders
{
    public class Texture : DLink
    {
        public enum Name
        {
            Default,
            Aliens,
            NullObject,
            Uninitialized
        }

        // Data
        private Name name;
        private Azul.Texture pAzulTexture;
        static private readonly Azul.Texture pDefaultAzulTexture = new Azul.Texture("HotPink.tga");

        public Texture()
            : base()
        {
            this.ClearNode();
        }

        //methods
        public void Set(Name name, string pTextureName)
        {
            this.name = name;
            Debug.Assert(pTextureName != null);
            Debug.Assert(this.pAzulTexture != null);
            if (System.IO.File.Exists(pTextureName))
            {
                // Replace the Default with the new one
                this.pAzulTexture = new Azul.Texture(pTextureName, Azul.Texture_Filter.NEAREST, Azul.Texture_Filter.NEAREST);
            }
            Debug.Assert(this.pAzulTexture != null);
        }
        public void Wash()
        {
            this.ClearNode();
        }
        private void ClearNode()
        {
            Debug.Assert(Texture.pDefaultAzulTexture != null);
            this.pAzulTexture = pDefaultAzulTexture;
            Debug.Assert(this.pAzulTexture != null);
            this.name = Name.Default;
        }
        public Azul.Texture GetAzulTexture()
        {
            Debug.Assert(this.pAzulTexture != null);
            return this.pAzulTexture;
        }
        public void SetName(Texture.Name inName)
        {
            this.name = inName;
        }
        public Texture.Name GetName()
        {
            return this.name;
        }
        public void Dump()
        {
            Debug.WriteLine("    ID:     " + this.GetHashCode());
            Debug.WriteLine("    Name:   " + this.name);

            if (this.pAzulTexture != null)
            {
                Debug.WriteLine("    Texture: " + this.pAzulTexture.GetHashCode());
            }
            else
            {
                Debug.WriteLine("    Texture: null ");
            }

            if (this.pPrev == null)
            {
                Debug.WriteLine("    pPrev: null");
            }
            else
            {
                Texture pTmp = (Texture)this.pPrev;
                Debug.WriteLine("    pPrev: {0} ({1})", pTmp.name, pTmp.GetHashCode());
            }

            if (this.pNext == null)
            {
                Debug.WriteLine("    PNext: null");
            }
            else
            {
                Texture pTmp = (Texture)this.pNext;
                Debug.WriteLine("    pNext: {0} ({1})", pTmp.name, pTmp.GetHashCode());
            }
        }
    }
}
