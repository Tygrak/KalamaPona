using System;
using System.IO;
using System.Collections.Generic;

namespace KalamaPona {
    class Program {
        public static void AppendSynthNote(List<float> sound, float hz, float duration) {
            SoundCreator sc = new SoundCreator();
            var waveA = sc.AverageWaves(sc.SinWave(hz, duration), sc.AverageWaves(sc.SinWave(hz*2, duration), sc.SinWave(hz*4, duration)));
            var waveB = sc.SquareWave(hz, duration);
            var resultWave = sc.AverageWaves(waveA, waveB);
            sound.AddRange(sc.AdsrEnvelope(resultWave, 0.1f, 0.25f, 0.6f, 0.2f));
        }

        static void Main(string[] args) {
            TtetScale cMajor = new TtetScale(new float[] {60, 62, 64, 65, 67, 69, 71});
            SoundCreator soundCreator = new SoundCreator();
            List<float> sound = new List<float>();
            //var wave = soundCreator.AdsrEnvelope(soundCreator.HarmonicSinWave(440, 1, 5), 0.1f, 0.25f, 0.6f, 0.2f);
            AppendSynthNote(sound, cMajor.Note(0), 0.25f);
            AppendSynthNote(sound, cMajor.Note(1), 0.25f);
            AppendSynthNote(sound, cMajor.Note(2), 0.25f);
            AppendSynthNote(sound, cMajor.Note(3), 0.25f);
            AppendSynthNote(sound, cMajor.Note(-1), 0.25f);
            AppendSynthNote(sound, cMajor.Note(0), 0.25f);
            AppendSynthNote(sound, cMajor.Note(1), 0.25f);
            AppendSynthNote(sound, cMajor.Note(2), 0.25f);
            AppendSynthNote(sound, cMajor.Note(-2), 0.25f);
            AppendSynthNote(sound, cMajor.Note(-1), 0.25f);
            AppendSynthNote(sound, cMajor.Note(0), 0.25f);
            AppendSynthNote(sound, cMajor.Note(1), 0.25f);
            AppendSynthNote(sound, cMajor.Note(-3), 0.25f);
            AppendSynthNote(sound, cMajor.Note(-2), 0.25f);
            AppendSynthNote(sound, cMajor.Note(-1), 0.25f);
            AppendSynthNote(sound, cMajor.Note(0), 0.25f);
            AppendSynthNote(sound, cMajor.Note(14), 0.25f);
            AppendSynthNote(sound, cMajor.Note(-14), 0.25f);
            soundCreator.CreateSoundBinFile(sound.ToArray(), "output.bin");
            SoundPlayer soundPlayer = new SoundPlayer();
            soundPlayer.PlaySoundBinFile("output.bin");
        }
    }
}
