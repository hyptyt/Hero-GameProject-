using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RC_Framework;
using System;

namespace Final
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        RC_GameStateManager levelManager;

        SoundEffect intro;
        SoundEffect normalLevel;
        SoundEffect finalLevel;
        SoundEffect Town;
        SoundEffect Winning;

        SoundEffectInstance intro1;
        SoundEffectInstance normalLevel1;
        SoundEffectInstance finalLevel1;
        SoundEffectInstance Town1;
        SoundEffectInstance Winning1;

        LimitSound introBGM;
        LimitSound normalBGM;
        LimitSound finalBGM;
        LimitSound winningSound;
        
        
        int SCREEN_WIDTH = 500;
        int SCREEN_HEIGHT = 288;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = SCREEN_WIDTH;
            graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
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
            LineBatch.init(GraphicsDevice);
            intro = Content.Load<SoundEffect>("introduction");
            normalLevel = Content.Load<SoundEffect>("normallevel");
            finalLevel = Content.Load<SoundEffect>("final");
            Winning = Content.Load<SoundEffect>("winning");

            intro1 = intro.CreateInstance();
            normalLevel1 = normalLevel.CreateInstance();
            finalLevel1 = finalLevel.CreateInstance();
            Winning1 = Winning.CreateInstance();

            introBGM = new LimitSound(intro, 1);
            normalBGM = new LimitSound(normalLevel, 1);
            finalBGM = new LimitSound(finalLevel, 1);
            winningSound = new LimitSound(Winning, 1);

            levelManager = new RC_GameStateManager();

            levelManager.AddLevel(0, new GameLevel_0());
            levelManager.getLevel(0).InitializeLevel(GraphicsDevice, spriteBatch, Content, levelManager);
            levelManager.getLevel(0).LoadContent();
            levelManager.setLevel(0);

            levelManager.AddLevel(1, new GameLevel_1());
            levelManager.getLevel(1).InitializeLevel(GraphicsDevice, spriteBatch, Content, levelManager);
            levelManager.getLevel(1).LoadContent();

            levelManager.AddLevel(2, new GameLevel_2());
            levelManager.getLevel(2).InitializeLevel(GraphicsDevice, spriteBatch, Content, levelManager);
            levelManager.getLevel(2).LoadContent();

            levelManager.AddLevel(3, new GameLevel_3());
            levelManager.getLevel(3).InitializeLevel(GraphicsDevice, spriteBatch, Content, levelManager);
            levelManager.getLevel(3).LoadContent();

            levelManager.AddLevel(4, new GameLevel_4());
            levelManager.getLevel(4).InitializeLevel(GraphicsDevice, spriteBatch, Content, levelManager);
            levelManager.getLevel(4).LoadContent();

            levelManager.AddLevel(5, new GameLevel_5_Pause());
            levelManager.getLevel(5).InitializeLevel(GraphicsDevice, spriteBatch, Content, levelManager);
            levelManager.getLevel(5).LoadContent();

            levelManager.AddLevel(6, new GameLevel_6_Intro());
            levelManager.getLevel(6).InitializeLevel(GraphicsDevice, spriteBatch, Content, levelManager);
            levelManager.getLevel(6).LoadContent();

            levelManager.AddLevel(7, new GameLevel_7());
            levelManager.getLevel(7).InitializeLevel(GraphicsDevice, spriteBatch, Content, levelManager);
            levelManager.getLevel(7).LoadContent();

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

            KeyboardState keyState = Keyboard.GetState();

            // Moving 
            if (keyState.IsKeyDown(Keys.Escape)) this.Exit();

            levelManager.getCurrentLevel().Update(gameTime);

            switch(levelManager.getCurrentLevelNum())
            {
                case 0:
                    //introBGM.playSound();
                    intro1.Play();
                    normalLevel1.Stop();
                    finalLevel1.Stop();
                    Winning1.Stop();
                    break;
                case 4:
                    //finalBGM.playSound();
                    intro1.Stop();
                    normalLevel1.Stop();
                    finalLevel1.Play();
                    Winning1.Stop();
                    break;
                case 7:
                    //winningSound.playSoundIfOk();
                    intro1.Stop();
                    normalLevel1.Stop();
                    finalLevel1.Stop();
                    Winning1.Play();
                    break;
                default:
                    //normalBGM.playSoundIfOk();
                    intro1.Stop();
                    normalLevel1.Play();
                    finalLevel1.Stop();
                    Winning1.Stop();
                    break;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            levelManager.getCurrentLevel().Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}
