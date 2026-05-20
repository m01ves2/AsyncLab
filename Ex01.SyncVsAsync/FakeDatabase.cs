using System.Data;
using System.Diagnostics;

namespace Ex01.SyncVsAsync
{
    internal class FakeDatabase
    {
        public string GetUser() //sync method
        {

            Logger.Log($"{DateTime.Now}[Thread {Thread.CurrentThread.ManagedThreadId}] Start sync DB call");
            Thread.Sleep(3000);
            Logger.Log($"{DateTime.Now}[Thread {Thread.CurrentThread.ManagedThreadId}] Finish sync DB call");

            return "sync_db";
        }

        public async Task<string> GetUserAsync()
        {
            Logger.Log($"{DateTime.Now}[Thread {Thread.CurrentThread.ManagedThreadId}] Start async DB call");
            await Task.Delay(3000);
            Logger.Log($"{DateTime.Now}[Thread {Thread.CurrentThread.ManagedThreadId}] Finish async DB call");
            
            return "async_db";
        }
    }
}
