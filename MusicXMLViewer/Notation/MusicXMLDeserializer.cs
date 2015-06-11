using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace MusicXMLViewer.Android.Notation
{
    class MusicXMLDeserializer
    {

        public Task<T> DeserializeObjectAsync<T>(string xml)
        {
            return Task.Run(() =>
            {
                using (var fileStream = new FileStream(xml, FileMode.Open, FileAccess.Read))
                {
                    using (var streamReader = new StreamReader(fileStream))
                    {
                        XmlSerializer serializer =
                            new XmlSerializer(typeof(T));
                        T theObject = (T)serializer.Deserialize(fileStream);
                        return theObject;
                    }
                }
            });
        }
    }
}