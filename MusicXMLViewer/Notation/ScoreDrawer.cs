using System.Collections.Generic;
using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Widget;

namespace MusicXMLViewer.Android.Notation
{
    class ScorePageView : View
    {
        public ScorePageView(Context context, scorepartwise score)
            : base(context)
        {
            this.score = score;
            _dpiCoef = 1; //(float)Resources.DisplayMetrics.DensityDpi / 254;
            paint = new Paint();
            paint.SetARGB(255, 0, 0, 0);
            paint.StrokeWidth = _dpiCoef;
            paint.SetStyle(Paint.Style.Stroke);
            path = new Path();
        }


        private scorepartwise score;
        private Paint paint;
        private Path path;
        private const int PartHeight = 80;


        const int paddingTopBot = 100;
        private const int paddingLeftRight = 20;

        private float _dpiCoef;

        protected override void OnDraw(Canvas canvas)
        {
            int startX = paddingLeftRight, startY = 50;
            for (var i = 0; i < score.part.Length; i++)
            {
                DrawPart(score.part[i], canvas, startX, startY);
                startY += PartHeight + paddingTopBot;
            }
        }


        void DrawPart(scorepartwisePart part, Canvas canvas, int startX, int startY)
        {

            var clefList = new List<clef>();

                int x = (int)(startX * _dpiCoef);
            int y = (int)(startY * _dpiCoef);

            for (int i = 0; i < part.measure.Length; i++)
            {

                float measureWidth = (float)part.measure[i].width;

                var backupNum = 0;


                DrawLines(path, x, y, (float)part.measure[i].width);
                canvas.DrawPath(path, paint);
                foreach (var item in part.measure[i].Items)
                {
                    if (item.GetType() == typeof(attributes)) // если атрибуты, надо отрисовать клефы
                    {
                        if (((attributes) item).staves == "2")
                        {
                            DrawLines(path, x, y, (float)part.measure[i].width);
                            canvas.DrawPath(path, paint);
                        }
                        foreach (var clef in ((attributes) item).clef)
                        {
                            //TODO DrawClef()
                        }
                        
                    }
                }

                x += (int)(measureWidth * _dpiCoef);

            }


        }


        void DrawLines(Path path, int x, int y, float measureWidth)
        {
            path.Reset();
            int dest = 0;
            path.MoveTo(x, y);
            path.LineTo(x, y + (int)(PartHeight * _dpiCoef));
            for (var j = 0; j < 5; j++)
            {
                path.MoveTo(x, y + dest);
                path.LineTo((int)(measureWidth * _dpiCoef), y + dest);
                dest += (int)(((float)PartHeight / 4) * _dpiCoef);
            }
            path.MoveTo(x + (int)(measureWidth * _dpiCoef), y);
            path.LineTo(x + (int)(measureWidth * _dpiCoef), y + (int)(40 * _dpiCoef));

        }
    }
}