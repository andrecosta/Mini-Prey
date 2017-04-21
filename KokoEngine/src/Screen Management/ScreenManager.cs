using System.Collections.Generic;

namespace KokoEngine
{
    public class ScreenManager : IScreenManager
    {
        public Resolution CurrentResolution { get; private set; }
        public List<Resolution> SupportedResolutions { get; } = new List<Resolution>();
        public bool IsFullscreen { get; set; }

        public bool SetResolution(int width, int height) => SetResolution(new Resolution(width, height));
        public bool SetResolution(Resolution resolution)
        {
            if (SupportedResolutions.Contains(resolution))
            {
                CurrentResolution = resolution;
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