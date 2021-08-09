using System;
using System.Collections.Generic;
using NumSharp;

namespace SDC.Model
{
    public class Article
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public Dictionary<string, List<string>> Tags { get; set; }
        public List<List<string>> Paragraphs { get; set; }
        public string[] AllWords { get; set; }
        public string Label { get; set; }
        public string GuessedLabel { get; set; }
        public NDArray Features { get; set; }
        public double[] FeaturesD { get; set; }
        public bool[] IsFeaturesContinoused { get; set; }

        public bool HasAttachment { get; set; }
    }

}
