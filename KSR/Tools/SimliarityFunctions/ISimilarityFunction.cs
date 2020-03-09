namespace KSR.Tools.SimliarityFunctions
{
    public interface ISimilarityFunction
    { 
        public decimal CalculateSimilarity(string firstWord, string secondWord);
    }
}