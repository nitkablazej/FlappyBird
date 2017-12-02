using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FlappyBirdXNATutorial
{
    public class FlappyBird : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Scrolling scrolling1;
        Scrolling scrolling2;

        KeyboardState state;

        private bool startGame = false, gameOver = false;
        private SpriteFont fontLogger, fontGame, fontScore;
        private AnimatedSprite animatedSprite;
        private int descentForce = 1, elementPositionY = 200, keyPressTiming = 0;
        private double score = 0;

        public FlappyBird()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            scrolling1 = new Scrolling(Content.Load<Texture2D>("Backgrounds/background"), new Rectangle(0, 0, 1300, 800));
            scrolling2 = new Scrolling(Content.Load<Texture2D>("Backgrounds/background"), new Rectangle(1300, 0, 1300, 800));
            fontGame = Content.Load<SpriteFont>("Fonts/FontGame");
            fontLogger = Content.Load<SpriteFont>("Fonts/FontLogger");
            fontScore = Content.Load<SpriteFont>("Fonts/FontScore");
            Texture2D texture = Content.Load<Texture2D>("Images/SmileyWalk");
            animatedSprite = new AnimatedSprite(texture, 4, 4);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (scrolling1.rectangle.X + scrolling1.texture.Width <= 0)
            {
                scrolling1.rectangle.X = scrolling2.rectangle.X + scrolling2.texture.Width;
            }
            if (scrolling2.rectangle.X + scrolling2.texture.Width <= 0)
            {
                scrolling2.rectangle.X = scrolling1.rectangle.X + scrolling1.texture.Width;
            }

            if ((score != 0) && (score % 500 == 0))
            {
                scrolling1.speed++;
                scrolling2.speed++;
            }

            scrolling1.Update();
            scrolling2.Update();

            animatedSprite.Update();

            state = Keyboard.GetState();
            if (!startGame && state.IsKeyDown(Keys.Space))
            {
                startGame = true;
            }

            if (gameOver && state.IsKeyDown(Keys.Enter))
            {
                startGame = false;
                gameOver = false;
                descentForce = 1;
                elementPositionY = 200;
                keyPressTiming = 0;
                score = 0;
                scrolling1.speed = 3;
                scrolling2.speed = 3;
            }
            
            if (startGame && !gameOver)
            {
                if (keyPressTiming <= 0)
                {
                    if (state.IsKeyDown(Keys.Space))
                    {
                        descentForce = -15;
                        keyPressTiming = 10;
                    }
                }
                descentForce++;
                if (keyPressTiming > 0)
                {
                    keyPressTiming--;
                }
                elementPositionY += descentForce;
                score += 0.5;
            }

            if (elementPositionY < 5 || elementPositionY > 755)
            {
                gameOver = true;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            
            if (gameOver)
            {
                scrolling1.Draw(spriteBatch, true);
                scrolling2.Draw(spriteBatch, true);
            } else
            {
                scrolling1.Draw(spriteBatch, false);
                scrolling2.Draw(spriteBatch, false);
            }
            
            spriteBatch.DrawString(fontLogger, "KeyPressTiming: " + keyPressTiming, new Vector2(20, 50), Color.Black);
            spriteBatch.DrawString(fontLogger, "Y position: " + elementPositionY, new Vector2(20, 80), Color.Black);
            spriteBatch.DrawString(fontLogger, "DescentForce: " + descentForce, new Vector2(20, 110), Color.Black);
            spriteBatch.DrawString(fontScore, "Wynik: " + (int) score, new Vector2(50, 700), Color.Black);

            if (!startGame)
            {
                spriteBatch.DrawString(fontGame, "SPACJA - ZAGRAJ!", new Vector2(400, 300), Color.Black);
            }

            if (gameOver)
            {
                spriteBatch.DrawString(fontGame, "KONIEC GRY! WYNIK: " + (int) score, new Vector2(300, 300), Color.Black);
                spriteBatch.DrawString(fontGame, "ENTER - NOWA GRA", new Vector2(350, 380), Color.Black);
            }

            spriteBatch.End();
            animatedSprite.Draw(spriteBatch, new Vector2(550, elementPositionY));
            base.Draw(gameTime);
        }
    }
}
