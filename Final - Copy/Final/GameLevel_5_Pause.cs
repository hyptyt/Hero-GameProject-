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
    class GameLevel_5_Pause: RC_GameStateParent
    {
        public override void LoadContent()
        {
            font1 = Content.Load<SpriteFont>("Font");
        }
        public override void Update(GameTime gameTime)
        {
            prevKeyState = keyState;
            keyState = Keyboard.GetState();
            if(keyState.IsKeyDown(Keys.P)&& prevKeyState.IsKeyUp(Keys.P))
            {
                gameStateManager.popLevel();
            }
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(font1, "AAAA", new Vector2(100, 200), Color.White);
            spriteBatch.End();
        }
    }
}
