using System;
using System.IO;
using System.Collections.Generic;

namespace KalamaPona {
    public class TtetScale : Scale {
        public float[] ScaleNotes {get; set;}

        public TtetScale(float[] scaleNotes) {
            ScaleNotes = scaleNotes;
        }

        public override int Repetition() {
            return ScaleNotes.Length;
        }

        public override float Note(int n) {
            int octaves = n/ScaleNotes.Length;
            int note = n%ScaleNotes.Length;
            if (note < 0) {
                note += ScaleNotes.Length;
                octaves -= 1;
            }
            return Helpers.MidiNoteToHz(12*octaves+ScaleNotes[note]);
        }
    }
}