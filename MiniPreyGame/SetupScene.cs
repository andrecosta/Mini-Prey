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
                gc.OutlineSprite = new Sprite(assetManager.GetAsset<Texture2D>("outline"));
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
                                FireRate = 0,
                                Sprite = new Sprite(assetManager.GetAsset<Texture2D>("Planet1")),
                                PopGenerationLimit = 20,
                                PopGenerationRate = 2f
                            },
                            new Planet.Upgrade
                            {
                                Cost = 20,
                                FireRate = 0,
                                Sprite = new Sprite(assetManager.GetAsset<Texture2D>("Planet2")),
                                PopGenerationLimit = 40,
                                PopGenerationRate = 1.5f
                            },
                            new Planet.Upgrade
                            {
                                Cost = 30,
                                FireRate = 0,
                                Sprite = new Sprite(assetManager.GetAsset<Texture2D>("Planet3")),
                                PopGenerationLimit = 60,
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
                                FireRate = 2,
                                Sprite = new Sprite(assetManager.GetAsset<Texture2D>("Sentry1")),
                                PopGenerationLimit = 0,
                                PopGenerationRate = 0
                            },
                            new Planet.Upgrade
                            {
                                Cost = 25,
                                FireRate = 1,
                                Sprite = new Sprite(assetManager.GetAsset<Texture2D>("Sentry2")),
                                PopGenerationLimit = 0,
                                PopGenerationRate = 0
                            },
                            new Planet.Upgrade
                            {
                                Cost = 35,
                                FireRate = 0.5f,
                                Sprite = new Sprite(assetManager.GetAsset<Texture2D>("Sentry3")),
                                PopGenerationLimit = 0,
                                PopGenerationRate = 0
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
