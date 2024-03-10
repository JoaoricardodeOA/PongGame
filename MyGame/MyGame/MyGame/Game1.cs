using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace MyGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _ballTexture;
        private Vector2 ballPosition = new Vector2(50,50);
        private int positionY = 0;
        private int positionX = 5;
        private bool isDown = false;
        private bool isReturning = false;
        private Texture2D _rectanglePixel;
        private Rectangle Rectangle1;
        private Rectangle Rectangle2;
        private int p1 = 0;
        private int p2 = 0;
        private SpriteFont font;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 420;
            _graphics.ApplyChanges();

            _rectanglePixel = new Texture2D(GraphicsDevice, 1, 1);
            _rectanglePixel.SetData<Color>(new Color[] { Color.White });
            Rectangle1 = new Rectangle(0, 0, 25, 100);
            Rectangle2 = new Rectangle(_graphics.PreferredBackBufferWidth - 25, 0, 25, 100);


            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            _ballTexture = Content.Load<Texture2D>("ball");
            font = Content.Load<SpriteFont>("Font");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Random rnd = new Random();
            positionY = rnd.Next(0, 5);

            // TODO: Add your update logic here

            if (Keyboard.GetState().IsKeyDown(Keys.S) && _graphics.PreferredBackBufferHeight - Rectangle2.Height > Rectangle1.Y)
            {
                Rectangle1.Y += 5;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.W) && Rectangle1.Y > 5)
            {
                Rectangle1.Y -= 5;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.W) && Rectangle1.Y > 0)
            {
                Rectangle1.Y = 0;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Down) && _graphics.PreferredBackBufferHeight - Rectangle1.Height > Rectangle2.Y)
            {
                Rectangle2.Y += 5; 
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Up) && Rectangle2.Y > 5)
            {
                Rectangle2.Y -= 5;
            }else if (Keyboard.GetState().IsKeyDown(Keys.Up) && Rectangle2.Y > 0)
            {
                Rectangle2.Y = 0;
            }


            if (ballPosition.X >= _graphics.PreferredBackBufferWidth - _ballTexture.Width ||(( ballPosition.Y + _ballTexture.Height ) >= Rectangle2.Y  && ballPosition.Y <= (Rectangle2.Y + Rectangle2.Height) && ballPosition.X + _ballTexture.Width >= Rectangle2.X))
            {
                isReturning = true;
                if (ballPosition.X >= _graphics.PreferredBackBufferWidth - _ballTexture.Width)
                {
                    p1++;
                }
            }
            if (ballPosition.X <= 0 || ((ballPosition.Y + _ballTexture.Height) >= Rectangle1.Y && ballPosition.Y <= (Rectangle1.Y + Rectangle1.Height) && ballPosition.X <= Rectangle1.X + Rectangle1.Width))
            {
                isReturning = false;
                if (ballPosition.X <= 0)
                {
                    p2++;
                }
            }   

            if (ballPosition.Y >= _graphics.PreferredBackBufferHeight - _ballTexture.Height)
            {
                isDown = true;
            }
            if (ballPosition.Y <= 0)
            {
                isDown = false;
            }


            if (isReturning == true)
            {
                ballPosition.X -= positionX;
            }
            else
            {
                ballPosition.X += positionX;
            }

            if (isDown == true)
            {
                ballPosition.Y -= positionY;
            }
            else
            {
                ballPosition.Y += positionY;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(_ballTexture, ballPosition, Color.White);
            _spriteBatch.Draw(_rectanglePixel,Rectangle1, Color.White);
            _spriteBatch.Draw(_rectanglePixel, Rectangle2, Color.White);
            _spriteBatch.DrawString(font, "Score P1: "+ p1, new Vector2(100, 25), Color.White);
            _spriteBatch.DrawString(font, "Score P2: " + p2, new Vector2(_graphics.PreferredBackBufferWidth - 300, 25), Color.White);
            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}