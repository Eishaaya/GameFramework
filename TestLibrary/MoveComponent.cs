using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseGameLibrary
{
    public class MoveComponent
    {
        public enum MoveState
        {
            Stalled,
            Forwards,
            Backwards
        };
        public MoveState CurrentState { get; set; }
        public Vector2 Momentum { get; private set; }
        public Vector2 Force { get; set; }
        public float Speed { get; private set; }
        float lerpSpeed;

        public MoveComponent (float speed, float lSpeed)
        {
            CurrentState = MoveState.Stalled;
            Momentum = Vector2.Zero;
            Force = Vector2.Zero;
            Speed = speed;
            lerpSpeed = lSpeed;
        }

        public void SetSpeed (float rotation)
        {
            if (CurrentState == MoveState.Forwards)
            {
                Force = new Vector2(Speed * (float)Math.Sin((double)rotation), Speed * -(float)Math.Cos((double)rotation));
            }
            else if (CurrentState == MoveState.Backwards)
            {
                Force = new Vector2(Speed * (float)Math.Sin((double)rotation), Speed * (float)Math.Cos((double)rotation));
            }
        }
        public void Update()
        {
             Momentum = Vector2.LerpPrecise(Momentum, Force, lerpSpeed);
        }
    }
}
