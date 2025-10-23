using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Bonus_AI_game
{
    public class Bonus_AI_game : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D _pixel;
        private Rectangle _blueRect;
        private Rectangle _redRect;

        private KeyboardState _prevKeyboard;
        private bool _gameOver = false;
        private Color _backgroundColor = new Color(180, 255, 180); // vybledle zelená

        public Bonus_AI_game()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 600;
            _graphics.ApplyChanges();

            int size = 100;

            _blueRect = new Rectangle(200, 500, size, size);
            _redRect = new Rectangle(500, 500, size, size);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _pixel = new Texture2D(GraphicsDevice, 1, 1);
            _pixel.SetData(new[] { Color.White });
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (_gameOver)
                return;

            KeyboardState keyboard = Keyboard.GetState();

            // Blue player (S)
            if (keyboard.IsKeyDown(Keys.S) && !_prevKeyboard.IsKeyDown(Keys.S))
                _blueRect.Y -= 20;

            // Red player (Down Arrow)
            if (keyboard.IsKeyDown(Keys.Down) && !_prevKeyboard.IsKeyDown(Keys.Down))
                _redRect.Y -= 20;

            // Check win conditions
            if (_blueRect.Y <= 0)
            {
                _blueRect.Y = 0;
                _gameOver = true;
                _backgroundColor = Color.Blue;
            }

            if (_redRect.Y <= 0)
            {
                _redRect.Y = 0;
                _gameOver = true;
                _backgroundColor = Color.Red;
            }

            _prevKeyboard = keyboard;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(_backgroundColor);

            _spriteBatch.Begin();

            _spriteBatch.Draw(_pixel, _blueRect, Color.Blue);
            _spriteBatch.Draw(_pixel, _redRect, Color.Red);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
