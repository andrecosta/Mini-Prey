namespace KokoEngine
{
    public interface IAnimator : IBehaviour
    {
        bool IsPlaying { get; }
        bool Looping { get; set; }
        float Speed { get; set; }

        void AddAnimation(string key, IAnimationClip clip);
        void Play(string animationName);
        void Play(string animationName, float speed);
        void Stop();
    }
}