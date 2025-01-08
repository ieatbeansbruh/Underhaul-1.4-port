using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.ID;
using Terraria.DataStructures;
using System.Linq;

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

        public override void OnKill(NPC npc)
        {
            if (npc.type == NPCID.Bunny)
            {
                IEntitySource source = npc.GetSource_Death();

                Projectile.NewProjectile(source, npc.Center, Vector2.Zero, ProjectileID.Grenade, 400, 0f, Main.myPlayer);

                for (int i = 0; i < 5; i++)
                {
                    Vector2 randomVelocity = new Vector2(Main.rand.NextFloat(-10f, 10f), Main.rand.NextFloat(-10f, 10f));
                    Projectile.NewProjectile(source, npc.Center, randomVelocity, ProjectileID.Bullet, 400, 0f, Main.myPlayer);
                }
            }

            if (npc.townNPC)
            {
                string soundPath = "QuestionableIdeas/Sounds/Scream" + (Main.rand.Next(1, 6));
                SoundEngine.PlaySound(new SoundStyle(soundPath));
            }

            base.OnKill(npc);
        }



        public override void AI(NPC npc)
        {
            IEntitySource source = npc.GetSource_FromAI();

            if (npc.type == NPCID.BrainofCthulhu)
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

            if (npc.type == NPCID.Worm)
            {
                customAI[0]++;
                if (customAI[0] > 60)
                {
                    if (NPC.CountNPCS(NPCID.Worm) < 128)
                    {
                        int gamber = NPC.NewNPC(source, (int)(npc.Center.X + Main.rand.Next(-10, 10)), (int)(npc.Center.Y), NPCID.Worm);
                        Main.npc[gamber].velocity.Y = -2f;
                    }
                    customAI[0] = 0;
                }
            }

            if (npc.type == NPCID.MoonLordCore)
            {
                Main.NewText("THIS IS MY LAST ATTACK -- MY TRUMP CARD AS LIBTARDS WOULD SAY. SURRENDER NOW OR FACE MY GAMER DAB!", (byte)Main.DiscoR, (byte)Main.DiscoG, (byte)Main.DiscoB);
            }

            if (npc.type == NPCID.KingSlime)
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

            if (npc.type == NPCID.WallofFlesh)
            {
                customAI[0]++;
                if (customAI[0] > 10)
                {
                    int i2 = NPC.NewNPC(source, (int)(npc.Center.X), (int)(npc.Center.Y), NPCID.Bee);
                    Main.npc[i2].velocity = npc.DirectionTo(Main.player[npc.target].Center) * 15f;
                    customAI[0] = 0;
                }
            }

            if (npc.type == NPCID.Spazmatism)
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