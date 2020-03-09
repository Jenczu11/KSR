namespace KSR.Tools.SimliarityFunctions
{
    public interface ISimilarityFunction
    { 
        decimal CalculateSimilarity(string firstWord, string secondWord);
    }
}