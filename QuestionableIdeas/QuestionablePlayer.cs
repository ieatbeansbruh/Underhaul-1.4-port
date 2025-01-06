using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.Audio;

namespace QuestionableIdeas
{
    public class QuestionablePlayer : ModPlayer
    {
        private int lifeCounter;

        public override void PostUpdate()
        {
            Player.AddBuff(BuffID.Gravitation, 20);
            Player.gravControl = true;

            if (PlayerInput.Triggers.JustPressed.Jump)
            {
                Player.gravDir = -Player.gravDir;
            }

            Player.maxFallSpeed = 50f;
            Player.gravity = 2f;

            if (Player.ZoneSkyHeight)
            {
                lifeCounter++;
                if (lifeCounter >= 7)
                {
                    Player.statLife--;
                    lifeCounter = 0;
                    if (Player.statLife <= 0)
                    {
                        PlayerDeathReason gay = new PlayerDeathReason
                        {
                            SourceCustomReason = "Wow, you big binch. You utter cretin. ((Chippy contemplates demonetization.)) You died in space? lol (lol)"
                        };
                        Player.KillMe(gay, 69.0, 0);
                    }
                }
            }
        }


        public override void OnHitByProjectile(Projectile proj, Player.HurtInfo hurtInfo)
        {
            base.OnHitByProjectile(proj, hurtInfo);
            if (proj.type == ProjectileID.SpikyBall)
            {
                foreach (NPC npc in Main.npc)
                {
                    if (npc.active && npc.type == NPCID.RuneWizard)
                    {
                        npc.life += 4500;
                        npc.HealEffect(4500, true);
                    }
                }
            }
        }

        public override void OnHitByNPC(NPC npc, Player.HurtInfo hurtInfo)
        {
         base.OnHitByNPC(npc, hurtInfo);
            SoundEngine.PlaySound(new SoundStyle("QuestionableIdeas/Sounds/SlapAAAA"), Player.position);
        }

        public override void UpdateLifeRegen()
        {
            Player.manaRegen = 0;
            Player.manaRegenBonus = 0;
            Player.manaRegenCount = 0;
        }

   //     public override void UpdateBiomeVisuals()
   //     {
   //     }

     //   public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
     //   {
      //  }

      //  public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
     //   {
    //    }
    }
}
