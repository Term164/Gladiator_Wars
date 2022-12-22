using System.Collections.Generic;

namespace Gladiator_Wars
{
    enum Difficulty
    {
        easy,
        medium,
        hard,
        boss,
    }

    internal class GladiatorFactory
    {    

        public GladiatorFactory()
        {

        }

        public Gladiator generateNewGladiator(Difficulty difficulty)
        {
            return null;
        }

        public List<Gladiator> generateGladiatorList(List<Difficulty> difficultyList) {
            return null;
        }


    }
}
