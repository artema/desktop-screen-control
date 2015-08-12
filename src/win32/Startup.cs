using System.Threading.Tasks;

namespace DesktopScreenControl
{
    public class Startup
    {
        public Task<object> GetBrightness(object arg)
        {
            return Task.FromResult<object>(ScreenBrightnessControl.GetBrightness());
        }

        public Task<object> SetBrightness(object arg)
        {
            return Task<object>.Factory.StartNew(() =>
            {
                ScreenBrightnessControl.SetBrightness((double) arg);
                return null;
            });
        }

        public Task<object> FlipScreen(object arg)
        {
            return Task<object>.Factory.StartNew(() =>
            {
                ScreenOrientationControl.Flip();
                return null;
            });
        }

        public Task<object> RotateScreenClockwise(object arg)
        {
            return Task<object>.Factory.StartNew(() =>
            {
                ScreenOrientationControl.RotateClockwise();
                return null;
            });
        }

        public Task<object> RotateScreenCounterClockwise(object arg)
        {
            return Task<object>.Factory.StartNew(() =>
            {
                ScreenOrientationControl.RotateCounterClockwise();
                return null;
            });
        }
    }
}
