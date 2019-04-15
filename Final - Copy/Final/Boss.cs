using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RC_Framework
{
    class Boss :Sprite3
    {
        Color boss_Color;
        bool FacingRight = false;
        Rectangle bodyBB;
        Rectangle attackBB;
        Texture2D fireBall;
        Texture2D finalEffect;
        SpriteList finalBoom;
        SpriteList FireBall;
        int ticks = 0;
        bool Attacking1 = false;
        bool Attacking2 = false;
        int XSpeed = 1;
        bool farEnough = false;
        bool Firing = false;
        int Health = 300;
        bool Dead = false;
        int die = 0;
        bool introEnd = false;
        SoundEffect fire;
        SoundEffect flame;
        SoundEffect hurt;
        SoundEffect death;
        SoundEffect explosion;

        LimitSound fireSound;
        LimitSound flameSound;
        LimitSound hurtSound;
        LimitSound dieSound;
        LimitSound explosionSound;


        public Boss(bool visible, Texture2D bossImg, float x, float y ): base(visible, bossImg, x,y)
        {
            Xframe = 0;
            Yframe = 0;
            XframeWidth = bossImg.Width/11;
            YframeHeight = bossImg.Height/3;
            setPosX(x);
            setPosY(y);
            setVisible(visible);
            boss_Color = Color.White;
            FireBall = new SpriteList();
            finalBoom = new SpriteList();
        }

        /// <summary>
        /// SETTING TEXTURE FOR FIREBALLS AND EXPLOSION
        /// </summary>
        /// <param name="fb"></param>
        public void setFireBall(Texture2D fb)
        {
            fireBall = fb;
        }
        public void setFinalExplosion(Texture2D fe)
        {
            finalEffect = fe;
        }

        //ACTIONS
        public void Idle()
        {
            boss_Color = Color.White;
            Attacking1 = false;
            Attacking2 = false;
            Vector2[] seq = new Vector2[6];
            seq[0].X = 0; seq[0].Y = 0;
            seq[1].X = 1; seq[1].Y = 0;
            seq[2].X = 2; seq[2].Y = 0;
            seq[3].X = 3; seq[0].Y = 0;
            seq[4].X = 4; seq[1].Y = 0;
            seq[5].X = 5; seq[2].Y = 0;
            setAnimationSequence(seq, 0, 5, 10);
        }
        public void Walk(float desX)
        {
            boss_Color = Color.White;
            Attacking1 = false;
            Attacking2 = false;
            Vector2[] seq = new Vector2[6];
            seq[0].X = 0; seq[0].Y = 0;
            seq[1].X = 1; seq[1].Y = 0;
            seq[2].X = 2; seq[2].Y = 0;
            seq[3].X = 3; seq[0].Y = 0;
            seq[4].X = 4; seq[1].Y = 0;
            seq[5].X = 5; seq[2].Y = 0;
            setAnimationSequence(seq, 0, 5, 10);

            if (ticks % 5 == 0)
            {
                if (!checkFacing(desX))
                {
                    setPosX(getPosX() - 2 * XSpeed);

                }
                else
                {
                    setPosX(getPosX() + 2 * XSpeed);
                }
            }
        }
        public void Attack1()
        {
            boss_Color = Color.White;
            Attacking1 = true;
            Attacking2 = false;
            Firing = false;

            Vector2[] seq = new Vector2[8];
            seq[0].X = 0; seq[0].Y = 1;
            seq[1].X = 1; seq[1].Y = 1;
            seq[2].X = 2; seq[2].Y = 1;
            seq[3].X = 3; seq[3].Y = 1;
            seq[4].X = 4; seq[4].Y = 1;
            seq[5].X = 5; seq[5].Y = 1;
            seq[6].X = 6; seq[6].Y = 1;
            seq[7].X = 7; seq[7].Y = 1;
            setAnimationSequence(seq, 0, 7, 10);
            
            if( Xframe == 7)
            {
                Firing = true;
            }
        }
        public void Fire(Character Hero)
        {
            if (Firing)
            {
                float oriX = getbodyBoundingBox().X;
                float oriY = getbodyBoundingBox().Y + 25;
                int width = getbodyBoundingBox().Width;
                Sprite3 fireball;
                Vector2 speed;

                if (ticks % 20 == 0)
                {
                    if (!FacingRight)
                    {
                        fireball = new Sprite3(true, fireBall, oriX, oriY);

                        speed = new Vector2(-(oriX - Hero.getCenterX()) / 40, -(oriY - Hero.getCenterY()) / 40);
                    }
                    else
                    {
                        fireball = new Sprite3(true, fireBall, oriX + width - 13, oriY);

                        speed = new Vector2(-(oriX - Hero.getCenterX()) / 40, -(oriY - Hero.getCenterY()) / 40);
                    }

                    fireball.setDeltaSpeed(speed);
                    FireBall.addSpriteReuse(fireball);
                }
            }
        }
        public void Attack2(float desX)
        {
            boss_Color = Color.White;
            Attacking1 = false;
            Attacking2 = true;
            Vector2[] seq = new Vector2[11];
            seq[0].X = 0; seq[0].Y = 2;
            seq[1].X = 1; seq[1].Y = 2;
            seq[2].X = 2; seq[2].Y = 2;
            seq[3].X = 3; seq[3].Y = 2;
            seq[4].X = 4; seq[4].Y = 2;
            seq[5].X = 5; seq[5].Y = 2;
            seq[6].X = 6; seq[6].Y = 2;
            seq[7].X = 7; seq[7].Y = 2;
            seq[8].X = 8; seq[8].Y = 2;
            seq[9].X = 9; seq[9].Y = 2;
            seq[10].X = 10; seq[10].Y = 2;

            setAnimationSequence(seq, 0, 10, 10);
            

            if (farEnough)
            {
                if (ticks % 5 == 0)
                {
                    if (!checkFacing(desX))
                    {
                        setPosX(getPosX() - 2 * XSpeed);

                    }
                    else
                    {
                        setPosX(getPosX() + 2 * XSpeed);
                    }
                }
            }
        }
        public void Hurt()
        {
            boss_Color = Color.Orange;
            Health = Health - 10;
        }
        public void Die()
        {
            Vector2[] seq = new Vector2[6];
            seq[0].X = 0; seq[0].Y = 2;
            seq[1].X = 1; seq[1].Y = 2;
            seq[2].X = 2; seq[2].Y = 2;
            seq[3].X = 3; seq[3].Y = 2;
            seq[4].X = 4; seq[4].Y = 2;
            seq[5].X = 5; seq[5].Y = 2;
            setAnimationSequence(seq, 0, 5, 30);
        }

        // DRAW
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!Dead)
            {
                if (!FacingRight)
                    spriteBatch.Draw(getTextureBase(), new Rectangle((int)getPosX(), (int)getPosY(), XframeWidth, YframeHeight), new Rectangle(Xframe * XframeWidth, Yframe * YframeHeight, XframeWidth, YframeHeight), boss_Color);
                else
                    spriteBatch.Draw(getTextureBase(), new Rectangle((int)getPosX(), (int)getPosY(), XframeWidth, YframeHeight), new Rectangle(Xframe * XframeWidth, Yframe * YframeHeight, XframeWidth, YframeHeight), boss_Color, 0, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0);
                
                FireBall.Draw(spriteBatch);
            }
            
            finalBoom.Draw(spriteBatch);
           
        }

        // CHECKING AND GETTING BOUNDING BOX AND OTHER INFORMATION OF THE SPRITES
        public Rectangle getbodyBoundingBox()
        {
            return bodyBB;
        }
        public Rectangle getAttackBoundingBox()
        {
            return attackBB;
        }
        // check which way the boss is facing, return false boss is facing left and true if facing right
        public bool checkFacing(float objectX)
        {
            if (getCenterX() >= objectX + 10)
            {
                FacingRight = false;
            }
            else if (getCenterX() < objectX - 10)
            {
                FacingRight = true;
            }
            return FacingRight;
        }
        public float getCenterX()
        {
            float centerX;

            if (!FacingRight)
            {
                centerX = getPosX() + 105 + (XframeWidth - 180) / 2;
            }
            else
            {
                centerX = getPosX() + 75 + (XframeWidth - 180) / 2;
            }


            return centerX;
        }
        public bool isAttacking1()
        {
            return Attacking1;
        }
        public bool isAttacking2()
        {
            return Attacking2;
        }
        public bool getFacing()
        {
            return FacingRight;
        }
        public bool checkAlive()
        {
            if (Health > 0)
            {
                return true;
            }
            else return false;
        }
        public int getHealth()
        {
            return Health;
        }
        public SpriteList returnFireBallList()
        {
            return FireBall;
        }

        //UPDATE
        public void Update(GameTime gameTime, Character Hero)
        {
            ticks++;
            animationTick(gameTime);

            FireBall.animationTick(gameTime);
            FireBall.moveDeltaXY();

            float desX = Hero.getCenterX();
            checkFacing(desX);

            finalBoom.animationTick(gameTime);
            attackBB = new Rectangle(0, 0, 0, 0);

            if (checkAlive())
            {
                if (!FacingRight)
                {
                    bodyBB = new Rectangle((int)getPosX() + 105, (int)getPosY() + 90, XframeWidth - 180, YframeHeight - 110);
                }
                else
                {
                    bodyBB = new Rectangle((int)getPosX() + 75, (int)getPosY() + 90, XframeWidth - 180, YframeHeight - 110);
                }

                float distance = Math.Abs(desX - getCenterX());

                if (distance <= 300 && distance >= 200)
                {
                    farEnough = true;
                    Walk(desX);
                }
                else if (distance < 200 && distance >= 100)
                {

                    farEnough = true;
                    Attack1();
                    //if(ticks%20==0)
                    //{
                        Fire(Hero);
                    //}
                }
                else if (distance < 100)
                {
                    farEnough = true;
                    Attack2(desX);
                    if (!FacingRight)
                    {
                        switch (Xframe)
                        {
                            case 6:
                                attackBB = new Rectangle((int)getPosX() + 79, (int)getPosY() + 122, 38, 34);
                                break;
                            case 7:
                                attackBB = new Rectangle((int)getPosX() + 24, (int)getPosY() + 121, 86, 70);
                                break;
                            case 8:
                                attackBB = new Rectangle((int)getPosX() + 15, (int)getPosY() + 124, 105, 70);
                                break;
                            case 9:
                                attackBB = new Rectangle((int)getPosX() + 12, (int)getPosY() + 120, 108, 70);
                                break;
                            case 10:
                                attackBB = new Rectangle((int)getPosX() + 42, (int)getPosY() + 152, 40, 40);
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        switch (Xframe)
                        {
                            case 6:
                                attackBB = new Rectangle((int)getPosX() + 123, (int)getPosY() + 122, 38, 34);
                                break;
                            case 7:
                                attackBB = new Rectangle((int)getPosX() + 130, (int)getPosY() + 121, 86, 70);
                                break;
                            case 8:
                                attackBB = new Rectangle((int)getPosX() + 120, (int)getPosY() + 124, 105, 70);
                                break;
                            case 9:
                                attackBB = new Rectangle((int)getPosX() + 120, (int)getPosY() + 120, 108, 70);
                                break;
                            case 10:
                                attackBB = new Rectangle((int)getPosX() + 158, (int)getPosY() + 152, 40, 40);
                                break;
                            default:
                                break;
                        }
                    }
                }
                else
                {
                    farEnough = false;
                    Idle();
                }
            }
            else
            {            
                Die();
                die++;
                
                if (die == 5)
                {
                    Dead = true;
                    finalExplosion();
                    setActiveAndVisible(false);
                    bodyBB = new Rectangle(0, 0, 0, 0);
                    attackBB = new Rectangle(0, 0, 0, 0);
                }
            }
        }
      

        // EFFECTS
        public void finalExplosion()
        {
            float desX = bodyBB.X;
            float desY = getPosY();

            Sprite3 Boom;
            Vector2[] seq;

            Boom = new Sprite3(true, finalEffect, desX - 80, desY + 20);
            Boom.setXframes(10);
            Boom.setYframes(10);
            Boom.setWidthHeight(finalEffect.Width / 10, finalEffect.Height / 10);

            seq = new Vector2[100];
            for (int i = 0; i < 100; i++)
            {
                seq[i].X = i % 10;
                seq[i].Y = i / 10;
            }
            Boom.setAnimationSequence(seq, 0, 99, 3);
            Boom.setAnimFinished(2); // make it inactive and invisible
            Boom.animationStart();

            finalBoom.addSpriteReuse(Boom);
        }

        // INTRO
        public void introAttack()
        {
            boss_Color = Color.White;

            Vector2[] seq = new Vector2[11];
            seq[0].X = 0; seq[0].Y = 2;
            seq[1].X = 1; seq[1].Y = 2;
            seq[2].X = 2; seq[2].Y = 2;
            seq[3].X = 3; seq[3].Y = 2;
            seq[4].X = 4; seq[4].Y = 2;
            seq[5].X = 5; seq[5].Y = 2;
            seq[6].X = 6; seq[6].Y = 2;
            seq[7].X = 7; seq[7].Y = 2;
            seq[8].X = 8; seq[8].Y = 2;
            seq[9].X = 9; seq[9].Y = 2;
            seq[10].X = 10; seq[10].Y = 2;
            setAnimationSequence(seq, 0, 10, 10);

            if (FacingRight)
            {
                if (getPosX() < 360)
                {
                    setPosX(getPosX() + XSpeed);
                }
                else
                {
                    FacingRight = false;
                }
                XSpeed = 1;
            }
            else
            {

                if (getPosX() > -50)
                {
                    setPosX(getPosX() + XSpeed);
                }
                else
                {
                    FacingRight = true;
                }
                XSpeed = -1;
            }
        }
        public void leaveAfterIntro()
        {
            boss_Color = Color.White;

            Vector2[] seq = new Vector2[6];
            seq[0].X = 0; seq[0].Y = 0;
            seq[1].X = 1; seq[1].Y = 0;
            seq[2].X = 2; seq[2].Y = 0;
            seq[3].X = 3; seq[0].Y = 0;
            seq[4].X = 4; seq[1].Y = 0;
            seq[5].X = 5; seq[2].Y = 0;
            setAnimationSequence(seq, 0, 5, 10);

            if (FacingRight)
            {
                setPosX(getPosX() + 2);
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

                introAttack();
            else
                leaveAfterIntro();

        }
        public void endIntro()
        {
            introEnd =true;
        }
        public int getSpeed()
        {
            return XSpeed;
        }
    }
}
