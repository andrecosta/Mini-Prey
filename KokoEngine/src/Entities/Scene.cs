﻿using System.Collections.Generic;

namespace KokoEngine
{
    public class Scene : IScene
    {
        public string Name { get; }

        private readonly List<IGameObject> _rootGameObjects = new List<IGameObject>();

        internal Scene(string name)
        {
            Name = name;
        }

        public IGameObject AddGameObject(IGameObject go)
        {
            var gameObjectInternal = go as IGameObjectInternal;
            if (gameObjectInternal == null) return null;

            gameObjectInternal.Scene = this;

            _rootGameObjects.Add(go);

            return go;
        }

        public List<IGameObject> GetRootGameObjects() => _rootGameObjects;
    }
}
