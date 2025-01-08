using Terraria;
using Terraria.ModLoader;

namespace QuestionableIdeas.QuestionableIdeas
{
    // The ModPlayer class where we handle the CBF detection logic
    public class QuestionableCBFDetectorPlayer : ModPlayer
    {
        private bool itemUsedThisFrame = false;

        public override void PreUpdate()
        {
            base.PreUpdate();

            // Reset the flag every frame
            itemUsedThisFrame = false;
        }

        public override void PostUpdate()
        {
            base.PostUpdate();

            // Check if the player used an item and it's a fast pickaxe
            if (itemUsedThisFrame && Player.HeldItem != null && IsFast(Player.HeldItem))
            {
                // Display the message if CBF is detected
                Main.NewText("CBF Detected, Loser! Click Between Frames is illegitimate and will not be allowed for use in Terraria. Please disable the mod in order to continue playing.", 255, 0, 0);

                // Remove the pickaxe from the player's inventory
                Player.HeldItem.TurnToAir();
            }
        }

        public override bool CanUseItem(Item item)
        {
            // Detect when the player uses an item (i.e., when they click to use it)
            if (item != null && IsFast(item))
            {
                itemUsedThisFrame = true;
            }

            return base.CanUseItem(item);
        }

        // Function to check if the item is a fast pickaxe or drill
        private bool IsFast(Item item)
        {
            return item.useTime == 1;
        }
    }

    // The main mod class
    public class QuestionableIdeas : Mod
    {
        public QuestionableIdeas()
        {
            // Constructor to initialize the mod
        }

        public override void Load()
        {
            // Register the QuestionableCBFDetectorPlayer for player-specific updates
            ModContent.GetInstance<QuestionableCBFDetectorPlayer>();
        }

        public override void Unload()
        {
            // Clean up if necessary when the mod is unloaded
        }
    }
}
