using System;

namespace KokoEngine
{
    public sealed class AudioClip : Asset
    {
        public TimeSpan Duration { get; private set; }

        public void SetData(object rawData, TimeSpan duration)
        {
            Duration = Duration;

            base.SetData(rawData);
        }
    }
}
