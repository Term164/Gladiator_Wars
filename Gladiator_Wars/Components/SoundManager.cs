using Gladiator_Wars.Infastructure;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Gladiator_Wars
{
    internal class SoundManager : GameComponent
    {

        public static Song backgroundMusic;
        public static SoundEffect buttonClick;
        public static SoundEffect meeleAttackSound;

        public SoundManager(Game game) : base(game)
        {
            LoadContent();
        }

        public void LoadContent()
        {
            buttonClick = Game.Content.Load<SoundEffect>("Assets/button_click");
            meeleAttackSound = Game.Content.Load<SoundEffect>("Assets/Melee_Attack");
            backgroundMusic = Game.Content.Load<Song>("Assets/background_music");
        }

        public static void PlayButtonClickSound()
        {
            if (Settings.gameSettings.SoundOn) buttonClick.Play();
        }
        public static void PlayMelleAttackSound()
        {
            if (Settings.gameSettings.SoundOn) meeleAttackSound.Play();
        }

        public static void PlayBackgroundMusic()
        {
            MediaPlayer.IsMuted = !Settings.gameSettings.SoundOn;
            MediaPlayer.Play(backgroundMusic);
        }

        public static void soundToggled()
        {
            MediaPlayer.IsMuted = !MediaPlayer.IsMuted;
        }
    }
}
