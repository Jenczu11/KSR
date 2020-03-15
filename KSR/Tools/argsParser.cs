using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using KSR.Tools.Features;
using KSR.Tools.Frequency;
using KSR.Tools.SimliarityFunctions;
using NDesk.Options;
using NumSharp.Extensions;
using TwinFinder.Base.Extensions;

namespace KSR.Tools
{
    public class argsParser
    {
        static int verbosity;
        Dictionary<string, string> options = new Dictionary<string, string>();

        public argsParser(string[] args)
        {
            bool show_help = false;

            #region OptionSet

            var p = new OptionSet()
            {
                {
                    "f1|feature1=", "True/False for first feature set",
                    v =>
                    {
                        if (v.Length == 4)
                        {
                            options.Add("f1", v);
                        }
                        else
                        {
                            throw new OptionException("Feature set 1 is not equal to 4 values", "-f1");
                        }
                    }
                },
                {
                    "f2|feature2=", "True/False for second feature set",
                    v =>
                    {
                        if (v.Length == 6)
                        {
                            options.Add("f2", v);
                        }
                        else
                        {
                            throw new OptionException("Feature set 2 is not equal to 6 values", "-f2");
                        }
                    }
                },
                {
                    "f3|feature3=", "True/False for third feature set",
                    v =>
                    {
                        if (v.Length == 6)
                        {
                            options.Add("f3", v);
                        }
                        else
                        {
                            throw new OptionException("Feature set 6 is not equal to 6 values", "-f4");
                        }
                    }
                },
                {
                    "f4|feature4=", "True/False for fourth feature set",
                    v =>
                    {
                        if (v.Length == 6)
                        {
                            options.Add("f4", v);
                        }
                        else
                        {
                            throw new OptionException("Feature set 4 is not equal to 6 values", "-f4");
                        }
                    }
                },
                {
                    "t|testingpercentage=",
                    "Percentage of testing data " +
                    "this must be an integer.",
                    (int v) => Settings.testingPercentage = v
                },
                {
                    "l|learningpercentage=",
                    "Percentage of learning data " +
                    "this must be an integer.",
                    (int v) => Settings.learningPercentage = v
                },
                {
                    "k|kneighbors=",
                    "Number of neighbours for knn " +
                    "this must be an integer.",
                    (int v) => Settings.kNNNeighbours = v
                },
                {
                    "kw|keywords=",
                    "Number of keyWords to search " +
                    "this must be an integer.",
                    (int v) => Settings.keyWords = v
                },
                {
                    "kwe|keywordsExtractor=",
                    "Choose keyWordExtractor from (1->3)\n" +
                    "1 -> TFFrequency\n" +
                    "2 -> TDFrequency\n" +
                    "3 -> IDFFrequency\n" +
                    "this must be an integer.",
                    (int v) =>
                    {
                        if (v >= 1 && v <= 3)
                            options.Add("kwe", v.ToString());
                        else
                        {
                            throw new OptionException("keyWordExtractor out of bounds", "-kew");
                        }
                    }
                },
                {
                    "v", "increase debug message verbosity",
                    v =>
                    {
                        if (v != null) ++verbosity;
                    }
                },
                {
                    "h|help", "show this message and exit",
                    v => show_help = v != null
                },
            };
            

            #endregion
            
            List<string> extra;
            try
            {
                extra = p.Parse(args);
            }
            catch (OptionException e)
            {
                Console.Write("Error: ");
                Console.WriteLine(e.Message);
                Console.WriteLine("Try `KSR --help' for more information.");
                Environment.Exit(2);
            }

            if (show_help)
            {
                ShowHelp(p);
                Environment.Exit(1);
            }

            if (args.Length == 0)
            {
                Console.WriteLine("Using Default settings defined in code");
            }
            else
            {
                Settings.featuresSettings.Clear();
                Debug("Settings Cleared");
                // Set feature settings 
                if (options.Keys.Contains("f1"))
                    setSettingsFirstArgument();
                if (options.Keys.Contains("f2"))
                    setSettingsSimilarityBodyFeature();
                if (options.Keys.Contains("f3"))
                    setSettingsFirstParagraph();
                if (options.Keys.Contains("f4"))
                    setSettings30PercentBody();
                // Set keywordExtractor
                if (options.Keys.Contains("kwe"))
                    setSettingsKeyWordExtractor();
            }

            Settings.printCurrentSettings();
        }
        
        static void ShowHelp(OptionSet p)
        {
            Console.WriteLine("Usage: KSR.exe [OPTIONS]+ ");
            Console.WriteLine("loremipsum");
            Console.WriteLine("loremipsum");
            Console.WriteLine();
            Console.WriteLine("Options:");
            p.WriteOptionDescriptions(Console.Out);
        }

        #region SettingsSetters

        public void setSettingsKeyWordExtractor()
        {
            Debug("setSettingsKeyWordExtractor");
            var sw = options["kwe"];
            switch (sw)
            {
                case "1":
                    Settings.keyWordsExtractor = new TFFrequency();
                    break;
                case "2":
                    Settings.keyWordsExtractor = new TDFrequency();
                    break;
                case "3":
                    Settings.keyWordsExtractor = new IDFFrequency();
                    break;
            }
        }

        public void setSettingsFirstArgument()
        {
            Debug("setSettingsFirstArgument");
            var sw = options["f1"].ToCharArray();
            if (sw[0] == 't' || sw[0] == 'T')
            {
                Debug("Added BinaryArticleBodyFeature");
                Settings.featuresSettings.Add(new BinaryArticleBodyFeature(), true);
            }

            if (sw[1] == 't' || sw[1] == 'T')
            {
                Debug("Added KeyWords20PercentArticleBodyFeature");
                Settings.featuresSettings.Add(new KeyWords20PercentArticleBodyFeature(), true);
            }

            if (sw[2] == 't' || sw[2] == 'T')
            {
                Debug("Added KeyWordsArticleBodyFeature");
                Settings.featuresSettings.Add(new BinaryArticleBodyFeature(), true);
            }

            if (sw[3] == 't' || sw[3] == 'T')
            {
                Debug("Added KeyWordsFirstParagraphArticleBodyFeature");
                Settings.featuresSettings.Add(new KeyWordsFirstParagraphArticleBodyFeature(), true);
            }
        }

        public void setSettingsSimilarityBodyFeature()
        {
            Console.WriteLine("setSettingsSimilarityBodyFeature");
            var sw = options["f2"].ToCharArray();
            if (sw[0] == 't' || sw[0] == 'T')
            {
                Debug("Added SimilarityBodyFeature->BinaryFunction");
                Settings.featuresSettings.Add(new SimilarityBodyFeature() {SimilarityFunction = new BinaryFunction()},
                    true);
            }

            if (sw[1] == 't' || sw[1] == 'T')
            {
                Debug("Added SimilarityBodyFeature->JaccardFunction");
                Settings.featuresSettings.Add(new SimilarityBodyFeature() {SimilarityFunction = new JaccardFunction()},
                    true);
            }

            if (sw[2] == 't' || sw[2] == 'T')
            {
                Debug("Added SimilarityBodyFeature->LCSFunction");
                Settings.featuresSettings.Add(new SimilarityBodyFeature() {SimilarityFunction = new LCSFunction()},
                    true);
            }

            if (sw[3] == 't' || sw[3] == 'T')
            {
                Debug("Added SimilarityBodyFeature->LevenshteinFunction");
                Settings.featuresSettings.Add(
                    new SimilarityBodyFeature() {SimilarityFunction = new LevenshteinFunction()}, true);
            }

            if (sw[4] == 't' || sw[4] == 'T')
            {
                Debug("Added SimilarityBodyFeature->NGramFunction");
                Settings.featuresSettings.Add(new SimilarityBodyFeature() {SimilarityFunction = new NGramFunction()},
                    true);
            }

            if (sw[5] == 't' || sw[5] == 'T')
            {
                Debug("Added SimilarityBodyFeature->NiewiadomskiFunction");
                Settings.featuresSettings.Add(
                    new SimilarityBodyFeature() {SimilarityFunction = new NiewiadomskiFunction()}, true);
            }
        }

        public void setSettingsFirstParagraph()
        {
            Debug("setSettingsFirstParagraph");
            var sw = options["f3"].ToCharArray();
            if (sw[0] == 't' || sw[0] == 'T')
            {
                Debug("Added SimilarityFirstParagraph->BinaryFunction");
                Settings.featuresSettings.Add(
                    new SimliarityFirstParagraph() {SimilarityFunction = new BinaryFunction()}, true);
            }

            if (sw[1] == 't' || sw[1] == 'T')
            {
                Debug("Added SimliarityFirstParagraph->BinaryFunction");
                Settings.featuresSettings.Add(
                    new SimliarityFirstParagraph() {SimilarityFunction = new JaccardFunction()}, true);
            }

            if (sw[2] == 't' || sw[2] == 'T')
            {
                Debug("Added SimliarityFirstParagraph->BinaryFunction");
                Settings.featuresSettings.Add(new SimliarityFirstParagraph() {SimilarityFunction = new LCSFunction()},
                    true);
            }

            if (sw[3] == 't' || sw[3] == 'T')
            {
                Debug("Added SimliarityFirstParagraph->BinaryFunction");
                Settings.featuresSettings.Add(
                    new SimliarityFirstParagraph() {SimilarityFunction = new LevenshteinFunction()}, true);
            }

            if (sw[4] == 't' || sw[4] == 'T')
            {
                Debug("Added SimliarityFirstParagraph->BinaryFunction");
                Settings.featuresSettings.Add(new SimliarityFirstParagraph() {SimilarityFunction = new NGramFunction()},
                    true);
            }

            if (sw[5] == 't' || sw[5] == 'T')
            {
                Debug("Added SimliarityFirstParagraph->BinaryFunction");
                Settings.featuresSettings.Add(
                    new SimliarityFirstParagraph() {SimilarityFunction = new NiewiadomskiFunction()}, true);
            }
        }

        public void setSettings30PercentBody()
        {
            var sw = options["f4"].ToCharArray();
            if (sw[0] == 't' || sw[0] == 'T')
            {
                Debug("Added Simliarity30PercentBody->BinaryFunction");
                Settings.featuresSettings.Add(new Simliarity30PercentBody() {SimilarityFunction = new BinaryFunction()},
                    true);
            }

            if (sw[1] == 't' || sw[1] == 'T')
            {
                Debug("Added Simliarity30PercentBody->BinaryFunction");
                Settings.featuresSettings.Add(
                    new Simliarity30PercentBody() {SimilarityFunction = new JaccardFunction()}, true);
            }

            if (sw[2] == 't' || sw[2] == 'T')
            {
                Debug("Added Simliarity30PercentBody->BinaryFunction");
                Settings.featuresSettings.Add(new Simliarity30PercentBody() {SimilarityFunction = new LCSFunction()},
                    true);
            }

            if (sw[3] == 't' || sw[3] == 'T')
            {
                Debug("Added Simliarity30PercentBody->BinaryFunction");
                Settings.featuresSettings.Add(
                    new Simliarity30PercentBody() {SimilarityFunction = new LevenshteinFunction()}, true);
            }

            if (sw[4] == 't' || sw[4] == 'T')
            {
                Debug("Added Simliarity30PercentBody->BinaryFunction");
                Settings.featuresSettings.Add(new Simliarity30PercentBody() {SimilarityFunction = new NGramFunction()},
                    true);
            }

            if (sw[5] == 't' || sw[5] == 'T')
            {
                Debug("Added Simliarity30PercentBody->BinaryFunction");
                Settings.featuresSettings.Add(
                    new Simliarity30PercentBody() {SimilarityFunction = new NiewiadomskiFunction()}, true);
            }
        }
        

        #endregion
        
        static void Debug(string format, params object[] args)
        {
            if (verbosity > 0)
            {
                Console.Write("# ");
                Console.WriteLine(format, args);
            }
        }
    }
}