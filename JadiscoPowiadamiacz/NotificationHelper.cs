using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JadiscoPowiadamiacz
{
    public class NotificationHelper
    {
        public NotificationHelper()
        {

        }

        public NotificationData DeserializeData(string json)
        {
            NotificationData notificationData = new NotificationData();

            var settings = new JsonSerializerSettings {
                DateFormatString = "yyyy-MM-ddTHH:mm:ssK",
                DateTimeZoneHandling = DateTimeZoneHandling.Local
            };
            /*"offline_at":"2018-08-23T19:21:34+00:00" */
            notificationData =  JsonConvert.DeserializeObject<NotificationData>(json,settings);

            return notificationData;
        }
        public String SerializeData(NotificationData notificationData)
        {
            if (notificationData == null) {
                return "";
            }
            var settings = new JsonSerializerSettings {
                DateFormatString = "yyyy-MM-ddTHH:mm:ss",
            };
            return JsonConvert.SerializeObject(notificationData);
        }

    }
}
