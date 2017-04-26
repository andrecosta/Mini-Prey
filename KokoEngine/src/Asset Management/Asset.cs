using System;
using System.IO;

namespace KokoEngine
{
    public abstract class Asset : Entity, IAssetInternal
    {
        public string Filename { get; private set; }
        public object RawData { get; private set; }
        public Action OnLoaded { get; set; }
        public bool IsLoaded => RawData != null;

        internal Asset() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rawData">The loaded MonoGame resource itself (stored in an unspecified format).</param>
        public void SetData(object rawData)
        {
            // Set the asset raw data
            RawData = rawData;

            // Notify whoever is subscribed to this event that the asset is loaded
            OnLoaded?.Invoke();
        }

        void IAssetInternal.SetName(string filename)
        {
            Filename = filename;
            Name = Path.GetFileNameWithoutExtension(filename);
        }
    }
}
