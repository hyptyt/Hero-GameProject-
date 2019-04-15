using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using RC_Framework;

namespace Final
{
    class GameLevel_0: RC_GameStateParent
    {
        ScrollBackGround bgLayer1 = null;

        SpriteFont font;
        Texture2D bg1 = null;
        Texture2D hero = null;
        Texture2D bossImg = null;
        Texture2D logo = null;
        Sprite3 logo1;
        Character hero1;
        Boss boss;
        int SCREEN_WIDTH = 500;
        int SCREEN_HEIGHT = 288;
        int CurrentX = 0;
        int ticks = 0;
        bool switchingCha = false;
        int textY = 120;

        public override void LoadContent()
        {
            font = Content.Load<SpriteFont>("magicschool");
            bg1 = Content.Load<Texture2D>("intro");

            bgLayer1 = new ScrollBackGround(bg1, new Rectangle(0, 0, 500, 288), new Rectangle(0, 0, SCREEN_WIDTH, SCREEN_HEIGHT), -0.5f, 2);

            hero = Content.Load<Texture2D>("hero");
            bossImg = Content.Load<Texture2D>("demon");
            
            hero1 = new Character(true, hero, 200, 174);
            boss = new Boss(true, bossImg, 190, 79);

            logo = Content.Load<Texture2D>("logo");

            logo1 = new Sprite3(true, logo, 250 - logo.Width / 2, 30);
            
        }
        public override void Update(GameTime gameTime)
        {
            ticks++;
            if (ticks % 20 == 0)
            {
                if (textY >= 119)
                    textY -= 2;
                else
                    textY += 2;
            }

            prevKeyState = keyState;
            keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.S) && !prevKeyState.IsKeyDown(Keys.S))
            {
                switchingCha = !switchingCha;
            }

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
            else if (keyState.IsKeyDown(Keys.D7) && !prevKeyState.IsKeyDown(Keys.D7))
            {
                gameStateManager.setLevel(7);
            }
            else if (keyState.IsKeyDown(Keys.Space) && !prevKeyState.IsKeyDown(Keys.Space))
            {
                gameStateManager.setLevel(6);
            }
            logo1.animationTick(gameTime);
            
            hero1.UpdateIntro(gameTime, keyState);
            boss.UpdateIntro(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            graphicsDevice.Clear(Color.Aqua);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
            DrawBackground(gameTime);
            logo1.Draw(spriteBatch);
            if (!switchingCha)
                hero1.Draw(spriteBatch);
            else
                boss.Draw(spriteBatch);

            spriteBatch.DrawString(font, "Press Space to start", new Vector2(165, textY), Color.White);

            spriteBatch.End();
        }
        public void DrawBackground(GameTime gameTime)
        {
            bgLayer1.UpdateRectangle(CurrentX);
            bgLayer1.Update(gameTime);
            bgLayer1.Draw(spriteBatch);
        }
       
        public void Attack()
        {

        }
 
    }
}
