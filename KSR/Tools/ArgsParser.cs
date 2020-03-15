using System;
using KSR.Tools.Features;
using KSR.Tools.SimliarityFunctions;
using TwinFinder.Base.Extensions;

namespace KSR.Tools
{
    public class ArgsParser
    {
        public string[] args;

        public ArgsParser(string[] args)
        {
            Console.WriteLine(args.ToSingleString());
            this.args = args;
        }

        
        
        public void setSettings()
        {
            /* Zrobilem na szybko takie chaotyczne rozwiazanie aby moc puszczac testy skryptem
            Jezeli nie podasz argumentow to skorzysta z default w kodzie
            Jak podasz to w sumie leci podobnie jak w settingsach
            mozliwe ustawienia t/f to: args->
            [0]   [1]   [2]    [3]
            tttt tttttt tttttt tttttt
            [1] -> ustawia na bodyfeature
            [2] -> ustawia na first paragraph
            [3] -> ustawia na 30percentbody
            */
            if (args.Length == 0)
            {
                Console.WriteLine("Using Default settings defined in code");
            }
            else
            {
                Settings.featuresSettings.Clear();
                Console.WriteLine("Settings Cleared");
                Console.WriteLine(args.Length);
                if(args.Length>0)
                    setSettingsFirstArgument();
                if(args.Length>1 )
                    setSettingsSimilarityBodyFeature();
                if(args.Length>2)
                    setSettingsFirstParagraph();
                if(args.Length>3)
                    setSettings30PercentBody();
            }
            
            
        }
        public void setSettingsFirstArgument()
        {
            Console.WriteLine("setSettingsFirstArgument");
            var sw = args[0].ToCharArray();
            if (sw[0] == 't' || sw[0] == 'T')
            {
                Console.WriteLine("Added BinaryArticleBodyFeature");
                Settings.featuresSettings.Add(new BinaryArticleBodyFeature(), true);
            }
             if (sw[1] == 't' || sw[1] == 'T')
            {
                Console.WriteLine("Added KeyWords20PercentArticleBodyFeature");
                Settings.featuresSettings.Add(new KeyWords20PercentArticleBodyFeature(), true);
            }
             if (sw[2] == 't' || sw[2] == 'T')
            {
                Console.WriteLine("Added KeyWordsArticleBodyFeature");
                Settings.featuresSettings.Add(new BinaryArticleBodyFeature(), true);
            }
             if (sw[3] == 't' || sw[3] == 'T')
            {
                Console.WriteLine("Added KeyWordsFirstParagraphArticleBodyFeature");
                Settings.featuresSettings.Add(new KeyWordsFirstParagraphArticleBodyFeature(), true);
            }
        }

        public void setSettingsSimilarityBodyFeature()
        {
            Console.WriteLine("setSettingsSimilarityBodyFeature");
            Console.WriteLine(args[1].ToStringOrEmpty());
            var sw = args[1].ToCharArray();
            if (sw[0] == 't' || sw[0] == 'T')
            {
                Console.WriteLine("Added SimilarityBodyFeature->BinaryFunction");
                Settings.featuresSettings.Add(new SimilarityBodyFeature(){SimilarityFunction = new BinaryFunction()}, true);
            }
            if (sw[1] == 't' || sw[1] == 'T')
            {
                Console.WriteLine("Added SimilarityBodyFeature->JaccardFunction");
                Settings.featuresSettings.Add(new SimilarityBodyFeature(){SimilarityFunction = new JaccardFunction()}, true);
            }
            if (sw[2] == 't' || sw[2] == 'T')
            {
                Console.WriteLine("Added SimilarityBodyFeature->LCSFunction");
                Settings.featuresSettings.Add(new SimilarityBodyFeature(){SimilarityFunction = new LCSFunction()}, true);
            }
            if (sw[3] == 't' || sw[3] == 'T')
            {
                Console.WriteLine("Added SimilarityBodyFeature->LevenshteinFunction");
                Settings.featuresSettings.Add(new SimilarityBodyFeature(){SimilarityFunction = new LevenshteinFunction()}, true);
            }
            if (sw[4] == 't' || sw[4] == 'T')
            {
                Console.WriteLine("Added SimilarityBodyFeature->NGramFunction");
                Settings.featuresSettings.Add(new SimilarityBodyFeature(){SimilarityFunction = new NGramFunction()}, true);
            }
            if (sw[5] == 't' || sw[5] == 'T')
            {
                Console.WriteLine("Added SimilarityBodyFeature->NiewiadomskiFunction");
                Settings.featuresSettings.Add(new SimilarityBodyFeature(){SimilarityFunction = new NiewiadomskiFunction()}, true);
            }
        }
        
        public void setSettingsFirstParagraph()
        {
            Console.WriteLine("setSettingsFirstParagraph");
            var sw = args[2].ToCharArray();
            if (sw[0] == 't' || sw[0] == 'T')
            {
                Console.WriteLine("Added SimilarityFirstParagraph->BinaryFunction");
                Settings.featuresSettings.Add(new SimliarityFirstParagraph(){SimilarityFunction = new BinaryFunction()}, true);
            }
            if (sw[1] == 't' || sw[1] == 'T')
            {
                Console.WriteLine("Added SimliarityFirstParagraph->BinaryFunction");
                Settings.featuresSettings.Add(new SimliarityFirstParagraph(){SimilarityFunction = new JaccardFunction()}, true);
            }
            if (sw[2] == 't' || sw[2] == 'T')
            {
                Console.WriteLine("Added SimliarityFirstParagraph->BinaryFunction");
                Settings.featuresSettings.Add(new SimliarityFirstParagraph(){SimilarityFunction = new LCSFunction()}, true);
            }
            if (sw[3] == 't' || sw[3] == 'T')
            {
                Console.WriteLine("Added SimliarityFirstParagraph->BinaryFunction");
                Settings.featuresSettings.Add(new SimliarityFirstParagraph(){SimilarityFunction = new LevenshteinFunction()}, true);
            }
            if (sw[4] == 't' || sw[4] == 'T')
            {
                Console.WriteLine("Added SimliarityFirstParagraph->BinaryFunction");
                Settings.featuresSettings.Add(new SimliarityFirstParagraph(){SimilarityFunction = new NGramFunction()}, true);
            }
            if (sw[5] == 't' || sw[5] == 'T')
            {
                Console.WriteLine("Added SimliarityFirstParagraph->BinaryFunction");
                Settings.featuresSettings.Add(new SimliarityFirstParagraph(){SimilarityFunction = new NiewiadomskiFunction()}, true);
            }
        }
        
        public void setSettings30PercentBody()
        {
            var sw = args[3].ToCharArray();
            if (sw[0] == 't' || sw[0] == 'T')
            {
                Console.WriteLine("Added Simliarity30PercentBody->BinaryFunction");
                Settings.featuresSettings.Add(new Simliarity30PercentBody(){SimilarityFunction = new BinaryFunction()}, true);
            }
            if (sw[1] == 't' || sw[1] == 'T')
            {
                Console.WriteLine("Added Simliarity30PercentBody->BinaryFunction");
                Settings.featuresSettings.Add(new Simliarity30PercentBody(){SimilarityFunction = new JaccardFunction()}, true);
            }
            if (sw[2] == 't' || sw[2] == 'T')
            {
                Console.WriteLine("Added Simliarity30PercentBody->BinaryFunction");
                Settings.featuresSettings.Add(new Simliarity30PercentBody(){SimilarityFunction = new LCSFunction()}, true);
            }
            if (sw[3] == 't' || sw[3] == 'T')
            {
                Console.WriteLine("Added Simliarity30PercentBody->BinaryFunction");
                Settings.featuresSettings.Add(new Simliarity30PercentBody(){SimilarityFunction = new LevenshteinFunction()}, true);
            }
            if (sw[4] == 't' || sw[4] == 'T')
            {
                Console.WriteLine("Added Simliarity30PercentBody->BinaryFunction");
                Settings.featuresSettings.Add(new Simliarity30PercentBody(){SimilarityFunction = new NGramFunction()}, true);
            }
            if (sw[5] == 't' || sw[5] == 'T')
            {
                Console.WriteLine("Added Simliarity30PercentBody->BinaryFunction");
                Settings.featuresSettings.Add(new Simliarity30PercentBody(){SimilarityFunction = new NiewiadomskiFunction()}, true);
            }
        }
    }
}