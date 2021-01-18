namespace SDC.Tools.SimliarityFunctions
{
    public interface ISimilarityFunction
    { 
        double CalculateSimilarity(string firstWord, string secondWord);
    }
}