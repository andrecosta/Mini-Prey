using System.Collections.Generic;
using KokoEngine;

namespace MiniPreyGame
{
    public static partial class Program
    {
        static IScene SetupScene(IAssetManager assetManager)
        {
            // Create scene
            IScene scene = new Scene("Game");

            // Create the human player
            IGameObject playerControllerGameObject = scene.CreateGameObject("PlayerController");
            PlayerController playerController = playerControllerGameObject.AddComponent<PlayerController>();
            playerController.TeamColor = new Color(183, 68, 231);
            playerControllerGameObject.AddComponent<LineRenderer>();

            // Create the AI player
            IGameObject aiControllerGameObject = scene.CreateGameObject("AIController");
            AIController aiController = aiControllerGameObject.AddComponent<AIController>();
            aiController.TeamColor = new Color(68, 231, 118);

            // Create the Neutral player
            IGameObject neutralControllerGameObject = scene.CreateGameObject("NeutralController");
            AIController neutralController = neutralControllerGameObject.AddComponent<AIController>();
            neutralController.TeamColor = new Color(230, 230, 230);
            neutralController.IsNeutral = true;

            // Create the GameController
            IGameObject gameControllerObject = scene.CreateGameObject("GameController");
            {
                var gc = gameControllerObject.AddComponent<GameController>();
                gc.ShipSprite = new Sprite(assetManager.GetAsset<Texture2D>("ship"));
                gc.ShotSprite = new Sprite(assetManager.GetAsset<Texture2D>("bullet"));
                gc.OutlineSprite = new Sprite(assetManager.GetAsset<Texture2D>("outline"));
                gc.RangeSprite = new Sprite(assetManager.GetAsset<Texture2D>("range"));
                gc.PlanetPopulationFont = assetManager.GetAsset<Font>("main_font");
                gc.Players = new Player[] {playerController, aiController, neutralController};

                // Set the planet types and upgrades
                gc.PlanetTypes = new[]
                {
                    new Planet.Type
                    {
                        UpgradeLevels = new[]
                        {
                            new Planet.Upgrade
                            {
                                Cost = 10,
                                Sprite = new Sprite(assetManager.GetAsset<Texture2D>("Planet1")),
                                PopGenerationLimit = 30,
                                PopGenerationRate = 1.6f
                            },
                            new Planet.Upgrade
                            {
                                Cost = 20,
                                Sprite = new Sprite(assetManager.GetAsset<Texture2D>("Planet2")),
                                PopGenerationLimit = 50,
                                PopGenerationRate = 1.3f
                            },
                            new Planet.Upgrade
                            {
                                Cost = 30,
                                Sprite = new Sprite(assetManager.GetAsset<Texture2D>("Planet3")),
                                PopGenerationLimit = 80,
                                PopGenerationRate = 1f
                            }
                        }
                    },
                    new Planet.Type
                    {
                        UpgradeLevels = new[]
                        {
                            new Planet.Upgrade
                            {
                                Cost = 15,
                                FireRate = 0.8f,
                                Range = 120,
                                Sprite = new Sprite(assetManager.GetAsset<Texture2D>("Sentry1")),
                            },
                            new Planet.Upgrade
                            {
                                Cost = 25,
                                FireRate = 0.6f,
                                Range = 170,
                                Sprite = new Sprite(assetManager.GetAsset<Texture2D>("Sentry2")),
                            },
                            new Planet.Upgrade
                            {
                                Cost = 35,
                                FireRate = 0.3f,
                                Range = 210,
                                Sprite = new Sprite(assetManager.GetAsset<Texture2D>("Sentry3")),
                            }
                        }
                    }
                };

                playerController.GameController = gc;
                aiController.GameController = gc;
            }

            // Create the Custom Cursor
            IGameObject customCursorGameObject = scene.CreateGameObject("CustomCursor");
            {
                customCursorGameObject.AddComponent<SpriteRenderer>();

                var cc = customCursorGameObject.AddComponent<CustomCursor>();
                cc.Percent25Sprite = new Sprite(assetManager.GetAsset<Texture2D>("cursor_25"));
                cc.Percent50Sprite = new Sprite(assetManager.GetAsset<Texture2D>("cursor_50"));
                cc.Percent75Sprite = new Sprite(assetManager.GetAsset<Texture2D>("cursor_75"));
                cc.Percent100Sprite = new Sprite(assetManager.GetAsset<Texture2D>("cursor_100"));
                playerController.CustomCursor = cc;
            }

            return scene;
        }
    }
}
