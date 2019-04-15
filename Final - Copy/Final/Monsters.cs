using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RC_Framework
{
    class Monsters : Sprite3
    {
        Color mons_Color;
        bool right2Left = true;
        int counting = 0;
        int Health = 100;
        float XSpeed = 1;
        float introSpeed = 1;
        Rectangle BB;
        Texture2D explosion;
        SpriteList EP;
        bool farEnough = true;
        int ticks = 0;
        bool Dead = false;
        int die = 0;
        bool introEnd = false;

        SoundEffect pop;
        SoundEffect bite;

        LimitSound popSound;
        LimitSound biteSound;

        float targetX;
        private bool FacingRight = false;
        public Monsters(bool visible, Texture2D monster, float x, float y) : base(visible, monster, x, y)
        {
            Xframe = 0;
            Yframe = 0;
            XframeWidth = 16;
            YframeHeight = 16;
            setPosX(x);
            setPosY(y);
            mons_Color = Color.White;
            setVisible(visible);
            EP = new SpriteList();
        }
        public void setExplosion(Texture2D e)
        {
            explosion = e;
        }
        // Idle
        public void Idle()
        {

            mons_Color = Color.White;
            Vector2[] seq = new Vector2[3];
            seq[0].X = 0; seq[0].Y = 0;
            seq[1].X = 1; seq[1].Y = 0;
            seq[2].X = 2; seq[2].Y = 0;
            setAnimationSequence(seq, 0, 2, 10);

            if (ticks % 8 == 0)
            {
                if (right2Left)
                {
                    FacingRight = false;
                    setPosX(getPosX() - XSpeed);
                    if (counting < 20)
                        counting++;
                    else
                    {
                        counting = 0;
                        right2Left = !right2Left;
                    }
                }
                else
                {
                    FacingRight = true;
                    setPosX(getPosX() + XSpeed);
                    if (counting < 20)
                        counting++;
                    else
                    {
                        counting = 0;
                        right2Left = !right2Left;
                    }

                }
            }
        }

        // Monsters walking
        public void Walk(float targetX)
        {
                mons_Color = Color.White;
            
                Vector2[] seq = new Vector2[3];
                seq[0].X = 0; seq[0].Y = 0;
                seq[1].X = 1; seq[1].Y = 0;
                seq[2].X = 1; seq[2].Y = 0;
                setAnimationSequence(seq, 0, 2, 10);

            if (ticks % 5 == 0)
            {
                if (!checkFacing(targetX))
                {
                    setPosX(getPosX() - 2 * XSpeed);

                }
                else
                {
                    setPosX(getPosX() + 2 * XSpeed);
                }
            }
        }
        //Monsters hurt
        public void Hurt()
        {
            mons_Color = Color.OrangeRed;
            Health = Health - 5;
        }
        //Monster Dies
        public void Die()
        {
            setActiveAndVisible(false);
        }
        // change side of the monster when walk pass the hero
        public void changeSide(bool Side)
        {
            FacingRight = Side;
        }

        // check if the monster is facing right or left by comparing to the main character. the monster will always heading toward the character while not staying idle
        public bool checkFacing(float objectX)
        {
            if (getCenterX() >= objectX + 25)
            {
                FacingRight = false;
            }
            else if (getCenterX() < objectX - 25)
            {
            FacingRight = true;
            }
            return FacingRight;
        }
        // check if the monster is still alive
        public bool checkAlive()
        {
            if (Health > 0)
                return true;
            else
                return false;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!Dead)
            {
                if (FacingRight)
                    spriteBatch.Draw(getTextureBase(), new Rectangle((int)getPosX(), (int)getPosY(), XframeWidth, YframeHeight), new Rectangle(Xframe * XframeWidth, Yframe * YframeHeight, XframeWidth, YframeHeight), mons_Color);
                else
                    spriteBatch.Draw(getTextureBase(), new Rectangle((int)getPosX(), (int)getPosY(), XframeWidth, YframeHeight), new Rectangle(Xframe * XframeWidth, Yframe * YframeHeight, XframeWidth, YframeHeight), mons_Color, 0, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0);
            }

            EP.Draw(spriteBatch);
        }

        // return the bouding box of the monster body
        public Rectangle getBodyBB()
        {
            return BB;
        }

        public override void Update(GameTime gameTime)
        {
            ticks++;
            animationTick(gameTime);

            EP.animationTick(gameTime);

            if (checkAlive())
            {
                BB = new Rectangle((int)getPosX(), (int)getPosY(), XframeWidth, YframeHeight);
                if (Math.Abs(targetX - getCenterX()) < 200)
                {
                    farEnough = true;
                    Walk(targetX);
                }
                else
                {
                    farEnough = false;
                    Idle();
                }
            }
            
            else
            {
                die++;
                if(die ==5)
                {
                    Dead = true;
                    explode();
                    //setActiveAndVisible(false);
                    BB = new Rectangle(0, 0, 0, 0);
                }
                if(die ==20)
                {
                    popSound.playSoundIfOk();
                }
            }
        }
        // return the center X position of the monster
        public float getCenterX()
        {
            float centerX = getPosX() + (XframeWidth) / 2;
            return centerX;
        }

        // return the center Y position of the monster
        public float getCenterY()
        {
            float centerY = getPosY() + (YframeHeight) / 2;
            return centerY;
        }
        public void PushBack(float collideX)
        {
            if(FacingRight)
                setPosX(collideX - XSpeed * 10);
            else
                setPosX(collideX + XSpeed * 10);
        }
        public int getHealth()
        {
            return Health;
        }
        public void setTarget(float desX)
        {
            targetX = desX;
        }
        public void explode()
        {
            Sprite3 Boom = new Sprite3(true, explosion, getPosX(), getPosY()-10);

            Boom.setXframes(6);
            Boom.setYframes(1);

            Boom.setWidthHeight(explosion.Width / 6, explosion.Height / 1);

            Vector2[] seq = new Vector2[6];
            seq[0].X = 0; seq[0].Y = 0;
            seq[1].X = 1; seq[1].Y = 0;
            seq[2].X = 2; seq[2].Y = 0;
            seq[3].X = 3; seq[3].Y = 0;
            seq[4].X = 4; seq[4].Y = 0;
            seq[5].X = 5; seq[5].Y = 0;
            Boom.setAnimationSequence(seq, 0, 5, 10);
            Boom.setAnimFinished(2); // make it inactive and invisible
            Boom.animationStart();

            EP.addSpriteReuse(Boom);
        }


        // INTRO

        public void introWalk()
        {
            mons_Color = Color.White;

            Vector2[] seq = new Vector2[3];
            seq[0].X = 0; seq[0].Y = 0;
            seq[1].X = 1; seq[1].Y = 0;
            seq[2].X = 1; seq[2].Y = 0;
            setAnimationSequence(seq, 0, 2, 10);

            if (FacingRight)
            {
                if (getPosX() < 500)
                {
                    setPosX(getPosX() + introSpeed);
                }
                else
                {
                    FacingRight = false;
                }
            }
            else
            {

                if (getPosX() > 0)
                {
                    setPosX(getPosX() - introSpeed);
                }
                else
                {
                    FacingRight = true;
                }
            }
        }
        public void leave()
        {
            mons_Color = Color.White;

            Vector2[] seq = new Vector2[3];
            seq[0].X = 0; seq[0].Y = 0;
            seq[1].X = 1; seq[1].Y = 0;
            seq[2].X = 1; seq[2].Y = 0;
            setAnimationSequence(seq, 0, 2, 10);

            if (FacingRight)
            {
                    setPosX(getPosX() + introSpeed);
            }
            else
            {
                    FacingRight = true;
            }
        }

        public void UpdateIntro(GameTime gameTime)
        {
            ticks++;
            animationTick(gameTime);
            if (!introEnd)

                introWalk();
            else
                leave();
        }
        
        // GETTING AND SETTING
        public void setSpeed(float S)
        {
            introSpeed = S;
        }
        public void endIntro()
        {
            introEnd = true;
        }
        public void setPopSound(SoundEffect sound)
        {
            pop = sound;
        }
        public void setBiteSound(SoundEffect sound)
        {
            bite = sound;
        }
        public void setSound()
        {
            biteSound = new LimitSound(bite, 1);
            popSound = new LimitSound(pop, 1);
        }
    }
}
