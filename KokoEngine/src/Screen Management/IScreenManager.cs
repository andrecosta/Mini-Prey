using System;
using System.Collections.Generic;

namespace KokoEngine
{
    public interface IScreenManager
    {
        Resolution CurrentResolution { get; }
        List<Resolution> SupportedResolutions { get; }
        bool IsFullScreen { get; set; }
        int Width { get; }
        int Height { get; }
        Action<IScreenManager> ResolutionUpdateHandler { get; set; }

        bool SetResolution(int index);
        bool SetResolution(int width, int height);
        bool SetResolution(Resolution resolution);
        void AddSupportedResolution(int width, int height);
    }
}
