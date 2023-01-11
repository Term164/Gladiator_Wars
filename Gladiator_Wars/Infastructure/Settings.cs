using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gladiator_Wars.Infastructure
{
    
    [Serializable]
    public class Settings
    {
        public static Settings gameSettings;
        public bool SoundOn { get; set; }

        public void toggleSound()
        {
            SoundOn = !SoundOn;
            SoundManager.soundToggled();
        }
    }
}
