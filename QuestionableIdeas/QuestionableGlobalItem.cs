using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace QuestionableIdeas
{
    public class QuestionableGlobalItem : GlobalItem
    {
        public override void SetDefaults(Item item)
        {
            // Set maximum stack size for all items except coins
            if (item.type != ItemID.CopperCoin && item.type != ItemID.SilverCoin && item.type != ItemID.GoldCoin && item.type != ItemID.PlatinumCoin)
            {
                item.maxStack = 4206942;
            }

            // Set custom sound for Spiky Ball
            if (item.type == ItemID.SpikyBall)
            {
                item.UseSound = new SoundStyle("QuestionableIdeas/Sounds/SpikyBall");
            }

            // Set custom sound for ranged weapons using bullets with useTime >= 30
            if (item.DamageType == DamageClass.Ranged && item.useAmmo == AmmoID.Bullet && item.useTime >= 30)
            {
                item.UseSound = new SoundStyle("QuestionableIdeas/Sounds/BOOM");
            }
        }

        public override void HoldItem(Item item, Player player)
        {
            if (ModLoader.TryGetMod("TerrariaOverhaul", out Mod overhaulMod) && item.damage > 0)
            {
                switch (Main.rand.Next(5))
                {
                    case 0:
                        item.DamageType = DamageClass.Melee;
                        break;
                    case 1:
                        item.DamageType = DamageClass.Ranged;
                        break;
                    case 2:
                        item.DamageType = DamageClass.Magic;
                        break;
                    case 3:
                        item.DamageType = DamageClass.Summon;
                        break;
                    case 4:
                        item.DamageType = DamageClass.Throwing;
                        break;
                }

                item.damage = Main.rand.Next(5, 9999);
                item.useTime = Main.rand.Next(5, 60);
                item.useAnimation = item.useTime;
                item.knockBack = Main.rand.Next(15);
                item.shootSpeed = Main.rand.Next(25);
            }

        }


        public override void UpdateEquip(Item item, Player player)
        {
            if (ModLoader.TryGetMod("TerrariaOverhaul", out Mod overhaulMod) && item.accessory)
            {
                item.defense = Main.rand.Next(25);
                player.GetDamage(DamageClass.Melee) += Main.rand.NextFloat(1f);
                player.GetDamage(DamageClass.Ranged) += Main.rand.NextFloat(1f);
                player.GetDamage(DamageClass.Magic) += Main.rand.NextFloat(1f);
                player.GetDamage(DamageClass.Throwing) += Main.rand.NextFloat(1f);
                player.GetDamage(DamageClass.Summon) += Main.rand.NextFloat(1f);
            }
        }
    }
}
