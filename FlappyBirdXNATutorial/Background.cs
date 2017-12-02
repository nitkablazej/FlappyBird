using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlappyBirdXNATutorial
{
    class Background
    {
        public Texture2D texture;
        public Rectangle rectangle;
        public int speed = 3;
        public void Draw(SpriteBatch spriteBatch, bool gameOver)
        {
            if (gameOver)
            {
                spriteBatch.Draw(texture, rectangle, Color.Red);
            } else
            {
                spriteBatch.Draw(texture, rectangle, Color.White);
            }
        }
    }

    class Scrolling : Background
    {
        public Scrolling(Texture2D newTexture, Rectangle newRectangle)
        {
            texture = newTexture;
            rectangle = newRectangle;
        }
        public void Update()
        {
            rectangle.X -= speed;
        }
    }
}
