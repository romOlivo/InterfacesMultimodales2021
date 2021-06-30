using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WiimoteLib;

namespace WiimoteGestureLib
{

    public class GestureManager
    {
        private readonly GertureRecognizer gr;
        private readonly GerturerCapturer gc;
        private List<Gesture> gestures;

        public enum StatesGestureCapure { Capture, Recog };
        public event Action<string> GestureRecognized;
        public event Action GestureCaptured;

        private StatesGestureCapure state;

        #region Métodos Publicos

        public void OnWiimoteChanged(object sender, WiimoteChangedEventArgs e)
        {
            gc.OnWiimoteChanged(sender, e);
        }

        public void SetStateGestureCapure(StatesGestureCapure state)
        {
            this.state = state;
        }

        public void SetNames(IReadOnlyList<string> names)
        {
            for (int i = 0; i < names.Count && i < gestures.Count; i++)
            {
                gestures[i].Name = names[i];
            }
        }

        public void Save(string file)
        {
            string jsonString = JsonConvert.SerializeObject(gestures);
            File.WriteAllText(file, jsonString);
        }

        public void Load(string file)
        {

        }

        #endregion

        #region Métodos Privados

        public GestureManager()
        {
            gestures = new List<Gesture>();
            gr = new GertureRecognizer(gestures);
            gc = new GerturerCapturer();
            state = StatesGestureCapure.Capture;

            gc.GestureCaptured += Gc_GestureCaptured;
            gr.GestureRecognized += Gr_GestureRecognized;
        }

        private void Gr_GestureRecognized(string label)
        {
            GestureRecognized.Invoke(label);
        }

        private void Gc_GestureCaptured(Gesture g)
        {
            if (state == StatesGestureCapure.Capture)
            {
                gestures.Add(g);
                GestureCaptured.Invoke();
            }
            if (state == StatesGestureCapure.Recog)
            {
                gr.OnGestureCaptured(g);
            }
        }

        #endregion

    }
}
