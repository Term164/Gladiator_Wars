using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Gladiator_Wars
{
    public delegate void PassObject(object i);
    public delegate object PassObjectAndReturn(object i);

    internal class Button : GraphicsObject
    {
        public bool isPressed, isHovered, isToggleButton, isToggled;
        public string text;

        public Sprite icon;
        float scale;

        MouseState mouse;
        MouseState previousMouseState;

        public Color hoverColor;
        public Color defaultColor = Color.White;
        public Color pressedColor = Color.Gray;
        public Color toggledColor = Color.Lime;
        public SpriteFont font;

        public object info;

        PassObject ButtonClicked;


        public Button(Vector2 positon, Vector2 dimensions, Sprite sprite, SpriteFont font, string TEXT, PassObject BUTTONCLICKED, object INFO, bool toggleButton) : base(positon, dimensions, sprite) 
        { 
            text = TEXT;
            ButtonClicked = BUTTONCLICKED;
            this.font = font;
            isPressed = false;
            isToggled = false;
            hoverColor = new Color(200, 230, 255);
            isToggleButton = toggleButton;
            scale = 4;
        }

        public Button(Vector2 positon, Vector2 dimensions, Sprite buttonSprite, Sprite iconSprite, float scale,PassObject BUTTONCLICKED, object INFO, bool toggleButton) : base(positon, dimensions, buttonSprite)
        {
            icon = iconSprite;
            isToggleButton = toggleButton;
            isPressed = false;
            isToggled = false;
            this.scale = scale;
            hoverColor = new Color(200, 230, 255);
        }

        public override void Update()
        {
            mouse = Mouse.GetState();
            if (Hover())
            {
                if (!isToggleButton)
                {
                    isHovered = true;
                    color = hoverColor;
                }
                

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
            
            if (icon != null)
            {
                spriteBatch.Draw(sprite.texture, position, sprite.sourceRectangle, color, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0);
                spriteBatch.Draw(icon.texture, position + new Vector2(8,8),icon.sourceRectangle,color,0,Vector2.Zero,scale-0.5f,SpriteEffects.None,0);
            }
            else {
                base.Draw(spriteBatch);
                Vector2 stringDimensions = font.MeasureString(text);
                spriteBatch.DrawString(font, text, position + new Vector2(dimensions.X * Renderer.SCALE / 2 - stringDimensions.X / 2 + 6, dimensions.Y * Renderer.SCALE / 2 - stringDimensions.Y / 2 + 2), Color.Black);
            }
            
        }

        public virtual void Reset()
        {
            if (isToggleButton)
            {
                ResetToggle();
                return;
            }

            isPressed = false;
            isHovered = false;
            color = defaultColor;
        }

        private void ResetToggle()
        {
            
            isPressed = false;
            isHovered = false;
            if (isToggled) color = toggledColor;
            else color = defaultColor;

        }

        public virtual void RunButtonClick()
        {
            if(ButtonClicked != null)
            {
                ButtonClicked(info);
            }

            if (isToggleButton) {
                isToggled = !isToggled;
            } 

            Reset();
        }

        public virtual bool Hover()
        {
            MouseState mouse = Mouse.GetState();
            Vector2 mousePos = new Vector2(mouse.X, mouse.Y);

            if (mousePos.X >= position.X
                && mousePos.X <= position.X + dimensions.X * scale
                && mousePos.Y >= position.Y
                && mousePos.Y <= position.Y + dimensions.Y * scale)
            {
                return true;
            }

            return false;
        }

    }
}
