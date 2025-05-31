using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RPG_Cs;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    Texture2D playerTexture;
    Vector2 playerPos;
    Rectangle playerSprite;
    int playerCurrentSprite;
    float playerSpeed = 200f;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        playerPos = new Vector2(150, 150);

        playerSprite = new Rectangle(0, 0, 32, 32);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here

        playerTexture = Content.Load<Texture2D>("player-mage");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        KeyboardState state = Keyboard.GetState();
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        Vector2 direction = Vector2.Zero;

        if (state.IsKeyDown(Keys.W)) {
            playerCurrentSprite = 2;
            direction.Y -= 1;
        }
        if (state.IsKeyDown(Keys.S)) {
            playerCurrentSprite = 0;
            direction.Y += 1;
        }
        if (state.IsKeyDown(Keys.A)) {
            playerCurrentSprite = 3;
            direction.X -= 1;
        }
        if (state.IsKeyDown(Keys.D)) {
            playerCurrentSprite = 1;
            direction.X += 1;
        }

        if (direction != Vector2.Zero) direction.Normalize();

        playerSprite = new Rectangle((32 * playerCurrentSprite), 0, 32, 32);

        playerPos += direction * playerSpeed * deltaTime;

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here

        _spriteBatch.Begin();

        //_spriteBatch.Draw(playerTexture, playerPos, Color.White);
        _spriteBatch.Draw(playerTexture, playerPos, playerSprite, Color.White);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
