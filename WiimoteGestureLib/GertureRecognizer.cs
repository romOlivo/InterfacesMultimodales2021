using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WiimoteGestureLib
{
    class GertureRecognizer
    {
        public event Action<string> GestureRecognized;

        private IReadOnlyList<Gesture> gestures;

        public GertureRecognizer(IReadOnlyList<Gesture> gestures)
        {
            this.gestures = gestures;
        }

        void OnGestureCaptured(Gesture h)
        {
            string label = "Unknown";
            if (gestures != null && gestures.Count > 0)
                label = gestures.Select(g => (d: g.DistanceTo(h), Name: h.Name)).Min().Name;
            if (GestureRecognized != null)
                GestureRecognized(label);
        }
    }
}
