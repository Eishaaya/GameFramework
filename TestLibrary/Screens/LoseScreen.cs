using BaseGameLibrary.Inputs;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

using static BaseGameLibrary.ActionButton;

namespace BaseGameLibrary
{
    class LoseScreen : Screen
    {
        Button menu;
        Button back;
        Sprite tint;
        Sprite lose;
        Label score;
        List<int> scores;
        List<Label> topScores;
        SpriteFont font;
        public LoseScreen(Sprite dark, Sprite Loser, Button menuButt, Button RestartButt, SpriteFont Font)
            : base()
        {
            tint = dark;
            lose = Loser;
            menu = menuButt;
            back = RestartButt;
            font = Font;
        }

        public override void Transfer (int gamescore)
        {
            score = new Label(font, Color.LightGray, new Vector2(240, 285), $"Your Score: {gamescore}");

            //Read
            topScores = new List<Label>();
            scores = new List<int>();
            var dataJSON = File.ReadAllText("data.json");
            scores = JsonSerializer.Deserialize<List<int>>(dataJSON);
            scores.Add(gamescore);
            scores.Sort();
            scores.Reverse();
            for (int e = 0; e < 2; e++)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (i + e * 5 >= scores.Count)
                    {
                        for (int j = i; j < 5; j++)
                        {
                            topScores.Add(new Label(font, Color.LightGray, new Vector2(170 + 130 * e, 320 + j * 30), $"#{j + 1 + e * 5} Score: 0"));
                        }
                        break;
                    }
                    topScores.Add(new Label(font, Color.LightGray, new Vector2(170 + 130 * e, 320 + i * 30), $"#{i + 1 + e * 5} Score: {scores[i + e * 5]}"));
                    if (scores[i + e * 5] == gamescore)
                    {
                        topScores[i + e * 5].Color = Color.Yellow;
                    }
                }
            }

            var stuffToWrite = JsonSerializer.Serialize(scores);
            File.WriteAllText("data.json", stuffToWrite);
        }

        public override void Update(GameTime time, Screenmanager manny, CursorRoot cursor)
        {
            base.Update(time, manny, cursor);
            if (heldMouse)
            {
                return;
            }
            if (menu.Check(mousy.Position.ToVector2(), mouseClicks[(int)ClickType.Left]))
            {
                manny.Next(0, true);
                manny.PreviousScreens.Pop();
                manny.PreviousScreens.Pop().Reset();
                manny.ClearMemory();
                return;
            }
            if (back.Check(mousy.Position.ToVector2(), mouseClicks[(int)ClickType.Left]))
            {
                manny.Back();
            }
        }

        public override void Draw(SpriteBatch batch)
        {
            tint.Draw(batch);
            lose.Draw(batch);
            back.Draw(batch);
            menu.Draw(batch);
            score.Draw(batch);
            for (int i = 0; i < topScores.Count; i++)
            {
                topScores[i].Draw(batch);
            }    
        }
    }
}
