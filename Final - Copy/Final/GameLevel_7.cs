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
    class GameLevel_7 : RC_GameStateParent
    {
        ScrollBackGround bgLayer1 = null;

        Texture2D bg1 = null;
        Texture2D hero = null;
        Texture2D monster = null;
        Texture2D logo = null;
        Texture2D par1 = null;
        Texture2D par2 = null;
        Texture2D par3 = null;
        Texture2D par4 = null;
        Texture2D par5 = null;
        Texture2D par6 = null;
        Texture2D par7 = null;

        Sprite3 logo1;
        Character hero3;

        ParticleSystem p1;
        ParticleSystem p2;
        ParticleSystem p3;
        ParticleSystem p4;
        ParticleSystem p5;
        ParticleSystem p6;
        ParticleSystem p7;


        int SCREEN_WIDTH = 500;
        int SCREEN_HEIGHT = 288;
        int CurrentX = 0;
        int ticks = 0;

        public override void LoadContent()
        {
            font1 = Content.Load<SpriteFont>("magicschool");

            bg1 = Content.Load<Texture2D>("intro");

            par1 = Content.Load<Texture2D>("c1");
            par2 = Content.Load<Texture2D>("c2");
            par3 = Content.Load<Texture2D>("c3");
            par4 = Content.Load<Texture2D>("c4");
            par5 = Content.Load<Texture2D>("c5");
            par6 = Content.Load<Texture2D>("c6");
            par7 = Content.Load<Texture2D>("c7");

            bgLayer1 = new ScrollBackGround(bg1, new Rectangle(0, 0, 500, 288), new Rectangle(0, 0, SCREEN_WIDTH, SCREEN_HEIGHT), 0.5f, 2);

            hero = Content.Load<Texture2D>("hero");

            hero3 = new Character(true, hero, 200, 174);
            hero3.setFacing(false);

            logo = Content.Load<Texture2D>("win");

            logo1 = new Sprite3(true, logo, 250 - logo.Width/2, 30);

            runParticle();

        }
        public override void Update(GameTime gameTime)
        {
            ticks++;

            prevKeyState = keyState;
            keyState = Keyboard.GetState();
            

            if (keyState.IsKeyDown(Keys.D1) && !prevKeyState.IsKeyDown(Keys.D1))
            {
                gameStateManager.setLevel(1);
            }
            else if (keyState.IsKeyDown(Keys.D2) && !prevKeyState.IsKeyDown(Keys.D2))
            {
                gameStateManager.setLevel(2);
            }
            else if (keyState.IsKeyDown(Keys.D3) && !prevKeyState.IsKeyDown(Keys.D3))
            {
                gameStateManager.setLevel(3);
            }
            else if (keyState.IsKeyDown(Keys.D4) && !prevKeyState.IsKeyDown(Keys.D4))
            {
                gameStateManager.setLevel(4);
            }
            else if (keyState.IsKeyDown(Keys.D6) && !prevKeyState.IsKeyDown(Keys.D6))
            {
                gameStateManager.setLevel(6);
            }
            logo1.animationTick(gameTime);

            hero3.UpdateIntro(gameTime, keyState);

            p7.Update(gameTime);
            p1.Update(gameTime);
            p2.Update(gameTime);
            p3.Update(gameTime);
            p4.Update(gameTime);
            p5.Update(gameTime);
            p6.Update(gameTime);

        }
        public override void Draw(GameTime gameTime)
        {
            graphicsDevice.Clear(Color.Aqua);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
            DrawBackground(gameTime);
            logo1.Draw(spriteBatch);
            
            hero3.Draw(spriteBatch);
            p7.Draw(spriteBatch);
            p1.Draw(spriteBatch);
            p2.Draw(spriteBatch);
            p3.Draw(spriteBatch);
            p4.Draw(spriteBatch);
            p5.Draw(spriteBatch);
            p6.Draw(spriteBatch);
            spriteBatch.End();
        }
        public void DrawBackground(GameTime gameTime)
        {
            bgLayer1.UpdateRectangle(CurrentX);
            bgLayer1.Update(gameTime);
            bgLayer1.Draw(spriteBatch);
        }
        
        public void runParticle()
        {

            Rectangle rectangle = new Rectangle(0, 0, 500, 288);

            p7 = new ParticleSystem(new Vector2(300, 100), 15, 3000, 103);
            p7.setMandatory1(par7, new Vector2(2, 4), new Vector2(3, 6 ), Color.White, new Color(255, 255, 255, 90));
            p7.setMandatory2(-1, 1, 1, 3, 0);
            p7.setMandatory3(120, rectangle);
            p7.setMandatory4(new Vector2(0, 0.1f), new Vector2(1, 0), new Vector2(2, 2));
            p7.randomDelta = new Vector2(0.1f, 0.1f);
            p7.Origin = 1;
            p7.originRectangle = new Rectangle(0, 0, 500, 10);
            p7.setDisplayAngle = true;
            p7.activate();

            p1 = new ParticleSystem(new Vector2(300, 100), 14, 3000, 103);
            p1.setMandatory1(par1, new Vector2(2, 4), new Vector2(3, 6 ), Color.White, new Color(255, 255, 255, 90));
            p1.setMandatory2(-1, 1, 1, 5, 5000);
            p1.setMandatory3(120, rectangle);
            p1.setMandatory4(new Vector2(0, 0.2f), new Vector2(1, 0), new Vector2(2, 2));
            p1.randomDelta = new Vector2(0.15f, 0.1f);
            p1.Origin = 1;
            p1.originRectangle = new Rectangle(0, 0, 500, 10);
            p1.setDisplayAngle = true;
            p1.activate();

            p2 = new ParticleSystem(new Vector2(400, 100), 13, 3000, 103);
            p2.setMandatory1(par2, new Vector2(2, 4), new Vector2(3, 6 ), Color.White, new Color(255, 255, 255, 90));
            p2.setMandatory2(-1, 1, 1, 5, 3000);
            p2.setMandatory3(120, rectangle);
            p2.setMandatory4(new Vector2(0, 0.2f), new Vector2(1, 0), new Vector2(2, 2));
            p2.randomDelta = new Vector2(0.12f, 0.1f);
            p2.Origin = 1;
            p2.originRectangle = new Rectangle(0, 0, 500, 10);
            p2.setDisplayAngle = true;
            p2.activate();

            p3 = new ParticleSystem(new Vector2(300, 100), 14, 3000, 103);
            p3.setMandatory1(par3, new Vector2(2, 4), new Vector2(3, 6 ), Color.White, new Color(255, 255, 255, 90));
            p3.setMandatory2(-1, 1, 1, 5, 4000);
            p3.setMandatory3(120, rectangle);
            p3.setMandatory4(new Vector2(0, 0.17f), new Vector2(1, 0), new Vector2(2, 2));
            p3.randomDelta = new Vector2(0.13f, 0.1f);
            p3.Origin = 1;
            p3.originRectangle = new Rectangle(0, 0, 500, 10);
            p3.setDisplayAngle = true;
            p3.activate();

            p4 = new ParticleSystem(new Vector2(300, 100), 16, 3000, 103);
            p4.setMandatory1(par4, new Vector2(2, 4), new Vector2(3, 6 ), Color.White, new Color(255, 255, 255, 90));
            p4.setMandatory2(-1, 1, 1, 5, 5400);
            p4.setMandatory3(120, rectangle);
            p4.setMandatory4(new Vector2(0, 0.16f), new Vector2(1, 0), new Vector2(2, 2));
            p4.randomDelta = new Vector2(0.1f, 0.12f);
            p4.Origin = 1;
            p4.originRectangle = new Rectangle(0, 0, 500, 10);
            p4.setDisplayAngle = true;
            p4.activate();

            p5 = new ParticleSystem(new Vector2(300, 100), 17, 3000, 103);
            p5.setMandatory1(par5, new Vector2(2, 4), new Vector2(3, 6 ), Color.White, new Color(255, 255, 255, 90));
            p5.setMandatory2(-1, 1, 1, 5, 3800);
            p5.setMandatory3(120, rectangle);
            p5.setMandatory4(new Vector2(0, 0.15f), new Vector2(1, 0), new Vector2(2, 2));
            p5.randomDelta = new Vector2(0.1f, 0.12f);
            p5.Origin = 1;
            p5.originRectangle = new Rectangle(0, 0, 500, 10);
            p5.setDisplayAngle = true;
            p5.activate();

            p6 = new ParticleSystem(new Vector2(300, 100), 14, 3000, 103);
            p6.setMandatory1(par6, new Vector2(2, 4), new Vector2(3, 6 ), Color.White, new Color(255, 255, 255, 90));
            p6.setMandatory2(-1, 1, 1, 5, 5300);
            p6.setMandatory3(120, rectangle);
            p6.setMandatory4(new Vector2(0, 0.2f), new Vector2(1, 0), new Vector2(2, 2));
            p6.randomDelta = new Vector2(0.1f, 0.31f);
            p6.Origin = 1;
            p6.originRectangle = new Rectangle(0, 0, 500, 10);
            p6.setDisplayAngle = true;
            p6.activate();
        }

    }
}
