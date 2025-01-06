using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace QuestionableIdeas
{
    public class QuestionableProjectile : GlobalProjectile
    {
        public override void AI(Projectile projectile)
        {
            if (projectile.type == 99)
            {
                Player player = Main.LocalPlayer;
                projectile.tileCollide = false;
                projectile.velocity += projectile.DirectionTo(player.Center);
            }

            if (projectile.velocity.Y > 0f)
            {
                int tileX = (int)(projectile.Center.X / 16);
                int tileY = (int)(projectile.Center.Y / 16);
                Tile tile = Main.tile[tileX, tileY];

                if (tile != null && tile.HasTile && tile.TileType == 19 && projectile.tileCollide)
                {
                    projectile.Kill();
                }
            }
        }

        public override void OnHitPlayer(Projectile projectile, Player target, Player.HurtInfo info)
        {
            base.OnHitPlayer(projectile, target, info);
            if (projectile.type == 99)
            {
                projectile.active = false;
            }
        }

        public override bool PreKill(Projectile projectile, int timeLeft)
        {
            if (projectile.type == 183)
            {
                for (int i = 0; i < 11; i++)
                {
                    Projectile.NewProjectileDirect(projectile.GetSource_FromAI(), projectile.Center, new Vector2(Main.rand.NextFloat(-10f, 10f), Main.rand.NextFloat(-10f, 10f)), ModContent.ProjectileType<TheHungryProj>(), 15, 1f, projectile.owner);
                }

                SoundEngine.PlaySound(SoundID.Item14, projectile.Center);
                projectile.active = false;
                return false;
            }

            return true;
        }
    }
}
