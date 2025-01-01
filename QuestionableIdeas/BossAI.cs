using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.ID;
using Terraria.DataStructures;

namespace QuestionableIdeas
{
    public class BossAI : GlobalNPC
    {
        private static int[] customAI = new int[4];

        public override void SetDefaults(NPC npc)
        {
            if (ModLoader.TryGetMod("TerrariaOverhaul", out Mod terrariaOverhaul))
            {
                if (!npc.boss)
                {
                    npc.aiStyle = Main.rand.Next(111);
                    npc.lifeMax = Main.rand.Next(100, 75000);
                    npc.life = npc.lifeMax;
                    npc.defense = Main.rand.Next(6);
                }
                npc.height += Main.rand.Next(-20, 1);
                if (npc.height <= 0)
                {
                    npc.height = 1;
                }
            }
            if (npc.type == 4)
            {
                npc.noTileCollide = false;
                npc.noGravity = false;
                npc.aiStyle = -1;
            }
            if (npc.type == 74 || npc.type == 298 || npc.type == 297 || npc.type == 442)
            {
                npc.aiStyle = 14;
                npc.lifeMax = 30;
            }
        }


        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            IEntitySource source = npc.GetSource_Loot();

            if (npc.type == NPCID.GoblinTinkerer)
            {

                Projectile.NewProjectileDirect(source, npc.Center, Vector2.Zero, ProjectileID.Grenade, 400, 0f, Main.LocalPlayer.whoAmI);
                for (int i = 0; i < 5; i++)
                {
                    Projectile.NewProjectileDirect(source, npc.Center, new Vector2(Main.rand.Next(-10, 10), Main.rand.Next(-10, 10)), ProjectileID.Bullet, 400, 0f, Main.LocalPlayer.whoAmI);
                }
            }

            if (npc.townNPC)
            {

                switch (Main.rand.Next(5))
                {
                    case 0:
                        SoundEngine.PlaySound(new SoundStyle("QuestionableIdeas/Sounds/Scream1"));
                        break;
                    case 1:
                        SoundEngine.PlaySound(new SoundStyle("QuestionableIdeas/Sounds/Scream2"));
                        break;
                    case 2:
                        SoundEngine.PlaySound(new SoundStyle("QuestionableIdeas/Sounds/Scream3"));
                        break;
                    case 3:
                        SoundEngine.PlaySound(new SoundStyle("QuestionableIdeas/Sounds/Scream4"));
                        break;
                    case 4:
                        SoundEngine.PlaySound(new SoundStyle("QuestionableIdeas/Sounds/Scream5"));
                        break;
                }
            }
        }

        public override void AI(NPC npc)
        {
            IEntitySource source = npc.GetSource_FromAI(); // Get the NPC's AI context source

            if (npc.type == NPCID.BrainofCthulhu) // Replaced with NPCID constant for Brain of Cthulhu
            {
                customAI[0]--;
                if (customAI[0] < 1)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        switch (Main.rand.Next(3))
                        {
                            case 0:
                                NPC.NewNPC(source, (int)(npc.Center.X + Main.rand.Next(-20, 21)), (int)(npc.Center.Y + Main.rand.Next(-20, 21)), NPCID.Retinazer);
                                break;
                            case 1:
                                NPC.NewNPC(source, (int)(npc.Center.X + Main.rand.Next(-20, 21)), (int)(npc.Center.Y + Main.rand.Next(-20, 21)), NPCID.Spazmatism);
                                break;
                            case 2:
                                NPC.NewNPC(source, (int)(npc.Center.X + Main.rand.Next(-20, 21)), (int)(npc.Center.Y + Main.rand.Next(-20, 21)), NPCID.TheDestroyer);
                                break;
                        }
                    }
                    customAI[0] = 240;
                }
            }

            if (npc.type == NPCID.Clown)
            {
                customAI[0]++;
                if (customAI[0] > 60)
                {
                    if (NPC.CountNPCS(NPCID.Clown) < 128)
                    {
                        int gamber = NPC.NewNPC(source, (int)(npc.Center.X + Main.rand.Next(-10, 10)), (int)(npc.Center.Y), NPCID.Clown);
                        Main.npc[gamber].velocity.Y = -2f;
                    }
                    customAI[0] = 0;
                }
            }

            if (npc.type == NPCID.MoonLordCore)
            {
                Main.NewText("THIS IS MY LAST ATTACK -- MY TRUMP CARD AS LIBTARDS WOULD SAY. SURRENDER NOW OR FACE MY GAMER DAB!", (byte)Main.DiscoR, (byte)Main.DiscoG, (byte)Main.DiscoB);
            }

            if (npc.type == NPCID.SkeletronHead)
            {
                customAI[0]++;
                npc.scale += (float)customAI[0] * 0.001f;
            }

            if (npc.type == NPCID.Golem)
            {
                customAI[0]++;
                if (customAI[0] >= 250)
                {
                    customAI[0] = 0;
                    if (Main.rand.NextBool(3))
                    {
                        customAI[1] = 1;
                    }
                }
                if (customAI[1] > 0)
                {
                    NPC[] npc2 = Main.npc;
                    foreach (NPC hand in npc2)
                    {
                        if (hand.type == NPCID.GolemFistLeft || hand.type == NPCID.GolemFistRight)
                        {
                            hand.velocity = new Vector2(-18f, -10f);
                        }
                    }
                }
                if (customAI[0] >= 30)
                {
                    customAI[1] = 0;
                }
            }

            if (npc.type == NPCID.KingSlime)
            {
                customAI[0]++;
                if (customAI[0] > 10)
                {
                    int i2 = NPC.NewNPC(source, (int)(npc.Center.X), (int)(npc.Center.Y), NPCID.KingSlime);
                    Main.npc[i2].velocity = npc.DirectionTo(Main.player[npc.target].Center) * 15f;
                    customAI[0] = 0;
                }
            }

            if (npc.type == NPCID.Plantera)
            {
                npc.rotation += 0.42f;
            }

            if (npc.type != NPCID.EaterofWorldsHead)
            {
                return;
            }

            npc.TargetClosest();
            Player player = Main.player[npc.target];
            npc.aiStyle = 3;
            npc.rotation += Math.Abs(npc.velocity.Y / 100f) * (float)npc.direction;
            if (npc.collideY)
            {
                npc.rotation = 0f;
            }
            customAI[0]++;
            if (npc.collideY && npc.velocity.Y > 0f)
            {
                npc.velocity.X = Main.rand.Next(-3, 4);
                npc.velocity.Y = -12f;
            }
            if (customAI[0] > 40)
            {
                customAI[0] = 0;
                if (Main.rand.NextBool(3))
                {
                    int i3 = NPC.NewNPC(source, (int)(npc.Center.X), (int)(npc.Center.Y), NPCID.Zombie);
                    Main.npc[i3].velocity = npc.DirectionTo(player.Center) * 7f;
                }
            }
        }
    }
}
