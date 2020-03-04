﻿using System;
using System.Collections.Generic;

namespace KSR.Model
{
    public class Article
    {
        public string Title { get; set; }
        public string DateLine { get; set; }
        public Dictionary<string, List<string>> Tags { get; set; }
        public List<List<string>> Paragraphs { get; set; }
        public List<string> GetAllWords()
        {
            var words = new List<string>();
            this.Paragraphs.ForEach(item => words.AddRange(item));
            return words;
        }
    }

}
