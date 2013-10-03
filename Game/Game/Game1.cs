using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using System.IO;

namespace WinGame
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch,spriteComponentBatch;
        private Texture2D kart,txt;
        SpriteFont tr;   //шрифт


        Vector2[] position_kart;
        Kartinki[] kartin;

        Vector2[] position_text;
        Kartinki[] text;

        MouseState mState, old_mState;

        Random r = new Random();

        int nom_kart=-1, nom_text=-1,vibor_kar=-1,vibor_text=-1;

        int maxX,Xkol,dX,kol_kar;

        int[] otvet;
        int[] sootvetstvie;
        int[,] vibrani;

        int rezalt=-1;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }


        protected override void Initialize()
        {
            maxX=1350;
            Xkol = 280;
            kol_kar = 4;
            
            graphics.PreferredBackBufferWidth = 1350;            //ширина
            graphics.PreferredBackBufferHeight = 700;           //высота
            graphics.ApplyChanges();

            

            kartin = new Kartinki[4];
            position_kart = new Vector2[4];
            text = new Kartinki[4];
            position_text = new Vector2[4];

            
            otvet = new int[4];
            for(int i=0;i<4;i++)
                otvet[i]=-1;
            vibrani = new int[4, 2];
            for (int i = 0; i < 4; i++)
            {
                vibrani[i, 0] = 0;
                vibrani[i, 1] = 0;
            }

                this.IsMouseVisible = true;

            tr = Content.Load<SpriteFont>("segoe_print");
            base.Initialize();
        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteComponentBatch = new SpriteBatch(GraphicsDevice);
            Services.AddService(typeof(SpriteBatch), spriteComponentBatch);

            kart = Content.Load<Texture2D>("1234");
            txt = Content.Load<Texture2D>("text12");

            AddKartinki(4);
        }

        void AddKartinki(int kol)
        {
            int k = 0;
            int x = 5;
            for (int i = 0; i < 4; i++)
            {
                position_kart[i].X = -1;
                position_text[i].X = -1;
            }
            
            dX=(maxX - Xkol*kol)/(kol+1);

            for (int i = 0; i < 4; )
            {
                k = r.Next(4);

                if (position_kart[k].X == -1)
                {
                    position_kart[k].Y = 370;
                    position_kart[k].X = dX*(i+1)+Xkol*i;
                    i++;
                }
            }
            for (int i = 0; i < 4; i++)
            {

                kartin[i] = new Kartinki(this,ref kart,new Rectangle(0 , i*222 , 279, 222),position_kart[i]);
                Components.Add(kartin[i]);
            }
            for (int i = 0; i < 4; )
            {
                k = r.Next(4);

                if (position_text[k].X == -1)
                {
                    position_text[k].Y = 130;
                    position_text[k].X = dX*(i+1)+Xkol*i;                    
                    i++;
                }
                
            }
            for (int i = 0; i < 4; i++)
            {
                text[i] = new Kartinki(this, ref txt, new Rectangle(0, i * 90, 278, 90), position_text[i]);
                Components.Add(text[i]);
            }
            

        }


        protected override void UnloadContent()
        {
            kart.Dispose();
            //knopki.Dispose();
            txt.Dispose();
            for (int i = 0; i < 4; i++)
            {
                kartin[i].Dispose();
                text[i].Dispose();
            }

            spriteBatch.Dispose();
            spriteComponentBatch.Dispose();
        }

        int vibrana_kartinka(int x, int y)
        {
            for (int i = 0; i < 4; i++)
            {
                if(otvet[i]==-1)
                if (x > kartin[i].sprPosition.X && x < (kartin[i].sprPosition.X + kartin[i].sprRectangle.Width))
                    if (y > kartin[i].sprPosition.Y && y < (kartin[i].sprPosition.Y + kartin[i].sprRectangle.Height))
                        return i;
            }
            return -1;
        }

        int vibran_text(int x, int y)
        {
            for (int i = 0; i < 4; i++)
            {
                if (x > text[i].sprPosition.X && x < (text[i].sprPosition.X + text[i].sprRectangle.Width))
                    if (y > text[i].sprPosition.Y && y < (text[i].sprPosition.Y + text[i].sprRectangle.Height))
                        return i;
            }
            return -1;
        }

        int proverka()
        {
            int otv=0;
            for (int i = 0; i < 4; i++)
            {
                if (otvet[i] == i)
                    otv++;
                else if (otvet[i] == -1)
                    return -1;
            }
            return otv;
        }

        void smena_position(int kol)
        {
            int k;
            if (kol > 0)
            {
                dX = (maxX - Xkol * kol) / (kol + 1);

                for (int i = 0; i < 4; i++)
                {
                    position_kart[i].X = -1;
                }

                for (int i = 0; i < kol; )
                {
                    k = r.Next(kol);

                    if (position_kart[k].X == -1)
                    {
                        position_kart[k].X = dX * (i + 1) + Xkol * i;
                        i++;
                    }
                }
                int tek_kar = 0;
                for (int i = 0; i < 4; i++)
                {
                    if (otvet[i] == -1)
                    {
                        kartin[i].sprPosition.X = position_kart[tek_kar].X;
                        tek_kar++;
                    }

                }
            }
        }

        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            old_mState = mState;            
            mState = Mouse.GetState(); //получаю текущий статус мышки
            if (mState.LeftButton == ButtonState.Pressed && old_mState.LeftButton == ButtonState.Released) //если нажата левая клавиша
            {
                
                vibor_text = vibran_text(mState.X, mState.Y);
                if (vibor_text != -1)
                {
                    nom_text = vibor_text;
                }
            }
            if (nom_text != -1)
            {
                text[nom_text].Update_1(gameTime,mState.X-old_mState.X,mState.Y-old_mState.Y);
            }
            if (mState.LeftButton == ButtonState.Released && old_mState.LeftButton == ButtonState.Pressed) //если нажата левая клавиша
            {
                               
                vibor_kar = vibrana_kartinka(mState.X, mState.Y);
                if (vibor_kar != -1 && nom_text!=-1)
                {
                    otvet[vibor_kar] = nom_text;
                    text[nom_text].Dispose();
                    kartin[vibor_kar].Dispose();
                    kol_kar--;
                    smena_position(kol_kar);

                }
                nom_text = -1;
                vibor_kar = -1;
                rezalt=proverka();
                               
            }


            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Bisque);

            spriteBatch.Begin();
            if(rezalt==4) spriteBatch.DrawString(tr, "Правильно!", new Vector2(350, 250), Color.SaddleBrown);
            if (rezalt == 3) spriteBatch.DrawString(tr, "Верно на 75%!", new Vector2(350, 250), Color.SaddleBrown);
            if (rezalt == 2) spriteBatch.DrawString(tr, "Верно на 50%!", new Vector2(350, 250), Color.SaddleBrown);
            if (rezalt == 1) spriteBatch.DrawString(tr, "Верно на 25%!", new Vector2(350, 250), Color.SaddleBrown);
            if (rezalt == 0) spriteBatch.DrawString(tr, "Не верно!", new Vector2(350, 250), Color.SaddleBrown);
            spriteBatch.End();

            if (rezalt == -1)
            {
                spriteComponentBatch.Begin();
                base.Draw(gameTime);
                spriteComponentBatch.End();
            }
        }
    }
}
