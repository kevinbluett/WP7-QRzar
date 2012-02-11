using System;
using System.Net;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ServiceModel;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace QRzar
{
    [DataContract]
    public class Game
    {
        [DataMember]
        public int ID;

        [DataMember]
        public Player[] players;

        /// <summary>
        /// Serializes the game object using Json serialization
        /// </summary>
        /// <returns>The Json string</returns>
        public string ToJson()
        {
            DataContractJsonSerializer dcjs = new DataContractJsonSerializer(this.GetType());
            string json;
            using (MemoryStream stream = new MemoryStream())
            {
                dcjs.WriteObject(stream, this);
                stream.Position = 0;
                StreamReader sr = new StreamReader(stream);
                json = sr.ReadToEnd();
            }

            return json;
        }


        public Game(String text)
        {
            DataContractJsonSerializer dcjs = new DataContractJsonSerializer(typeof(Game));

            byte[] byteArray = Encoding.UTF8.GetBytes(text);
            MemoryStream stream = new MemoryStream(byteArray);

            Game newGame = (Game)dcjs.ReadObject(stream);

            this.ID = newGame.ID;
            this.players = newGame.players;
        }

        public Game(int ID, Player[] players)
        {
            this.ID = ID;
            this.players = players;
        }



    }

}