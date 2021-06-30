using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WiimoteGestureLib
{
    public class GertureRecognizer
    {
        public event Action<string> GestureRecognized;

        private IReadOnlyList<Gesture> gestures;

        public GertureRecognizer(IReadOnlyList<Gesture> gestures = null)
        {
            this.gestures = gestures;
        }

        public void SetPrototypes(IReadOnlyList<Gesture> gestures)
        {
            this.gestures = gestures;
        }

        public void OnGestureCaptured(Gesture h)
        {
            string label = "Unknown";
            if (gestures != null && gestures.Count > 0)
                label = gestures.Select(g => (d: g.DistanceTo(h), Name: g.Name)).Min().Name;
            if (GestureRecognized != null)
                GestureRecognized(label);
        }
    }
}
