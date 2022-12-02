using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Gladiator_Wars
{
    public enum Action { 
        Move,
        Attack,
        Block,

    }

    internal class Gladiator : GameObject, MovableObject
    {

        public Vector2? nextNode;
        public int moveDistance = 4;
        private float _velocity = 100;

        public float velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        }
        
        public Gladiator(Vector2 position, Vector2 boardPosition) : base(position, boardPosition){
            texturePath = "Assets/spritesheet";
            sourceRectangle = new Rectangle(1, 102, 32, 32);
        }

        public void MoveObject(GameTime gameTime)
        {
            if (nextNode != null) {
                Vector2 unitVectorInDirectionOfNextNode = (Vector2)nextNode - position;
                unitVectorInDirectionOfNextNode.Normalize();
                Vector2 moveVector = Vector2.Multiply(unitVectorInDirectionOfNextNode, _velocity * gameTime.ElapsedGameTime.Milliseconds/1000);
                position += moveVector;
                
                if (Vector2.Distance(position, (Vector2)nextNode) < 2) {
                    nextNode = null;
                }
            }
            
        }
    }
}
