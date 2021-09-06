using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace game
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D balltexture;
        Texture2D personTexture;
        Texture2D bullet; //กระสุน
        Vector2 bulletPosition = new Vector2(0, 0);
        Vector2 ballPosition = new Vector2(750,0); // ลูกบอล
        Vector2 personPosition = new Vector2(0, 400);
        int ballMoveSpeed = 5;
        int personMoveSpeed = 2;
        int bulletSpeed = 5; //กระสุน
        bool personHit = false;
        // animation
        int direction = 0;
        int frame;
        int totalframe;
        int framePessec;
        float timeperframe;
        float totalelapsed;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            Content.RootDirectory = "Content";
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
            balltexture = Content.Load<Texture2D>("ball");
            personTexture = Content.Load<Texture2D>("char01");
            bullet = Content.Load<Texture2D>("gun");
            // animation 
            frame = 0;
            totalframe = 4;
            framePessec = 8;
            timeperframe = (float)1 / framePessec;
            totalelapsed = 0;

            // TODO: use this.Content to load your game content here
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            // control player
            KeyboardState keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.Left))
            {
                personPosition.X = personPosition.X - personMoveSpeed;// move
                direction = 1; // animation
                UpdateFrame((float)gameTime.ElapsedGameTime.TotalSeconds);// animation

            }
            if (keyboard.IsKeyDown(Keys.Right))
            {
                personPosition.X = personPosition.X + personMoveSpeed;// move
                direction = 2; // animation
                UpdateFrame((float)gameTime.ElapsedGameTime.TotalSeconds);// animation
            }
            if (keyboard.IsKeyDown(Keys.Up))
            {
                personPosition.Y = personPosition.Y - personMoveSpeed; // move
                direction = 3; // animation
                UpdateFrame((float)gameTime.ElapsedGameTime.TotalSeconds);// animation
            }
            if (keyboard.IsKeyDown(Keys.Down))
            {
                personPosition.Y = personPosition.Y + personMoveSpeed;// move
                direction = 0; // animation
                UpdateFrame((float)gameTime.ElapsedGameTime.TotalSeconds);// animation
            }
            if (keyboard.IsKeyDown(Keys.Space))
            {
                
            }

            //ball
            Random r = new Random();
            ballPosition.X = ballPosition.X - ballMoveSpeed;
            if(ballPosition.X == 0)
            {
                ballPosition.Y = r.Next(0, 400);
                ballPosition.X = 800;
            }
            //create rectangle
            Rectangle personRectangle = new Rectangle((int)personPosition.X ,(int)personPosition.Y, 32, 48);
            Rectangle ballRectangle = new Rectangle((int)ballPosition.X, (int)ballPosition.Y, 24, 24);
            if (personRectangle.Intersects(ballRectangle)==true)
            {
                personHit = true;
            }
            else if (personRectangle.Intersects(ballRectangle) == false)
            {
                personHit = false;
            }

                base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            

            // TODO: Add your drawing code here
            if(personHit==true)
            {
                GraphicsDevice.Clear(Color.Red);
            }
            else if (personHit==false)
            {
                GraphicsDevice.Clear(Color.CornflowerBlue);
            }
            spriteBatch.Begin();
            spriteBatch.Draw(balltexture, ballPosition, new Rectangle(24, 0, 24, 24), Color.White);
            spriteBatch.Draw(personTexture, personPosition, new Rectangle(32*frame, 48*direction, 32, 48), Color.White);
            
            spriteBatch.End();

            base.Draw(gameTime);
        }

        void UpdateFrame(float elepsed)
        {
            totalelapsed += elepsed;
            if(totalelapsed > timeperframe)
            {
                frame = (frame + 1) % totalframe;
                totalelapsed -= timeperframe;   
            }
        }
    }
}

