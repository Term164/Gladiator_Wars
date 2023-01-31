namespace Gladiator_Wars.Infastructure
{
    internal class Move
    {
        public Tile startPos;
        public Tile endPos;
        public Gladiator unit;
        public Action unitAction;

        public Move(Gladiator unit, Action unitAction, Tile endPos) {
            this.unit = unit;
            this.unitAction = unitAction;
            this.endPos = endPos;
            startPos = unit.boardPosition;
        }
    }
}
