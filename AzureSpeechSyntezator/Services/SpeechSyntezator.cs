using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureSpeechSyntezator.Services
{
    public class SpeechSyntezator
    {
        private readonly ConfigurationService _configurationService;
        private readonly AudioService _audioService;

        public SpeechSyntezator(ConfigurationService configurationService, AudioService audioService)
        {
            _configurationService = configurationService;
            _audioService = audioService;
        }

        public async Task<string> Speak(string text)
        {
            var configuration = _configurationService.GetSpeechConfiguration();
            var config = SpeechConfig.FromSubscription(configuration.Key1, configuration.Location);
            config.SpeechSynthesisLanguage = "en-GB";
            string filePath = $"../AzureSpeechSyntezator/Output/{text}.wav";
            if (!File.Exists(filePath))
            {
                using (var audioConfig = AudioConfig.FromWavFileOutput(filePath))
                using (var synthesizer = new SpeechSynthesizer(config, audioConfig))
                {
                    await synthesizer.SpeakTextAsync(text).ConfigureAwait(false);
                }
                _audioService.LouderWavFile(filePath, 1.5f);
            }
            return Path.GetFullPath(filePath);
        }
    }
}