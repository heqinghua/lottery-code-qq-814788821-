using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ytg.Scheduler.Comm;

namespace Ytg.Scheduler.Service
{
    public partial class frmtest : Form
    {
        public frmtest()
        {
            InitializeComponent();
        }

        private int lotteryid = 1;//彩票id 

        private string lotteryCode = "";

        private void btnOpen_Click(object sender, EventArgs e)
        {
           List<OpenResultEntity> entitys = new List<OpenResultEntity>();
            entitys.Add(new OpenResultEntity()
            {
                expect=this.txtIssue.Text.Trim(),
                opencode=this.txtOpenResult.Text.Trim(),
                opentime=DateTime.Now
               
            });
            if (this.comboBox1.Text == "福彩3d")
                lotteryid = 7;
            else if (this.comboBox1.Text == "上海时时乐")
                lotteryid = 8;
            else if (this.comboBox1.Text == "五分11选5")
                lotteryid = 18;
            else if (this.comboBox1.Text == "排列三、五")
                lotteryid = 9;
            else if (this.comboBox1.Text == "江苏快三")
                lotteryid = 22;
            else if(this.comboBox1.Text=="三分11选5")
                lotteryid = 17;
            else if (this.comboBox1.Text == "香港六合彩")
                lotteryid = 21;
            else if (this.comboBox1.Text == "广东11选5")
                lotteryid = 6;
            else if (this.comboBox1.Text == "埃及分分彩")
            {
                lotteryCode = "FFC1";
                lotteryid = 13;
            }
            else if (this.comboBox1.Text == "埃及二分彩")
            {
                lotteryCode = "FFC2";
                lotteryid = 24;
            }
            else if (this.comboBox1.Text == "埃及五分彩")
            {
                lotteryCode = "FFC5";
                lotteryid = 25;
            }
            var lt = new LotteryIssuesData();
            Ytg.Scheduler.Tasks.Jobs.OpenOfficialResultJob job = new Tasks.Jobs.OpenOfficialResultJob();
            Ytg.Scheduler.Tasks.Jobs.PoolParam param = new Ytg.Scheduler.Tasks.Jobs.PoolParam()
            {
                lotteryid = lotteryid,//7,
                LotteryIssues = entitys,
            };
            job.UpdateLotteryIssueResult(param);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.comboBox1.Text == "福彩3d")
                lotteryid = 7;
            else if (this.comboBox1.Text == "上海时时乐")
                lotteryid = 8;
            else if (this.comboBox1.Text == "五分11选5")
                lotteryid = 18;
            else if (this.comboBox1.Text == "排列三、五")
                lotteryid = 9;
            else if (this.comboBox1.Text == "江苏快三")
                lotteryid = 22;
            else if (this.comboBox1.Text == "三分11选5")
                lotteryid = 17;
            else if (this.comboBox1.Text == "香港六合彩")
                lotteryid = 21;
            else if (this.comboBox1.Text == "埃及分分彩")
            {
                lotteryCode = "FFC1";
                lotteryid = 13;
            }
            else if (this.comboBox1.Text == "埃及二分彩")
            {
                lotteryCode = "FFC2";
                lotteryid = 24;
            }
            else if (this.comboBox1.Text == "埃及五分彩")
            {
                lotteryCode = "FFC5";
                lotteryid = 25;
            }
            else if (this.comboBox1.Text == "河内时时彩")
            {
                lotteryid = 14;
            }
            else if (this.comboBox1.Text == "印尼时时彩") {
                lotteryid = 23;
            }


                var issues = new LotteryIssuesData().GetIssueResultDTO(lotteryid);
            if (null == issues)
            {
                MessageBox.Show("获取失败！");
                return;
            }
            
            this.txtIssue.Text = issues.datesn;
            this.lbtimes.Text = issues.code.ToString("yyyy/MM/dd HH:mm:ss");
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (this.comboBox1.Text == "福彩3d")
            {
                lotteryCode = "fc3d";
                lotteryid = 7;
            }
            else if (this.comboBox1.Text == "上海时时乐")
            {
                lotteryCode = "shssl";
                lotteryid = 8;
            }
            else if (this.comboBox1.Text == "五分11选5")
            {
                lotteryCode = "wf11x5";
                lotteryid = 18;
            }
            else if (this.comboBox1.Text == "排列三、五")
            {
                lotteryCode = "pl5";
                lotteryid = 9;
            }
            else if (this.comboBox1.Text == "江苏快三")
            {
                lotteryCode = "jsk3";
                lotteryid = 22;
            }
            else if (this.comboBox1.Text == "三分11选5")
            {
                lotteryCode = "sf11x5";
                lotteryid = 17;
            }
            else if (this.comboBox1.Text == "香港六合彩")
            {
                lotteryCode = "hk6";
                lotteryid = 21;
            }
            else if (this.comboBox1.Text == "广东11选5")
            {
                lotteryCode = "gd11x5";
                lotteryid = 6;
            }
            else if (this.comboBox1.Text == "埃及分分彩")
            {
                lotteryCode = "FFC1";
                lotteryid = 13;
            }
            else if (this.comboBox1.Text == "埃及二分彩")
            {
                lotteryCode = "FFC2";
                lotteryid = 24;
            }
            else if (this.comboBox1.Text == "埃及五分彩")
            {
                lotteryCode = "FFC5";
                lotteryid = 25;
            }
            else if (this.comboBox1.Text == "重庆时时彩")
            {
                lotteryCode = "cqssc";
                lotteryid = 1;
            }
            else if (this.comboBox1.Text == "印尼时时彩")
            {
                lotteryCode = "INFFC5";
                lotteryid = 23;
            }
            else if (this.comboBox1.Text == "河内时时彩")
            {
                lotteryCode = "VIFFC5";
                lotteryid = 14;
            }
            else if (this.comboBox1.Text == "河内时时彩")
            {
                lotteryCode = "VIFFC5";
                lotteryid = 14;
            }

            var mLotteryIssuesData = new LotteryIssuesData();
            //计算开奖结果
            Ytg.Scheduler.Comm.Bets.BetDetailsCalculate betDetailsCalculate = new Ytg.Scheduler.Comm.Bets.BetDetailsCalculate();
            string openresult = betDetailsCalculate.Calculate(lotteryCode, this.txtIssue.Text.Trim(), this.txtOpenResult.Text.Trim());
            txtOpenResult.Text = openresult;
            bool compled = mLotteryIssuesData.UpdateResult(new OpenResultEntity()
            {
                expect = this.txtIssue.Text.Trim(),
                opencode = openresult,
                opentime = DateTime.Now
            }, lotteryCode);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox1.Checked)
                Ytg.Scheduler.Tasks.YtgJob.AijFenfenAuto = 1;
            else
                Ytg.Scheduler.Tasks.YtgJob.AijFenfenAuto = 0;
          
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox2.Checked)
                Ytg.Scheduler.Tasks.YtgJob.AiJErFenAuto = 1;
            else
                Ytg.Scheduler.Tasks.YtgJob.AiJErFenAuto = 0;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox3.Checked)
                Ytg.Scheduler.Tasks.YtgJob.AiJWuFenAuto = 1;
            else
                Ytg.Scheduler.Tasks.YtgJob.AiJWuFenAuto = 0;
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox5.Checked)
                Ytg.Scheduler.Tasks.YtgJob.YiNiWuFenAuto = 1;
            else
                Ytg.Scheduler.Tasks.YtgJob.YiNiWuFenAuto = 0;
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox4.Checked)
                Ytg.Scheduler.Tasks.YtgJob.YiNiWuFenAuto = 1;
            else
                Ytg.Scheduler.Tasks.YtgJob.YiNiWuFenAuto = 0;
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox6.Checked)
                Ytg.Scheduler.Tasks.YtgJob.Hg15Auto = 1;
            else
                Ytg.Scheduler.Tasks.YtgJob.Hg15Auto = 0;
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox7.Checked)
                Ytg.Scheduler.Tasks.YtgJob.tjAuto = 1;
            else
                Ytg.Scheduler.Tasks.YtgJob.tjAuto = 0;
        }
    }
}
