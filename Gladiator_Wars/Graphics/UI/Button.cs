using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Gladiator_Wars
{
    public delegate void PassObject(object i);
    public delegate object PassObjectAndReturn(object i);

    internal class Button : GraphicsObject
    {
        public bool isPressed, isHovered, isToggleButton;
        public string text;

        MouseState mouse;
        MouseState previousMouseState;

        public Color hoverColor;
        public Color defaultColor = Color.Blue;
        public Color pressedColor = Color.Gray;
        public SpriteFont font;

        public object info;

        PassObject ButtonClicked;


        public Button(Vector2 positon, Vector2 dimensions, Sprite sprite, SpriteFont font, string TEXT, PassObject BUTTONCLICKED, object INFO, bool toggleButton) : base(positon, dimensions, sprite) 
        { 
            text = TEXT;
            ButtonClicked = BUTTONCLICKED;
            this.font = font;
            isPressed = false;
            hoverColor = new Color(200, 230, 255);
        }

        public override void Update()
        {
            mouse = Mouse.GetState();
            if (Hover())
            {
                isHovered = true;
                color = hoverColor;

                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    isHovered = false;
                    isPressed = true;
                    color = pressedColor;
                }
                else if(mouse.LeftButton == ButtonState.Released && previousMouseState.LeftButton == ButtonState.Pressed)
                {
                    RunButtonClick();
                }
                
            }
            else
            {
                Reset();
            }

            previousMouseState = mouse;

            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            Vector2 stringDimensions = font.MeasureString(text);
            spriteBatch.DrawString(font, text, position + new Vector2(dimensions.X * Renderer.SCALE/2 - stringDimensions.X/2 + 6,dimensions.Y * Renderer.SCALE/2 - stringDimensions.Y/2 + 2), Color.Black);
        }

        public virtual void Reset()
        {
            isPressed = false;
            isHovered = false;
            color = defaultColor;
        }

        public virtual void RunButtonClick()
        {
            if(ButtonClicked != null)
            {
                ButtonClicked(info);
            }

            Reset();
        }

    }
}
