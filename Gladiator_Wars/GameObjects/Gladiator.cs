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
        private readonly int BASE_INITIATIVE = 100;
        private readonly int BASE_HEALTH = 100;
        


        public Tile? nextNode;
        private float _velocity = 100;
        public Player player;

        // Gladiators stats
        public int healthPoints;
        public int experiencePoints;
        public int totalWeight;
        public int maxWeight;
        public int moveDistance = 1;
        public int ArmourPoints;

        // Stats that can be changed with experiencePoints
        public int strength; // Increases maxWeight capacity + melee damage
        public int toughness; // Increases HP
        public int Athletics; // Increases movement distance + dodge chance
        public int dexterity; // Increases melee weapon damage
        public int perception; // Increases chance to dodge/block + damage with throwing weapons

        // Gladiator equipment
        Weapon weapon;
        Armour armour;
        Shield shield;



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

        public int getDamageValue()
        {
            return (int)(weapon.damage * (1.0 + (dexterity + strength) / 200.0));
        }

        public void RemoveGladiator()
        {
            player.RemoveGladiator(this);
            boardPosition.unit = null;
        }

        public int getInitiative()
        {
            return BASE_INITIATIVE - totalWeight + Athletics;
        }
    }
}
