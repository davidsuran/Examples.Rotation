using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Examples.Rotation
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private Model _model;
        private Matrix _rotationMatrix;

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
            _rotationMatrix = Matrix.Identity;
            _rotationMatrix = Matrix.CreateFromAxisAngle(_rotationMatrix.Forward, MathHelper.ToRadians(115.0f));

            base.Initialize();
        }

        /// <summary>
        /// Loads the content.
        /// </summary>
        protected override void LoadContent()
        {
            _model = Content.Load<Model>("box");
        }

        /// <summary>
        /// Updates the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        protected override void Update(GameTime gameTime)
        {
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
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _rotationMatrix *= Matrix.CreateFromAxisAngle(_rotationMatrix.Forward, MathHelper.ToRadians(ROTATE_DEGREE));
            DrawModel(_model, new Vector3(0, 0, 0));

            base.Draw(gameTime);
        }

        /// <summary>
        /// Draws the model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="matrix">The matrix.</param>
        /// <param name="position">The position.</param>
        private void DrawModel(Model model, Vector3 position)
        {
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.World = Matrix.CreateTranslation(position) * (_rotationMatrix * Matrix.CreateTranslation(position));
                    effect.EnableDefaultLighting();
                    effect.PreferPerPixelLighting = true;
                    effect.View = Matrix.CreateLookAt(
                        cameraPosition: new Vector3(0, 16, 4),
                        cameraTarget: new Vector3(0, 0, 0),
                        Vector3.UnitZ);
                    effect.Projection = Matrix.CreatePerspectiveFieldOfView(
                      fieldOfView: MathHelper.PiOver4,
                      aspectRatio: 16f / 9f,
                      nearPlaneDistance: 1,
                      farPlaneDistance: 400);

                    for (int i = 0; i < effect.CurrentTechnique.Passes.Count; i++)
                    {
                        effect.CurrentTechnique.Passes[i].Apply();
                    }
                }

                mesh.Draw();
            }
        }
    }
}
