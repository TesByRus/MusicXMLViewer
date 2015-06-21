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
        public int pageNumber { get; private set; }
        public Dictionary<string, List<scorepartwisePartMeasure>> Parts { get; private set; }

        public Page(int pageNumber)
        {
            this.pageNumber = pageNumber;
            Parts = new Dictionary<string, List<scorepartwisePartMeasure>>();
        }

        public void AddMeasures(string partId, List<scorepartwisePartMeasure> measureList)
        {
            Parts.Add(partId, measureList);
        }
    }
}