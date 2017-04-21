﻿using System;
using System.IO;

namespace KokoEngine
{
    public abstract class Asset : Entity, IAssetInternal
    {
        public string Filename { get; private set; }
        public object RawData { get; private set; }

        internal Asset() { }

        public void SetData(object rawData)
        {
            if (RawData == null)
                RawData = rawData;
        }

        void IAssetInternal.SetName(string filename)
        {
            Filename = filename;
            Name = Path.GetFileNameWithoutExtension(filename);
        }
    }
}
