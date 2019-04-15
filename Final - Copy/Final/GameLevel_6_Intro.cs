using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RC_Framework;

namespace Final
{
    class GameLevel_6_Intro : RC_GameStateParent
    {
        Random ran;
        
        
        Texture2D bg1 = null;
        ScrollBackGround bg;

        Texture2D[] monList = null;
        Texture2D bossImg = null;
        

        MonsterList Monster;

        Boss Bss;
        SpriteFont font;

        int timerTicks = 0;

        int numOfMons = 0;
        int transparent = 0;
        
        bool showBB = false;

        int Xmap = 0;
        public override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(graphicsDevice);
            LineBatch.init(graphicsDevice);
            
            // TODO: use this.Content to load your game content here

            font = Content.Load<SpriteFont>("Font");
            bossImg = Content.Load<Texture2D>("demon");

            bg1 = Content.Load<Texture2D>("map2");
            bg = new ScrollBackGround(bg1, new Rectangle(0, 0, 630, 288), new Rectangle(0, 0, 500, 288), -1, 2);
            //bgLayer1 = new ImageBackground(bg1, Color.White, graphicsDevice);

            Bss = new Boss(true, bossImg, -100, 79);

            Monster = new MonsterList();

            ran = new Random();

            monList = new Texture2D[4];

            monList[0] = Content.Load<Texture2D>("frog");
            monList[1] = Content.Load<Texture2D>("bat");
            monList[2] = Content.Load<Texture2D>("ghost");
            monList[3] = Content.Load<Texture2D>("skeleton");

            numOfMons = 15;
            for (int i = 0; i < numOfMons; i++)
            {
                float X = 0;
                float Y = 0;
                int pickMon = ran.Next(0, 4);

                X = -100 - i * 10;

                switch (pickMon)
                {
                    case 0:
                        Y = 239;
                        break;
                    case 1:
                        Y = 229;
                        break;
                    case 2:
                        Y = 229;
                        break;
                    case 3:
                        Y = 239;
                        break;
                    default:
                        break;
                }
                Monsters mon = new Monsters(true, monList[pickMon], X, Y);
                mon.setSpeed((float) ran.Next(0,8)/5);
                Monster.addSpriteReuse(mon);
            }

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        public override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            prevKeyState = keyState;
            keyState = Keyboard.GetState();
            // TODO: Add your update logic here

            if (timerTicks < 1800)
            {
                for(int i =0; i< Monster.count(); i++)
                {
                    Monsters m = Monster.getSprite(i);
                    m.UpdateIntro(gameTime);
                }
            }
            else
            {
                Bss.endIntro();
                for (int i = 0; i < Monster.count(); i++)
                {
                    Monsters m = Monster.getSprite(i);
                    m.endIntro();
                    m.UpdateIntro(gameTime);
                }
                //if (timerTicks % 18 == 0)
                    transparent++;
            }
            if (Bss.getSpeed() > 0)
                bg.setScrollSpeed(-1);
            else
                bg.setScrollSpeed(1);
            if(timerTicks == 2000)
            {
                gameStateManager.setLevel(1);
            }

            Bss.UpdateIntro(gameTime);
            timerTicks++;
            
            //HERO
            if (keyState.IsKeyDown(Keys.P) && prevKeyState.IsKeyUp(Keys.P))
            {
                gameStateManager.pushLevel(5);
            }
            

            if (keyState.IsKeyDown(Keys.B) && prevKeyState.IsKeyUp(Keys.B))
            {
                showBB = !showBB;
            }
            bg.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            graphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            bg.Draw(spriteBatch);
           

            Monster.Draw(spriteBatch);

            Bss.Draw(spriteBatch);
            
            if (showBB)
            {
                LineBatch.drawLineRectangle(spriteBatch, Bss.getbodyBoundingBox(), Color.Red);
                LineBatch.drawLineRectangle(spriteBatch, Bss.getAttackBoundingBox(), Color.Yellow);
               
                for (int i = 0; i < Monster.count(); i++)
                {
                    Monsters m = Monster.getSprite(i);
                    LineBatch.drawLineRectangle(spriteBatch, m.getBodyBB(), Color.Blue);
                }
                //LineBatch.drawLineRectangle(spriteBatch, foodBB, Color.Red);
            }

            string distance =" Boss Health" + Bss.getHealth() + "  Ticks" + timerTicks;
            spriteBatch.DrawString(font, distance, new Vector2(100, 30), Color.White);

            //Food.Draw(spriteBatch);
            spriteBatch.End();

        }

        // find the distance between 2 sprites
        public float getDistance(Sprite3 a, Sprite3 b)
        {
            return (Math.Abs(a.getPosX() - b.getPosX()));
        }
        public bool Intersect(Sprite3 a, Sprite3 b)
        {
            return true;
        }
        //public void createExplosion(float desX, float desY)
        //{
        //    Sprite3 Boom = new Sprite3(true, explosion, desX, desY);

        //    Boom.setXframes(8);
        //    Boom.setYframes(1);
        //    Boom.setWidthHeight(explosion.Width / 8, explosion.Height / 1);

        //    Vector2[] seq = new Vector2[8];
        //    seq[0].X = 0; seq[0].Y = 0;
        //    seq[1].X = 1; seq[1].Y = 0;
        //    seq[2].X = 2; seq[2].Y = 0;
        //    seq[3].X = 3; seq[3].Y = 0;
        //    seq[4].X = 4; seq[4].Y = 0;
        //    seq[5].X = 5; seq[5].Y = 0;
        //    seq[6].X = 6; seq[6].Y = 0;
        //    seq[7].X = 7; seq[7].Y = 0;
        //    Boom.setAnimationSequence(seq, 0, 7, 8);
        //    Boom.setAnimFinished(2); // make it inactive and invisible
        //    Boom.animationStart();

        //    Explosion.addSpriteReuse(Boom);
        //}
        

    }
}
