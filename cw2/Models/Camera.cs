namespace cw2.Models;

public class Camera : Equipment
{
    public string LensType { get; private set; }
    public bool Has4KVideo { get; private set; }

    public Camera(string name, string lensType, bool has4KVideo) : base(name)
    {
        LensType = lensType;
        Has4KVideo = has4KVideo;
    }
}