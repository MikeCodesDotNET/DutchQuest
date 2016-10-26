using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DutchQuest.Models;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Plugin.Connectivity;

namespace DutchQuest.Services.Azure
{
    public class AppServiceData : Abstractions.IDataService
    {
        public MobileServiceClient MobileService { get; set; }
        IMobileServiceSyncTable<Topic> topicsTable;
        IMobileServiceSyncTable<Word> wordsTable;

        bool isInitialized;
        public async Task Initialize()
        {
            if (isInitialized)
                return;

            MobileService = new MobileServiceClient(Helpers.Keys.AzureAppServiceKey, null)
            {
                SerializerSettings = new MobileServiceJsonSerializerSettings()
                {
                    CamelCasePropertyNames = true
                }
            };

            var store = new MobileServiceSQLiteStore("DutchQuestData.db");
            store.DefineTable<Topic>();
            store.DefineTable<Word>();

            await MobileService.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());

            wordsTable = MobileService.GetSyncTable<Word>();
            topicsTable = MobileService.GetSyncTable<Topic>();

            isInitialized = true;
        }

        public async Task Sync()
        {
            var connected = await CrossConnectivity.Current.IsReachable("google.com");
            if (connected == false)
                return;

            try
            {
                await MobileService.SyncContext.PushAsync();
                await wordsTable.PullAsync("allWords", wordsTable.CreateQuery());
                await topicsTable.PullAsync("allTopics", wordsTable.CreateQuery());
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to sync items, that is alright as we have offline capabilities: " + ex);
            }
        }
        
        public async Task<IEnumerable<Topic>> GetTopics()
        {
            await Initialize();
            await Sync();
            return await topicsTable.ToEnumerableAsync();
        }
        
    }
}
