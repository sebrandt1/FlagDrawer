using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlagDrawer.Drawer
{
    //abstract class to inherit to each kind of flag object
    public abstract class Flag
    {
        //Width should be larger than height to get a laying rectangular flag
        //Protected accessor since we want to access them in inheriting classes but never outside
        protected int Width => Height * 2;
        protected int Height { get; set; }
        protected FlagDirection Type { get; set; }
        protected ConsoleColor[] Colors { get; set; }

        //Possibly want to override this in more complex flag inheritors
        public virtual void DrawFlag()
        {
            switch(this.Type)
            {
                case FlagDirection.Horizontal:
                    DrawHorizontalFlag();
                    break;

                case FlagDirection.Vertical:
                    DrawVerticalFlag();
                    break;
            }
        }

        public Flag(int scale, FlagDirection type = FlagDirection.None)
        {
            this.Height = scale;
            this.Type = type;
        }

        //only change color on correct Y-axis block
        protected void DrawHorizontalFlag()
        {
            AdjustProperDimensions();
            int blocks = CalculateBlocks();

            for(int y = 0; y < this.Height; y++)
            {
                for(int x = 0; x < this.Width; x++)
                {
                    Draw(blocks, y);
                }
                Console.WriteLine();
            }
        }
        
        //only change color on correct X-axis block
        protected void DrawVerticalFlag()
        {
            AdjustProperDimensions();
            int blocks = CalculateBlocks();

            for (int y = 0; y < this.Height; y++)
            {
                for (int x = 0; x < this.Width; x++)
                {
                    Draw(blocks, x);
                }
                Console.WriteLine();
            }
        }

        //Blocks is how many color blocks we split the flag up in
        //So 2 blocks of a 6x vertical flag will have 3x of color x on left side and 3x of color y on right side
        private void Draw(int blocks, int index)
        {
            if (index < blocks) Console.BackgroundColor = this.Colors[0];
            else if (index >= blocks && index < blocks * 2) Console.BackgroundColor = this.Colors[1];

            //if this instance is TriColorFlag we have an expected 3rd color which should be set on the 3rd block
            if (this is TriColorFlag)
            {
                if (index >= blocks * 2) Console.BackgroundColor = this.Colors[2];
            }
            Console.Write("  ");
        }

        public void SetColors(params ConsoleColor[] colors)
        {
            if(this is TriColorFlag && colors.Length != 3 || (this is TwoColorFlag || this is CrossFlag) && colors.Length != 2)
            {
                throw new IndexOutOfRangeException();
            }
            this.Colors = colors;
        }

        //Use this to calculate how many color blocks we should split the flag into
        //Horizontal tricolor flag 6x height will be 2x color A, 2x color B, 2x Color C on the Y-axis
        private int CalculateBlocks()
        {
            switch(this.Type)
            {
                case FlagDirection.Horizontal:
                    return this.Height / this.Colors.Length;

                case FlagDirection.Vertical:
                    return this.Width / this.Colors.Length;

                default:
                    throw new ArgumentException();
            }
        }

        //Use this to adjust the flag scale for even color distribution
        protected virtual void AdjustProperDimensions()
        {
            while (!(this.Height % 2 == 0))
            {
                this.Height++;
            }
        }
    }

    public enum FlagDirection
    {
        Horizontal,
        Vertical,
        None //If type is cross we'll use none as direction
    }

    public enum FlagType
    {
        TriColor,
        TwoColor,
        Cross
    }
}
