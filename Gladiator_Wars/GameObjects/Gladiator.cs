using Gladiator_Wars.Components;
using Microsoft.Xna.Framework;
using System;

namespace Gladiator_Wars
{
    public enum Action { 
        Move,
        Attack,
        Block,
        Heal
    }

    public enum Type
    {
        Tank,
        Warrior,
        Bowman,
        Spearman
    }

    internal class Gladiator : GameObject, MovableObject, IComparable<Gladiator>
    {
        private readonly int BASE_INITIATIVE = 100;
        private readonly int BASE_HEALTH = 100;

        public bool movePoint = true; // Action point for moving
        public bool attackPoint = true; // Action point for attacking
        public bool alive = true;
        public bool isDefending = false;
        public bool hasHeald = false;

        public Tile? nextNode;
        private float _velocity = 100;
        public Player player;

        public bool isBoss = false;
        public string name;
        public Type type;

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
        public int athletics = 1; // Increases movement distance + dodge chance
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
        }

        public void attack(Gladiator gladiator)
        {
            gladiator.recieveDamage(calculateGivenDamage());
        }

        public int calculateGivenDamage()
        {
            return (int)(weapon.damage * (1.0f + (dexterity + strength) / 100.0f));
        }

        public void recieveDamage(int damage){
            healthPoints -= calculateRecievedDamage(damage);
            // TODO: dodge chance
            if (healthPoints < 0)
            {
                alive = false;
                RemoveGladiator();
            }
        }

        public int calculateRecievedDamage(int damage)
        {
            int damageToRecieve = damage - (int)((ArmourPoints + (shield != null ? shield.armourPoints : 0)) * (isDefending ? 1.2f : 1f));
            return (damageToRecieve < 0 ? 0 : damageToRecieve); 
        }

        public void Defend()
        {
            isDefending = true;
        }

        public void Heal(Gladiator gladiator)
        {
            hasHeald = true;
            gladiator.recieveHealing(10);
        }

        public void recieveHealing(int health)
        {
            if (healthPoints + health > BASE_HEALTH) healthPoints = BASE_HEALTH;
            else healthPoints += health;
        }

        // ============================= STAT BASED METHODS =====================
        public int getDamageValue()
        {
            return calculateGivenDamage(); 
        }

        public void calculateHealthPoints()
        {
            healthPoints = (int)(BASE_HEALTH * 1.0+toughness/100);
        }

        public void calculateTotalWeight()
        {
            totalWeight = weapon.weight + armour.weight;
            if(shield != null) totalWeight += shield.weight;
        }
        
        public void calcualteTotalArmourPoints()
        {
            ArmourPoints = armour.armourPoints;
            if(shield != null) ArmourPoints += shield.armourPoints;
        }

        public void CalculateMoveDistance()
        {
            moveDistance += athletics / 10 - totalWeight / 50;
            if(moveDistance < 1) moveDistance = 1;
        }

        public int getInitiative()
        {
            return BASE_INITIATIVE - totalWeight + athletics;
        }

        public void assignUnitType()
        {
            if (weapon.type == WeaponType.Ranged) type = Type.Bowman;
            else if (totalWeight > 70 && armour is HeavyArmour) type = Type.Tank;
            else type = Type.Warrior;
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
                Vector2 unitVectorInDirectionOfNextNode = nextNode.position - position;
                unitVectorInDirectionOfNextNode.Normalize();
                Vector2 moveVector = Vector2.Multiply(unitVectorInDirectionOfNextNode, _velocity * gameTime.ElapsedGameTime.Milliseconds / 1000);
                position += moveVector;

                if (Vector2.Distance(position, nextNode.position) < 2)
                {
                    position = nextNode.position;
                    nextNode = null;            
                }
            }
        }
    }
}
