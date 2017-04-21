using System.Collections.Generic;

namespace KokoEngine
{
    public interface IScreenManager
    {
        Resolution CurrentResolution { get; }
        List<Resolution> SupportedResolutions { get; }
        bool IsFullscreen { get; set; }

        bool SetResolution(int width, int height);
        bool SetResolution(Resolution resolution);
        void AddSupportedResolution(int width, int height);
    }
}
