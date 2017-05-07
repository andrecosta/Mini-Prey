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
            PlayerController player = scene.CreateGameObject<PlayerController>("PlayerController", Vector2.Zero);
            {
                player.TeamColor = new Color(183, 68, 231);
                player.AttackCommandSound = assetManager.GetAsset<AudioClip>("AttackCommand");
                player.AddComponent<LineRenderer>();
                player.AddComponent<AudioSource>();
            }

            // Create the AI player
            AIController aiPlayer = scene.CreateGameObject<AIController>("AIController", Vector2.Zero);
            {
                aiPlayer.TeamColor = new Color(68, 231, 118);
            }

            // Create the Neutral player
            AIController neutralPlayer = scene.CreateGameObject<AIController>("NeutralController", Vector2.Zero);
            {
                neutralPlayer.TeamColor = new Color(230, 230, 230);
                neutralPlayer.IsNeutral = true;
            }

            // Create the GameController
            GameController gc = scene.CreateGameObject<GameController>("GameController", Vector2.Zero);
            {
                gc.ShipSprite = new Sprite(assetManager.GetAsset<Texture2D>("ship"));
                gc.ShotSprite = new Sprite(assetManager.GetAsset<Texture2D>("bullet"));
                gc.OutlineSprite = new Sprite(assetManager.GetAsset<Texture2D>("outline"));
                gc.RangeSprite = new Sprite(assetManager.GetAsset<Texture2D>("range"));
                gc.PlanetConqueredSound = assetManager.GetAsset<AudioClip>("PlanetConquered");
                gc.PlanetSelectSound = assetManager.GetAsset<AudioClip>("PlanetSelect");
                gc.PlanetUpgradeSound = assetManager.GetAsset<AudioClip>("PlanetUpgrade");
                gc.ShipShotSound = assetManager.GetAsset<AudioClip>("ShipShotDown");
                gc.PlanetPopulationFont = assetManager.GetAsset<Font>("main_font");
                gc.Players = new Player[] {player, aiPlayer, neutralPlayer};

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
                                PopGenerationLimit = 20,
                                PopGenerationRate = 2f
                            },
                            new Planet.Upgrade
                            {
                                Cost = 15,
                                Sprite = new Sprite(assetManager.GetAsset<Texture2D>("Planet2")),
                                PopGenerationLimit = 40,
                                PopGenerationRate = 1.5f
                            },
                            new Planet.Upgrade
                            {
                                Cost = 20,
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
                                Cost = 10,
                                FireRate = 0.5f,
                                Range = 120,
                                Sprite = new Sprite(assetManager.GetAsset<Texture2D>("Sentry1")),
                            },
                            new Planet.Upgrade
                            {
                                Cost = 20,
                                FireRate = 0.4f,
                                Range = 170,
                                Sprite = new Sprite(assetManager.GetAsset<Texture2D>("Sentry2")),
                            },
                            new Planet.Upgrade
                            {
                                Cost = 30,
                                FireRate = 0.3f,
                                Range = 210,
                                Sprite = new Sprite(assetManager.GetAsset<Texture2D>("Sentry3")),
                            }
                        }
                    }
                };

                player.GameController = gc;
                aiPlayer.GameController = gc;
            }

            // Create the Custom Cursor
            CustomCursor cc = scene.CreateGameObject<CustomCursor>("CustomCursor", Vector2.Zero);
            {
                cc.AddComponent<SpriteRenderer>();
                cc.Percent25Sprite = new Sprite(assetManager.GetAsset<Texture2D>("cursor_25"));
                cc.Percent50Sprite = new Sprite(assetManager.GetAsset<Texture2D>("cursor_50"));
                cc.Percent75Sprite = new Sprite(assetManager.GetAsset<Texture2D>("cursor_75"));
                cc.Percent100Sprite = new Sprite(assetManager.GetAsset<Texture2D>("cursor_100"));
                player.CustomCursor = cc;
            }

            // Create the HUD
            HUD hud = scene.CreateGameObject<HUD>("HUD", Vector2.Zero);
            {
                var leftBar = scene.CreateGameObject<LineRenderer>("LeftBar", Vector2.Zero);
                var rightBar = scene.CreateGameObject<LineRenderer>("RightBar", Vector2.Zero);
                var leftText = scene.CreateGameObject<TextRenderer>("LeftText", Vector2.Zero);
                var rightText = scene.CreateGameObject<TextRenderer>("RightText", Vector2.Zero);

                hud.LeftBar = leftBar;
                hud.RightBar = rightBar;
                hud.LeftText = leftText;
                hud.RightText = rightText;
                hud.UIFont = assetManager.GetAsset<Font>("main_font");
                hud.GameController = gc;
            }

            return scene;
        }
    }
}
