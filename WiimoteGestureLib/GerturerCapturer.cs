using System;
using WiimoteLib;

namespace WiimoteGestureLib
{
    

    public class GerturerCapturer
    {
        public event Action<Gesture> GestureCaptured;

        private EnumStateB stateB = EnumStateB.Off;
        private Gesture g;
        private enum EnumStateB { On, Off };

        private void SendEvent(Gesture g)
        {
            if (GestureCaptured != null)
                GestureCaptured(g);
        }

        void OnWiimoteChanged(object sender, WiimoteChangedEventArgs e)
        {
            if (stateB == EnumStateB.Off && e.WiimoteState.ButtonState.B)
            {
                stateB = EnumStateB.On;
                g = new Gesture();
                g.AddSample(e.WiimoteState.AccelState.Values.X,
                    e.WiimoteState.AccelState.Values.Y,
                    e.WiimoteState.AccelState.Values.Z);
            }
            else if (stateB == EnumStateB.On)
            {
                if (e.WiimoteState.ButtonState.B)
                    g.AddSample(e.WiimoteState.AccelState.Values.X,
                                        e.WiimoteState.AccelState.Values.Y,
                                        e.WiimoteState.AccelState.Values.Z);
                else
                    SendEvent(g);
            }
        }
    }
}
