using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Newtonsoft.Json;
using WebSocket4Net;

namespace JadiscoPowiadamiacz
{
    class Powiadamiacz
    {
        private const String LAST_ONLINE_STREAMER_ID = "last_online_streamer_id";
        private const String LAST_TOPIC_ID = "last_topic_id";
        private const String TEST_TOPIC_ID = "test_topic_id";

        private System.Timers.Timer timer = new System.Timers.Timer(); // name space(using System.Timers;)  
        public void Run()
        {
            FileCache.WriteToLogFile("Utworzona");
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            timer.Interval = 15 * 1000; //number in milisecinds  
            timer.Enabled = true;



        }
        private void OnElapsedTime(object s, ElapsedEventArgs events)
        {
            //   FileCache.WriteToLogFile("OnElapsedTime");

            WebSocket websocket = new WebSocket("wss://api.pancernik.info/notifier");
            websocket.Opened += Websocket_Opened;
            websocket.Error += Websocket_Error;
            websocket.Closed += Websocket_Closed;
            websocket.MessageReceived += Websocket_MessageReceived;
            websocket.Open();

        }

        private void Websocket_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            timer.Stop();
            if (e != null) {
                HandleMessage(e.Message);
            }
            timer.Start();
        }

        private void HandleMessage(string jsonMessage)
        {
            FileCache cache = new FileCache();

            Console.WriteLine(jsonMessage);

            NotificationHelper helper = new NotificationHelper();
            NotificationData data = helper.DeserializeData(jsonMessage);
            bool noOneOnline = true;

            if (data.Type != "status") return;
            FileCache.WriteToLogFile("MessageReceived: " + jsonMessage);

            FileCache.WriteToLogFile("Checking streamers");
            foreach (var item in data.Data.Stream.Services) {
                if (item.Status == true) {
                    if (cache.GetValue<string>(LAST_ONLINE_STREAMER_ID,"")=="" && !cache.GetValue<string>(LAST_ONLINE_STREAMER_ID, "-1").Equals(item.ID)) {
                        cache.SetValue<string>(LAST_ONLINE_STREAMER_ID, item.ID);
                        string streamerName = data.Data.Streamers.First((streamer) => streamer.ID == item.StreamerID).Name;
                        SendPostRequestToFirebase(String.Format("Strumień trwa od {1}, {0} zaprasza!", streamerName, data.Data.Stream.OnlineAt.ToShortTimeString()), "Strumień się zaczął!", streamerName);
                    }
                    noOneOnline = false;
                }
            }

            if (noOneOnline) {
                cache.SetValue<string>(LAST_ONLINE_STREAMER_ID, "");
            }

            FileCache.WriteToLogFile("Checking topic");
            if (cache.GetValue<long>(LAST_TOPIC_ID, 0) != data.Data.Topic.ID) {
                SendPostRequestToFirebase("Powiadamiacz", data.Data.Topic.Text, "topic");
                cache.SetValue<long>(LAST_TOPIC_ID, data.Data.Topic.ID);
            }

            FileCache.WriteToLogFile("Checking test topic");
            if (cache.GetValue<long>(TEST_TOPIC_ID, 0) == 0) {
                SendPostRequestToFirebase("Offline, kalendarz na ten tydzień został zaktualizowany. A dzisiaj zaczniemy tak do 18:00. Offline, kalendarz na ten tydzień został zaktualizowany. A dzisiaj zaczniemy tak do 18:00", "Test title", "topicTest");
                cache.SetValue<long>(TEST_TOPIC_ID, 1);
            }
        }
        private void Websocket_Closed(object sender, EventArgs e)
        {
            Console.WriteLine("Closed");
            FileCache.WriteToLogFile("Closed");
        }

        private void Websocket_Error(object sender, SuperSocket.ClientEngine.ErrorEventArgs e)
        {
            Console.WriteLine(e.Exception);
        }

        private void Websocket_Opened(object sender, EventArgs e)
        {
            //    Console.WriteLine("Opened");
            //   FileCache.WriteToLogFile("Opened");
        }


        private string SendPostRequestToFirebase(string notificationBody, string notifiationTitle, string notificationTopic)
        {
            return "Done";
        }
     
    }
}
