using System;
using System.IO;
using System.Collections.Generic;

namespace KalamaPona {
    class Program {
        static void Main(string[] args) {
            TtetScale bassCMajor = new TtetScale(new float[] {36, 38, 40, 41, 43, 45, 47});
            TtetScale cMajor = new TtetScale(new float[] {60, 62, 64, 65, 67, 69, 71});
            MusicCreator musicCreator = new MusicCreator();
            MelodyGenerator melodyGenerator = new MelodyGenerator(bassCMajor);
            Random random = new Random();
            int seed = random.Next();
            Console.WriteLine($"Seed: {seed}");
            melodyGenerator.SetSeed(seed);
            List<float> arpeggio1 = new List<float> ();
            var melody = melodyGenerator.Generate(8, 2f, 0);
            foreach (var note in melody) {
                arpeggio1.AddRange(musicCreator.CreateBassNote(note.freq, note.duration));
            }
            List<float> arpeggio2 = new List<float> ();
            melodyGenerator.GeneratorScale = cMajor;
            melody = melodyGenerator.Generate(16, 1f, 2);
            foreach (var note in melody) {
                arpeggio2.AddRange(musicCreator.CreateSynthNote(note.freq, note.duration));
            }
            musicCreator.AddWaveAt(arpeggio1.ToArray(), 0.0f);
            musicCreator.AddWaveAt(arpeggio1.ToArray(), 8.0f);
            musicCreator.AddWaveAt(arpeggio1.ToArray(), 16.0f);
            musicCreator.AddWaveAt(arpeggio2.ToArray(), 8.0f);
            musicCreator.soundCreator.CreateSoundBinFile(musicCreator.ResultWave.ToArray(), "output.bin");
            SoundPlayer soundPlayer = new SoundPlayer();
            soundPlayer.PlaySoundBinFile("output.bin");
            //soundPlayer.CreateWavFromSoundBinFile("output.bin", "output.wav");
        }
    }
}
