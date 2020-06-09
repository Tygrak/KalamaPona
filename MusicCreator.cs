using System;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;

namespace KalamaPona {
    public class MusicCreator {
        public SoundCreator soundCreator {get; set;} = new SoundCreator();
        public float Bpm {get; set;} = 120;
        public List<float> ResultWave {get; set;} = new List<float>();

        public float[] CreateSynthNote(float hz, float beats, float volume = 1) {
            float duration = (beats*60)/Bpm;
            var waveA = soundCreator.AverageWaves(soundCreator.SinWave(hz, duration, volume), soundCreator.SinWave(hz*2, duration, volume));
            var waveB = soundCreator.SquareWave(hz, duration, volume);
            return soundCreator.AdsrEnvelope(soundCreator.LowPass(soundCreator.AverageWaves(waveA, waveB), 5, 50), 0.1f, 0.15f, 0.7f, 0.1f);
            //return soundCreator.AdsrEnvelope(soundCreator.AverageWaves(waveA, waveB), 0.1f, 0.15f, 0.7f, 0.1f);
        }

        public float[] CreateBassNote(float hz, float beats, float volume = 1) {
            float duration = (beats*60)/Bpm;
            var waveA = soundCreator.AverageWaves(soundCreator.SinWave(hz, duration, volume), soundCreator.SinWave(hz*2, duration, volume));
            var waveB = soundCreator.SquareWave(hz, duration, volume);
            return soundCreator.AdsrEnvelope(soundCreator.AverageWaves(waveA, waveB), 0.05f, 0.05f, 0.8f, 0.05f);
        }

        public float[] CreateRest(float beats) {
            float duration = (beats*60)/Bpm;
            return soundCreator.Rest(duration);
        }

        public void AddWaveAt(float[] wave, float beatsPosition) {
            int position = (int) (soundCreator.SampleRate*beatsPosition*60/Bpm);
            if (ResultWave.Count <= position) {
                ResultWave.AddRange(new float[position-ResultWave.Count]);
            }
            for (int i = 0; i < wave.Length; i++) {
                if (ResultWave.Count <= position+i) {
                    ResultWave.Add(wave[i]);
                } else {
                    ResultWave[position+i] += wave[i];
                }
            }
        }
    }
}
