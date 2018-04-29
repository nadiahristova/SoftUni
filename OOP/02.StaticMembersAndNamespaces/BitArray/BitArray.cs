namespace BitArray
{
    using System;

    class BitArray
    {
        private const int MaxBitsCount = 100000;
        private const int MinBitsCount = 1;

        public readonly int MaxNumber;
        public static int MinNumber = 0;

        private readonly int MaxAllowedBits;
        public static int MinAllowedBits = 1;

        public BitArray(int numAllowedBits)
        {
            if (numAllowedBits < MinBitsCount || numAllowedBits > MaxBitsCount)
                throw new ArgumentOutOfRangeException("This program does not support numbers with less than "
                    + MinBitsCount + " or greater than " + MaxBitsCount + " number of bits representation.");

            this.Number = MinNumber;
            this.MaxAllowedBits = numAllowedBits;
            this.MaxNumber = (int)Math.Pow(numAllowedBits, 2) - 1;
        }

        public int Number { get; private set; } //if we keep the bit data in int[] changing and retrieving it would be faster

        public byte this [int index]
        {
            get
                {
                    if (index < MinAllowedBits - 1 || index >= MaxAllowedBits)
                        throw new IndexOutOfRangeException("Index was out of range! Number of bits allowed for this array are in the range [" 
                            + (MinAllowedBits - 1) + ", " + (MaxAllowedBits - 1) + "].");

                    if ((this.Number & (1 << index)) == 0)
                        return 0;
                    else
                        return 1;
                }

            set
                {
                    if (index < MinAllowedBits - 1 || index >= MaxAllowedBits)
                        throw new IndexOutOfRangeException("Index was out of range! Number of bits allowed for this array are in the range ["
                            + (MinAllowedBits - 1) + ", " + (MaxAllowedBits - 1) + "].");

                    if (value != 0 && value != 1)
                        throw new ArgumentException("Bits are represented by 0 or 1.");

                    // Clear the bit at position index //see presentation
                    this.Number &= ~(1 << index);
                    // Set the bit at position index to value //see presentation
                    this.Number |= (value << index);
                }
        }
        
        public override string ToString()
        {
            return this.Number.ToString();
        }

        public string BitsSnapShot(bool shortVersion = true)
        {
            string bitsSS = null;

            int length = shortVersion ? BitLength(this.Number) : MaxAllowedBits;

            for (int i = length - 1; i >= 0; i--)
                bitsSS += this[i].ToString();

            return bitsSS;
        }

        private static int BitLength(int number)
        {
            return (int)Math.Floor(Math.Log(number, 2)) + 1;
        }
    }
}
