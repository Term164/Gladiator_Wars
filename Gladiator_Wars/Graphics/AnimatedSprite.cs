
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Gladiator_Wars
{
    internal class AnimatedSprite : Sprite
    {
        Vector2 spriteSheetData;
        Vector2 frameSize;
        int numberOfFrames;


        float timer;
        int threshold = 100;

        Vector2 currentFrame;

        public AnimatedSprite(Texture2D texture, Vector2 spriteSheetData, Vector2 frameSize, int numberOfFrames) : base(texture, new Rectangle(0,0,(int)frameSize.X, (int)frameSize.Y))
        {
            this.spriteSheetData = spriteSheetData;
            this.frameSize = frameSize;
            this.numberOfFrames = numberOfFrames;
        }

        public override void Update(GameTime gametime)
        {
            if(timer > threshold) {

                if(currentFrame.Y * spriteSheetData.Y + currentFrame.X + 1 == numberOfFrames)
                {
                    currentFrame.X = 0;
                    currentFrame.Y = 0;
                }
                else if (currentFrame.X < spriteSheetData.X) currentFrame.X++;
                else
                {
                    currentFrame.X = 0;
                    currentFrame.Y++;
                }
                sourceRectangle = new Rectangle((int)(currentFrame.X * frameSize.X), (int)(currentFrame.Y * frameSize.Y),(int)frameSize.X, (int)frameSize.Y);
                timer = 0;
            }
            else
            {
                timer += (float)gametime.ElapsedGameTime.TotalMilliseconds;
            }
        }

    }
}
