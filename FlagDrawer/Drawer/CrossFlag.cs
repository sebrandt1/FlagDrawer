using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlagDrawer.Drawer
{
    public class CrossFlag : Flag
    {
        public CrossFlag(int scale) : 
            base(scale)
        {

        }
        
        public override void DrawFlag()
        {
            AdjustProperDimensions();
            int xBlocks = (this.Width - 1) / 3;
            int yBlocks = this.Height / 3;

            for(int y = 0; y < this.Height; y++)
            {
                for(int x = 0; x < this.Width - 1; x++)
                {
                    if ((x > xBlocks && x <= xBlocks * 2) || (y >= yBlocks && y < yBlocks * 2)) Console.BackgroundColor = Colors[1];
                    else Console.BackgroundColor = Colors[0];
                    Console.Write($"  ");
                }
                Console.WriteLine();
            }
        }

        protected override void AdjustProperDimensions()
        {
            while ((this.Height % 2 == 0))
            {
                this.Height++;
            }
        }
    }
}
