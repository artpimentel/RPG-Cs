using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RPG_Cs;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    int screenWidth;
    int screeHeight;

    KeyboardState previousKeyboardState;

    Texture2D pixel;

    Texture2D playerTexture;
    Vector2 playerPos;
    Rectangle playerSprite;
    int playerCurrentSprite;
    float playerSpeed;

    Rectangle playerMenuBackground;
    Vector2 playerMenuPos;
    bool playerMenuIsOpen;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        _graphics.PreferredBackBufferWidth = 1280;
        _graphics.PreferredBackBufferHeight = 720;
        _graphics.ApplyChanges();
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        screenWidth = GraphicsDevice.Viewport.Width;
        screeHeight = GraphicsDevice.Viewport.Height;

        playerPos = new Vector2((screenWidth / 2), (screeHeight / 2));
        playerSprite = new Rectangle(0, 0, 32, 32);
        playerSpeed = 200f;

        playerMenuBackground = new Rectangle(0, 0, 800, 600);
        playerMenuPos = new Vector2((screenWidth / 2), (screeHeight / 2));
        playerMenuIsOpen = false;

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here

        pixel = new Texture2D(GraphicsDevice, 1, 1);
        pixel.SetData(new[] { Color.White });

        playerTexture = Content.Load<Texture2D>("player-mage");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        KeyboardState currentKeyboardState = Keyboard.GetState();

        //----------------------------

        if (currentKeyboardState.IsKeyDown(Keys.Tab) && previousKeyboardState.IsKeyUp(Keys.Tab))
        {
            playerMenuIsOpen = !playerMenuIsOpen;
        }

        previousKeyboardState = currentKeyboardState;

        //----------------------------

        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        Vector2 direction = Vector2.Zero;

        if (!playerMenuIsOpen)
        {
            if (currentKeyboardState.IsKeyDown(Keys.W))
            {
                playerCurrentSprite = 2;
                direction.Y -= 1;
            }
            if (currentKeyboardState.IsKeyDown(Keys.S)) {
                playerCurrentSprite = 0;
                direction.Y += 1;
            }
            if (currentKeyboardState.IsKeyDown(Keys.A)) {
                playerCurrentSprite = 3;
                direction.X -= 1;
            }
            if (currentKeyboardState.IsKeyDown(Keys.D)) {
                playerCurrentSprite = 1;
                direction.X += 1;
            }
            if (direction != Vector2.Zero) direction.Normalize();

            playerSprite = new Rectangle((32 * playerCurrentSprite), 0, 32, 32);

            playerPos += direction * playerSpeed * deltaTime;
        }
        //----------------------------

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here

        _spriteBatch.Begin();

        Vector2 playerOrigin = new Vector2((playerSprite.Width / 2), (playerSprite.Height / 2));
        _spriteBatch.Draw(playerTexture, playerPos, playerSprite, Color.White, 0f, playerOrigin, 2f, SpriteEffects.None, 0f);

        Vector2 playerMenuOrigin = new Vector2((playerMenuBackground.Width / 2), (playerMenuBackground.Height / 2));
        if (playerMenuIsOpen)
        {
            _spriteBatch.Draw(pixel, playerMenuPos, playerMenuBackground, Color.Gray, 0f, playerMenuOrigin, 1f, SpriteEffects.None, 0f);
        }

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
