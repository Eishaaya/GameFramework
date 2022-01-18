using Microsoft.Xna.Framework;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using static BaseGameLibrary.VisualObject;
using Microsoft.Xna.Framework.Graphics;

namespace BaseGameLibrary
{
    public static class Extensions
    {

        public static Random random = new Random();


        //Wrapping for sequence
        public static bool ChangeColor(VisualObject me, Color newColor, float sped = .1f) => me.ChangeColor(newColor, sped);
        public static bool FadeTo(VisualObject me, ColorNums colorChoice) => me.FadeTo(colorChoice);
        public static bool Pulsate(VisualObject me, int size, float sped, bool rando) => me.Pulsate(size, sped, rando);
        public static bool Vibrate(VisualObject me, int distance, float sped, bool rando) => me.Vibrate(distance, sped, rando);
        public static bool Rotate(VisualObject me, float target, float sped, bool rando) => me.Rotate(target, sped, rando);
        public static bool Fade(VisualObject me, int speed) => me.Fade(speed);
        public static bool Fade(VisualObject me, Color tint, int speed) => me.Fade(tint, speed);
        public static bool Wait(Timer timer, GameTime time)
        {
            timer.Tick(time);
            return timer.Ready();
        }

        public static Vector2 GetDimensions(this Label label) => label.Font.MeasureString(label.Text());

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

        public static void ColorPoints(this List<Sprite> points, Color newColor)
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

        public static int RunSequence(ref int current, params IParamFunc<bool>[] funcs)
        {
            if (current >= funcs.Length)
            {
                current = 0;
            }
            current += funcs[current].Call() ? 1 : 0;
            return current;
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

        public static Vector2 ConvertPos(this Vector2 location, Vector2 scale, Vector2 offset)
        {
            return (location - offset) / scale;
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
