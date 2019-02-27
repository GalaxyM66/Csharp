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
    public partial class ChannelPriceForm : DockContent
    {
        #region header
        PMPriceDao dao = new PMPriceDao();
        PMBaseDao baseDao = new PMBaseDao();
        SortableBindingList<ChannelPrice> priceList = new SortableBindingList<ChannelPrice>();

        SortableBindingList<ClientsGroups> clientsGroupList = new SortableBindingList<ClientsGroups>();
        SortableBindingList<PubClients> clientsList = new SortableBindingList<PubClients>();
        SortableBindingList<ChannelPrice> clientsPriceList = new SortableBindingList<ChannelPrice>();

        SortableBindingList<GoodsGroups> goodsGroupList = new SortableBindingList<GoodsGroups>();
        SortableBindingList<PubWaredict> goodsList = new SortableBindingList<PubWaredict>();
        SortableBindingList<ChannelPrice> goodsPriceList = new SortableBindingList<ChannelPrice>();

        SortableBindingList<ChannelPrice> importList = new SortableBindingList<ChannelPrice>();
        SortableBindingList<ChannelPrice> importListTmp = new SortableBindingList<ChannelPrice>();

        ResultInfo resultMsg = new ResultInfo();
        public ChannelPriceForm()
        {
            InitializeComponent();
        }

        private void ChannelPriceForm_Load(object sender, EventArgs e)
        {
            SortableBindingList<Dictionary> stopFlagList = StringUtils.TableToEntity<Dictionary>(baseDao.GetDictionaryByTypeId("1"));
            SortableBindingList<Dictionary> originList = StringUtils.TableToEntity<Dictionary>(baseDao.GetDictionaryByTypeId("36"));
            stopFlagList.Insert(0, new Dictionary { Name = "全部", Code = "" });
            FormUtils.SetComboBox(stopFlag_p1, stopFlagList, "Name", "TagPtr");
            FormUtils.SetComboBox(stopFlag_p2, stopFlagList, "Name", "TagPtr");
            FormUtils.SetComboBox(stopFlag_p3, stopFlagList, "Name", "TagPtr");
            FormUtils.SetComboBox(priceOrigin, originList, "Name", "TagPtr");

            beginDateBegin.Format = DateTimePickerFormat.Custom;
            beginDateBegin.CustomFormat = "yyyy-MM-dd";
            beginDateBegin.Value = DateTime.Now.AddDays(1 - DateTime.Now.Day);

            beginDateEnd.Format = DateTimePickerFormat.Custom;
            beginDateEnd.CustomFormat = "yyyy-MM-dd";
            beginDateEnd.Value = DateTime.Now.AddYears(1);
        }
        #endregion
        #region queryPage
        private void QueryPriceBtn_Click(object sender, EventArgs e)
        {
            if (StringUtils.IsNull(clientsCode_p1.Text.Trim() + clientsName_p1.Text.Trim() + goodsCode_p1.Text.Trim() + goodsName_p1.Text.Trim() + spec.Text.Trim() + producer_p1.Text.Trim()))
            {
                MessageBox.Show("请输入查询条件");
                return;
            }
            RefreshDataGridView1(((Dictionary)priceOrigin.SelectedValue).Code);
        }

        private void ExportBtn_Click(object sender, EventArgs e)
        {
            FormUtils.ExcelExport(channelPriceDgv1, saveFileDialog1, "渠道价");
        }
        #endregion
        #region clientsPricePage
        private void AddGoodsBtn_Click(object sender, EventArgs e)
        {
            object obj = FormUtils.SelectRow(channelPriceDgv3);
            if (obj == null) return;
            PubClients cst = (PubClients)obj;
            SetPriceByGoodsForm form = new SetPriceByGoodsForm()
            {
                StartPosition = FormStartPosition.CenterScreen,
                clients = cst
            };
            form.ShowDialog();
        }

        private void SetPriceByClientsBtn_Click(object sender, EventArgs e)
        {
            object obj = FormUtils.SelectRows(channelPriceDgv4);
            if (obj == null) return;
            DataGridViewSelectedRowCollection row = (DataGridViewSelectedRowCollection)obj;
            ChannelPrice price = OpenForm(row.Count == 1 ? 1 : 0, (ChannelPrice)row[0].Cells[0].Value);
            if (price == null) return;
            foreach (DataGridViewRow dgvr in row)
            {
                ((ChannelPrice)dgvr.Cells[0].Value).AddValue(price);
                string value = ((ChannelPrice)dgvr.Cells[0].Value).GetString("");
                string[] result = dao.SavePrice(value, "", "executed");
                FormUtils.SetResult(result, resultMsg);
            }
            FormUtils.SendMsg(resultMsg);
            GoodsPriceQueryBtn_Click(sender, e);
        }

        private void ClientsGtoupQueryBtn_Click(object sender, EventArgs e)
        {
            RefreshDataGridView2();
        }

        private void ChannelPriceDgv2_SelectionChanged(object sender, EventArgs e)
        {
            object obj = FormUtils.SelectRow((DataGridView)sender);
            if (obj == null) return;
            ClientsGroups group = (ClientsGroups)obj;
            RefreshDataGridView3("", group.Id, group.Type);
        }

        private void GoodsPriceQueryBtn_Click(object sender, EventArgs e)
        {
            object obj = FormUtils.SelectRows(channelPriceDgv3);
            if (obj == null) return;
            DataGridViewSelectedRowCollection row = (DataGridViewSelectedRowCollection)obj;
            string idSet = "";
            int i = 0;
            foreach (DataGridViewRow dgvr in row)
            {
                idSet += FormUtils.AddMarks(((PubClients)dgvr.Cells[0].Value).Cstid, i);
                i++;
            }
            GoodsGroups cbo = new GoodsGroups();
            if (goodsGroup_p2.SelectedValue != null)
                cbo = (GoodsGroups)(goodsGroup_p2.SelectedValue);

            RefreshDataGridView4(idSet, goodsCode_p2.Text.Trim(), goodsName_p2.Text.Trim(), cbo.Id, cbo.Type);
        }

        private void GoodsGroup_p2_Click(object sender, EventArgs e)
        {
            FormUtils.GroupComboBoxSetting(sender, "goods");
        }

        private void EnableBtn_p2_Click(object sender, EventArgs e)
        {
            StopController(channelPriceDgv4, "00");
            GoodsPriceQueryBtn_Click(sender, e);
        }

        private void DisableBtn_p2_Click(object sender, EventArgs e)
        {
            StopController(channelPriceDgv4, "99");
            GoodsPriceQueryBtn_Click(sender, e);
        }

        private void ClientsQueryBtn_Click(object sender, EventArgs e)
        {
            TextForm form = new TextForm() {
                StartPosition = FormStartPosition.CenterScreen
            };
            form.ShowDialog();
            if (StringUtils.IsNull(form.resultText))
                return;
            RefreshDataGridView3(form.resultText, "", "");
        }

        private void ChannelPriceDgv3_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex >= 0)
                {
                    //若行已是选中状态就不再进行设置
                    if (channelPriceDgv3.Rows[e.RowIndex].Selected == false)
                    {
                        channelPriceDgv3.ClearSelection();
                        channelPriceDgv3.Rows[e.RowIndex].Selected = true;
                    }
                    copyClientsPriceBtn.Visible = true;
                    copyGoodsPriceBtn.Visible = false;
                    //弹出操作菜单
                    contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);
                }
            }
        }

        private void CopyClientsPriceBtn_Click(object sender, EventArgs e)
        {
            object obj = FormUtils.SelectRow(channelPriceDgv3);
            if (obj == null) return;
            PubClients cst = (PubClients)obj;
            CopyClientsPriceForm form = new CopyClientsPriceForm()
            {
                StartPosition = FormStartPosition.CenterScreen,
                copyClients = cst
            };
            form.ShowDialog();
        }

        private void ClientsCbo_Click(object sender, EventArgs e)
        {
            FormUtils.ClientsComboBoxSetting(sender);
        }

        private void ExportBtn_p2_Click(object sender, EventArgs e)
        {
            FormUtils.ExcelExport(channelPriceDgv4, saveFileDialog1, "渠道价");
        }

        private void ExpandBtn_Click(object sender, EventArgs e)
        {
            string[] result;
            object obj = FormUtils.SelectRow(channelPriceDgv3);
            if (obj == null) return;
            PubClients cst = (PubClients)obj;
            result=dao.ClientsPriceControl(cst.Cstid, "p_scm_exec_clientdata_exist");
            if ("0".Equals(result[0]))
            {
               if( MessageBox.Show("客户存在渠道价数据，是否删除原有数据？", "是否删除", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    result = dao.ClientsPriceControl(cst.Cstid, "p_scm_exec_clientdata_del");
                    if (!"1".Equals(result[0]))
                    {
                        MessageBox.Show(result[1]);
                        return;
                    }
                }
                else
                    return;
            }
            else if(!"1".Equals(result[0]))
            {
                MessageBox.Show(result[1]);
                return;
            }

            ClientPriceExpand form = new ClientPriceExpand()
            {
                StartPosition = FormStartPosition.CenterScreen,
                clients = cst
            };
            form.ShowDialog();
        }

        #endregion
        #region goodsPricePage
        private void AddClinetsBtn_Click(object sender, EventArgs e)
        {
            object obj = FormUtils.SelectRow(channelPriceDgv6);
            if (obj == null) return;
            PubWaredict goods = (PubWaredict)obj;

            SetPriceByClientsForm form = new SetPriceByClientsForm()
            {
                StartPosition = FormStartPosition.CenterScreen,
                selectGoods = goods
            };
            form.ShowDialog();
        }

        private void SetPriceByGoodsBtn_Click(object sender, EventArgs e)
        {
            object obj = FormUtils.SelectRows(channelPriceDgv7);
            if (obj == null) return;
            DataGridViewSelectedRowCollection row = (DataGridViewSelectedRowCollection)obj;
            ChannelPrice price = OpenForm(row.Count, (ChannelPrice)row[0].Cells[0].Value);
            if (price == null) return;
            foreach (DataGridViewRow dgvr in (DataGridViewSelectedRowCollection)obj)
            {
                ((ChannelPrice)dgvr.Cells[0].Value).AddValue(price);
                string value = ((ChannelPrice)dgvr.Cells[0].Value).GetString("");
                string[] result = dao.SavePrice(value, "", "executed");
                FormUtils.SetResult(result, resultMsg);
            }
            FormUtils.SendMsg(resultMsg);
            ClientsPriceQueryBtn_Click(sender, e);
        }

        private void GoodsGroupQueryBtn_Click(object sender, EventArgs e)
        {
            RefreshDataGridView5();
        }

        private void ChannelPriceDgv5_SelectionChanged(object sender, EventArgs e)
        {
            object obj = FormUtils.SelectRow((DataGridView)sender);
            if (obj == null) return;
            GoodsGroups group = (GoodsGroups)obj;
            RefreshDataGridView6("",group.Id, group.Type);
        }

        private void ClientsPriceQueryBtn_Click(object sender, EventArgs e)
        {
            object obj = FormUtils.SelectRows(channelPriceDgv6);
            if (obj == null) return;
            DataGridViewSelectedRowCollection row = (DataGridViewSelectedRowCollection)obj;
            string idSet = "";
            int i = 0;
            foreach (DataGridViewRow dgvr in row)
            {
                idSet += FormUtils.AddMarks(((PubWaredict)dgvr.Cells[0].Value).Goodid, i);
                i++;
            }
            ClientsGroups cbo = new ClientsGroups();
            if (clientsGroup_p3.SelectedValue != null)
                cbo = (ClientsGroups)(clientsGroup_p3.SelectedValue);
            DictionarySub area = new DictionarySub();
            if (areaCbo.SelectedValue != null)
                area = (DictionarySub)areaCbo.SelectedValue;
            RefreshDataGridView7(idSet, clientsCode_p3.Text.Trim(), clientsName_p3.Text.Trim(), cbo.Id, cbo.Type, area.Name);
        }

        private void ClientsGroup_p3_Click(object sender, EventArgs e)
        {
            FormUtils.GroupComboBoxSetting(sender, "clients");
        }

        private void EnableBtn_p3_Click(object sender, EventArgs e)
        {
            StopController(channelPriceDgv7, "00");
            ClientsPriceQueryBtn_Click(sender, e);
        }

        private void DisableBtn_p3_Click(object sender, EventArgs e)
        {
            StopController(channelPriceDgv7, "99");
            ClientsPriceQueryBtn_Click(sender, e);
        }

        private void GoodsQueryBtn_Click(object sender, EventArgs e)
        {
            TextForm form = new TextForm()
            {
                StartPosition = FormStartPosition.CenterScreen
            };
            form.ShowDialog();
            if (StringUtils.IsNull(form.resultText))
                return;
            RefreshDataGridView6(form.resultText, "", "");
        }

        private void GoodsCbo_Click(object sender, EventArgs e)
        {
            FormUtils.GoodsComboBoxSetting(sender);
        }

        private void ExportBtn_p3_Click(object sender, EventArgs e)
        {
            FormUtils.ExcelExport(channelPriceDgv7, saveFileDialog1, "渠道价");
        }

        private void ChannelPriceDgv6_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex >= 0)
                {
                    //若行已是选中状态就不再进行设置
                    if (channelPriceDgv6.Rows[e.RowIndex].Selected == false)
                    {
                        channelPriceDgv6.ClearSelection();
                        channelPriceDgv6.Rows[e.RowIndex].Selected = true;
                    }
                    //弹出操作菜单
                    copyClientsPriceBtn.Visible = false;
                    copyGoodsPriceBtn.Visible = true;
                    contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);
                }
            }
        }

        private void CopyGoodsPriceBtn_Click(object sender, EventArgs e)
        {
            object obj = FormUtils.SelectRow(channelPriceDgv6);
            if (obj == null) return;
            PubWaredict goods = (PubWaredict)obj;
            CopyGoodsPriceForm form = new CopyGoodsPriceForm()
            {
                StartPosition = FormStartPosition.CenterScreen,
                copyGoods = goods
            };
            form.ShowDialog();
        }

        private void AreaCbo_Click(object sender, EventArgs e)
        {
            FormUtils.AreasComboBoxSetting(sender);
        }
        #endregion
        #region excelPage
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
                    importList = StringUtils.TableToEntity<ChannelPrice>(ds.Tables[0]);
                    importList.Remove(importList.First());
                }
                catch (Exception ex)
                {
                    msg = ex.Message;
                }
                MessageBox.Show(msg);
            }
            FormUtils.RefreshDataGridView(channelPriceDgv8, importList);
            checkBtn.Enabled = true;
            importBtn.Enabled = false;
            errorExport.Visible = false;
        }
        private void CheckBtn_Click(object sender, EventArgs e)
        {
            int i = 0;
            if (importList.Count == 0) return;
            foreach (ChannelPrice importData in importList)
            {
                importData.Needcheck = "1";
                importData.Checktext = "";
                DataChack(importData);
                if ("0".Equals(importData.Needcheck))
                {
                    i++;
                    continue;
                }
                string[] result = PMPriceDao.CheckData(importData.GetString("E"), "executed");
                importData.Needcheck = result[0];
                importData.Checktext += result[1];
                if ("0".Equals(importData.Needcheck)) i++;
            }
            FormUtils.RefreshDataGridView(channelPriceDgv8, importList);
            importBtn.Enabled = true;
            if (i > 0) errorExport.Visible = true;
        }

        private void ImportBtn_Click(object sender, EventArgs e)
        {
            importListTmp.Clear();
            foreach (ChannelPrice price in importList)
            {
                if (!"1".Equals(price.Needcheck))
                {
                    importListTmp.Add(price);
                    continue;
                }
                price.Source = "03";
                string value = price.GetString("E");
                //MessageBox.Show(value);
                string[] result = dao.SavePrice(value, "excel", "executed");
                result[1] += ",序号:" + price.Id;
                FormUtils.SetResult(result, resultMsg);
            }
            FormUtils.SendMsg(resultMsg);
            FormUtils.RefreshDataGridView(channelPriceDgv8, importListTmp);
        }

        private void ChannelPriceDgv8_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < channelPriceDgv8.Rows.Count; i++)
            {
                ChannelPrice price = (ChannelPrice)(channelPriceDgv8.Rows[i].Cells[0].Value);
                if (!"1".Equals(price.Needcheck))
                {
                    channelPriceDgv8.Rows[i].DefaultCellStyle.ForeColor = Color.Green;
                }
            }
        }

        private void TemplateDownload_Click(object sender, EventArgs e)
        {
           FormUtils.TmpDownload(saveFileDialog1, "渠道价模板", Properties.Settings.Default.PRICE_TMP_URL);
        }


        private void ErrorExport_Click(object sender, EventArgs e)
        {
            FormUtils.ExcelExport(channelPriceDgv8, saveFileDialog1, "错误列表");
        }
        #endregion
        #region public
        private void DataChack(ChannelPrice data)
        {
            Regex folatReg = new Regex(@"^(([0-9]+\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\.[0-9]+)|([0-9]*[1-9][0-9]*))$");
            //Regex dateReg = new Regex(@"^(([0-9][0-9])|([0-9]{3}[1-9]|[0-9]{2}[1-9][0-9]{1}|[0-9]{1}[1-9] [0-9]{2}|[1-9][0-9]{3}))([-]|[.]|[/])(((([13578]|1[02])([-]|[.]|[/])([1-9]|[12][0-9]|3[01]))|((0[13578]|1[02])([-]|[.]|[/])(0[1-9]|[12][0-9]|3[01])))|((([469]|11)([-]|[.]|[/])([1-9]|[12][0-9]|30))|((0[469]|11)([-]|[.]|[/])(0[1-9]|[12] [0-9]|30)))|((2([-]|[.]|[/])([1-9]|[1][0-9]|2[0-8]))|(02([-]|[.]|[/])(0[1-9]|[1][0-9]|2[0-8]))))$");
            Regex bargainReg = new Regex(@"^[\u662f\u5426]$");
            //Regex creditReg = new Regex(@"(\u8d4a\u9500\u4ef7|\u73b0\u6b3e\u4ef7)");
            if (!folatReg.IsMatch(data.Prc) ||
             !folatReg.IsMatch(data.Costprc) ||
             !folatReg.IsMatch(data.Bottomprc))
            {
                data.Needcheck = "0";
                data.Checktext += "数值非法,";
            }
            //if (!DateTime.TryParse(data.Begindate, out DateTime begindate) ||
            //!DateTime.TryParse(data.Enddate,out DateTime enddate))
            //{
            //    data.Needcheck = "0";
            //    data.Checktext += "日期非法,";
            //}
            if (!bargainReg.IsMatch(data.Bargain))
            {
                data.Needcheck = "0";
                data.Checktext += "参数非法,";
            }
        }

        private ChannelPrice OpenForm(int count,ChannelPrice price)
        {
            if (count == 0) return null;
            SetPriceForm form = new SetPriceForm()
            {
                StartPosition = FormStartPosition.CenterScreen
            };
            if (count == 1)
                form.priceResult = price;
            else
                form.priceResult.Outrate = price.Outrate;
            if (form.ShowDialog(this) == DialogResult.OK)
                return form.priceResult;
            else
                return null;
        }

        private void RefreshDataGridView1(string tableName)
        {
            priceList = StringUtils.TableToEntity<ChannelPrice>(dao.GetPriceByTable(clientsCode_p1.Text.Trim(), clientsName_p1.Text.Trim(), goodsCode_p1.Text.Trim(), goodsName_p1.Text.Trim(), spec.Text.Trim(), producer_p1.Text.Trim(), ((Dictionary)stopFlag_p1.SelectedValue).Code, tableName, beginDateBegin.Text, beginDateEnd.Text, ""));
            FormUtils.RefreshDataGridView(channelPriceDgv1, priceList);
            count.Text = "共" + priceList.Count.ToString() + "条数据。";
        }

        private void RefreshDataGridView2()
        {
            clientsGroupList = StringUtils.TableToEntity<ClientsGroups>(dao.GetCliensGroups(clientsGroupCode_p2.Text.Trim(), clientsGroupName_p2.Text.Trim(),"00"));
            FormUtils.RefreshDataGridView(channelPriceDgv2, clientsGroupList);
        }

        private void RefreshDataGridView3(string clientsCodes, string groupId, string type)
        {
            clientsPriceList.Clear();
            clientsList = StringUtils.TableToEntity<PubClients>(baseDao.GetClientsByGroupId(clientsCodes, "", groupId, type, ""));
            FormUtils.RefreshDataGridView(channelPriceDgv3, clientsList);
        }

        private void RefreshDataGridView4(string clientsId, string goodsCode, string goodsName, string goodsGroupId, string goodsGroupType)
        {
            clientsPriceList = StringUtils.TableToEntity<ChannelPrice>(dao.GetPriceByClientsId(clientsId, goodsCode, goodsName, goodsGroupId, goodsGroupType, ((Dictionary)stopFlag_p2.SelectedValue).Code));
            FormUtils.RefreshDataGridView(channelPriceDgv4, clientsPriceList);
            channelPriceDgv4.ClearSelection();
            count_p2.Text = "共" + clientsPriceList.Count.ToString() + "条数据。";
        }

        private void RefreshDataGridView5()
        {
            goodsGroupList = StringUtils.TableToEntity<GoodsGroups>(dao.GetGoodsGroups(goodsGroupCode_p3.Text.Trim(), goodsGroupName_p3.Text.Trim(),"00"));
            FormUtils.RefreshDataGridView(channelPriceDgv5, goodsGroupList);
        }

        private void RefreshDataGridView6(string goodsCodes, string groupId, string type)
        {
            goodsPriceList.Clear();
            goodsList = StringUtils.TableToEntity<PubWaredict>(baseDao.GetGoodsByGroupId(goodsCodes, "", groupId, type, ""));
            FormUtils.RefreshDataGridView(channelPriceDgv6, goodsList);
        }

        private void RefreshDataGridView7(string goodsId, string clientsCode, string clientsName, string clientsGroupId, string clientsGroupType,string area)
        {
            goodsPriceList = StringUtils.TableToEntity<ChannelPrice>(dao.GetPriceByGoodsId(goodsId, clientsCode, clientsName, clientsGroupId, clientsGroupType, ((Dictionary)stopFlag_p3.SelectedValue).Code,area));
            FormUtils.RefreshDataGridView(channelPriceDgv7, goodsPriceList);
            channelPriceDgv7.ClearSelection();
            count_p3.Text = "共" + goodsPriceList.Count.ToString() + "条数据。";
        }

        private void CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            FormUtils.CellFormatting(sender, e);
        }

        private void StopController(DataGridView dgv, string stopFlag)
        {
            object obj = FormUtils.SelectRows(dgv);
            if (obj == null) return;
            foreach (DataGridViewRow dgvr in (DataGridViewSelectedRowCollection)obj)
            {
                ((ChannelPrice)dgvr.Cells[0].Value).Stopflag = stopFlag;
                string value = ((ChannelPrice)dgvr.Cells[0].Value).GetString("");
                string[] result = dao.SavePrice(value,"", "executed");
                FormUtils.SetResult(result, resultMsg);
            }
            FormUtils.SendMsg(resultMsg);
        }

        private void DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                ChannelPrice price = (ChannelPrice)(dgv.Rows[i].Cells[0].Value);
                //if ("below".Equals(price.Belowbottomprc))
                //{
                //    dgv.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                //}
                if ("draft".Equals(price.Origin))
                {
                    dgv.Rows[i].DefaultCellStyle.BackColor = Color.LightGray;
                }
            }
        }
        #endregion

        private void ChannelPriceForm_KeyDown(object sender, KeyEventArgs e)
        {
            if ("gfDataGridView".Equals(ActiveControl.GetType().Name))
                return;
            if (e.KeyCode == Keys.Enter)
            {
                switch (tabControl1.SelectedTab.Name)
                {
                    case "queryPage":
                        QueryPriceBtn_Click(sender, e);
                        break;
                    case "clientsPricePage":
                        GoodsPriceQueryBtn_Click(sender, e);
                        break;
                    case "goodsPricePage":
                        ClientsPriceQueryBtn_Click(sender, e);
                        break;
                    default: break;
                }
            }
        }

    }
}