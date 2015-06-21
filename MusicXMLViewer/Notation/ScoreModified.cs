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
            Pages = new List<Page>();
            SetPages(scorepartwise);
        }


        void SetPages(scorepartwise scorepartwise)
        {
            //foreach (var p in scorepartwise.part.Select((value,i) => new {i, value}))
            //{
            //    for (var i = 0; i < p.value.measure.Length; i++)
            //    {
            //        if (p.i == 0)
            //        {

            //        }
            //    }
            //}

            foreach (var part in scorepartwise.part)
            {
                string partId = part.id;
                var measureList = new List<scorepartwisePartMeasure>();

                int pageNumber = 0;
                foreach (var measure in part.measure)
                {
                    if (measure.Items.ToList().Find(item => item.GetType() == typeof(print)) != null)
                    {
                        if (measureList.Count != 0)
                        {
                            if (Pages.Find(p => p.pageNumber == pageNumber) == null)
                            {
                                Pages.Add(new Page(pageNumber));
                            }
                            Pages.Find(p => p.pageNumber == pageNumber).AddMeasures(partId, measureList);

                            pageNumber++;
                        }
                        measureList.Clear();
                    }
                    measureList.Add(measure);
                }
                if (measureList.Count != 0)
                {
                    if (Pages.Find(p => p.pageNumber == pageNumber) == null)
                    {
                        Pages.Add(new Page(pageNumber));
                    }
                    Pages.Find(p => p.pageNumber == pageNumber).AddMeasures(partId, measureList);
                }
            }
        }
    }
}