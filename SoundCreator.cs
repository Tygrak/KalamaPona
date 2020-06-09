using System;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;

namespace KalamaPona {
    public class SoundCreator {
        public float SampleRate {get; set;} = 48000;
        public float BaseVolume {get; set;} = 0.05f;

        public float[] AdsrEnvelope(float[] wave, float attack, float decay, float sustain, float release) {
            float[] result = new float[wave.Length];
            float attackTime = wave.Length*attack;
            float decayTime = wave.Length*decay+attackTime;
            float releaseStartTime = wave.Length-wave.Length*release;
            for (int i = 0; i < wave.Length; i++) {
                if (i < attackTime) {
                    result[i] = wave[i] * i/((float) wave.Length);
                } else if (i < decayTime) {
                    result[i] = wave[i] * Helpers.Lerp(1, sustain, (i-attackTime)/(decayTime-attackTime));
                } else if (i > releaseStartTime) {
                    result[i] = wave[i] * Helpers.Lerp(sustain, 0, (i-releaseStartTime)/(wave.Length-releaseStartTime));
                } else {
                    result[i] = wave[i] * sustain;
                }
            }
            return result;
        }

        public float[] LowPass(float[] wave, float dt, float rc) {
            // Return RC low-pass filter output samples, given input samples,
            // time interval dt, and time constant RC
            //function lowpass(real[0..n] x, real dt, real RC)
            float[] result = new float[wave.Length];
            //var real[0..n] y
            float alpha = dt / (rc + dt);
            result[0] = alpha * wave[0];
            for (int i = 1; i < wave.Length; i++) {
                result[i] = alpha * wave[i] + (1-alpha) * result[i-1];
            }  
            return result;
        }

        public float[] AddWaves(float[] a, float[] b) {
            int length = Math.Max(a.Length, b.Length);
            float[] result = new float[length];
            for (int i = 0; i < length; i++) {
                if (a.Length > i) {
                    result[i] += a[i];
                }
                if (b.Length > i) {
                    result[i] += b[i];
                }
            }
            return result;
        }

        public float[] AverageWaves(float[] a, float[] b) {
            int length = Math.Max(a.Length, b.Length);
            float[] result = new float[length];
            for (int i = 0; i < length; i++) {
                if (a.Length > i) {
                    result[i] += a[i]/2;
                }
                if (b.Length > i) {
                    result[i] += b[i]/2;
                }
            }
            return result;
        }

        public float[] SinWave(float hz, float duration, float volume = 1) {
            float step = (hz * 2 * MathF.PI) / SampleRate;
            float[] result = new float[(int) (SampleRate*duration)];
            for (int i = 0; i < result.Length; i++) {
                result[i] = MathF.Sin(i*step)*BaseVolume*volume;
            }
            return result;
        }

        public float[] SquareWave(float hz, float duration, float volume = 1) {
            float step = (hz * 2 * MathF.PI) / SampleRate;
            float[] result = new float[(int) (SampleRate*duration)];
            for (int i = 0; i < result.Length; i++) {
                result[i] = MathF.Sign(MathF.Sin(i*step))*BaseVolume*volume;
            }
            return result;
        }

        public float[] TriangleWave(float hz, float duration, float volume = 1) {
            float period = 1/hz;
            float[] result = new float[(int) (SampleRate*duration)];
            for (int i = 0; i < result.Length; i++) {
                float t = i/SampleRate;
                result[i] = (2*MathF.Abs(2*(t/period-MathF.Floor(t/period+0.5f)))-1)*BaseVolume*volume;
            }
            return result;
        }

        public float[] HarmonicSinWave(float hz, float duration, int harmonics) {
            float step = (hz * 2 * MathF.PI) / SampleRate;
            float[] result = new float[(int) (SampleRate*duration)];
            for (int i = 0; i < result.Length; i++) {
                for (int j = 1; j < harmonics+1; j++) {
                    float volume = BaseVolume*(1+harmonics-j)/(1+harmonics);
                    result[i] += MathF.Sin(i*step*j)*volume;
                }
            }
            return result;
        }

        public float[] Rest(float duration) {
            return new float[(int) (SampleRate*duration)];
        }

        public void CreateSoundBinFile(float[] sound, string path) {
            using (BinaryWriter binWriter = new BinaryWriter(File.Open(path, FileMode.Create))) {  
                for (int i = 0; i < sound.Length; i++) {
                    binWriter.Write(sound[i]);
                }
            }  
        }
    }
}
