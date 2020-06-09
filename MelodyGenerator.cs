using System;
using System.IO;
using System.Collections.Generic;

namespace KalamaPona {
    public class MelodyGenerator {
        public Scale GeneratorScale {get; set;}
        private Random random = new Random();

        public MelodyGenerator(Scale generatorScale) {
            GeneratorScale = generatorScale;
        }

        public void SetSeed(int seed) {
            random = new Random(seed);
        }

        public List<(float freq, float duration)> Generate(float duration, float maxNoteLength, int allowedDivisions = 2, float noteDivisions = 0.5f) {
            List<(float freq, float duration)> result = new List<(float freq, float duration)>();
            while (duration > 0) {
                float nextDuration = maxNoteLength;
                int randDivisions = random.Next(0, allowedDivisions);
                for (int i = 0; i < randDivisions; i++) {
                    nextDuration = nextDuration*noteDivisions;
                }
                if (nextDuration > duration) {
                    continue;
                }
                float nextNote = GeneratorScale.Note(random.Next(GeneratorScale.Repetition()/2, GeneratorScale.Repetition()));
                result.Add((nextNote, nextDuration));
                duration -= nextDuration;
            }
            return result;
        }
    }
}