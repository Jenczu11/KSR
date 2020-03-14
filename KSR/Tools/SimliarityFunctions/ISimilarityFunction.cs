namespace KSR.Tools.SimliarityFunctions
{
    public interface ISimilarityFunction
    { 
        double CalculateSimilarity(string firstWord, string secondWord);
    }
}