using Gladiator_Wars.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Gladiator_Wars
{
    public enum Action { 
        Move,
        Attack,
        Block,

    }

    internal class Gladiator : GameObject, MovableObject, IComparable<Gladiator>
    {
        private readonly int BASE_INITIATIVE = 100;
        private readonly int BASE_HEALTH = 100;

        public bool movePoint = true; // Action point for moving
        public bool attackPoint = true; // Action point for attacking
        public bool alive = true;

        public Tile? nextNode;
        private float _velocity = 100;
        public Player player;

        public string name;

        // Gladiators stats
        public int healthPoints;
        public int experiencePoints = 0;
        public int totalWeight = 0;
        public int maxWeight = 100;
        public int moveDistance = 1;
        public int ArmourPoints = 1;

        // Stats that can be changed with experiencePoints
        public int strength = 1; // Increases maxWeight capacity + melee damage
        public int toughness = 1; // Increases HP
        public int Athletics = 1; // Increases movement distance + dodge chance
        public int dexterity = 1; // Increases melee weapon damage
        public int perception = 1; // Increases chance to dodge/block + damage with throwing weapons

        // Gladiator equipment
        public Weapon weapon;
        public Armour armour;
        public Shield shield;

        public float velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        }
        
        public Gladiator(Tile boardPosition, Player player) : base(boardPosition.position, boardPosition){
            
            tint = Color.RosyBrown; //TODO: Randomise gladiator skin color

            this.player = player;
            player.units.Add(this);
            weapon = new Hands();
            calculateHealthPoints();
        }

        public void attack(Gladiator gladiator)
        {
            gladiator.recieveDamage(weapon.damage);
        }

        public void recieveDamage(int damage){
            healthPoints -= damage - ArmourPoints;
            // TODO: dodge chance
            if (healthPoints < 0)
            {
                alive = false;
                RemoveGladiator();
            }
        }


        // ============================= STAT BASED METHODS =====================
        public int getDamageValue()
        {
            return (int)(weapon.damage * (1.0 + (dexterity + strength) / 200.0));
        }

        public void calculateHealthPoints()
        {
            healthPoints = (int)(BASE_HEALTH * 1.0+toughness/100);
        }

        public int getInitiative()
        {
            return BASE_INITIATIVE - totalWeight + Athletics;
        }

        // ========================== HELPER METHODS ==============================

        public int CompareTo(Gladiator other)
        {
            return other.getInitiative().CompareTo(this.getInitiative());
        }

        public bool hasActionPoints()
        {
            return movePoint || attackPoint;
        }

        public int getActionPoints()
        {
            return (attackPoint ? 1 : 0) + (movePoint ? 1 : 0);
        }
          public void RemoveGladiator()
        {
            player.RemoveGladiator(this);
            boardPosition.unit = null;
        }


        public void MoveObject(GameTime gameTime)
        {
            if (nextNode != null)
            {
                Vector2 unitVectorInDirectionOfNextNode = (Vector2)nextNode.position - position;
                unitVectorInDirectionOfNextNode.Normalize();
                Vector2 moveVector = Vector2.Multiply(unitVectorInDirectionOfNextNode, _velocity * gameTime.ElapsedGameTime.Milliseconds / 1000);
                position += moveVector;

                if (Vector2.Distance(position, (Vector2)nextNode.position) < 2)
                {
                    position = nextNode.position;
                    nextNode = null;
                }
            }
        }
    }
}
