using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace QuestionableIdeas;

public class TheHungryProj : ModProjectile
{
    public override void SetStaticDefaults()
    {
        ProjectileID.Sets.TrailCacheLength[Type] = 5;
        ProjectileID.Sets.TrailingMode[Type] = 0;
    }

    public override void SetDefaults()
    {
        Projectile.friendly = true;        // Friendly projectile
        Projectile.DamageType = DamageClass.Melee;  // Melee damage type
        Projectile.timeLeft = 120;        // Time before the projectile disappears
        Projectile.width = 28;            // Width of the projectile
        Projectile.height = 28;           // Height of the projectile
        Projectile.penetrate = 1;         // Can penetrate once
        Projectile.ignoreWater = true;    // Ignore water
        Projectile.alpha = 50;            // Transparency
    }

    public override void AI()
    {
        // Apply gravity and slow down horizontal velocity
        Projectile.velocity.Y += 0.3f;
        Projectile.velocity.X *= 0.92f;

        // Set rotation to match velocity direction
        Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
    }
}
