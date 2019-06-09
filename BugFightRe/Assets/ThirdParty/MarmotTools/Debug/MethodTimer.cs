﻿


namespace MyMarmot.Tools
{
    using System.Diagnostics;

    public static class MethodTimer
    {        
        public static Stopwatch StartTimer()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            UnityEngine.Debug.Log("Timer Start");
            return sw;
        }
        public static void StopTimer(string yourDesc,Stopwatch timerWatch)
        {
            timerWatch.Stop();
            UnityEngine.Debug.Log(yourDesc + "-Timer Stop-걸린시간:" + timerWatch.ElapsedMilliseconds.ToString() + "ms");
        }
        public static void StopTimer(Stopwatch timerWatch)
        {
            timerWatch.Stop();
            UnityEngine.Debug.Log("Timer Stop-걸린시간:" + timerWatch.ElapsedMilliseconds.ToString() + "ms");
        }
    }
}