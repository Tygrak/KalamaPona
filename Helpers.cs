using System;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;

namespace KalamaPona {
    public static class Helpers {
        public static float Lerp(float a, float b, float t) {
            t = t > 1 ? 1 : t < 0 ? 0 : t;
            return (1-t)*a + t*b;
        }

        //69 == A4 (440 hz)
        public static float MidiNoteToHz(float note) {
            return 8.1758f * MathF.Pow(MathF.Pow(2, 1/12f), note);
        } 
    }
}
