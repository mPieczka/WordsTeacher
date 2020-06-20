using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureSpeechSyntezator.Services
{
    public class AudioService
    {
        public void LouderWavFile(string path, float ratio)
        {
            var wave = new WaveFileReader(path);
            float[] samples = new float[wave.SampleCount];
            var sample = wave.ReadNextSampleFrame();
            int i = 0;
            while (sample != null)
            {
                samples[i] = sample[0] * ratio;
                sample = wave.ReadNextSampleFrame();
                i++;
            }

            WaveFormat waveFormat = wave.WaveFormat;
            wave.Dispose();
            WaveFileWriter writer = new WaveFileWriter(path, waveFormat);
            for (int u = 0; u < i; u++)
            {
                writer.WriteSample(samples[u]);
            }
            writer.Dispose();
        }
    }
}