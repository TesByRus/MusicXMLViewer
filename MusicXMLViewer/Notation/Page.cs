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

namespace MusicXMLViewer.Android.Notation
{
    class Page
    {
        public Dictionary<List<scorepartwisePartMeasure>, List<int>> Parts { get; private set; } 

        public Page(scorepartwise scorepartwise)
        {
            foreach (var part in scorepartwise.part)
            {
                Parts.Add(new List<scorepartwisePartMeasure>(part.measure), new List<int>());
            }
        }

        public void AddMeasures(string partId, int newPageIndex)
        {
            
        }
    }
}