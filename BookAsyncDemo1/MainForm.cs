using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookAsyncDemo1
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        /// <summary>页面加载的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {

            //ListView初始化
            this.datashow.View = View.Details;
            this.datashow.HeaderStyle = ColumnHeaderStyle.None;
            this.datashow.FullRowSelect = true;
            this.datashow.Columns.Add("");
            this.datashow.Columns[0].Width = this.datashow.Width - 24;

            init();
        }
        /// <summary>初始化数据
        /// </summary>
        public void init()
        {
            this.datashow.Items.Insert(0, "初始化完成：");
            this.datashow.Items[0].ForeColor = Color.Red;

        }
        #region 事件
        /// <summary>等待一组任务完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void button1_Click(object sender, EventArgs e)
        {
            Task<int> task1 = Task.Run(() =>
            {
                Thread.Sleep(1000);
                this.datashow.Invoke((MethodInvoker)
                   (() =>
                   {
                       this.datashow.Items.Insert(0, "run：task1");
                       this.datashow.Items[0].ForeColor = Color.Red;
                   }));
                return Enumerable.Range(1, 1000).Sum();
            });

            Task<int> task2 = Task.Run(() =>
            {
                Thread.Sleep(1000);
                this.datashow.Invoke((MethodInvoker)
                   (() =>
                   {
                       this.datashow.Items.Insert(0, "run：task2");
                       this.datashow.Items[0].ForeColor = Color.Red;
                   }));
                return Enumerable.Range(1, 1000).Sum();
            });
            Task<int> task3 = Task.Run(() =>
            {
                Thread.Sleep(1000);
                this.datashow.Invoke((MethodInvoker)
                   (() =>
                   {
                       this.datashow.Items.Insert(0, "run：task3");
                       this.datashow.Items[0].ForeColor = Color.Red;
                   }));
                return Enumerable.Range(1, 1000).Sum();
            });

            int[] results = await Task.WhenAll(task1, task2, task3);
            this.datashow.Invoke((MethodInvoker)
                   (() =>
                   {
                       this.datashow.Items.Insert(0, string.Join("#", results));
                       this.datashow.Items[0].ForeColor = Color.Red;
                   }));
        }
        /// <summary>任意任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void button3_Click(object sender, EventArgs e)
        {
            Task<int> task1 = Task.Run(() =>
            {
                Thread.Sleep(1000);
                this.datashow.Invoke((MethodInvoker)
                   (() =>
                   {
                       this.datashow.Items.Insert(0, "run：task1");
                       this.datashow.Items[0].ForeColor = Color.Red;
                   }));
                return Enumerable.Range(1, 1000).Sum();
            });

            Task<int> task2 = Task.Run(() =>
            {
                Thread.Sleep(3000);
                this.datashow.Invoke((MethodInvoker)
                   (() =>
                   {
                       this.datashow.Items.Insert(0, "run：task2");
                       this.datashow.Items[0].ForeColor = Color.Red;
                   }));
                return Enumerable.Range(1, 1000).Sum();
            });
            Task<int> task3 = Task.Run(() =>
            {
                Thread.Sleep(1000);
                this.datashow.Invoke((MethodInvoker)
                   (() =>
                   {
                       this.datashow.Items.Insert(0, "run：task3");
                       this.datashow.Items[0].ForeColor = Color.Red;
                   }));
                return Enumerable.Range(1, 1000).Sum();
            });
            //第一个完成的任务
            Task<int> results = await Task.WhenAny(task1, task2, task3);
            //防止等待
            Task cwt = results.ContinueWith(m =>
            {
                this.datashow.Invoke((MethodInvoker)
                (() =>
                {
                    this.datashow.Items.Insert(0, m.Result.ToString());
                    this.datashow.Items[0].ForeColor = Color.Red;
                }));
            });


        }
        /// <summary>系统消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 1; i < 10; i++)
            {
                Task.Run(() =>
                {
                    Thread.Sleep(100);
                    this.datashow.Invoke((MethodInvoker)
                    (() =>
                    {
                        this.datashow.Items.Insert(0, "出现如下错误：" + i);
                        this.datashow.Items[0].ForeColor = Color.Red;
                    }));
                });
            }

        }
        #endregion



    }
}
