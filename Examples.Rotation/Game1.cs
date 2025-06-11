using System.Collections.Generic;
using CameraControllerDemo;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Vector3Struct = Microsoft.Xna.Framework.Vector3;
using Vector3 = CameraControllerDemo.Vector3;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using System;

namespace Examples.Rotation
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private ModelModel _model;
        private ModelModel _modelRotateAround;
        private Camera _camera;
        private SpriteBatch _spriteBatch;

        private Matrix _projectionMatrix;
        private Matrix _viewMatrix;
        private Matrix _worldMatrix;

        private BasicEffect _basicEffect;

        private bool _orbit;
        private SpriteFont _spriteFont;
        const float ROTATE_DEGREE = 4.0f;

        /// <summary>
        /// Initializes a new instance of the <see cref="Game1"/> class.
        /// </summary>
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

            //_camera = new Camera(new Vector3(0, 16, 4), new Vector3(0, 0, 0));
            _camera = new Camera(new Vector3(0f, 0f, -100f), new Vector3(0f, 0f, 0f));
            float aspectRatio = GraphicsDevice.DisplayMode.AspectRatio;//16f / 9f;

            //_projectionMatrix = Matrix.CreatePerspectiveFieldOfView(
            //          fieldOfView: MathHelper.PiOver4,
            //          aspectRatio: aspectRatio,
            //          nearPlaneDistance: 1,
            //          farPlaneDistance: 400);

            _projectionMatrix = Matrix.CreatePerspectiveFieldOfView(
                fieldOfView: MathHelper.ToRadians(45f),
                aspectRatio: aspectRatio,
                nearPlaneDistance: 1f,
                farPlaneDistance: 1000f);

            //_viewMatrix = Matrix.CreateLookAt(
            //    cameraPosition: _camera.Position,
            //    cameraTarget: _camera.Target,
            //    Vector3.UnitZ);

            _viewMatrix = Matrix.CreateLookAt(
                cameraPosition: _camera.Position,
                cameraTarget: _camera.Target,
                new Vector3(0f, 1f, 0f));

            _worldMatrix = Matrix.CreateWorld(_camera.Target, Vector3.Forward, Vector3.Up);

            _basicEffect = new BasicEffect(GraphicsDevice);
            _basicEffect.Alpha = 1.0f;
            _basicEffect.VertexColorEnabled = true;
            _basicEffect.LightingEnabled = false;

            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        /// <summary>
        /// Loads the content.
        /// </summary>
        protected override void LoadContent()
        {
            _model = new ModelModel(Content.Load<Model>("box"));
            //_model.RotationMatrix = Matrix.CreateFromAxisAngle(_rotationMatrix.Forward, MathHelper.ToRadians(115.0f));
            _modelRotateAround = new ModelModel(Content.Load<Model>("box"), new Vector3(0, 6, 0));
            _spriteFont = Content.Load<SpriteFont>("baseFont");
        }

        /// <summary>
        /// Updates the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        protected override void Update(GameTime gameTime)
        {
            InputManager.Update();

            int w = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width; //whole screen
            int h = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            var r = GraphicsDevice.PresentationParameters.Bounds;

            Rectangle rect = new Rectangle(0, 0, r.Width, r.Height);
            if (InputManager.Hover(rect) || true)
            {
                Vector2 normalizedMouseCursor = InputManager.LastMouseCoordsNormalized(rect);

                if (InputManager.LeftClicked)
                {
                    //Exit();
                }

                //Vector3 difference = _camera.Position - _camera.Target;
                //_camera.CameraRotationMatrix *= Matrix.CreateFromAxisAngle(_camera.Target, MathHelper.ToRadians(1.0f));

                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    _camera.Position.X -= 1f;
                    _camera.Target.X -= 1f;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    _camera.Position.X += 1f;
                    _camera.Target.X += 1f;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    _camera.Position.Y -= 1f;
                    _camera.Target.Y -= 1f;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    _camera.Position.Y += 1f;
                    _camera.Target.Y += 1f;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.OemPlus))
                {
                    _camera.Position.Z += 1f;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.OemMinus))
                {
                    _camera.Position.Z -= 1f;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    _orbit = !_orbit;
                }

                if (_orbit)
                {
                    Matrix rotationMatrix = Matrix.CreateRotationY(MathHelper.ToRadians(1f));
                    //_camera.Position = Vector3.Transform(_camera.Position, rotationMatrix);

                    //Vector3 pos = _camera.Position;
                    //Vector3 dir = (_camera.Position - _camera.Target);
                    //dir.Normalize();
                    //float distance = Vector3.Distance(_camera.Position, _camera.Target);


                    //Matrix translationMatrix;
                    //if (distance > 200f)
                    //{
                    //    //pos = pos + (dir * -1f);
                    //    translationMatrix = Matrix.CreateTranslation(_camera.Position * -1f);

                    //}
                    //else
                    //{
                    //    translationMatrix = Matrix.CreateTranslation(_camera.Position * 1f);
                    //    //pos = pos + (dir * +1f);
                    //}

                    // https://stackoverflow.com/questions/42281226/rotation-matrix-causing-sprite-position-to-change
                    //A rotation matrix rotates around 0,0 and your rectangle is already placed in the world.
                    //To solve first subtract the rectangle's center from each vertex (translate the rectangle to be centered at 0,0)
                    //and then add it again after rotating (place again on original location). In the code im assuming Y goes from top to bottom:

                    //objectPosition = Vector3.Transform(ObjectPosition - objectToRotateAboutPosition, Matrix.CreateRotationX(angle)) + objectToRotateAboutPosition;
                    //https://gamedev.stackexchange.com/questions/51737/how-to-rotate-one-object-around-another-moving-object-in-3-d
                    //_camera.Position = Vector3.Transform(pos, rotationMatrix);
                    _camera.Position = Vector3.Transform(_camera.Position - _camera.Target, Matrix.CreateRotationY(MathHelper.ToRadians(1f))) + _camera.Target;
                    //_camera.Position = Vector3.Transform(MultiplyMatrix4ByVector3(translationMatrix, _camera.Position), rotationMatrix);
                }

                _viewMatrix = Matrix.CreateLookAt(_camera.Position, _camera.Target, Vector3.Up);
            }
            else
            {
                //_camera.Position = new Vector3(0, 16, 4);

                //undo hover code here
            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            base.Update(gameTime);
        }

        private Vector3 MultiplyMatrix4ByVector3(Matrix matrix4, Vector3 vector3)
        {
            return new Vector3(
                (matrix4[0, 0] * vector3.X) + (matrix4[0, 1] * vector3.Y) + (matrix4[0, 2] * vector3.Z) + matrix4[0, 3],
                (matrix4[1, 0] * vector3.X) + (matrix4[1, 1] * vector3.Y) + (matrix4[1, 2] * vector3.Z) + matrix4[1, 3],
                (matrix4[2, 0] * vector3.X) + (matrix4[2, 1] * vector3.Y) + (matrix4[2, 2] * vector3.Z) + matrix4[2, 3]);
        }

        /// <summary>
        /// Draws the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        protected override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(Color.CornflowerBlue);

            ////Vector3 cameraReference = new Vector3(0, 0, 1);

            //_model.RotationMatrix *= Matrix.CreateFromAxisAngle(_model.RotationMatrix.Forward, MathHelper.ToRadians(ROTATE_DEGREE));
            //_modelRotateAround.RotationMatrix *= Matrix.CreateFromAxisAngle(_modelRotateAround.RotationMatrix.Forward, MathHelper.ToRadians(10.0f));

            ////Matrix cameraRotationMatrix = Matrix.CreateRotationY(MathHelper.Pi);
            ////Vector3 cameraTransformedReference = Vector3.Transform(cameraReference, cameraRotationMatrix);
            ////Vector3 cameraLookat = _camera.Position + cameraTransformedReference;

            //DrawModel(_model);
            //DrawModel(_modelRotateAround, false);

            //base.Draw(gameTime);

            _basicEffect.Projection = _projectionMatrix;
            _basicEffect.View = _viewMatrix;
            _basicEffect.World = _worldMatrix;

            GraphicsDevice.Clear(Color.Coral);
            VertexBuffer vertexBuffer = MakeVertexBuffer();
            GraphicsDevice.SetVertexBuffer(vertexBuffer);

            RasterizerState rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;
            GraphicsDevice.RasterizerState = rasterizerState;

            foreach (EffectPass pass in _basicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, vertexBuffer.VertexCount);
            }

            _spriteBatch.Begin();
            _spriteBatch.DrawString(_spriteFont, $"Camera z: {_camera.Position.Z}", new Vector2(50, 100), Color.Blue);
            _spriteBatch.DrawString(_spriteFont, $"Camera x: {_camera.Position.X}", new Vector2(50, 130), Color.Black);
            _spriteBatch.DrawString(_spriteFont, $"_viewMatrix: {_viewMatrix.Up} {_viewMatrix.Right} {_viewMatrix.Forward}", new Vector2(50, 160), Color.BlanchedAlmond);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Draws the model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="matrix">The matrix.</param>
        /// <param name="position">The position.</param>
        private void DrawModel(ModelModel model, bool add = false)
        {
            foreach (ModelMesh mesh in model.Model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    if (add)
                    {
                        model.WorldMatrix.Decompose(out Vector3Struct scale, out Quaternion rotation, out Vector3Struct translation);
                        model.WorldMatrix = Matrix.CreateTranslation(model.Position * translation) * (model.RotationMatrix * Matrix.CreateTranslation(translation));
                        //model.WorldMatrix = Matrix.CreateTranslation(model.Position) * (model.RotationMatrix * Matrix.CreateTranslation(model.Position));

                    }
                    else
                    {
                        model.WorldMatrix = Matrix.CreateTranslation(model.Position) * (model.RotationMatrix * Matrix.CreateTranslation(model.Position));
                    }

                    effect.World = model.WorldMatrix;
                    effect.EnableDefaultLighting();
                    effect.PreferPerPixelLighting = true;
                    effect.View = Matrix.CreateLookAt(
                        cameraPosition: _camera.Position,
                        cameraTarget: _camera.Target,
                        Vector3.UnitZ);
                    effect.Projection = _projectionMatrix;

                    for (int i = 0; i < effect.CurrentTechnique.Passes.Count; i++)
                    {
                        effect.CurrentTechnique.Passes[i].Apply();
                    }
                }

                mesh.Draw();
            }
        }

        private VertexBuffer CreateTriangle()
        {
            VertexPositionColor[] triangleVertices = new VertexPositionColor[3];
            triangleVertices[0] = new VertexPositionColor(new Vector3(0, 20, 0), Color.Red);
            triangleVertices[1] = new VertexPositionColor(new Vector3(-20, -20, 0), Color.Green);
            triangleVertices[2] = new VertexPositionColor(new Vector3(20, -20, 0), Color.Blue);

            VertexBuffer vertexBuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionColor), 3, BufferUsage.WriteOnly);
            vertexBuffer.SetData<VertexPositionColor>(triangleVertices);
            return vertexBuffer;
        }

        private VertexBuffer MakeVertexBuffer()
        {
            List<VertexPositionColor> triangleVertices = new List<VertexPositionColor>();
            triangleVertices.AddRange(Primitives.MakeFloor());
            triangleVertices.AddRange(Primitives.MakeBox());
            triangleVertices.AddRange(Primitives.MakeCameraTarget(_camera.Target));

            VertexBuffer vertexBuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionColor), triangleVertices.Count, BufferUsage.WriteOnly);
            vertexBuffer.SetData<VertexPositionColor>(triangleVertices.ToArray());
            return vertexBuffer;
        }
        
        public static Quaternion MyLookRotation(Vector3 lookAt, Vector3 upDirection)
        {
            Vector3 forward = lookAt;
            Vector3 up = upDirection;

            forward = forward.Normalized();
            up = up - (forward * Vector3.Dot(up, forward));
            up = up.Normalized();

            ///////////////////////

            Vector3 vector = forward.Normalized();
            Vector3 vector2 = Vector3.Cross(up, vector);
            Vector3 vector3 = Vector3.Cross(vector, vector2);
            float m00 = vector2.X;
            float m01 = vector2.Y;
            float m02 = vector2.Z;
            float m10 = vector3.X;
            float m11 = vector3.Y;
            float m12 = vector3.Z;
            float m20 = vector.X;
            float m21 = vector.Y;
            float m22 = vector.Z;

            float num8 = (m00 + m11) + m22;
            Quaternion quaternion = new Quaternion();
            if (num8 > 0.0f)
            {
                float num = MathF.Sqrt(num8 + 1.0f);
                quaternion.W = num * 0.5f;
                num = 0.5f / num;
                quaternion.X = (m12 - m21) * num;
                quaternion.Y = (m20 - m02) * num;
                quaternion.Z = (m01 - m10) * num;

                return quaternion;
            }
            if ((m00 >= m11) && (m00 >= m22))
            {
                float num7 = MathF.Sqrt(((1.0f + m00) - m11) - m22);
                float num4 = 0.5f / num7;
                quaternion.X = 0.5f * num7;
                quaternion.Y = (m01 + m10) * num4;
                quaternion.Z = (m02 + m20) * num4;
                quaternion.W = (m12 - m21) * num4;
                return quaternion;
            }
            if (m11 > m22)
            {
                float num6 = (float)MathF.Sqrt(((1.0f + m11) - m00) - m22);
                float num3 = 0.5f / num6;
                quaternion.X = (m10 + m01) * num3;
                quaternion.Y = 0.5f * num6;
                quaternion.Z = (m21 + m12) * num3;
                quaternion.W = (m20 - m02) * num3;
                return quaternion;
            }
            float num5 = (float)MathF.Sqrt(((1.0f + m22) - m00) - m11);
            float num2 = 0.5f / num5;
            quaternion.X = (m20 + m02) * num2;
            quaternion.Y = (m21 + m12) * num2;
            quaternion.Z = 0.5f * num5;
            quaternion.W = (m01 - m10) * num2;

            return quaternion;
        }

        internal class ModelModel
        {
            public Vector3 Position { get; set; }
            public Matrix RotationMatrix { get; set; }
            public Matrix WorldMatrix { get; set; }

            public Model Model { get; set; }
            
            public ModelModel(Model model, Vector3 position)
            {
                Position = position;
                Model = model;
                RotationMatrix = Matrix.Identity;
                WorldMatrix = Matrix.CreateWorld(position.Struct, Vector3.Forward, Vector3.Up);
            }

            public ModelModel(Model model) : this(model, Vector3.Zero) 
            {
            }
        }
    }
}
