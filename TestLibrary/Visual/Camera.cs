using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseGameLibrary
{
    public interface IFollowable
    {
        Vector2 Position { get; }
    }

    public class Camera
    {
        public BasicEffect Effect;
        private IFollowable focus;
        public float zoom = 1f;

        public Camera(IFollowable focus, Rectangle bounds, GraphicsDevice graphicsDevice)
        {
            this.focus = focus;
            Effect = new BasicEffect(graphicsDevice);
            Effect.TextureEnabled = true;
            Effect.VertexColorEnabled = true;
            Effect.Projection = Matrix.CreateOrthographic(bounds.Width, bounds.Height, 1, 10);
            Effect.World = Matrix.Identity;

            Update();
        }
        public void Update()
        {
            Effect.View = Matrix.CreateLookAt(new Vector3(focus.Position, -9), new Vector3(focus.Position, 0), Vector3.Down);
            Effect.View *= Matrix.CreateScale(zoom);
        }
    }
}
