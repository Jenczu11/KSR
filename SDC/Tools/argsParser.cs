using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using SDC.Tools.Features;
using SDC.Tools.Frequency;
using SDC.Tools.SimliarityFunctions;
using NDesk.Options;
using NumSharp.Extensions;
using TwinFinder.Base.Extensions;

namespace SDC.Tools
{
    public class ArgsParser
    {
        static int verbosity;
        Dictionary<string, string> options = new Dictionary<string, string>();

        public ArgsParser(string[] args)
        {
            bool show_help = false;

            #region OptionSet

            var p = new OptionSet()
            {
                {
                    "f1|feature1=", "True/False for first feature set",
                    v =>
                    {
                        if (v.Length == 5)
                        {
                            options.Add("f1", v);
                        }
                        else
                        {
                            throw new OptionException("Feature set 1 is not equal to 5 values", "-f1");
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
                        if (v.Length == 4)
                        {
                            options.Add("f4", v);
                        }
                        else
                        {
                            throw new OptionException("Feature set 4 is not equal to 4 values", "-f4");
                        }
                    }
                },
                {
                    "f5|feature5=", "True/False for fifth feature set",
                    v =>
                    {
                        if (v.Length == 6)
                        {
                            options.Add("f5", v);
                        }
                        else
                        {
                            throw new OptionException("Feature set 5 is not equal to 6 values", "-f4");
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
                Console.WriteLine("Try `SDC --help' for more information.");
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
                    setSettingsFirst();
                if (options.Keys.Contains("f3"))
                    setSettingsLast();
                if (options.Keys.Contains("f4"))
                    setSettingsWords();
                if (options.Keys.Contains("f5"))
                    setSettings30PercentBody();
                // Set keywordExtractor
                if (options.Keys.Contains("kwe"))
                    setSettingsKeyWordExtractor();
            }

            Settings.printCurrentSettings();
        }

        static void ShowHelp(OptionSet p)
        {
            Console.WriteLine("Usage: SDC.exe [OPTIONS]+ ");
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
                Debug("Added KeyWordsArticleBodyFeature");
                Settings.featuresSettings.Add(new KeyWordsArticleBodyFeature(), true);
            }

            if (sw[2] == 't' || sw[2] == 'T')
            {
                Debug("Added KeyWordsPercentageArticleBodyFeature");
                Settings.featuresSettings.Add(new KeyWordsPercentageArticleBodyFeature(), true);
            }

            if (sw[3] == 't' || sw[3] == 'T')
            {
                Debug("Added KeyWordsFirstParagraphArticleBodyFeature");
                Settings.featuresSettings.Add(new KeyWordsFirstParagraphArticleBodyFeature(), true);
            }
            if (sw[4] == 't' || sw[4] == 'T')
            {
                Debug("Added KeyWordsLastParagraphArticleBodyFeature");
                Settings.featuresSettings.Add(new KeyWordsLastParagraphArticleBodyFeature(), true);
            }
        }

        public void setSettingsFirst()
        {
            Console.WriteLine("setSettingsFirst");
            var sw = options["f2"].ToCharArray();
            if (sw[0] == 't' || sw[0] == 'T')
            {
                Debug("Added KeyWordsIn10PercentArticleBodyFeature");
                Settings.featuresSettings.Add(new KeyWordsInNPercentArticleBodyFeature() { N = 10 }, true);
            }
            if (sw[1] == 't' || sw[1] == 'T')
            {
                Debug("Added KeyWordsIn20PercentArticleBodyFeature");
                Settings.featuresSettings.Add(new KeyWordsInNPercentArticleBodyFeature() { N = 20 }, true);
            }
            if (sw[2] == 't' || sw[2] == 'T')
            {
                Debug("Added KeyWordsIn50PercentArticleBodyFeature");
                Settings.featuresSettings.Add(new KeyWordsInNPercentArticleBodyFeature() { N = 50 }, true);
            }
            if (sw[3] == 't' || sw[3] == 'T')
            {
                Debug("Added KeyWordsIn20WordsArticleBodyFeature");
                Settings.featuresSettings.Add(new KeyWordsInNWordsArticleBodyFeature() { N = 20 }, true);
            }
            if (sw[4] == 't' || sw[4] == 'T')
            {
                Debug("Added KeyWordsIn50WordsArticleBodyFeature");
                Settings.featuresSettings.Add(new KeyWordsInNWordsArticleBodyFeature() { N = 50 }, true);
            }
            if (sw[5] == 't' || sw[5] == 'T')
            {
                Debug("Added KeyWordsIn70WordsArticleBodyFeature");
                Settings.featuresSettings.Add(new KeyWordsInNWordsArticleBodyFeature() { N = 70 }, true);
            }

        }

        public void setSettingsLast()
        {
            Console.WriteLine("setSettingsFirst");
            var sw = options["f3"].ToCharArray();
            if (sw[0] == 't' || sw[0] == 'T')
            {
                Debug("Added KeyWordsIn10LastPercentArticleBodyFeature");
                Settings.featuresSettings.Add(new KeyWordsInNLastPercentArticleBodyFeature() { N = 10 }, true);
            }
            if (sw[1] == 't' || sw[1] == 'T')
            {
                Debug("Added KeyWordsIn20LastPercentArticleBodyFeature");
                Settings.featuresSettings.Add(new KeyWordsInNLastPercentArticleBodyFeature() { N = 20 }, true);
            }
            if (sw[2] == 't' || sw[2] == 'T')
            {
                Debug("Added KeyWordsIn50LastPercentArticleBodyFeature");
                Settings.featuresSettings.Add(new KeyWordsInNLastPercentArticleBodyFeature() { N = 50 }, true);
            }
            if (sw[3] == 't' || sw[3] == 'T')
            {
                Debug("Added KeyWordsIn20LastWordsArticleBodyFeature");
                Settings.featuresSettings.Add(new KeyWordsInNLastWordsArticleBodyFeature() { N = 20 }, true);
            }
            if (sw[4] == 't' || sw[4] == 'T')
            {
                Debug("Added KeyWordsIn50LastWordsArticleBodyFeature");
                Settings.featuresSettings.Add(new KeyWordsInNLastWordsArticleBodyFeature() { N = 50 }, true);
            }
            if (sw[5] == 't' || sw[5] == 'T')
            {
                Debug("Added KeyWordsIn70LastWordsArticleBodyFeature");
                Settings.featuresSettings.Add(new KeyWordsInNLastWordsArticleBodyFeature() { N = 70 }, true);
            }

        }

        public void setSettingsSimilarityBodyFeature()
        {
            Console.WriteLine("setSettingsSimilarityBodyFeature");
            var sw = options["f2"].ToCharArray();
            if (sw[0] == 't' || sw[0] == 'T')
            {
                Debug("Added SimilarityBodyFeature->BinaryFunction");
                Settings.featuresSettings.Add(new SimilarityBodyFeature() { SimilarityFunction = new BinaryFunction() },
                    true);
            }

            if (sw[1] == 't' || sw[1] == 'T')
            {
                Debug("Added SimilarityBodyFeature->JaccardFunction");
                Settings.featuresSettings.Add(new SimilarityBodyFeature() { SimilarityFunction = new JaccardFunction() },
                    true);
            }

            if (sw[2] == 't' || sw[2] == 'T')
            {
                Debug("Added SimilarityBodyFeature->LCSFunction");
                Settings.featuresSettings.Add(new SimilarityBodyFeature() { SimilarityFunction = new LCSFunction() },
                    true);
            }

            if (sw[3] == 't' || sw[3] == 'T')
            {
                Debug("Added SimilarityBodyFeature->LevenshteinFunction");
                Settings.featuresSettings.Add(new SimilarityBodyFeature() { SimilarityFunction = new LevenshteinFunction() }, true);
            }

            if (sw[4] == 't' || sw[4] == 'T')
            {
                Debug("Added SimilarityBodyFeature->NGramFunction");
                Settings.featuresSettings.Add(new SimilarityBodyFeature() { SimilarityFunction = new NGramFunction() },
                    true);
            }

            if (sw[5] == 't' || sw[5] == 'T')
            {
                Debug("Added SimilarityBodyFeature->NiewiadomskiFunction");
                Settings.featuresSettings.Add(
                    new SimilarityBodyFeature() { SimilarityFunction = new NiewiadomskiFunction() }, true);
            }
        }

        public void setSettingsWords()
        {
            Debug("setSettingsWords");
            var sw = options["f4"].ToCharArray();
            if (sw[0] == 't' || sw[0] == 'T')
            {
                Debug("Added WordsCounter");
                Settings.featuresSettings.Add(new WordsCounter(), true);
            }

            if (sw[1] == 't' || sw[1] == 'T')
            {
                Debug("Added UniqueWordsCounter");
                Settings.featuresSettings.Add(new UniqueWordsCounter(), true);
            }

            if (sw[2] == 't' || sw[2] == 'T')
            {
                Debug("Added ShortWords");
                Settings.featuresSettings.Add(new ShortWords(), true);
            }

            if (sw[3] == 't' || sw[3] == 'T')
            {
                Debug("Added LongWords");
                Settings.featuresSettings.Add(new LongWords(), true);
            }
        }

        public void setSettings30PercentBody()
        {
            var sw = options["f5"].ToCharArray();
            if (sw[0] == 't' || sw[0] == 'T')
            {
                Debug("Added Simliarity30PercentBody->BinaryFunction");
                Settings.featuresSettings.Add(new Simliarity30PercentBody() { SimilarityFunction = new BinaryFunction() }, true);
            }

            if (sw[1] == 't' || sw[1] == 'T')
            {
                Debug("Added Simliarity30PercentBody->BinaryFunction");
                Settings.featuresSettings.Add(new Simliarity30PercentBody() { SimilarityFunction = new JaccardFunction() }, true);
            }

            if (sw[2] == 't' || sw[2] == 'T')
            {
                Debug("Added Simliarity30PercentBody->BinaryFunction");
                Settings.featuresSettings.Add(new Simliarity30PercentBody() { SimilarityFunction = new LCSFunction() }, true);
            }

            if (sw[3] == 't' || sw[3] == 'T')
            {
                Debug("Added Simliarity30PercentBody->BinaryFunction");
                Settings.featuresSettings.Add(new Simliarity30PercentBody() { SimilarityFunction = new LevenshteinFunction() }, true);
            }

            if (sw[4] == 't' || sw[4] == 'T')
            {
                Debug("Added Simliarity30PercentBody->BinaryFunction");
                Settings.featuresSettings.Add(new Simliarity30PercentBody() { SimilarityFunction = new NGramFunction() }, true);
            }

            if (sw[5] == 't' || sw[5] == 'T')
            {
                Debug("Added Simliarity30PercentBody->BinaryFunction");
                Settings.featuresSettings.Add(new Simliarity30PercentBody() { SimilarityFunction = new NiewiadomskiFunction() }, true);
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