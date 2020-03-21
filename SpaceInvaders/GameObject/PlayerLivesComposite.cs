using System;


namespace SpaceInvaders
{
    public class PlayerLivesComposite : Composite
    {
        //data
        

        public PlayerLivesComposite()
            : base(Name.Null_Object, GameSprite.Name.NullObject)
        {}

        public override void Accept(CollisionVisitor other)
        {
           
        }

        public void RemoveLife(int count) {
            ForwardIterator pFor = new ForwardIterator(this);
            Component pNode = pFor.First();
            GameObject pGameObj;

            for (int i = 0; i < count; i++)
            {
                pGameObj = (GameObject)pNode;

                pGameObj.pProxySprite.Set(GameSprite.Name.NullObject);
                pNode = pFor.Next();

            }
        }

        public void ResetLives()
        {
            ForwardIterator pFor = new ForwardIterator(this);
            Component pNode = pFor.First();
            GameObject pGameObj;

            while (!pFor.IsDone())
            {

                pGameObj = (GameObject)pNode;

                pGameObj.pProxySprite.Set(GameSprite.Name.Ship);

                pNode = pFor.Next();
            }
        }
    }
}
