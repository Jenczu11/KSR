namespace KSR.Tools.SimliarityFunctions
{
    public interface ISimiliarityFunction
    { 
        decimal CalculateSimilarity(string firstWord, string secondWord);
    }
}