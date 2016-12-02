using System;
using System.Windows.Forms;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GoogleGeoCode
{
    public partial class Form1 : Form
    {
   
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Information info = new Information();
            info.Data1 = textBox1.Text;
            info.Data2 = textBox2.Text;
            info.Data3 = textBox3.Text;
            info.Data4 = textBox4.Text;

            info.Data1 = info.Data1.Replace(" ", "+");
            info.Data2 = info.Data2.Replace(" ", "+");

            string requestString = "https://maps.googleapis.com/maps/api/geocode/json?address=" + info.Data1 + ",+" + info.Data2 + ",+" + info.Data3 + ",+" + info.Data4 + "GOOGLEAPIKEY";

            try
            {
                //create webrequest to googlemaps and pass in application data
                WebRequest request = WebRequest.Create(requestString);

                //get the response
                WebResponse response = request.GetResponse();

                //create the response stream
                Stream dataStream = response.GetResponseStream();

                //read the response stream
                StreamReader reader = new StreamReader(dataStream);

                //resonse stream to a string
                string responseFromServer = reader.ReadToEnd();

                //using Newtonsoft nuget parse the json string into linq object
                JObject rss = JObject.Parse(responseFromServer);

                //get child nodes and create lat/lng strings
                string lat = (string)rss["results"][0]["geometry"]["location"]["lat"];
                string lng = (string)rss["results"][0]["geometry"]["location"]["lng"];

                //concatenate the messagebox message
                string msg = "latitude: " + lat + Environment.NewLine + "longitude: " + lng;

                //show the message
                MessageBox.Show(msg);
               
                reader.Close();
                response.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

    }
}
