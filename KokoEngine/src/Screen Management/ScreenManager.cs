using System;
using System.Collections.Generic;

namespace KokoEngine
{
    public class ScreenManager : IScreenManagerInternal
    {
        public Resolution CurrentResolution { get; private set; }
        public List<Resolution> SupportedResolutions { get; } = new List<Resolution>();
        public int Width => CurrentResolution.Width;
        public int Height => CurrentResolution.Height;
        public Action<IScreenManager> ResolutionUpdateHandler { get; set; }

        private bool _isFullScreen;
        public bool IsFullScreen
        {
            get { return _isFullScreen; }
            set
            {
                _isFullScreen = value;
                ResolutionUpdateHandler?.Invoke(this);
            }
        }

        public bool SetResolution(int index) => SetResolution(SupportedResolutions[index]);
        public bool SetResolution(int width, int height) => SetResolution(new Resolution(width, height));
        public bool SetResolution(Resolution resolution)
        {
            if (SupportedResolutions.Contains(resolution))
            {
                CurrentResolution = resolution;
                ResolutionUpdateHandler?.Invoke(this);
                return true;
            }

            return false;
        }

        public void AddSupportedResolution(int width, int height)
        {
            Resolution resolution = new Resolution(width, height);

            if (!SupportedResolutions.Contains(resolution))
                SupportedResolutions.Add(resolution);
        }
    }
}