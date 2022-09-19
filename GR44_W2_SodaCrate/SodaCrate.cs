
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks.Dataflow;

namespace GR44_W2_SodaCrate
{
    internal class SodaCrate : Product
    {
        private SodaBottle?[,] SodaBottles; // a 2-dimensional array of undefined size
        public byte CrateRows { get; }
        public byte CrateColumns { get; }

        public SodaCrate(int ID, string Name, decimal Price, decimal DepositFee, byte rowsInCrate, byte columnsInCrate) : base(ID, Name, Price, DepositFee)
        {
            SodaBottles = new SodaBottle[rowsInCrate, columnsInCrate];
            CrateRows = rowsInCrate;
            CrateColumns = columnsInCrate;
        }

        public int EmptySlots => FindEmptySlots().Count;
        public bool IsEmpty => FindEmptySlots().Count == CrateRows * CrateColumns;
        public bool IsFull => FindEmptySlots().Count == 0;
        public int SodaBottleCount (SodaBottle? onlyKind = null) => FindBottles(onlyKind).Count;

        

        public void AddSodaBottles(int itemCount, SodaBottle itemKind)
        {
            if (itemCount < 1)
                throw new ArgumentException("3498576394");

            var theEmptySlots = FindEmptySlots();
            if (itemCount > theEmptySlots.Count)
                throw new Exception("398475983475");

            for (int i = 0; i < itemCount; i++)
                if (SodaBottles[theEmptySlots[i].row, theEmptySlots[i].col] is null) // Just making sure
                    SodaBottles[theEmptySlots[i].row, theEmptySlots[i].col] = itemKind;
                else
                    throw new Exception("7349856734985");
        }
        public void RemoveSodaBottles(int itemCount, SodaBottle itemKind)
        {
            if (itemCount < 1)
                throw new Exception("739487563984");

            var theSlotsContainingTheItemKind = FindBottles(itemKind);
            if (itemCount > theSlotsContainingTheItemKind.Count)
                throw new Exception("38945679384756");

            for (int i = 0; i < itemCount; i++)
                if (SodaBottles[theSlotsContainingTheItemKind[i].row, theSlotsContainingTheItemKind[i].col] == itemKind) // Just making sure
                    SodaBottles[theSlotsContainingTheItemKind[i].row, theSlotsContainingTheItemKind[i].col] = null;
                else
                    throw new Exception("83947509384");
        }

        private List<(int row, int col)> FindBottles(SodaBottle? onlyKind)
        {
            // Returns a list of "positions" in the crate that contains the the specified type of bottle (or any kind of bottle).
            List<(int row, int col)> list = new();
            for (int row = 0; row < CrateRows; row++)
                for (int col = 0; col < CrateColumns; col++)
                    if (SodaBottles[row, col] is not null && (onlyKind is null || SodaBottles[row, col] == onlyKind))
                        list.Add((row, col));

            return list;
        }
        private List<(int row, int col)> FindEmptySlots()
        {
            // Returns a list of "positions" in the crate that are empty.
            List<(int row, int col)> list = new();
            for (int row = 0; row < CrateRows; row++)
                for (int col = 0; col < CrateColumns; col++)
                    if (SodaBottles[row, col] is null)
                        list.Add((row, col));

            return list;
        }

        public void Visualize()
        {
            int pX = CursorLeft;
            int pY = CursorTop;

            for (int row = 0; row < CrateRows; row++)
            {
                for (int col = 0; col < CrateColumns; col++)
                {
                    Write(SodaBottles[row, col]?.Name.Substring(0, 1) ?? ".");
                    Write(" ");
                }
                WriteLine();
            }

            var total = CalculateTotalPrice();
            WriteLine($"{total.PriceSum} + {total.DepositSum} = {total.TotalSum}");

            SetCursorPosition(pX, pY);
        }

        public (decimal PriceSum, decimal DepositSum, decimal TotalSum) CalculateTotalPrice()
        {
            decimal priceSum = Price;
            decimal depositSum = DepositFee;

            for (int row = 0; row < CrateRows; row++)
            {
                for (int col = 0; col < CrateColumns; col++)
                {
                    priceSum += SodaBottles[row, col]?.Price ?? 0m;
                    depositSum += SodaBottles[row, col]?.DepositFee ?? 0m;
                }
            }

            return (priceSum, depositSum, priceSum + depositSum);

        }

    }

}
