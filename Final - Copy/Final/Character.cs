using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RC_Framework

{
    class Character : Sprite3
    {
        int XSpeed = 1;
        int SCREEN_WIDTH;
        int SCREEN_HEIGHT;
        int Road_travel = 20;
        int Road_Speed = 0;
        int farRight = 0;
        int Damage = 1;
        bool canGoRight = false;
        bool canMoveout = false;
        bool isCrouching = false;

        Rectangle BodyBB;
        Rectangle AttackBB;
        Color chaColor;
        float standY;
        private bool FacingRight = true;
        private bool beingAttacked = false;
        KeyboardState prevK;
        KeyboardState kbs;
        HealthBar HP;

        SoundEffect hurt;
        SoundEffect slash;
        LimitSound hurtSound;
        LimitSound slashSound;
        
        public Character(bool visible, Texture2D chara, float x, float y) : base(visible, chara, x, y)
        {
            chaColor= Color.White;
            Xframe = 0;
            Yframe = 0;
            XframeWidth = chara.Width/12;
            YframeHeight = chara.Height/4;
            setPosX(x);
            setPosY(y);
            standY = y;
            setVisible(visible);
            HP = new HealthBar(Color.Red, Color.White, Color.Black, 10, true);
            maxHitPoints = 100;
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //-------------------------------------------------------------------------ACTIONS-------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public void WalkStill()
        {
            chaColor = Color.White;
            Vector2[] seq = new Vector2[12];
            seq[0].X = 4; seq[0].Y = 2;
            seq[1].X = 5; seq[1].Y = 2;
            seq[2].X = 6; seq[2].Y = 2;
            seq[3].X = 7; seq[3].Y = 2;
            seq[4].X = 4; seq[4].Y = 2;
            seq[5].X = 5; seq[5].Y = 2;
            seq[6].X = 6; seq[6].Y = 2;
            seq[7].X = 7; seq[7].Y = 2;
            seq[8].X = 4; seq[8].Y = 2;
            seq[9].X = 5; seq[9].Y = 2;
            seq[10].X = 6; seq[10].Y = 2;
            seq[11].X = 7; seq[11].Y = 2;
            setAnimationSequence(seq, 0, 11, 10);
            AttackBB = new Rectangle(0, 0, 0, 0);

            if (FacingRight)
            {
                    BodyBB = new Rectangle((int)getPosX() + 45, (int)getPosY() + 35, 24, 43);
            }
            else
            {
                    BodyBB = new Rectangle((int)getPosX() + 28, (int)getPosY() + 35, 24, 43);
            }
        }
        public void Walk()
        {
            chaColor= Color.White;
            Vector2[] seq = new Vector2[12];
            seq[0].X = 4; seq[0].Y = 2;
            seq[1].X = 5; seq[1].Y = 2;
            seq[2].X = 6; seq[2].Y = 2;
            seq[3].X = 7; seq[3].Y = 2;
            seq[4].X = 4; seq[4].Y = 2;
            seq[5].X = 5; seq[5].Y = 2;
            seq[6].X = 6; seq[6].Y = 2;
            seq[7].X = 7; seq[7].Y = 2;
            seq[8].X = 4; seq[8].Y = 2;
            seq[9].X = 5; seq[9].Y = 2;
            seq[10].X = 6; seq[10].Y = 2;
            seq[11].X = 7; seq[11].Y = 2;
            setAnimationSequence(seq, 0, 11, 10);

            if (FacingRight && getPosX() < SCREEN_WIDTH - 70)
                XSpeed = 1;
            else if (!FacingRight && getPosX() > -27)
                XSpeed = -1;
            else
                if (canGoRight)
                XSpeed = 1;
            else XSpeed = 0;


            if (FacingRight && Road_travel < farRight)
                Road_Speed = 3 / 2;
            else if (!FacingRight && Road_travel > 0)
                Road_Speed = -3 / 2;
            else
                if (canGoRight)
                Road_Speed = 3 / 2;
            else
                Road_Speed = 0;

            setPosX(getPosX() + XSpeed);

            AttackBB = new Rectangle(0, 0, 0, 0);
            if (FacingRight)
            {
                BodyBB = new Rectangle((int)getPosX() + 45, (int)getPosY() + 35, 24, 43);
            }
            else
            {

                BodyBB = new Rectangle((int)getPosX() + 28, (int)getPosY() + 35, 24, 43);
            }
        }
        public void Move()
        {
            if (FacingRight && getPosX() < SCREEN_WIDTH - 70)
                XSpeed = 6 / 5;
            else if (!FacingRight && getPosX() > -27)
                XSpeed = -6 / 5;
            else
                if (canGoRight)
                XSpeed = 6 / 5;
            else
                XSpeed = 0;

            if (FacingRight && Road_travel < farRight)
                Road_Speed = 2;
            else if (!FacingRight && Road_travel > 0)
                Road_Speed = -2;
            else
                if (canGoRight)
                Road_Speed = 3 / 2;
            else
                Road_Speed = 0;

            setPosX(getPosX() + XSpeed);

            AttackBB = new Rectangle(0, 0, 0, 0);
        }
        public void Run()
        {
            chaColor= Color.White;
            Vector2[] seq = new Vector2[12];
            seq[0].X = 4; seq[0].Y = 2;
            seq[1].X = 5; seq[1].Y = 2;
            seq[2].X = 6; seq[2].Y = 2;
            seq[3].X = 7; seq[3].Y = 2;
            seq[4].X = 4; seq[4].Y = 2;
            seq[5].X = 5; seq[5].Y = 2;
            seq[6].X = 6; seq[6].Y = 2;
            seq[7].X = 7; seq[7].Y = 2;
            seq[8].X = 4; seq[8].Y = 2;
            seq[9].X = 5; seq[9].Y = 2;
            seq[10].X = 6; seq[10].Y = 2;
            seq[11].X = 7; seq[11].Y = 2;
            setAnimationSequence(seq, 0, 11, 10);

            if (FacingRight && getPosX() < SCREEN_WIDTH - 70)
                XSpeed = 2;
            else if (!FacingRight && getPosX() > -27)
                XSpeed = -2;
            else
                if (canGoRight)
                XSpeed = 2;
            else XSpeed = 0;

            if (FacingRight && Road_travel < farRight)
                Road_Speed = 5/2;
            else if (!FacingRight && Road_travel > 0)
                Road_Speed = -5/2;
            else
                if (canGoRight)
                Road_Speed = 5 / 2;
            else
                Road_Speed = 0;

            setPosX(getPosX() + XSpeed);
            
            AttackBB = new Rectangle(0, 0, 0, 0);

            if (FacingRight)
            {
                BodyBB = new Rectangle((int)getPosX() + 45, (int)getPosY() + 35, 24, 43);
            }
            else
            {

                BodyBB = new Rectangle((int)getPosX() + 28, (int)getPosY() + 35, 24, 43);
            }
        }
       
        public void Idle()
        {
            chaColor= Color.White;
            Vector2[] seq = new Vector2[4];
            seq[0].X = 0; seq[0].Y = 0;
            seq[1].X = 1; seq[1].Y = 0;
            seq[2].X = 2; seq[2].Y = 0;
            seq[3].X = 3; seq[3].Y = 0;
            setAnimationSequence(seq, 0, 3, 10);

            AttackBB = new Rectangle(0, 0, 0, 0);
            if (FacingRight)
            {
                    BodyBB = new Rectangle((int)getPosX() + 35, (int)getPosY() + 40, 26, 40);
            }
            else
            {
                    BodyBB = new Rectangle((int)getPosX() + 35, (int)getPosY() + 40, 26, 40);
            }
        }
        public void Attack()
        {
            chaColor= Color.White;
            Vector2[] seq = new Vector2[6];
            seq[0].X = 0; seq[0].Y = 3;
            seq[1].X = 1; seq[1].Y = 3;
            seq[2].X = 2; seq[2].Y = 3;
            seq[3].X = 3; seq[3].Y = 3;
            seq[4].X = 4; seq[4].Y = 3;
            seq[5].X = 5; seq[5].Y = 3;
            setAnimationSequence(seq, 0, 5, 10);

            if (!FacingRight)
            {
                switch (Xframe)
                    {
                        case 2:
                            AttackBB = new Rectangle((int)getPosX() + 3, (int)getPosY() + 52, 39, 16);
                            slashSound.playSoundIfOk();
                            break;
                        case 3:
                            AttackBB = new Rectangle((int)getPosX() + 14, (int)getPosY() + 50, 16, 4);
                            break;
                        default:
                            AttackBB = new Rectangle(0, 0, 0, 0);
                            break;
                    }

                switch (Xframe)
                {
                    case 0:
                        BodyBB = new Rectangle((int)getPosX() + 35, (int)getPosY() + 41, 24, 39);
                        break;
                    case 1:
                        BodyBB = new Rectangle((int)getPosX() + 41, (int)getPosY() + 46, 19, 34);
                        break;
                    case 2:
                        BodyBB = new Rectangle((int)getPosX() + 31, (int)getPosY() + 41, 30, 39);
                        break;
                    case 3:
                        BodyBB = new Rectangle((int)getPosX() + 31, (int)getPosY() + 41, 30, 39);
                        break;
                    case 4:
                        BodyBB = new Rectangle((int)getPosX() + 33, (int)getPosY() + 41, 28, 39);
                        break;
                    case 5:
                        BodyBB = new Rectangle((int)getPosX() + 33, (int)getPosY() + 41, 28, 39);
                        break;
                    default:
                        BodyBB = new Rectangle(0, 0, 0, 0);
                        break;
                }
            }
            else
            {
                    switch (Xframe)
                    {
                        case 2:
                            AttackBB = new Rectangle((int)getPosX() + 54, (int)getPosY() + 52, 39, 16);

                        slashSound.playSoundIfOk();
                        break;
                        case 3:
                            AttackBB = new Rectangle((int)getPosX() + 66, (int)getPosY() + 50, 16, 4);
                            break;
                        default:
                            AttackBB = new Rectangle(0, 0, 0, 0);
                            break;
                    }
                    switch (Xframe)
                    {
                    case 0:
                        BodyBB = new Rectangle((int)getPosX() + 37, (int)getPosY() + 41, 24, 39);
                        break;
                    case 1:
                        BodyBB = new Rectangle((int)getPosX() + 36, (int)getPosY() + 46, 19, 34);
                        break;
                    case 2:
                        BodyBB = new Rectangle((int)getPosX() + 35, (int)getPosY() + 41, 30, 39);
                        break;
                    case 3:
                        BodyBB = new Rectangle((int)getPosX() + 35, (int)getPosY() + 41, 30, 39);
                        break;
                    case 4:
                        BodyBB = new Rectangle((int)getPosX() + 35, (int)getPosY() + 41, 28, 39);
                        break;
                    case 5:
                        BodyBB = new Rectangle((int)getPosX() + 35, (int)getPosY() + 41, 28, 39);
                        break;
                    default:
                        BodyBB = new Rectangle(0, 0, 0, 0);
                        break;
                }
            }
            
        }

        public void Hurt()
        {
            chaColor= Color.Orange;
            maxHitPoints = maxHitPoints - Damage;

            Vector2[] seq = new Vector2[3];
            seq[0].X = 7; seq[0].Y = 0;
            seq[1].X = 8; seq[1].Y = 0;
            seq[2].X = 9; seq[2].Y = 0;
            setAnimationSequence(seq, 0, 2, 10);
            AttackBB = new Rectangle(0, 0, 0, 0);
            hurtSound.playSoundIfOk();
        }

        public void Jump()
        {
            chaColor= Color.White;

            Vector2[] seq = new Vector2[5];
            seq[0].X = 0; seq[0].Y = 1;
            seq[1].X = 1; seq[1].Y = 1;
            seq[2].X = 2; seq[2].Y = 1;
            seq[3].X = 3; seq[3].Y = 1;
            seq[4].X = 4; seq[4].Y = 1;
            setAnimationSequence(seq, 0, 4, 10);

            AttackBB = new Rectangle(0, 0, 0, 0);

            if (FacingRight)
            {
                    switch (Xframe)
                    {
                        case 0:
                            BodyBB = new Rectangle((int)getPosX() + 35, (int)getPosY() + 40, 26, 40);
                            break;
                        case 1:
                            BodyBB = new Rectangle((int)getPosX() + 34, (int)getPosY() + 28, 27, 41);
                            break;
                        case 2:
                            BodyBB = new Rectangle((int)getPosX() + 33, (int)getPosY() + 19, 30, 32);
                            break;
                        case 3:
                            BodyBB = new Rectangle((int)getPosX() + 35, (int)getPosY() + 18, 30, 27);
                            break;
                        case 4:
                            BodyBB = new Rectangle((int)getPosX() + 28, (int)getPosY() + 22, 38, 41);
                            break;
                        default:
                            BodyBB = new Rectangle(0, 0, 0, 0);
                            break;
                    }
               
            }
            else
            {
                    switch (Xframe)
                    {
                        case 0:
                            BodyBB = new Rectangle((int)getPosX() + 35, (int)getPosY() + 40, 26, 40);
                            break;
                        case 1:
                            BodyBB = new Rectangle((int)getPosX() + 35, (int)getPosY() + 28, 27, 41);
                            break;
                        case 2:
                            BodyBB = new Rectangle((int)getPosX() + 33, (int)getPosY() + 19, 30, 32);
                            break;
                        case 3:
                            BodyBB = new Rectangle((int)getPosX() + 31, (int)getPosY() + 18, 30, 27);
                            break;
                        case 4:
                            BodyBB = new Rectangle((int)getPosX() + 30, (int)getPosY() + 22, 38, 41);
                            break;
                        default:
                            BodyBB = new Rectangle(0, 0, 0, 0);
                            break;
                    }
            }
        }
        public void JumpAttack()
        {
            chaColor= Color.White;
            Vector2[] seq = new Vector2[6];
            seq[0].X = 5; seq[0].Y = 1;
            seq[1].X = 6; seq[1].Y = 1;
            seq[2].X = 7; seq[2].Y = 1;
            seq[3].X = 8; seq[3].Y = 1;
            seq[4].X = 9; seq[4].Y = 1;
            seq[5].X = 10; seq[5].Y = 1;
            setAnimationSequence(seq, 0, 5, 10);

            if (!FacingRight)
            {
               switch (Xframe)
                    {
                        case 9:
                            AttackBB = new Rectangle((int)getPosX() + 8, (int)getPosY(), 40, 39);
                            slashSound.playSoundIfOk();
                            break;
                        default:
                            AttackBB = new Rectangle(0, 0, 0, 0);
                            break;
                    }
                switch (Xframe)
                {
                    case 5:
                        BodyBB = new Rectangle((int)getPosX() + 35, (int)getPosY() + 40, 26, 40);
                        break;
                    case 6:
                        BodyBB = new Rectangle((int)getPosX() + 28, (int)getPosY() + 28, 27, 41);
                        break;
                    case 2:
                        BodyBB = new Rectangle((int)getPosX() + 33, (int)getPosY() + 19, 30, 32);
                        break;
                    case 8:
                        BodyBB = new Rectangle((int)getPosX() + 35, (int)getPosY() + 18, 30, 27);
                        break;
                    case 9:
                        BodyBB = new Rectangle((int)getPosX() + 28, (int)getPosY() + 22, 38, 41);
                        break;
                    case 10:
                        BodyBB = new Rectangle((int)getPosX() + 28, (int)getPosY() + 22, 38, 41);
                        break;
                    default:
                        BodyBB = new Rectangle(0, 0, 0, 0);
                        break;
                }
            }
            else
            {
                switch (Xframe)
                    {
                        case 9:
                            AttackBB = new Rectangle((int)getPosX() + 48, (int)getPosY(), 40, 39);

                            slashSound.playSoundIfOk();
                             break;
                        default:
                            AttackBB = new Rectangle(0, 0, 0, 0);
                            break;
                    }
                switch (Xframe)
                {
                    case 5:
                        BodyBB = new Rectangle((int)getPosX() + 35, (int)getPosY() + 40, 26, 40);
                        break;
                    case 6:
                        BodyBB = new Rectangle((int)getPosX() + 28, (int)getPosY() + 28, 27, 41);
                        break;
                    case 2:
                        BodyBB = new Rectangle((int)getPosX() + 33, (int)getPosY() + 19, 30, 32);
                        break;
                    case 8:
                        BodyBB = new Rectangle((int)getPosX() + 35, (int)getPosY() + 18, 30, 27);
                        break;
                    case 9:
                        BodyBB = new Rectangle((int)getPosX() + 28, (int)getPosY() + 22, 38, 41);
                        break;
                    case 10:
                        BodyBB = new Rectangle((int)getPosX() + 28, (int)getPosY() + 22, 38, 41);
                        break;
                    default:
                        BodyBB = new Rectangle(0, 0, 0, 0);
                        break;
                }
            }
        }
        public void Crouch()
        {
            chaColor= Color.White;
            Vector2[] seq = new Vector2[3];
            seq[0].X = 5; seq[0].Y = 0;
            seq[1].X = 6; seq[1].Y = 0;
            seq[2].X = 7; seq[2].Y = 0;
            setAnimationSequence(seq, 0, 2, 10);

            AttackBB = new Rectangle(0, 0, 0, 0);
        }
        public void Crouch2()
        {
            chaColor = Color.White;
            Vector2[] seq = new Vector2[1];
            seq[0].X = 6; seq[0].Y = 0;
            setAnimationSequence(seq, 0, 0, 10);

            AttackBB = new Rectangle(0, 0, 0, 0);

            if (FacingRight)
            {
                BodyBB = new Rectangle((int)getPosX() + 35, (int)getPosY() + 52, 26, 28);
            }
            else
            {
                BodyBB = new Rectangle((int)getPosX() + 35, (int)getPosY() + 52, 26, 28);
            }
        }
        public void IncreaseHealth()
        {
            if (maxHitPoints < 90)
                maxHitPoints = maxHitPoints + 10;
            else
                maxHitPoints = 100;
        }

        public void changeSide(bool Side)
        {
            FacingRight = Side;
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------
        // -------------------------------------------------------------------------DRAW-------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public override void Draw(SpriteBatch spriteBatch)
        {
            if(FacingRight)
                spriteBatch.Draw(getTextureBase(), new Rectangle((int)getPosX(), (int)getPosY(), XframeWidth, YframeHeight), new Rectangle(Xframe * XframeWidth, Yframe * YframeHeight, XframeWidth, YframeHeight), chaColor);
            else if(!FacingRight)
                spriteBatch.Draw(getTextureBase(), new Rectangle((int)getPosX(), (int)getPosY(), XframeWidth, YframeHeight), new Rectangle(Xframe * XframeWidth, Yframe * YframeHeight, XframeWidth, YframeHeight), chaColor, 0, new Vector2(0,0),SpriteEffects.FlipHorizontally,0);

            HP.Draw(spriteBatch);
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //-------------------------------------------------------------------------UPDATE---------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public override void Update(GameTime gameTime)
        {
            animationTick(gameTime);
            ticks++;

            BodyBB = new Rectangle(0, 0, 0, 0);
            AttackBB = new Rectangle(0, 0, 0, 0);

            prevK = kbs;
            kbs = Keyboard.GetState();

            if (!beingAttacked)
            {
                if (kbs.IsKeyUp(Keys.Left) && kbs.IsKeyUp(Keys.Right) && kbs.IsKeyUp(Keys.V) && kbs.IsKeyUp(Keys.Space))
                    Idle();

                if (kbs.IsKeyDown(Keys.Left))
                {
                    changeSide(false);

                    if (kbs.IsKeyDown(Keys.V) && kbs.IsKeyUp(Keys.C) && kbs.IsKeyUp(Keys.Space))
                    {
                        Attack();
                    }
                    else if (kbs.IsKeyDown(Keys.C) && kbs.IsKeyUp(Keys.V) && kbs.IsKeyUp(Keys.Space))
                    {
                        Run();
                    }
                    else if (kbs.IsKeyDown(Keys.Space) && kbs.IsKeyUp(Keys.V) && kbs.IsKeyUp(Keys.C))
                    {
                        Jump();
                        Move();
                    }
                    else if (kbs.IsKeyDown(Keys.Space) && kbs.IsKeyDown(Keys.V) && kbs.IsKeyUp(Keys.C))
                    {
                        JumpAttack();
                        Move();
                    }
                    else if (kbs.IsKeyDown(Keys.C) && kbs.IsKeyDown(Keys.V) && kbs.IsKeyUp(Keys.Space))
                    {
                        Run();
                    }
                    else if (kbs.IsKeyDown(Keys.Space) && kbs.IsKeyDown(Keys.C) && kbs.IsKeyUp(Keys.V))
                    {
                        Jump();
                        Move();
                    }
                    else if (kbs.IsKeyDown(Keys.Space) && kbs.IsKeyDown(Keys.C) && kbs.IsKeyDown(Keys.Space))
                    {
                        JumpAttack();
                        Move();
                    }
                    else
                        Walk();
                    Road_travel = Road_travel + Road_Speed;
                }
                else if (kbs.IsKeyDown(Keys.Right))
                {
                    changeSide(true);
                    if (kbs.IsKeyDown(Keys.V) && kbs.IsKeyUp(Keys.C) && kbs.IsKeyUp(Keys.Space))
                    {
                        Attack();
                    }
                    else if (kbs.IsKeyDown(Keys.C) && kbs.IsKeyUp(Keys.V) && kbs.IsKeyUp(Keys.Space))
                    {
                        Run();
                    }
                    else if (kbs.IsKeyDown(Keys.Space) && kbs.IsKeyUp(Keys.V) && kbs.IsKeyUp(Keys.C))
                    {
                        Jump();
                        Move();
                    }
                    else if (kbs.IsKeyDown(Keys.Space) && kbs.IsKeyDown(Keys.V) && kbs.IsKeyUp(Keys.C))
                    {
                        JumpAttack();
                        Move();
                    }
                    else if (kbs.IsKeyDown(Keys.C) && kbs.IsKeyDown(Keys.V) && kbs.IsKeyUp(Keys.Space))
                    {
                        Run();
                    }
                    else if (kbs.IsKeyDown(Keys.Space) && kbs.IsKeyDown(Keys.C) && kbs.IsKeyUp(Keys.V))
                    {
                        Jump();
                        Move();
                    }
                    else if (kbs.IsKeyDown(Keys.Space) && kbs.IsKeyDown(Keys.C) && kbs.IsKeyDown(Keys.Space))
                    {
                        JumpAttack();
                        Move();
                    }
                    else
                        Walk();

                    Road_travel = Road_travel + Road_Speed;
                }
                else
                {
                    if (kbs.IsKeyDown(Keys.Down) && !isCrouching)
                    {
                        Crouch();
                        isCrouching = true;
                    }
                    else if (isCrouching && kbs.IsKeyDown(Keys.Down))
                    {
                        Crouch2();
                    }
                    if( kbs.IsKeyUp(Keys.Down))
                    {
                        isCrouching = false;
                    }
                    // Attacking
                    if (kbs.IsKeyDown(Keys.V))
                    {
                        Attack();
                    }

                    if (kbs.IsKeyDown(Keys.Space))
                    {
                        if (kbs.IsKeyUp(Keys.V))
                        {
                            Jump();
                        }
                        else if (kbs.IsKeyDown(Keys.V))
                        {
                            JumpAttack();
                        }
                        else
                        {
                            Jump();
                        }
                    }
                }
            }
            else
            {
               Hurt();
               beingAttacked = false;
               Damage = 1;
            }


            HP.Update(gameTime);

        }

        /// <summary>
        /// INTRO  
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="KbState"></param>
        public void UpdateIntro(GameTime gameTime, KeyboardState KbState)
        {
            animationTick(gameTime);
            ticks++;
            
            if (KbState.IsKeyDown(Keys.Space) && prevK.IsKeyUp(Keys.Space))
            {
                Attack();
            }
            else
                WalkStill();

            prevK = KbState;
        }

        public void UpdateLevel(GameTime gameTime)
        {
            animationTick(gameTime);
            ticks++;

            Walk();
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------
        // -------------------------------------------------------------------------SETTING AND GETTING-------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void setCanGo(bool value)
        {
            canGoRight = value;
        }
        public void setFarRight(int right)
        {
            farRight = right;
        }
        public void setScreenWidthHeight(int Width, int Height)
        {
            SCREEN_WIDTH = Width;
            SCREEN_HEIGHT = Height;
        }
        
        public float getCenterX()
        {
            float centerX = BodyBB.X + BodyBB.Width/2;
            return centerX;
        }
        public float getCenterY()
        {
            float centerY = BodyBB.Y + BodyBB.Height / 2;
            return centerY;
        }
        public Point getCenter()
        {
            Point center = new Point((int)getCenterX(), (int)getCenterY());

            return center;
        }
        public int getHealth()
        {
            return maxHitPoints;
        }

        public Rectangle getBodyBB()
        {
            return BodyBB;
        }
        public Rectangle getAttackBB()
        {
            return AttackBB;
        }
        public void checkHealth()
        {
        }
        public bool checkBeingAttacked(Rectangle a)
        {
            if (BodyBB.Intersects(a))
                beingAttacked = true;
            else
                beingAttacked = false;

            return beingAttacked;
        }
        public bool checkAttacking(Rectangle a)
        {
            if (AttackBB.Intersects(a) && !BodyBB.Intersects(a))

                return true;
            else
                return false;
        }
        public void setBeingAttacked(bool value)
        {
            beingAttacked = true;
        }
        public void setDamage(int value)
        {
            Damage = value;
        }
        public int getRoadTravel()
        {
            return Road_travel;
        }
        public int getUniSpeed()
        {
            return Road_Speed;
        }
        public int getXspeed()
        {
            return XSpeed;
        }
        public void setFacing(bool value)
        {
            FacingRight = value;
        }
        public void setHurtSound(SoundEffect sound)
        {
            hurt = sound;
        }
        public void setSlashSound(SoundEffect sound)
        {
            slash = sound;
        }
        public void setSound()
        {
            hurtSound = new LimitSound(hurt, 2);
            slashSound = new LimitSound(slash,2);
        }
    }
}
