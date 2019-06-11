using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
namespace GeneralKnowledge.Test.App.Tests
{




    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class samples
    {

        private samplesMeasurement[] measurementField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("measurement")]
        public samplesMeasurement[] measurement
        {
            get
            {
                return this.measurementField;
            }
            set
            {
                this.measurementField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class samplesMeasurement
    {

        private samplesMeasurementParam[] paramField;

        private System.DateTime dateField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("param")]
        public samplesMeasurementParam[] param
        {
            get
            {
                return this.paramField;
            }
            set
            {
                this.paramField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.DateTime date
        {
            get
            {
                return this.dateField;
            }
            set
            {
                this.dateField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class samplesMeasurementParam
    {

        private string nameField;

        private decimal valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public decimal Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }
    public class Parameters
    {
        public decimal Low { get; set; }
        public decimal Avg { get; set; }
        public decimal Max { get; set; }
        public string Name { get; set; }

    }

    /// <summary>
    /// This test evaluates the 
    /// </summary>
    public class XmlReadingTest : ITest
    {
        public string Name { get { return "XML Reading Test";  } }

        public void Run()
        {
            var xmlData = Resources.SamplePoints;

            // TODO: 
            // Determine for each parameter stored in the variable below, the average value, lowest and highest number.
            // Example output
            // parameter   LOW AVG MAX
            // temperature   x   y   z
            // pH            x   y   z
            // Chloride      x   y   z
            // Phosphate     x   y   z
            // Nitrate       x   y   z

            XmlSerializer serializer = new XmlSerializer(typeof(samples));
            using (TextReader reader = new StringReader(xmlData))
            {
                samples result = (samples)serializer.Deserialize(reader);

                var data = result.measurement.SelectMany(u => u.param.ToDictionary(x => x.name,y => y.Value)).GroupBy(x=> x.Key,z => z.Value).ToList();

                List<Parameters> list = new List<Parameters>();
                foreach (var item in data)
                {
                    list.Add(new Parameters
                    {
                        Avg = item.Average(),
                        Low = item.Min(),
                        Max = item.Max(),
                        Name = item.Key
                    });
                }
                PrintOverview(list);

            }


            
        }

        private void PrintOverview(List<Parameters> list)
        {
            Console.WriteLine("parameter   LOW AVG MAX");
            foreach (var item in list)
            {
                Console.WriteLine("{0} {1} {2} {3}", item.Name, item.Low, item.Avg, item.Max);
            }
        }
    }
}
