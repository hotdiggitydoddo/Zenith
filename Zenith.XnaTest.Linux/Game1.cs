using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Zenith.XnaTest
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private InputState _input;
        private GameWorld _gameWorld;
        private uint entity;

        public const long UPDATE_FREQUENCY = (long)(0.5d + 10000000.0d / 100); // Virtual frame rate is 100 updates per second
        public const long MAX_FRAME_TIME = (long)(0.5d + 10000000.0d / 15); // Game can slow down to max 15 frames per second

        private long _delta = 0;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsFixedTimeStep = false;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            _gameWorld = new GameWorld(1000, spriteBatch);
             entity = _gameWorld.EntityManager.CreateEntity();


            _gameWorld.HealthComponents[entity].MaxHealth = 100;
            _gameWorld.HealthComponents[entity].CurrentHealth = 100;

            _gameWorld.SpatialComponents[entity].Position = new Vector2(10);
            _gameWorld.SpriteComponents[entity].Texture = Content.Load<Texture2D>("blue-monster");

            _gameWorld.EntityManager.AddComponent(entity, XnaGameComponentType.Health);
            _gameWorld.EntityManager.AddComponent(entity, XnaGameComponentType.Regeneration);
            _gameWorld.EntityManager.AddComponent(entity, XnaGameComponentType.Sprite);
            _gameWorld.EntityManager.AddComponent(entity, XnaGameComponentType.Spatial);
            _gameWorld.EntityManager.AddComponent(entity, XnaGameComponentType.Physics);

            _input = new InputState();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            VariableStep(gameTime.ElapsedGameTime.Ticks);
            base.Update(gameTime);
        }

        private void VariableStep(long ticks)
        {
            if (ticks > MAX_FRAME_TIME)
                ticks = MAX_FRAME_TIME;

            long previous = _delta;
            _delta += ticks;

            while (_delta >= UPDATE_FREQUENCY)
            {
                Update(new TimeSpan(UPDATE_FREQUENCY - previous), false);
                _delta -= UPDATE_FREQUENCY;
                previous = 0;
            }

            long partial = _delta - previous;
            if (partial > 0)
                Update(new TimeSpan(partial), true);
        }

        private void Update(TimeSpan elapsedTime, bool isPartial)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            _input.Update();
            PlayerIndex p;

            if (_input.IsNewKeyPress(Keys.D, PlayerIndex.One, out p))
            {
                _gameWorld.HealthComponents[entity].Damage = 27;
            }
            if (_input.IsNewKeyPress(Keys.R, PlayerIndex.One, out p))
            {
                _gameWorld.RegenerationComponents[entity].AmountToHeal = 5;
                _gameWorld.RegenerationComponents[entity].Frequency = 2000;
                _gameWorld.EntityManager.AddComponent(entity, XnaGameComponentType.Regeneration);
            }
            if (_input.IsNewKeyPress(Keys.C, PlayerIndex.One, out p))
            {
                _gameWorld.EntityManager.RemoveComponent(entity, XnaGameComponentType.Regeneration);
            }

            if (_input.IsKeyPressed(Keys.Down, PlayerIndex.One, out p))
            {
                _gameWorld.PhysicsComponents[entity].VelY = .5f * (float)elapsedTime.TotalMilliseconds;
            }

            else if (_input.IsKeyPressed(Keys.Up, PlayerIndex.One, out p))
            {
                _gameWorld.PhysicsComponents[entity].VelY = -.5f * (float)elapsedTime.TotalMilliseconds;
            }

            else
                _gameWorld.PhysicsComponents[entity].VelY = 0;

            if (_input.IsKeyPressed(Keys.Right, PlayerIndex.One, out p))
            {
                _gameWorld.PhysicsComponents[entity].VelX = .5f * (float)elapsedTime.TotalMilliseconds;
            }
            else if (_input.IsKeyPressed(Keys.Left, PlayerIndex.One, out p))
            {
                _gameWorld.PhysicsComponents[entity].VelX = -.5f * (float)elapsedTime.TotalMilliseconds;
            }
            else
                _gameWorld.PhysicsComponents[entity].VelX = 0;

            _gameWorld.Update((float)elapsedTime.TotalMilliseconds);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            _gameWorld.Render();
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
