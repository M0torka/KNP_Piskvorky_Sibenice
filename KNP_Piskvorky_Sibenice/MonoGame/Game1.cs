using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace MonoGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D _cubeTexture;
        private Vector2 _position;
        private Vector2 _velocity;

        private float _acceleration = 600f;   // pixely/s²
        private float _maxSpeed = 1000f;       // max rychlost
        private float _friction = 600f;        // zpomalení

        private float _jitterBase = 0.5f;
        private float _jitterFactor = 5f;

        private Color _cubeColor = new Color(160, 0, 200);     // fialová
        private Color _backgroundColor = new Color(200, 255, 200); // světle zelená

        private int _cubeSize = 40;

        private Random _random = new Random();

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();

            _position = new Vector2(640, 360);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _cubeTexture = new Texture2D(GraphicsDevice, 1, 1);
            _cubeTexture.SetData(new[] { Color.White });
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            KeyboardState k = Keyboard.GetState();
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Vector2 input = Vector2.Zero;
            if (k.IsKeyDown(Keys.W)) input.Y -= 1;
            if (k.IsKeyDown(Keys.S)) input.Y += 1;
            if (k.IsKeyDown(Keys.A)) input.X -= 1;
            if (k.IsKeyDown(Keys.D)) input.X += 1;
            if (input != Vector2.Zero)
                input.Normalize();

            if (input != Vector2.Zero)
            {
                _velocity += input * _acceleration * dt;
                if (_velocity.Length() > _maxSpeed)
                {
                    _velocity.Normalize();
                    _velocity *= _maxSpeed;
                }
            }
            else
            {
                if (_velocity.Length() > 0)
                {
                    Vector2 dir = _velocity;
                    dir.Normalize();
                    _velocity -= dir * _friction * dt;
                    if (Vector2.Dot(_velocity, dir) < 0)
                        _velocity = Vector2.Zero;
                }
            }

            _position += _velocity * dt;

            // jitter efekt – čistě náhodný a pouze při pohybu
            Vector2 jitter = Vector2.Zero;
            float speed = _velocity.Length();
            if (speed > 0.1f)
            {
                float jitterAmount = _jitterBase + (speed / _maxSpeed) * _jitterFactor;
                float jx = (float)(_random.NextDouble() * 2 - 1) * jitterAmount;
                float jy = (float)(_random.NextDouble() * 2 - 1) * jitterAmount;
                jitter = new Vector2(jx, jy);
            }

            _position += jitter;

            // Omez pohyb do okna a zastav při nárazu
            int screenWidth = _graphics.PreferredBackBufferWidth;
            int screenHeight = _graphics.PreferredBackBufferHeight;

            if (_position.X < 0)
            {
                _position.X = 0;
                if (_velocity.X < 0) _velocity.X = 0;
            }
            else if (_position.X > screenWidth - _cubeSize)
            {
                _position.X = screenWidth - _cubeSize;
                if (_velocity.X > 0) _velocity.X = 0;
            }

            if (_position.Y < 0)
            {
                _position.Y = 0;
                if (_velocity.Y < 0) _velocity.Y = 0;
            }
            else if (_position.Y > screenHeight - _cubeSize)
            {
                _position.Y = screenHeight - _cubeSize;
                if (_velocity.Y > 0) _velocity.Y = 0;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(_backgroundColor);

            _spriteBatch.Begin();
            _spriteBatch.Draw(_cubeTexture, new Rectangle((int)_position.X, (int)_position.Y, _cubeSize, _cubeSize), _cubeColor);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
