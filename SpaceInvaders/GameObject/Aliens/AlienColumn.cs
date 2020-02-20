using System.Diagnostics;

namespace SpaceInvaders
{
    //Specific type of composite
    public class AlienColumn : Composite
    {
        public AlienColumn(GameObject.Name name, GameSprite.Name spriteName, float posX, float posY)
        : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;

            this.poColObj.pColSprite.SetLineColor(1, 1, 1);
        }
        public override void Accept(CollisionVisitor other)
        {
            // Important: at this point we have an BirdColumn
            // Call the appropriate collision reaction            
            other.VisitColumn(this);
        }

        public override void Update()
        {
            base.BaseUpdateBoundingBox(this);

            base.Update();
        }
    }
}
