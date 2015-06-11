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
    class ScoreModified
    {
        public List<Page> Pages { get; private set; } 

        public ScoreModified(scorepartwise scorepartwise)
        {
            SetPages(scorepartwise);

        }


        void SetPages(scorepartwise scorepartwise)
        {
            foreach (var p in scorepartwise.part.Select((value,i) => new {i, value}))
            {
                for (var i = 0; i < p.value.measure.Length; i++)
                {
                    if (p.i == 0)
                    {
                        
                    }
                }
            }
        }
    }
}