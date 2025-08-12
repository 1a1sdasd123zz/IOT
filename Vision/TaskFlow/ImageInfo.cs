using Vision.BaseClass.EnumHelper;

namespace Vision.TaskFlow
{
    public struct ImageInfo
    {
        public Cognex.VisionPro.ICogImage CogImage;
        public int Index;
        public EnumStation Type { get; set; }
    }
}
