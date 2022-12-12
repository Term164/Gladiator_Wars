using Gladiator_Wars.Components;
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

        public static Sprite GladiatorSprite;
        // TODO: Create sprites for every class and store it in that class as a static variable.

        public Tile? nextNode;
        public int moveDistance = 4;
        private float _velocity = 100;
        public Player player;

        public float velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        }
        
        public Gladiator(Vector2 position, Tile boardPosition, Player player) : base(position, boardPosition){
            this.player = player;
            player.units.Add(this);
        }

        public void MoveObject(GameTime gameTime)
        {
            if (nextNode != null) {
                Vector2 unitVectorInDirectionOfNextNode = (Vector2)nextNode.position - position;
                unitVectorInDirectionOfNextNode.Normalize();
                Vector2 moveVector = Vector2.Multiply(unitVectorInDirectionOfNextNode, _velocity * gameTime.ElapsedGameTime.Milliseconds/1000);
                position += moveVector;
                
                if (Vector2.Distance(position, (Vector2)nextNode.position) < 2) {
                    nextNode = null;
                }
            }
        }

        public void RemoveGladiator()
        {
            player.RemoveGladiator(this);
            boardPosition.unit = null;
        }
    }
}
