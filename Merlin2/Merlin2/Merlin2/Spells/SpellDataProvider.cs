using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Merlin2.Spells
{
    public class SpellDataProvider : ISpellDataProvider
    {
        private static SpellDataProvider instance = null;
        private Dictionary<string, SpellInfo> spellInfo = null;
        private Dictionary<string, int> spellEffects = null;

        private SpellDataProvider()
        {
        }

        public static SpellDataProvider GetInstance()
        {
            if (instance == null)
            {
                instance = new SpellDataProvider();
            }

            return instance;
        }

        private Dictionary<string, SpellInfo> LoadSpellInfo()
        {
            List<string> lines = File.ReadAllLines("resources/spells/spells.csv").Skip(1).ToList();
            Dictionary<string, SpellInfo> dictionary = new Dictionary<string, SpellInfo>();

            foreach (string line in lines)
            {
                try
                {
                    SpellInfo spellInfo = line;
                    dictionary.Add(spellInfo.Name, spellInfo);
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e);
                }
            }

            return dictionary;
        }

        public Dictionary<string, int> GetSpellEffects()
        {
            if (spellEffects == null)
            {
                spellEffects = LoadSpellEffects();
            }

            return spellEffects;
        }

        public Dictionary<string, SpellInfo> GetSpellInfo()
        {
            if (spellInfo == null)
            {
                spellInfo = LoadSpellInfo();
            }
            return spellInfo;
        }

        private Dictionary<string, int> LoadSpellEffects()
        {
            string json = File.ReadAllText("resources/spells/effects.json");
            List<SpellEffect> effects = JsonConvert.DeserializeObject<List<SpellEffect>>(json);
            Dictionary<string, int> spellEffects = new Dictionary<string, int>();

            effects.ForEach(ef => spellEffects.Add(ef.Name, ef.Cost));
            return spellEffects;
        }

        private class SpellEffect
        {
            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("cost")]
            public int Cost { get; set; }
        }
    }
}