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

        private float _acceleration = 600f;   // zrychlení
        private float _maxSpeed = 1000f;      // max rychlost
        private float _friction = 600f;       // rychlost zpomalení

        private float _jitterBase = 0.5f;
        private float _jitterFactor = 5f;
        private float _jitterTime;

        private Color _cubeColor = new Color(160, 0, 200);         // barva kostky
        private Color _backgroundColor = new Color(200, 255, 200); // barva pozadí

        private int _cubeSize = 40;

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

            // jitter efekt (pouze při pohybu)
            Vector2 jitter = Vector2.Zero;
            float speed = _velocity.Length();
            if (speed > 0.1f)
            {
                float jitterAmount = _jitterBase + (speed / _maxSpeed) * _jitterFactor;
                _jitterTime += dt * (1f + speed * 0.01f);
                float jx = (float)(Math.Sin(_jitterTime * 6.1) + Math.Cos(_jitterTime * 4.7)) * jitterAmount;
                float jy = (float)(Math.Cos(_jitterTime * 5.3) - Math.Sin(_jitterTime * 3.8)) * jitterAmount;
                jitter = new Vector2(jx, jy);
            }

            _position += jitter;

            // Omez pohyb do okna
            int screenWidth = _graphics.PreferredBackBufferWidth;
            int screenHeight = _graphics.PreferredBackBufferHeight;

            _position.X = Math.Clamp(_position.X, 0, screenWidth - _cubeSize);
            _position.Y = Math.Clamp(_position.Y, 0, screenHeight - _cubeSize);

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
