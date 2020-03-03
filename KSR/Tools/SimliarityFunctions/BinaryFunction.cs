namespace KSR.Tools.SimliarityFunctions
{
    public class BinaryFunction : ISimiliarityFunction
    {
        // If first == second return true
        public decimal CalculateSimilairty(string firstWord, string secondWord)
        {
            if (firstWord == secondWord)
                return 1;
            return 0;
        }

        public override string ToString()
        {
            return "Binary Function";
        }
    }
}