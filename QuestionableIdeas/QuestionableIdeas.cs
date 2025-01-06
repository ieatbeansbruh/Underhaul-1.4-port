using Terraria;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.ID;

namespace QuestionableIdeas
{
    public class DungeonGuardianMusicEffect : ModSceneEffect
    {
        public override bool IsSceneEffectActive(Player player)
        {
            return Main.musicVolume > 0f && Main.myPlayer != -1 && !Main.gameMenu && player.active && NPC.AnyNPCs(NPCID.DungeonGuardian);
        }

        public override int Music
        {
            get
            {
                return MusicLoader.GetMusicSlot(Mod, "Sounds/Music/DungeonGuardian");
            }
        }
        public override SceneEffectPriority Priority => SceneEffectPriority.BossHigh;
    }
}
