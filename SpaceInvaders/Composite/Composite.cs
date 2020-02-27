using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class Composite : GameObject
    {
        public DLink poHead;
        public DLink poLast;

        public Composite()
        {
            this.holder = Container.COMPOSITE;
            this.poHead = null;
            this.poLast = null;
            //Debug.Write(" creating--> ");
            //this.DumpNode();
        }

        public Composite(GameObject.Name gameName, GameSprite.Name spriteName)
        : base(gameName, spriteName)
        {
            this.holder = Container.COMPOSITE;
            this.poHead = null;
            this.poLast = null;
            Debug.Write(" creating--> ");
            //this.DumpNode();
        }

        override public void Add(Component pComponent)
        {
            Debug.Assert(pComponent != null);
            //DLink.PushFront(ref this.poHead, pComponent);
            DLink.PushBack(ref this.poHead, ref this.poLast, pComponent);
            pComponent.pParent = this;
        }

        override public void Remove(Component pComponent)
        {
            Debug.Assert(pComponent != null);
            DLink.RemoveNode(ref this.poHead, ref this.poLast, pComponent);
        }
        public override void Move()
        {
            DLink pNode = this.poHead;

            while (pNode != null)
            {
                Component pComponent = (Component)pNode;
                pComponent.Move();

                pNode = pNode.pNext;
            }
        }
        public override void Print()
        {
            DLink pNode = this.poHead;

            while (pNode != null)
            {
                Component pComponent = (Component)pNode;
                pComponent.Print();

                pNode = pNode.pNext;
            }

        }
        override public Component GetFirstChild()
        {
            DLink pNode = this.poHead;
            Debug.Assert(pNode != null);

            return (Component)pNode;
        }
        //override public void DumpNode()
        //{
        //    if (Iterator.GetParent(this) != null)
        //    {
        //        Debug.WriteLine(" GameObject Name:({0}) parent:{1} <---- Composite", this.GetHashCode(), Iterator.GetParent(this).GetHashCode());
        //    }
        //    else
        //    {
        //        Debug.WriteLine(" GameObject Name:({0}) parent:null <---- Composite", this.GetHashCode());
        //    }
        //}
    }
}
