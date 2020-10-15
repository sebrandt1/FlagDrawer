using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlagDrawer.Drawer
{
    public class TriColorFlag : Flag
    {
        public TriColorFlag(int scale, FlagDirection type) :
            base(scale, type)
        {

        }

        //We want tricolor flag colors to be evenly distributed in 3 blocks
        //therefore we need height to be divisible by 3 (our width is height * 2 which will be divisible by 3 as well)
        protected override void AdjustProperDimensions()
        {
            while(!(this.Height % 3 == 0))
            {
                this.Height++;
            }
        }
    }
}
