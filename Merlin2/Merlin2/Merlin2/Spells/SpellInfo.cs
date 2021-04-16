using System;
using System.Collections.Generic;

namespace Merlin2.Spells
{
    public class SpellInfo
    {
        public string Name { get; set; }
        public SpellType SpellType { get; set; }
        public IEnumerable<string> EffectNames { get; set; }
        public string AnimationPath { get; set; }
        public int AnimationWidth { get; set; }
        public int AnimationHeight { get; set; }

        public static implicit operator SpellInfo(string data)
        {
            SpellInfo spellInfo = new SpellInfo();
            string[] splitData = data.Split(";");
            spellInfo.Name = splitData[0];
            if (splitData[1] == "projectile")
            {
                spellInfo.SpellType = SpellType.Projectile;
            }
            else
            {
                spellInfo.SpellType = SpellType.SelfCast;
            }

            spellInfo.AnimationPath = splitData[2];
            spellInfo.AnimationWidth = int.Parse(splitData[3]);
            spellInfo.AnimationHeight = int.Parse(splitData[4]);
            spellInfo.EffectNames = splitData[5].Split(",");
            Console.WriteLine(spellInfo.EffectNames);
            //magic & stuff
            return spellInfo;
        }
    }
}