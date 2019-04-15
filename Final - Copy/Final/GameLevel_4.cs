using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RC_Framework;

namespace Final
{
    class GameLevel_4 : RC_GameStateParent
    {
        Random ran;

        ImageBackground bgLayer1 = null;

        Texture2D spriteSheet = null;
        Texture2D bg1 = null;

        Texture2D[] monList = null;
        Texture2D bossImg = null;
        Texture2D food = null;
        Texture2D explosion = null;
        Texture2D finalEffect = null;
        Texture2D fireBall = null;
        Texture2D apple = null;
        Texture2D back = null;

        Character hero;


        MonsterList Monster;
        SpriteList Explosion;
        SpriteList FireBall;
        SpriteList FoodList;

        Boss Bss;
        SpriteFont font;

        Rectangle heroBody;
        Rectangle heroAttack;
        Rectangle BssBody;
        Rectangle BssAttack;
        Rectangle foodBB;

        int SCREEN_WIDTH = 500;
        int SCREEN_HEIGHT = 288;
        int timerTicks = 0;
        int numOfMons = 0;
        int numOfFoods = 0;
        int total_distance = 1200;

        bool showBB = false;
        bool farLeft = false;
        bool farRight = false;

        int Xmap = 0;

        SoundEffect heroHurt;
        SoundEffect heroslash;
        SoundEffect monsterBite;
        SoundEffect monsterDie;

        public override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(graphicsDevice);
            LineBatch.init(graphicsDevice);

            ran = new Random();
            // TODO: use this.Content to load your game content here
            spriteSheet = Content.Load<Texture2D>("hero");

            // TEXTURE
            font = Content.Load<SpriteFont>("magicschool");
            food = Content.Load<Texture2D>("FOOD");
            bossImg = Content.Load<Texture2D>("demon");
            explosion = Content.Load<Texture2D>("enemy-deadth");
            finalEffect = Content.Load<Texture2D>("finalEffect");
            back = Content.Load<Texture2D>("moon");

            fireBall = Content.Load<Texture2D>("fireball");
            apple = Content.Load<Texture2D>("apple");

            bg1 = Content.Load<Texture2D>("map4");

            //bgLayer1 = new ImageBackground(bg1, Color.White, graphicsDevice);
            // SOUND

            heroHurt = Content.Load<SoundEffect>("herohurt");
            heroslash = Content.Load<SoundEffect>("slash");
            monsterBite = Content.Load<SoundEffect>("monster");
            monsterDie = Content.Load<SoundEffect>("pop");

            hero = new Character(true, spriteSheet, -50, 174);
            hero.setScreenWidthHeight(SCREEN_WIDTH, SCREEN_HEIGHT);
            hero.setFarRight(1100);
            hero.setHurtSound(heroHurt);
            hero.setSlashSound(heroslash);
            hero.setSound();

            Bss = new Boss(true, bossImg, 1300, 79);
            Bss.setFireBall(fireBall);
            Bss.setFinalExplosion(finalEffect);

            Monster = new MonsterList();
            Explosion = new SpriteList();
            FireBall = new SpriteList();
            FoodList = new SpriteList();

            ran = new Random();


            monList = new Texture2D[4];

            monList[0] = Content.Load<Texture2D>("frog");
            monList[1] = Content.Load<Texture2D>("bat");
            monList[2] = Content.Load<Texture2D>("ghost");
            monList[3] = Content.Load<Texture2D>("skeleton");

            numOfMons = 10;
            for (int i = 0; i < numOfMons; i++)
            {
                float X = 0;
                float Y = 0;
                int pickMon = ran.Next(0, 4);

                X = 200 + i * 100;

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
                mon.setExplosion(explosion);
                mon.setPopSound(monsterDie);
                mon.setBiteSound(monsterBite);
                mon.setSound();
                Monster.addSpriteReuse(mon);
            }
            numOfFoods = (int)total_distance / 300 + 2;

            for (int i = 0; i < numOfFoods; i++)
            {
                float X = 0;
                float Y = 0;

                X = 300 * i;
                Y = 220;
                if (i == 0)
                    X = -190;
                if (i == numOfFoods - 1)
                    X = (numOfFoods - 2) * 300 + 100;

                Sprite3 fd = new Sprite3(true, apple, X, Y);
                FoodList.addSpriteReuse(fd);
            }

            //createFood();
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

            if (timerTicks < 60)
            {
                hero.UpdateLevel(gameTime);
            }
            else
            {
                hero.Update(gameTime);
            }

            if (keyState.IsKeyDown(Keys.D0) && !prevKeyState.IsKeyDown(Keys.D0))
            {
                gameStateManager.pushLevel(0);
            }
            if (keyState.IsKeyDown(Keys.D1) && !prevKeyState.IsKeyDown(Keys.D1))
            {
                gameStateManager.pushLevel(1);
            }
            else if (keyState.IsKeyDown(Keys.D2) && !prevKeyState.IsKeyDown(Keys.D2))
            {
                gameStateManager.pushLevel(2);
            }
            else if (keyState.IsKeyDown(Keys.D3) && !prevKeyState.IsKeyDown(Keys.D3))
            {
                gameStateManager.pushLevel(3);
            }
            else if (keyState.IsKeyDown(Keys.D4) && !prevKeyState.IsKeyDown(Keys.D4))
            {
                gameStateManager.pushLevel(4);
            }
            else if (keyState.IsKeyDown(Keys.D6) && !prevKeyState.IsKeyDown(Keys.D6))
            {
                gameStateManager.pushLevel(6);
            }
            else if (keyState.IsKeyDown(Keys.D7) && !prevKeyState.IsKeyDown(Keys.D7))
            {
                gameStateManager.pushLevel(7);
            }


            timerTicks++;

            FireBall = Bss.returnFireBallList();

            heroBody = hero.getBodyBB();
            heroAttack = hero.getAttackBB();

            BssBody = Bss.getbodyBoundingBox();
            BssAttack = Bss.getAttackBoundingBox();

            //HERO
            if (keyState.IsKeyDown(Keys.P) && prevKeyState.IsKeyUp(Keys.P))
            {
                gameStateManager.pushLevel(5);
            }


            // Moving background
            if (keyState.IsKeyDown(Keys.Left) || keyState.IsKeyDown(Keys.Right))
                Xmap += hero.getUniSpeed();

            if (Xmap < 0)
                Xmap = 0;
            if (Xmap > 1100)
                Xmap = 1100;

            if (keyState.IsKeyDown(Keys.B) && prevKeyState.IsKeyUp(Keys.B))
            {
                showBB = !showBB;
            }

            //MONSTER
            for (int i = 0; i < Monster.count(); i++)
            {
                Monsters m = Monster.getSprite(i);
                Rectangle mBody;
                if (m.visible)
                {
                    if (keyState.IsKeyDown(Keys.Left) || keyState.IsKeyDown(Keys.Right))
                    {
                        if (!farLeft && !farRight)
                            m.setPosX(m.getPosX() - hero.getUniSpeed());
                    }
                    m.setTarget(hero.getCenterX());
                    mBody = m.getBodyBB();

                    if (m.getCenterX() == hero.getCenterX())
                        hero.setBeingAttacked(true);

                    if (hero.checkAttacking(mBody))
                    {
                        m.Hurt();
                        m.PushBack(m.getPosX());
                    }

                }
                else
                {
                    mBody = new Rectangle(0, 0, 0, 0);
                    m.setActiveAndVisible(false);
                    Monster.deleteSprite(i);
                }
            }
            Monster.Update(gameTime);

            //FOOD LIST
            for (int i = 0; i < FoodList.count(); i++)
            {
                Sprite3 f = FoodList.getSprite(i);
                if (f.visible)
                {
                    if (timerTicks % 20 == 0)
                    {
                        if (f.getPosY() >= 219)
                            f.setPosY(FoodList.getSprite(i).getPosY() - 2);
                        else
                            f.setPosY(FoodList.getSprite(i).getPosY() + 2);
                    }
                    foodBB = f.getBoundingBoxAA();

                    if (heroBody.Intersects(foodBB))
                    {
                        hero.IncreaseHealth();
                        FoodList.getSprite(i).setActiveAndVisible(false);
                    }
                }
                else
                {
                    f.setActiveAndVisible(false);
                }
            }

            //FIRE BALL
            for (int i = 0; i < FireBall.count(); i++)
            {
                Sprite3 fb = FireBall.getSprite(i);

                if (fb.visible)
                {
                    Rectangle fireBallBB = fb.getBoundingBoxAA();
                    Point hotSpot = new Point((int)fb.getPosX(), (int)(fb.getPosY()));
                    if (heroBody.Contains(hotSpot))
                    {
                        hero.setDamage(2);
                        hero.setBeingAttacked(true);
                        fb.setActiveAndVisible(false);
                    }
                    if (hero.checkAttacking(fireBallBB))
                    {
                        Vector2 bounce = new Vector2(ran.Next(0, 10), ran.Next(-10, 10));
                        fb.setDeltaSpeed(bounce);
                    }
                    if (fireBallBB.X < 0 || fireBallBB.Y > 256 || fireBallBB.X > 512 || fireBallBB.Y < -13)
                    {
                        fb.setActiveAndVisible(false);
                    }
                }
            }

            // BOSS
            if (Bss.visible)
            {
                if (keyState.IsKeyDown(Keys.Left) || keyState.IsKeyDown(Keys.Right))
                {
                    if (!farLeft && !farRight)
                        Bss.setPosX(Bss.getPosX() - hero.getUniSpeed());
                }
                if (hero.checkAttacking(BssBody))
                {
                    Bss.Hurt();
                }
                if (Bss.isAttacking2())
                {
                    if (BssAttack.Contains(hero.getCenter()))
                    {
                        hero.setDamage(5);
                        hero.setBeingAttacked(true);
                    }

                }
            }
            else
            {
                hero.setCanGo(true);
            }
            Bss.Update(gameTime, hero);

            //EFFECTS

            Explosion.animationTick(gameTime);
            FireBall.moveDeltaXY();

            if (!Bss.checkAlive() && hero.getRoadTravel() > 1100)
            {
                gameStateManager.setLevel(7);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            graphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(back, new Rectangle(0, 0, 500, 300), Color.White);
            spriteBatch.Draw(bg1, new Rectangle(0, 0, 500, 288), new Rectangle(Xmap, 0, 500, 288), Color.White);

            hero.Draw(spriteBatch);

            Monster.Draw(spriteBatch);

            Bss.Draw(spriteBatch);

            Explosion.Draw(spriteBatch);
            FireBall.Draw(spriteBatch);

            FoodList.Draw(spriteBatch);

            if (showBB)
            {
                LineBatch.drawLineRectangle(spriteBatch, hero.getBodyBB(), Color.Red);
                LineBatch.drawLineRectangle(spriteBatch, hero.getAttackBB(), Color.Yellow);
                LineBatch.drawLineRectangle(spriteBatch, Bss.getbodyBoundingBox(), Color.Red);
                LineBatch.drawLineRectangle(spriteBatch, Bss.getAttackBoundingBox(), Color.Yellow);
                LineBatch.drawLineRectangle(spriteBatch, BssAttack, Color.Blue);

                FoodList.drawInfo(spriteBatch, Color.Red, Color.Green);
                FireBall.drawInfo(spriteBatch, Color.Red, Color.Green);

                for (int i = 0; i < Monster.count(); i++)
                {
                    Monsters m = Monster.getSprite(i);
                    LineBatch.drawLineRectangle(spriteBatch, m.getBodyBB(), Color.Blue);
                }
                //LineBatch.drawLineRectangle(spriteBatch, foodBB, Color.Red);
            }

            string distance = " Health: " + hero.getHealth() + " Boss Health" + Bss.getHealth() + "  Road Travel " + hero.getRoadTravel();
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
        public void createExplosion(float desX, float desY)
        {
            Sprite3 Boom = new Sprite3(true, explosion, desX, desY);

            Boom.setXframes(8);
            Boom.setYframes(1);
            Boom.setWidthHeight(explosion.Width / 8, explosion.Height / 1);

            Vector2[] seq = new Vector2[8];
            seq[0].X = 0; seq[0].Y = 0;
            seq[1].X = 1; seq[1].Y = 0;
            seq[2].X = 2; seq[2].Y = 0;
            seq[3].X = 3; seq[3].Y = 0;
            seq[4].X = 4; seq[4].Y = 0;
            seq[5].X = 5; seq[5].Y = 0;
            seq[6].X = 6; seq[6].Y = 0;
            seq[7].X = 7; seq[7].Y = 0;
            Boom.setAnimationSequence(seq, 0, 7, 8);
            Boom.setAnimFinished(2); // make it inactive and invisible
            Boom.animationStart();

            Explosion.addSpriteReuse(Boom);
        }

        public void createFood()
        {
            Sprite3 food1 = new Sprite3(true, apple, 200, 220);
            FoodList.addSpriteReuse(food1);
        }

    }
}
