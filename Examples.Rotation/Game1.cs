using System.Collections.Generic;
using CameraControllerDemo;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Vector3Struct = Microsoft.Xna.Framework.Vector3;
using Vector3 = CameraControllerDemo.Vector3;

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
                    _camera.Position = Vector3.Transform(_camera.Position, rotationMatrix);

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
            _spriteBatch.DrawString(_spriteFont, "hello, world!", new Vector2(100, 100), Color.Blue);
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

        internal class Camera
        {
            public Vector3 Position { get; set; }
            public Vector3 Target { get; set; }
            public Matrix CameraRotationMatrix { get; set; }

            public Camera(Vector3 position, Vector3 target)
            {
                Position = position;
                Target = target;
                CameraRotationMatrix = Matrix.Identity;
            }
        }

    }
}
