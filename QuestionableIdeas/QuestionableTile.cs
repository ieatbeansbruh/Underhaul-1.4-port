using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.DataStructures;

namespace QuestionableIdeas;

public class QuestionableTile : GlobalTile
{
    public override void MouseOverFar(int i, int j, int type)
    {
        if (ModLoader.TryGetMod("TerrariaOverhaul", out Mod overhaul))
        {
            for (int iX = -2; iX <= 2; iX++)
            {
                for (int jY = -2; jY <= 2; jY++)
                {
                    Tile tile = Main.tile[i + iX, j + jY];
                    if (tile.HasTile && Main.rand.NextBool(10))
                    {
                        // I'm kinda stupid so I can't distort it the same as you could in 1.3, If you have a patch for this please dm my discord @cheezitofthetroll with the fix           
                        tile.TileFrameX = (short)(Main.rand.Next(0, 4) * 18);
                        tile.TileFrameY = (short)(Main.rand.Next(0, 4) * 18);
                    }
                }
            }
        }
    }

    public override void KillTile(int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem)
    {
      
        if (!ModLoader.TryGetMod("TerrariaOverhaul", out _)
            && Main.tile[i, j].TileType == TileID.Trees && !Main.tile[i, j - 1].HasTile && Main.rand.NextBool(8))
        {
         
            IEntitySource source = new EntitySource_TileBreak(i, j);

       
            for (int y = -7; y <= 7; y++)
            {
              
                int gamermomemt = NPC.NewNPC(source, i * 16, j * 16, NPCID.Squirrel);
                Main.npc[gamermomemt].velocity.X = y;
            }
        }
    }
}