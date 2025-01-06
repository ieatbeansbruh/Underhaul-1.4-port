using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ReLogic.Content;

namespace QuestionableIdeas.Content.Items.Tools
{
    public class Thicchammer : ModItem
    {
        private Asset<Texture2D> _customTexture;

        public override void SetDefaults()
        {
            Item.damage = 69420;
            Item.DamageType = DamageClass.Melee;
            Item.width = 178;
            Item.height = 178;
            Item.useTime = 10;
            Item.useAnimation = 60;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 15;
            Item.rare = ItemRarityID.Purple;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.scale = 2;

            Item.hammer = 500;
            Item.useTurn = true;
            Item.attackSpeedOnlyAffectsWeaponAnimation = true;
        }

        public override void Load()
        {
            _customTexture = ModContent.Request<Texture2D>("QuestionableIdeas/Items/Tools/Thicchammer");
        }

        public override void Unload()
        {
            _customTexture = null;
        }

        public override string Texture => "QuestionableIdeas/Items/Tools/Thicchammer";

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Wood, 500)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}