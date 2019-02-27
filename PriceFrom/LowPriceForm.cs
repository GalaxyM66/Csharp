using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
 
 
 
using System.Text.RegularExpressions;

namespace PriceManager
{
    public partial class LowPriceForm : DockContent
    {
        PMBaseDao baseDao = new PMBaseDao();
        PMPriceDao dao = new PMPriceDao();
        SortableBindingList<LowPrice> lowPriceList = new SortableBindingList<LowPrice>();
        SortableBindingList<PubWaredict> goods = null;
        SortableBindingList<LowPrice> importList = new SortableBindingList<LowPrice>();
        SortableBindingList<LowPrice> importListTmp = new SortableBindingList<LowPrice>();
        LowPrice lowPriceValue = null;
        ResultInfo resultMsg = new ResultInfo();
        public LowPriceForm()
        {
            InitializeComponent();
        }

        private void LowPriceForm_Load(object sender, EventArgs e)
        {
            //TODO 可优化
            SortableBindingList<Dictionary> stopFlagList = StringUtils.TableToEntity<Dictionary>(baseDao.GetDictionaryByTypeId("1"));
            SortableBindingList<Dictionary> stopFlagList_s = StringUtils.TableToEntity<Dictionary>(baseDao.GetDictionaryByTypeId("1"));
            SortableBindingList<Dictionary> clientTypeGroupList = StringUtils.TableToEntity<Dictionary>(baseDao.GetDictionaryByTypeId("26"));
            SortableBindingList<Dictionary> clientTypeGroupList_s = StringUtils.TableToEntity<Dictionary>(baseDao.GetDictionaryByTypeId("26"));
            SortableBindingList<Dictionary> isModifyExecList = StringUtils.TableToEntity<Dictionary>(baseDao.GetDictionaryByTypeId("27"));
            SortableBindingList<Dictionary> isModifyExecList_s = StringUtils.TableToEntity<Dictionary>(baseDao.GetDictionaryByTypeId("27"));
            stopFlagList.Insert(0, new Dictionary { Name = "全部", Code = "" });
            clientTypeGroupList.Insert(0, new Dictionary { Name = "全部", Code = "" });
            isModifyExecList.Insert(0, new Dictionary { Name = "全部", Code = "" });
            clientTypeGroupList_s.Insert(0, new Dictionary { Name = "全部", Code = "all" });
            FormUtils.SetComboBox(stopFlag_s, stopFlagList_s, "Name", "TagPtr");
            FormUtils.SetComboBox(stopFlag_q, stopFlagList, "Name", "TagPtr");
            FormUtils.SetComboBox(clientTypeGroup_q, clientTypeGroupList, "Name", "TagPtr");
            FormUtils.SetComboBox(clientTypeGroup_s, clientTypeGroupList_s, "Name", "TagPtr");
            FormUtils.SetComboBox(isModifyExec_q, isModifyExecList, "Name", "TagPtr");
            FormUtils.SetComboBox(isModifyExec_s, isModifyExecList_s, "Name", "TagPtr");

            //--声明事件,禁止鼠标滚轮操作
            bottomPrcUpDown.MouseWheel += new MouseEventHandler(FormUtils.Num_DiscountAmount_MouseWheel);
            costPrcUpDown.MouseWheel += new MouseEventHandler(FormUtils.Num_DiscountAmount_MouseWheel);
            deliveryFeeRateUpDown.MouseWheel += new MouseEventHandler(FormUtils.Num_DiscountAmount_MouseWheel);
            suggestPrcUpDown.MouseWheel += new MouseEventHandler(FormUtils.Num_DiscountAmount_MouseWheel);
        }
        #region 维护
        private void QueryBtn_Click(object sender, EventArgs e)
        {
            InitForm();
            RefreshDataGridView1();
        }

        private void RefreshDataGridView1()
        {
            PubWaredict cbo = new PubWaredict();
            if (goods_q.SelectedValue != null)
                cbo = (PubWaredict)(goods_q.SelectedValue);
            lowPriceList = StringUtils.TableToEntity<LowPrice>(dao.GetLowPrice(cbo.Goodid,"","","","", ((Dictionary)stopFlag_q.SelectedValue).Code,
                ((Dictionary)clientTypeGroup_q.SelectedValue).Code,((Dictionary)isModifyExec_q.SelectedValue).Code,""));
            FormUtils.RefreshDataGridView(lowPriceDgv1, lowPriceList);
            count.Text = "共" + lowPriceList.Count.ToString() + "条数据。";
        }

        private void LowPriceDgv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            FormUtils.CellFormatting(sender, e);
        }

        private void LowPriceDgv1_SelectionChanged(object sender, EventArgs e)
        {
            object obj = FormUtils.SelectRow(lowPriceDgv1);
            if (obj == null) return;
            LowPrice lowPrice = (LowPrice)obj;

            goods = new SortableBindingList<PubWaredict>
            {
                new PubWaredict()
                {
                    Goodid = lowPrice.Goodid,
                    Goods = lowPrice.Goods,
                    Outrate = lowPrice.Outrate,
                    Name = lowPrice.Name,
                    Spec = lowPrice.Spec,
                    Producer = lowPrice.Producer
                }
            };
            FormUtils.SetComboBox(goods_s, goods, "Name", "TagPtr");
            goodsCode.Text = lowPrice.Goods;
            spec.Text = lowPrice.Spec;
            producer.Text = lowPrice.Producer;
            goods_s.Enabled = false;
            bottomPrcUpDown.Value = StringUtils.ToDecimal(lowPrice.Prc);
            bottomPriceUpDown.Value = StringUtils.ToDecimal(lowPrice.Price);
            costPrcUpDown.Value = StringUtils.ToDecimal(lowPrice.Costprc);
            costPriceUpDown.Value = StringUtils.ToDecimal(lowPrice.Costprice);
            costRateUpDown.Value = StringUtils.ToDecimal(lowPrice.Costrate);
            stopFlag_s.SelectedIndex = stopFlag_s.FindString(lowPrice.Stopflagname);
            isModifyExec_s.SelectedIndex = isModifyExec_s.FindString(lowPrice.Ismodifyexecname);
            deliveryFeeRateUpDown.Value = StringUtils.ToDecimal(lowPrice.Deliveryfeerate);
            clientTypeGroup_s.SelectedIndex = clientTypeGroup_s.FindString(lowPrice.Clienttypegroupname);
            clientTypeGroup_s.Enabled = false;
        }

        private void Cbo_Click(object sender, EventArgs e)
        {
            FormUtils.GoodsComboBoxSetting(sender);
            if ("goods_q".Equals(((ComboBox)sender).Name) && goods_q.DataSource != null)
            {
                FormUtils.SetComboBox(goods_s,((SortableBindingList<PubWaredict>)goods_q.DataSource), "Name", "TagPtr");
            }

            if (goods_s.DataSource != null)
            {
                goodsCode.Text = ((PubWaredict)goods_s.SelectedValue).Goods;
                spec.Text = ((PubWaredict)goods_s.SelectedValue).Spec;
                producer.Text = ((PubWaredict)goods_s.SelectedValue).Producer;
            }
        }

        private void NewBtn_Click(object sender, EventArgs e)
        {
            InitForm();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (StringUtils.IsNull(goods_s.Text) || costRateUpDown.Value == 0 ||
                bottomPrcUpDown.Value == 0 || costPrcUpDown.Value == 0 ||
                suggestPrcUpDown.Value == 0)
            {
                MessageBox.Show("参数错误");
                return;
            }
            SetValue();
            string[] result = { "", "" };
            string value = lowPriceValue.GetString();
            result = dao.SavePrice(value, "", "bottom");
            if ("1".Equals(result[0]))
            {
                QueryBtn_Click(sender, e);
                ChangeConfirm();
            }
            else
            {
                MessageBox.Show(result[1]);
            }
        }

        private void ChangeConfirm()
        {
            ChangeConfirmation form = new ChangeConfirmation()
            {
                StartPosition = FormStartPosition.CenterScreen,
                lowPrice = lowPriceValue
            };
            form.ShowDialog();
        }

        private void SetValue()
        {
            lowPriceValue = new LowPrice()
            {
                Compid = Properties.Settings.Default.COMPID,
                Ownerid = Properties.Settings.Default.OWNERID,
                Prc = bottomPrcUpDown.Value.ToString(),
                Price = bottomPriceUpDown.Value.ToString(),
                Costprc = costPrcUpDown.Value.ToString(),
                Costprice = costPrcUpDown.Value.ToString(),
                Costrate = costRateUpDown.Value.ToString(),
                Stopflag = ((Dictionary)stopFlag_s.SelectedValue).Code,
                Goodid = ((PubWaredict)goods_s.SelectedValue).Goodid,
                Deliveryfeerate = deliveryFeeRateUpDown.Value.ToString(),
                Ismodifyexec = ((Dictionary)isModifyExec_s.SelectedValue).Code,
                Clienttypegroup = ((Dictionary)clientTypeGroup_s.SelectedValue).Code,
                Outrate = ((PubWaredict)goods_s.SelectedValue).Outrate,
                Suggestexecprc = suggestPrcUpDown.Value.ToString()
            };
        }

        private void InitForm()
        {
            bottomPrcUpDown.Value = 0;
            bottomPriceUpDown.Value = 0;
            costPrcUpDown.Value = 0;
            costPriceUpDown.Value = 0;
            costRateUpDown.Value = 0;
            stopFlag_s.SelectedIndex = 0;
            suggestPrcUpDown.Value = 0;
            //goods_s.DataSource = null;
            //spec.Text = "";
            //producer.Text = "";
            goods_s.Enabled = true;
            isModifyExec_s.SelectedIndex = 0;
            deliveryFeeRateUpDown.Value = 0;
            clientTypeGroup_s.SelectedIndex = 0;
            clientTypeGroup_s.Enabled = true;
        }

        private void PrcUpDown_ValueChanged(object sender, EventArgs e)
        {
            bottomPriceUpDown.Value = bottomPrcUpDown.Value / (1 + (goods_s.DataSource == null ? 0 : StringUtils.ToDecimal(((PubWaredict)goods_s.SelectedValue).Outrate)));
            DeliveryFeeRateUpDown_ValueChanged(sender, e);
        }

        private void CostprcUpDown_ValueChanged(object sender, EventArgs e)
        {
            costPriceUpDown.Value = costPrcUpDown.Value / (1 + (goods_s.DataSource == null ? 0 : StringUtils.ToDecimal(((PubWaredict)goods_s.SelectedValue).Outrate)));
            DeliveryFeeRateUpDown_ValueChanged(sender, e);
        }

        private void DeliveryFeeRateUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (bottomPrcUpDown.Value == 0 || bottomPrcUpDown.Value < costPrcUpDown.Value)
            {
                costRateUpDown.Value = 0;
                return;
            }
            suggestPrcUpDown.Value = bottomPrcUpDown.Value * (1 + deliveryFeeRateUpDown.Value);
            costRateUpDown.Value = 1 - (costPrcUpDown.Value / suggestPrcUpDown.Value);
        }

        private void SuggestPrcUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (bottomPrcUpDown.Value == 0 || bottomPrcUpDown.Value > suggestPrcUpDown.Value || (2 * bottomPrcUpDown.Value) < suggestPrcUpDown.Value)
            {
                deliveryFeeRateUpDown.Value = 0;
                return;
            }
            deliveryFeeRateUpDown.Value = (suggestPrcUpDown.Value / bottomPrcUpDown.Value) - 1;
        }

        private void ExportBtn_Click(object sender, EventArgs e)
        {
            FormUtils.ExcelExport(lowPriceDgv1, saveFileDialog1, "底价");
        }
        #endregion

        #region 导入
        private void SelectExcelBtn_Click(object sender, EventArgs e)
        {
            importList.Clear();
            openFileDialog1.Filter = "所有文件(*.*)|*.*";
            DataSet ds = null;
            string msg = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filePath.Text = openFileDialog1.FileName;
                try
                {
                    ds = ExcelHelper.GetDataSetByExcel(filePath.Text, "Sheet1", out msg);
                    importList = StringUtils.TableToEntity<LowPrice>(ds.Tables[0]);
                    importList.Remove(importList.First());
                }
                catch(Exception ex) {
                    msg = ex.Message;
                }
                MessageBox.Show(msg);
            }
            FormUtils.RefreshDataGridView(lowPriceDgv2, importList);
            checkBtn.Enabled = true;
            importBtn.Enabled = false;
            errorExport.Visible = false;
        }

        private void CheckBtn_Click(object sender, EventArgs e)
        {
            int i = 0;
            if (importList.Count == 0) return;
            foreach (var importData in importList)
            {
                importData.Needcheck = "1";
                importData.Checktext = "";
                DataChack(importData);
                if ("0".Equals(importData.Needcheck))
                {
                    i++;
                    continue;
                }
                string[] result = PMPriceDao.CheckData(importData.GetExcelString(), "bottom");
                importData.Needcheck = result[0];
                importData.Checktext += result[1];
                if ("0".Equals(importData.Needcheck)) i++;
            }
            FormUtils.RefreshDataGridView(lowPriceDgv2, importList);
            importBtn.Enabled = true;
            if (i > 0) errorExport.Visible = true;
        }

        private void DataChack(LowPrice data)
        {
            Regex folatReg = new Regex(@"^(([0-9]+\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\.[0-9]+)|([0-9]*[1-9][0-9]*))$");
            Regex flagReg = new Regex(@"(\u6b63\u5e38|\u505c\u7528)");
            Regex boolReg = new Regex(@"^[\u662f\u5426]$");
            if (!folatReg.IsMatch(data.Prc) ||
             !folatReg.IsMatch(data.Costprc))
            {
                data.Needcheck = "0";
                data.Checktext += "数值非法,";
            }
            if (!"0".Equals(data.Deliveryfeerate))
            {
                if (!folatReg.IsMatch(data.Deliveryfeerate))
                {
                    data.Needcheck = "0";
                    data.Checktext += "数值非法,";
                }
            }
            if (!flagReg.IsMatch(data.Stopflag) ||
                !boolReg.IsMatch(data.Ismodifyexec) ||
                !boolReg.IsMatch(data.Iscover) ||
                !boolReg.IsMatch(data.Iscreate))
            {
                data.Needcheck = "0";
                data.Checktext += "参数非法,";
            }
        }

        private void ImportBtn_Click(object sender, EventArgs e)
        {
            importListTmp.Clear();
            foreach (var price in importList)
            {
                if (!"1".Equals(price.Needcheck))
                {
                    importListTmp.Add(price);
                    continue;
                }
                string value = price.GetExcelString();
                //MessageBox.Show(value);
                string[] result = dao.SavePrice(value, "excel", "bottom");
                result[1] += ",序号:" + price.Id;
                FormUtils.SetResult(result, resultMsg);
            }
            FormUtils.SendMsg(resultMsg);
            FormUtils.RefreshDataGridView(lowPriceDgv2, importListTmp);
        }

        private void LowPriceDgv2_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < lowPriceDgv2.Rows.Count; i++)
            {
                LowPrice price = (LowPrice)(lowPriceDgv2.Rows[i].Cells[0].Value);
                if (!"1".Equals(price.Needcheck))
                {
                    lowPriceDgv2.Rows[i].DefaultCellStyle.ForeColor = Color.Green;
                }
            }
        }

        private void TemplateDownload_Click(object sender, EventArgs e)
        {
            FormUtils.TmpDownload(saveFileDialog1, "底价模板", Properties.Settings.Default.LOWPRICE_TMP_URL);
        }

        private void ErrorExport_Click(object sender, EventArgs e)
        {
            FormUtils.ExcelExport(lowPriceDgv2, saveFileDialog1, "错误数据");
        }
        #endregion

        private void LowPriceForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if ("tabPage1".Equals(tabControl1.SelectedTab.Name))
                {
                    QueryBtn_Click(sender, e);
                }
            }
        }
    }
}
