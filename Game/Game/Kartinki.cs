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


namespace WinGame
{

    public class Kartinki : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private Texture2D sprTexture;       //для хранения текстуры
        public Rectangle sprRectangle;     //для хранения прямоугольника, показывающего координаты спрайта в текстуре
        public Vector2 sprPosition;        //для хранения позиции вывода спрайта на экран

        public Kartinki(Game game, ref Texture2D newTexture,
            Rectangle newRectangle, Vector2 newPosition)
            : base(game)
        {
            sprTexture = newTexture;
            sprRectangle = newRectangle;
            sprPosition = newPosition;
        }


        public override void Initialize()
        {
            base.Initialize();
        }

        public void Update_1(GameTime gameTime, float X, float Y)
        {
            sprPosition.X += X;
            sprPosition.Y += Y;
            base.Update(gameTime);
        }
        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sprBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
            sprBatch.Draw(sprTexture, sprPosition, sprRectangle, Color.White);

            base.Draw(gameTime);
        }
    }
}