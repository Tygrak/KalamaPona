using System;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;

namespace KalamaPona {
    public class SoundPlayer {
        public float SampleRate {get; set;} = 48000;

        public void PlaySoundBinFile(string path) {
            var process = new Process() {
                StartInfo = new ProcessStartInfo {
                    FileName = "ffplay",
                    Arguments = $"-autoexit -showmode 1 -f f32le -ar {SampleRate} {path}",
                    WorkingDirectory = Directory.GetCurrentDirectory(),
                    //RedirectStandardOutput = true,
                    UseShellExecute = true,
                    CreateNoWindow = true,
                }
            };
            process.Start();
            //string result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
        }

        public void CreateWavFromSoundBinFile(string path, string outputPath) {
            var process = new Process() {
                StartInfo = new ProcessStartInfo {
                    FileName = "ffmpeg",
                    Arguments = $"-f f32le -ar {SampleRate} -i {path} {outputPath}",
                    WorkingDirectory = Directory.GetCurrentDirectory(),
                    //RedirectStandardOutput = true,
                    UseShellExecute = true,
                    CreateNoWindow = true,
                }
            };
            process.Start();
            process.WaitForExit();
        }
    }
}
