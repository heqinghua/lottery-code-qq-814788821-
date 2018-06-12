using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace Ytg.ServerWeb
{
    public partial class Content : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
                //DataGridGroupRows(gridData, 0);
                DataGridGroupRows(gridData,1);//都按第2列合并
                //DataGridGroupRows(gridData, 4);
                //DataGridGroupRows(gridData, 5);
                if (0 == 0)//判断是否有返点
                {
                    VisibleCell();
                }
            }
        }

        protected void VisibleCell()
        {
            gridData.Columns[4].Visible = true;
        }

        public void BindData()
        {
            try {
                string url = Server.MapPath(@"/xml/xmlData.xml");//获得当前文件夹下的XML文件
                StreamReader sRead = new StreamReader(url, System.Text.Encoding.GetEncoding("GB2312"));
                //以一种特定的编码从字节流读取字符,必须要转化成GB2312读取才不能出乱码
                XmlDataDocument datadoc = new XmlDataDocument();//操作XML文档
                datadoc.DataSet.ReadXml(sRead);//将读取的字节流存到DataSet里面去
                sRead.Close();
                this.gridData.DataSource = datadoc.DataSet.Tables[0];
                this.gridData.DataBind();
            }
            catch (Exception ex) {
                throw ex;
            }
        }
        /// <summary>
        /// 合并DataGrid行
        /// </summary>
        /// <param >DataGrid对象</param>
        /// <param >合并的行</param>
        private void DataGridGroupRows(DataGrid grd, int iCol)
        {
            if (grd.Items.Count <= 1)
            {
                return;
            }
            int col = iCol;
            TableCell oGrdCell = grd.Items[0].Cells[col];
            TableCell oGrdCell0 = grd.Items[0].Cells[0];
            TableCell oGrdCell4 = grd.Items[0].Cells[4];
            TableCell oGrdCell5 = grd.Items[0].Cells[5];
            for (int i = 1; i < grd.Items.Count; i++)
            {
                TableCell nGrdCell = grd.Items[i].Cells[col];
                TableCell nGrdCell0 = grd.Items[i].Cells[0];
                TableCell nGrdCell4 = grd.Items[i].Cells[4];
                TableCell nGrdCell5 = grd.Items[i].Cells[5];
                if (nGrdCell.Text == oGrdCell.Text)
                {
                    nGrdCell.Visible = false;
                    nGrdCell0.Visible = false;
                    nGrdCell4.Visible = false;
                    nGrdCell5.Visible = false;
                    if (oGrdCell.RowSpan == 0)
                    {
                        oGrdCell.RowSpan = 1;
                    }
                    if (oGrdCell0.RowSpan == 0)
                    {
                        oGrdCell0.RowSpan = 1;
                    }
                    if (oGrdCell4.RowSpan == 0)
                    {
                        oGrdCell4.RowSpan = 1;
                    }
                    if (oGrdCell5.RowSpan == 0)
                    {
                        oGrdCell5.RowSpan = 1;
                    }
                    oGrdCell.RowSpan = oGrdCell.RowSpan + 1;
                    oGrdCell.VerticalAlign = VerticalAlign.Middle;

                    oGrdCell0.RowSpan = oGrdCell0.RowSpan + 1;
                    oGrdCell0.VerticalAlign = VerticalAlign.Middle;

                    oGrdCell4.RowSpan = oGrdCell4.RowSpan + 1;
                    oGrdCell4.VerticalAlign = VerticalAlign.Middle;

                    oGrdCell5.RowSpan = oGrdCell5.RowSpan + 1;
                    oGrdCell5.VerticalAlign = VerticalAlign.Middle;
                }
                else
                {
                    oGrdCell = nGrdCell;
                    oGrdCell0 = nGrdCell0;
                    oGrdCell4 = nGrdCell4;
                    oGrdCell5 = nGrdCell5;
                }
            }
        }
    }
}