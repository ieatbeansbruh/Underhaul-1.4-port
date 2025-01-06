using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace QuestionableIdeas.QuestionableMenuTheme
{
    public class QuestionableMenuTheme : ModMenu
    {
        private const string menuAssetPath = "QuestionableIdeas";

        private Asset<Texture2D> LogoTexture;

        public override void Load()
        {
            LogoTexture = ModContent.Request<Texture2D>($"{menuAssetPath}/SupremeTerraria");
            ModContent.GetInstance<QuestionableMenuTheme>();
        }

        public override Asset<Texture2D> Logo => LogoTexture;

        public override string DisplayName => "Underhaul";
    }
}