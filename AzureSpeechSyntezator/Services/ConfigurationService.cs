using AzureSpeechSyntezator.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json.Serialization;

namespace AzureSpeechSyntezator.Services
{
    public class ConfigurationService
    {
        public SpeechConfiguration GetSpeechConfiguration()
        {
            var configurationStr = File.ReadAllText("../AzureSpeechSyntezator/Secrets/Speech.json");
            return JsonConvert.DeserializeObject<SpeechConfiguration>(configurationStr);
        }
    }
}