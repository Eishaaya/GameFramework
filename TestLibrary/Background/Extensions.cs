using Microsoft.Xna.Framework;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using BaseGameLibrary.Visual;
using static BaseGameLibrary.Visual.VisualObject;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BaseGameLibrary
{
    public static partial class Extensions
    {

        public static Random random = new();


        //Wrapping for sequence
        public static bool ChangeColor(VisualObject me, Color newColor, float sped = .1f) => me.ChangeColor(newColor, sped);
        public static bool FadeTo(VisualObject me, ColorNums colorChoice) => me.FadeTo(colorChoice);
        public static bool Pulsate(VisualObject me, int size, float sped, bool rando) => me.Pulsate(size, sped, rando);
        public static bool Vibrate(VisualObject me, int distance, float sped, bool rando) => me.Vibrate(distance, sped, rando);
        public static bool Rotate(VisualObject me, float target, float sped, bool rando) => me.Rotate(target, sped, rando);
        public static bool Fade(VisualObject me, int speed) => me.Fade(speed);
        public static bool Fade(VisualObject me, Color tint, int speed) => me.Fade(tint, speed);
        //public static bool Wait(Sequence waiter, int time)
        //{
        //    return waiter.Wait(time);
        //}

        public static Vector2 GetDimensions(this LabelBase label) => label.Font.MeasureString(label.Text());

        public static int Previous(this Random random, int min, int max)
        {
            switch (random.Next(0, 2))
            {
                case 0:
                    return random.Next(int.MinValue, min);
                default:
                    return random.Next(max + 1, int.MaxValue);
            }
        }

        public static float Slope(Vector2 point1, Vector2 point2)
        {
            return (point2.Y - point1.Y) / (point2.X - point1.X);
        }

        public static float PointAt(this Vector2 start, Vector2 target)
        {
            var difference = target - start;
            return (float)Math.Atan2(difference.X, difference.Y);
        }

        public static ulong Factorial(this ulong number)
        {
            ulong result = 1;
            while (number > 1)
            {
                result *= number;
                number--;
            }
            return result;
        }

        public static double Factorial(this double number)
        {
            double result = 1;
            while (number > 1)
            {
                result *= number;
                number--;
            }
            return result;
        }

        public static void ColorPoints(this List<SpriteBase> points, Color newColor)
        {
            for (int i = 0; i < points.Count; i++)
            {
                var degree = (float)Math.Pow(i / (points.Count != 1 ? (float)points.Count - 1 : 1), 1.75);
                points[i].Color = Color.Lerp(points[i].OriginalColor, newColor, degree);
            }
        }

        public static float AddTill(this float number, float endCondition, float amount)
        {
            return number + (float)(int)((endCondition - number) / amount + .99f) * amount;
        }
        #region there has to be better

        public static KeyValuePair<int, Setting> WithID(this Setting setting)
        {
            return new KeyValuePair<int, Setting>(setting.ID, setting);
        }

        public static KeyValuePair<int, Setting>[] WithIDs(this IEnumerable<Setting> settings)
        {
            var usefulSettings = settings.ToArray();
            var returner = new KeyValuePair<int, Setting>[usefulSettings.Length];

            for (int i = 0; i < usefulSettings.Length; i++)
            {
                returner[i] = usefulSettings[i].WithID();
            }

            return returner;
        }

        public static IEnumerable<Setting> Set(this IEnumerable<Setting> settings, IEnumerable<int> newSettings)
        {
            var useful = newSettings.ToArray();
            var returner = (Setting[])settings.ToArray().Clone();

            for (int i = 0; i < useful.Length; i++)
            {
                returner[i] = new Setting(returner[i].ID, Setting.Types.IntValue, useful[i]);
            }

            return returner;
        }

        public static IEnumerable<Setting> Set(this IEnumerable<Setting> settings, IEnumerable<bool> newSettings)
        {
            var useful = newSettings.ToArray();
            var returner = (Setting[])settings.ToArray().Clone();

            for (int i = 0; i < useful.Length; i++)
            {
                returner[i] = new Setting(returner[i].ID, Setting.Types.BoolValue, useful[i]);
            }

            return returner;
        }

        public static IEnumerable<Setting> Set(this IEnumerable<Setting> settings, IEnumerable<float> newSettings)
        {
            var useful = newSettings.ToArray();
            var returner = (Setting[])settings.ToArray().Clone();

            for (int i = 0; i < useful.Length; i++)
            {
                returner[i] = new Setting(returner[i].ID, Setting.Types.FloatValue, useful[i]);
            }

            return returner;
        }

        public static IEnumerable<Setting> Set(this IEnumerable<Setting> settings, IEnumerable<Keys> newSettings)
        {
            var useful = newSettings.ToArray();
            var returner = (Setting[])settings.ToArray().Clone();

            for (int i = 0; i < useful.Length; i++)
            {
                returner[i] = new Setting(returner[i].ID, Setting.Types.KeyValue, useful[i]);
            }

            return returner;
        }
    #endregion
        public static float LoopCalc(this float current, float target, float max, float min = 0)
        {
            current %= max - min;
            target %= max - min;
            current.AddTill(min, max - min);
            target.AddTill(min, max - min);

            float result;

            if (current < target)
            {
                result = target - current;
                var alt = current + max - min - target;
                result = result < alt ? result : alt * -1;
            }
            else
            {
                result = current - target;
                var alt = current - max + min - target;
                result = result < alt * 1 ? result * -1 : alt;
            }

            return result;
        }



        public static double[] AllAddBy(this double[] numbers, double addend)
        {
            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] += addend;
            }
            return numbers;
        }

        public static double[] InvertAgainst(this double[] numbers, double number)
        {
            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = number - numbers[i];
            }
            return numbers;
        }

        public static IEnumerable<VisualObject> ShallowClone(this IEnumerable<VisualObject> me)
        {
            var newMe = new VisualObject[me.Count()];
            for (int i = 0; i < newMe.Length; i++)
            {
                newMe[i] = me.ElementAt(i).Clone();
            }
            return newMe;
        }

        public static Vector2 ConvertPos(this Vector2 location, Vector2 scale, Vector2 offset)
        {
            return (location - offset) / scale;
        }

        //make generic cloning
        public static IEnumerable<ButtonBase> SpawnInGrid (Vector2 startLocation, float xDist, float yDist, int xCount, int yCount, ButtonBase template)
        {
            var bindButtons = new ButtonBase[xCount * yCount];
            for (int x = 0; x < xCount; x++)
            {
                for (int y = 0; y < yCount; y++)
                {
                    var temp = template.Clone();
                    bindButtons[x * y] = temp;
                    temp.Location = new Vector2(xDist * x, yDist * y) + startLocation;
                }
            }
            //for (int i = count / 2; i < count; i++)
            //{
            //    //bindLabels.Add(new ValueLabel(font, Color.White, new Vector2(350, (i - defaults.Count / 2) * 50 + 250), $"{keyTypes[i]} : {Settings[i]}", $"{keyTypes[i]} : "));
            //    bindButtons.Add(new Button(b, new Vector2(350, (i - defaults.Count / 2) * 50 + 250), Color.Black, 0, SpriteEffects.None, new Vector2(0, 0), 1, .1f, Color.DarkGray, Color.Gray));
            //}

            return bindButtons.AsEnumerable();
        }

        public static IEnumerable<ButtonBase> SpawnInGrid(Vector2 startLocation, float xDist, float yDist, int xCount, int yCount, IEnumerable<ButtonBase> buttons)
        {
            var arrayButtons = buttons.ToArray();
            for (int x = 0; x < xCount; x++)
            {
                for (int y = 0; y < yCount; y++)
                {
                    arrayButtons[x * y].Location = new Vector2(xDist * x, yDist * y) + startLocation;
                }
            }
            //for (int i = count / 2; i < count; i++)
            //{
            //    //bindLabels.Add(new ValueLabel(font, Color.White, new Vector2(350, (i - defaults.Count / 2) * 50 + 250), $"{keyTypes[i]} : {Settings[i]}", $"{keyTypes[i]} : "));
            //    bindButtons.Add(new Button(b, new Vector2(350, (i - defaults.Count / 2) * 50 + 250), Color.Black, 0, SpriteEffects.None, new Vector2(0, 0), 1, .1f, Color.DarkGray, Color.Gray));
            //}

            return arrayButtons.AsEnumerable();
        }

        #region behaviors

        public static Vector2 Bounds(this Game game)
        {
            return new Vector2(game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height);
        }
        public static Rectangle BoundingBox(this Game game)
        {
            return new Rectangle(game.GraphicsDevice.Viewport.X, game.GraphicsDevice.Viewport.Y, game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height);
        }

        // OLD ENEMY CODE TOO SPECIFIC

        //public static void Idle(this Enemy enemy)
        //{

        //}

        //public static void Move(this Enemy enemy, Vector2 targetLocation, Enemy.MoveType type, Game game, params int[] moveNumber)
        //{
        //    var bounds = game.Bounds();

        //    if (enemy.CurrentState != Enemy.EnemyState.Attacking && enemy.CurrentState != Enemy.EnemyState.Dying)
        //    {
        //        enemy.CurrentState = Enemy.EnemyState.Moving;
        //    }


        //    switch (type)
        //    {
        //        case Enemy.MoveType.Swoop:

        //            break;
        //        case Enemy.MoveType.Teleport:
        //            break;
        //        case Enemy.MoveType.Zigzag:
        //            break;
        //        case Enemy.MoveType.Charge:
        //            break;
        //        default:
        //            break;
        //    }
        //}

        #endregion
    }
}
