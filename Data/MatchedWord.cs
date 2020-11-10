using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace UrbanSlangSolver.Data
{
    public class MatchedWord
    {
        public string SlangWord { get; set; }
        public string Description { get; set; }
    }

    public class Root
    {
        public List<MatchedWord> MatchedWord { get; set; }
    }
}