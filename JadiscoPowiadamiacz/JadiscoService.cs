using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Timers;
using SlackAPI.WebSocketMessages;
using SlackAPI.RPCMessages;
using WebSocketSharp;
using System.IO;
using System.Net.Http;

namespace JadiscoPowiadamiacz
{
    public partial class JadiscoService : ServiceBase
    {
        Timer timer = new Timer(); // name space(using System.Timers;)  
        private static readonly HttpClient client = new HttpClient();
        public JadiscoService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Powiadamiacz powiadamiacz = new Powiadamiacz();
            powiadamiacz.Run();
        }

       
        protected override void OnStop()
        {
        }

    }
}
