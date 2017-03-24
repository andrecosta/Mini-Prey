﻿using System.Collections.Generic;

namespace KokoEngine
{
    public struct Scene : IScene
    {
        public string Name { get; }
        private List<IGameObject> _rootGameObjects;

        public Scene(string name)
        {
            _rootGameObjects = new List<IGameObject>();
            Name = name;
        }

        public void AddRootGameObject(IGameObject go)
        {
            if (_rootGameObjects == null)
                _rootGameObjects = new List<IGameObject>();

            var gameObjectInternal = go as IGameObjectInternal;
            if (gameObjectInternal != null) gameObjectInternal.Scene = this;

            _rootGameObjects.Add(go);
        }

        public List<IGameObject> GetRootGameObjects()
        {
            if (_rootGameObjects == null)
                _rootGameObjects = new List<IGameObject>();

            return _rootGameObjects;
        }
    }
}
