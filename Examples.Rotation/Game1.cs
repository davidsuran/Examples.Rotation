using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Vector3Struct = Microsoft.Xna.Framework.Vector3;

namespace Examples.Rotation
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private ModelModel _model;
        private ModelModel _modelRotateAround;
        private Camera _camera;

        private Matrix _projectionMatrix;
        private Matrix _viewMatrix;
        private Matrix _worldMatrix;

        private BasicEffect _basicEffect;

        private bool _orbit;
        
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
        }

        /// <summary>
        /// Loads the content.
        /// </summary>
        protected override void LoadContent()
        {
            _model = new ModelModel(Content.Load<Model>("box"));
            //_model.RotationMatrix = Matrix.CreateFromAxisAngle(_rotationMatrix.Forward, MathHelper.ToRadians(115.0f));
            _modelRotateAround = new ModelModel(Content.Load<Model>("box"), new Vector3(0, 6, 0));
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
            triangleVertices.AddRange(MakeFloor());
            triangleVertices.AddRange(MakeBox());
            triangleVertices.AddRange(MakeCameraTarget(_camera.Target));

            VertexBuffer vertexBuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionColor), triangleVertices.Count, BufferUsage.WriteOnly);
            vertexBuffer.SetData<VertexPositionColor>(triangleVertices.ToArray());
            return vertexBuffer;
        }

        private IEnumerable<VertexPositionColor> MakeFloor()
        {
            yield return MakeVertex(0, 0, 300, Color.Red);
            yield return MakeVertex(0, 0, 0, Color.Green);
            yield return MakeVertex(300, 0, 0, Color.Blue);

            yield return MakeVertex(300, 0, 0, Color.Yellow);
            yield return MakeVertex(0, 0, 0, Color.Wheat);
            yield return MakeVertex(0, 0, -300, Color.WhiteSmoke);

            yield return MakeVertex(0, 0, -300, Color.Orange);
            yield return MakeVertex(0, 0, 0, Color.Orchid);
            yield return MakeVertex(-300, 0, 0, Color.Olive);

            yield return MakeVertex(-300, 0, 0, Color.Salmon);
            yield return MakeVertex(0, 0, 0, Color.Sienna);
            yield return MakeVertex(0, 0, 300, Color.Aqua);
        }

        private IEnumerable<VertexPositionColor> MakeBox()
        {
            // Front back
            yield return MakeVertex(-30, 0, -30);
            yield return MakeVertex(30, 0, -30);
            yield return MakeVertex(30, 60, -30);

            yield return MakeVertex(-30, 60, -30);
            yield return MakeVertex(30, 60, -30);
            yield return MakeVertex(-30, 0, -30);

            // Box back
            yield return MakeVertex(-30, 0, 30);
            yield return MakeVertex(30, 0, 30);
            yield return MakeVertex(30, 60, 30);

            yield return MakeVertex(-30, 60, 30);
            yield return MakeVertex(30, 60, 30);
            yield return MakeVertex(-30, 0, 30);
        }

        private IEnumerable<VertexPositionColor> MakeCameraTarget(Vector3 cameraTarget)
        {
            float height = 600f;
            // Front back
            yield return MakeVertex(cameraTarget.X - 10, -height, cameraTarget.Z, Color.Red);
            yield return MakeVertex(cameraTarget.X + 10, -height, cameraTarget.Z, Color.Red);
            yield return MakeVertex(cameraTarget.X + 10, height, cameraTarget.Z, Color.Red);

            yield return MakeVertex(cameraTarget.X - 10, height, cameraTarget.Z, Color.Red);
            yield return MakeVertex(cameraTarget.X + 10, height, cameraTarget.Z, Color.Red);
            yield return MakeVertex(cameraTarget.X - 10, -height, cameraTarget.Z, Color.Red);

            // Box back
            yield return MakeVertex(cameraTarget.X, -height, cameraTarget.Z + 10, Color.BlueViolet);
            yield return MakeVertex(cameraTarget.X, -height, cameraTarget.Z - 10, Color.BlueViolet);
            yield return MakeVertex(cameraTarget.X, height, cameraTarget.Z - 10, Color.BlueViolet);

            yield return MakeVertex(cameraTarget.X, height, cameraTarget.Z - 10, Color.BlueViolet);
            yield return MakeVertex(cameraTarget.X, height, cameraTarget.Z + 10, Color.BlueViolet);
            yield return MakeVertex(cameraTarget.X, -height, cameraTarget.Z + 10, Color.BlueViolet);
        }

        private VertexPositionColor MakeVertex(float x, float y, float z, Color? color = null)
        {
            return new VertexPositionColor(new Vector3(x, y, z), color.HasValue ? color.Value : Color.Gray);
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

        internal class Vector3
        {
            public float X { get; set; }
            public float Y { get; set; }
            public float Z { get; set; }

            public Microsoft.Xna.Framework.Vector3 Struct => new Microsoft.Xna.Framework.Vector3(X, Y, Z);

            public static Microsoft.Xna.Framework.Vector3 Forward => Microsoft.Xna.Framework.Vector3.Forward;

            //public static Vector3Struct Zero => Vector3Struct.Zero;

            public static Microsoft.Xna.Framework.Vector3 Up => Microsoft.Xna.Framework.Vector3.Up;

            public static Vector3 Zero => new Vector3(0f, 0f, 0f);

            public static Microsoft.Xna.Framework.Vector3 UnitZ => Microsoft.Xna.Framework.Vector3.UnitZ;


            public static implicit operator Microsoft.Xna.Framework.Vector3(Vector3 v)
            {
                return v.Struct;
            }

            public static explicit operator Vector3(Microsoft.Xna.Framework.Vector3 v)
            {
                return new Vector3(v);
            }

            public Vector3(Microsoft.Xna.Framework.Vector3 vector3) : this(vector3.X, vector3.Y, vector3.Z)
            {
            }

            public Vector3(float x, float y, float z)
            {
                X = x;
                Y = y;
                Z = z;
            }

            internal static Vector3 Transform(Vector3 position, Matrix rotationMatrix)
            {
                return (Vector3)Microsoft.Xna.Framework.Vector3.Transform(position.Struct, rotationMatrix);
            }
        }
    }
}
