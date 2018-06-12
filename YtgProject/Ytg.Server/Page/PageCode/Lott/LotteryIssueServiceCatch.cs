using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ytg.BasicModel;

namespace Ytg.ServerWeb.Page.PageCode.Lott
{
    public class LotteryIssueServiceCatch
    {
        static object lockObj = new object();
        private Dictionary<string,LotteryIssue> mCatch = new Dictionary<string,LotteryIssue>();

        private static LotteryIssueServiceCatch mLotteryIssueServiceCatch;
        public static LotteryIssueServiceCatch CreateInstance()
        {
            if (null == mLotteryIssueServiceCatch)
                mLotteryIssueServiceCatch = new LotteryIssueServiceCatch();
            return mLotteryIssueServiceCatch;
        }



        public void Put(LotteryIssue result, int lotteryid)
        {
            lock (lockObj)
            {
                if (mCatch.Count > 10000)
                    mCatch = new Dictionary<string, LotteryIssue>() ;
                mCatch.Add(lotteryid+result.IssueCode, result);
            }
        }

        public LotteryIssue GetIssue(string issue, int lotteryid)
        {
            LotteryIssue outValue;
            if (!this.mCatch.TryGetValue(lotteryid + issue, out outValue))
                return null;
            return outValue;
        }
    }
}