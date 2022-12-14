using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
