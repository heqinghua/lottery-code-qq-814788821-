using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.Scheduler.Comm;

namespace Ytg.Scheduler.Tasks.Jobs
{
    public  class CalculationQueue
    {
        static object lockObject = new object();
        static CalculationQueue mCalculationQueue=null;

        Dictionary<string, QueueStat> mQueue = new Dictionary<string, QueueStat>();//开奖队列情况

        public static CalculationQueue CreateInstance()
        {
            if (mCalculationQueue == null)
                mCalculationQueue = new CalculationQueue();
            return mCalculationQueue;
        }

        /// <summary>
        /// 是否允许创建新任务
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool IsCompledTask(string key)
        {
            lock (lockObject)
            {
                //移除超过20分钟的
                var keys= this.mQueue.Keys.ToList();
                LogManager.Info("CalculationQueue length="+key.Length);
                foreach (var k in keys)
                {
                    if (this.mQueue[k].IsRemove())
                        this.mQueue.Remove(k);
                    
                }
                //

                if (this.mQueue.ContainsKey(key))
                {
                    QueueStat stat = this.mQueue[key];
                    if (stat.IsCompled)
                    {
                        this.mQueue.Remove(key);
                        return true;
                    }
                    return false;
                }
                return true;
            }
        }

        public void AddOrUpdate(string key,bool isCompled=false)
        {
            lock (lockObject)
            {

                if (!isCompled)
                {
                    if (!this.mQueue.ContainsKey(key))
                        this.mQueue.Add(key, new QueueStat());
                }
                else
                {
                    this.mQueue.Remove(key);
                }
            }
        }

    }

    public class QueueStat
    {
        public QueueStat() {

            this.NowTimeSpan = DateTime.Now.Ticks;
        }

        public bool IsCompled { get; set; }

        public long NowTimeSpan { get; set; }

        public bool IsRemove()
        {
            return DateTime.Now.Ticks - this.NowTimeSpan > 1000 * 60 * 20;//20分钟，移除掉
        }
    }
}
