using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
 
 
 
using System.Reflection;
using System.Text.RegularExpressions;

namespace PriceManager
{
    public partial class SalePriceForm : DockContent
    {
        #region header
        PMPriceDao dao = new PMPriceDao();
        PMBaseDao baseDao = new PMBaseDao();
        SortableBindingList<SalePrice> priceList = new SortableBindingList<SalePrice>();

        SortableBindingList<ClientsGroups> clientsGroupList = new SortableBindingList<ClientsGroups>();
        SortableBindingList<PubClients> clientsList = new SortableBindingList<PubClients>();
        SortableBindingList<SalePrice> clientsPriceList = new SortableBindingList<SalePrice>();

        SortableBindingList<GoodsGroups> goodsGroupList = new SortableBindingList<GoodsGroups>();
        SortableBindingList<PubWaredict> goodsList = new SortableBindingList<PubWaredict>();
        SortableBindingList<SalePrice> goodsPriceList = new SortableBindingList<SalePrice>();

        SortableBindingList<SalePrice> importList = new SortableBindingList<SalePrice>();
        SortableBindingList<SalePrice> importListTmp = new SortableBindingList<SalePrice>();

        ResultInfo resultMsg = new ResultInfo();
        public SalePriceForm()
        {
            InitializeComponent();
        }

        private void SalePriceForm_Load(object sender, EventArgs e)
        {
            SortableBindingList<Dictionary> stopFlagList = StringUtils.TableToEntity<Dictionary>(baseDao.GetDictionaryByTypeId("1"));
            stopFlagList.Insert(0, new Dictionary { Name = "全部", Code = "" });
            FormUtils.SetComboBox(stopFlag_p1, stopFlagList, "Name", "TagPtr");
            FormUtils.SetComboBox(stopFlag_p2, stopFlagList, "Name", "TagPtr");
            FormUtils.SetComboBox(stopFlag_p3, stopFlagList, "Name", "TagPtr");
        }
        #endregion
        #region queryPage
        private void QueryPriceBtn_Click(object sender, EventArgs e)
        {
            RefreshDataGridView1();
        }

        private void ExportBtn_Click(object sender, EventArgs e)
        {
            FormUtils.ExcelExport(salePriceDgv1, saveFileDialog1, "促销价表");
        }
        #endregion

        #region clientsSalePricePage
        private void ClientsGtoupQueryBtn_Click(object sender, EventArgs e)
        {
            RefreshDataGridView2();
        }

        private void ClientsQueryBtn_Click(object sender, EventArgs e)
        {
            if (StringUtils.IsNull(clientsName_p2.Text))
            {
                MessageBox.Show("避免查询时间过长,请填写查询参数");
                clientsName_p2.Focus();
                return;
            }
            RefreshDataGridView3(clientsName_p2.Text.Trim(), "", "");
        }

        private void GoodsPriceQueryBtn_Click(object sender, EventArgs e)
        {
            DataGridView3_SelectionChanged(salePriceDgv3, e);
        }

        private void AddGoodsBtn_Click(object sender, EventArgs e)
        {
            object obj = FormUtils.SelectRow(salePriceDgv3);
            if (obj == null) return;
            PubClients cst = (PubClients)obj;
            SetSalePriceByGoodsForm form = new SetSalePriceByGoodsForm()
            {
                StartPosition = FormStartPosition.CenterScreen,
                clients = cst
            };
            form.ShowDialog();
        }

        private void SetPriceByClientsBtn_Click(object sender, EventArgs e)
        {
            object obj = FormUtils.SelectRows(salePriceDgv4);
            if (obj == null) return;
            DataGridViewSelectedRowCollection row = (DataGridViewSelectedRowCollection)obj;
            SalePrice salePrice = OpenForm(row.Count == 1 ? 1 : 0, (SalePrice)row[0].Cells[0].Value);
            if (salePrice == null) return;
            foreach (DataGridViewRow dgvr in row)
            {
                ((SalePrice)dgvr.Cells[0].Value).AddValue(salePrice);
                string value = ((SalePrice)dgvr.Cells[0].Value).GetString("");
                string[] result = dao.SavePrice(value, "", "off");
                FormUtils.SetResult(result, resultMsg);
            }
            FormUtils.SendMsg(resultMsg);
            DataGridView3_SelectionChanged(salePriceDgv3, e);
        }

        private void EnableBtn_p2_Click(object sender, EventArgs e)
        {
            StopController(salePriceDgv4, "00");
            DataGridView3_SelectionChanged(salePriceDgv3, e);
        }

        private void DisableBtn_p2_Click(object sender, EventArgs e)
        {
            StopController(salePriceDgv4, "99");
            DataGridView3_SelectionChanged(salePriceDgv3, e);
        }

        private void DataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            object obj = FormUtils.SelectRow((DataGridView)sender);
            if (obj == null) return;
            ClientsGroups group = (ClientsGroups)obj;
            RefreshDataGridView3(clientsName_p2.Text.Trim(), group.Id, group.Type);
        }

        private void DataGridView3_SelectionChanged(object sender, EventArgs e)
        {
            object obj = FormUtils.SelectRow((DataGridView)sender);
            if (obj == null) return;
            PubClients cst = (PubClients)obj;
            GoodsGroups cbo = new GoodsGroups();
            if (goodsGroup_p2.SelectedValue != null)
                cbo = (GoodsGroups)(goodsGroup_p2.SelectedValue);
            RefreshDataGridView4(cst.Cstid, goodsCode_p2.Text.Trim(), goodsName_p2.Text.Trim(), cbo.Id, cbo.Type);
        }

        private void GoodsGroup_p2_Click(object sender, EventArgs e)
        {
            FormUtils.GroupComboBoxSetting(sender, "goods");
        }
        #endregion

        #region goodsSalePricePage
        private void GoodsGroupQueryBtn_Click(object sender, EventArgs e)
        {
            RefreshDataGridView5();
        }

        private void GoodsQueryBtn_Click(object sender, EventArgs e)
        {
            if (StringUtils.IsNull(goodsName_p3.Text))
            {
                MessageBox.Show("避免查询时间过长,请填写查询参数");
                goodsName_p3.Focus();
                return;
            }
            RefreshDataGridView6(goodsName_p3.Text.Trim(), "", "");
        }

        private void ClientsPriceQueryBtn_Click(object sender, EventArgs e)
        {
            DataGridView6_SelectionChanged(salePriceDgv6, e);
        }

        private void AddClinetsBtn_Click(object sender, EventArgs e)
        {
            object obj = FormUtils.SelectRow(salePriceDgv6);
            if (obj == null) return;
            PubWaredict goods = (PubWaredict)obj;

            SetSalePriceByClientsForm form = new SetSalePriceByClientsForm()
            {
                StartPosition = FormStartPosition.CenterScreen,
                goods = goods
            };
            form.ShowDialog();
        }

        private void SetPriceByGoodsBtn_Click(object sender, EventArgs e)
        {
            object obj = FormUtils.SelectRows(salePriceDgv7);
            if (obj == null) return;
            DataGridViewSelectedRowCollection row = (DataGridViewSelectedRowCollection)obj;
            SalePrice salePrice = OpenForm(row.Count, (SalePrice)row[0].Cells[0].Value);
            if (salePrice == null) return;
            foreach (DataGridViewRow dgvr in (DataGridViewSelectedRowCollection)obj)
            {
                ((SalePrice)dgvr.Cells[0].Value).AddValue(salePrice);
                string value = ((SalePrice)dgvr.Cells[0].Value).GetString("");
                string[] result = dao.SavePrice(value, "", "off");
                FormUtils.SetResult(result, resultMsg);
            }
            FormUtils.SendMsg(resultMsg);
            DataGridView6_SelectionChanged(salePriceDgv6, e);
        }

        private void EnableBtn_p3_Click(object sender, EventArgs e)
        {
            StopController(salePriceDgv7, "00");
            DataGridView6_SelectionChanged(salePriceDgv6, e);
        }

        private void DisableBtn_p3_Click(object sender, EventArgs e)
        {
            StopController(salePriceDgv7, "99");
            DataGridView6_SelectionChanged(salePriceDgv6, e);
        }

        private void DataGridView5_SelectionChanged(object sender, EventArgs e)
        {
            object obj = FormUtils.SelectRow((DataGridView)sender);
            if (obj == null) return;
            GoodsGroups group = (GoodsGroups)obj;
            RefreshDataGridView6(goodsName_p3.Text.Trim(), group.Id, group.Type);
        }

        private void DataGridView6_SelectionChanged(object sender, EventArgs e)
        {
            object obj = FormUtils.SelectRow((DataGridView)sender);
            if (obj == null) return;
            PubWaredict goods = (PubWaredict)obj;
            ClientsGroups cbo = new ClientsGroups();
            if (clientsGroup_p3.SelectedValue != null)
                cbo = (ClientsGroups)(clientsGroup_p3.SelectedValue);
            RefreshDataGridView7(goods.Goodid, clientsCode_p3.Text.Trim(), clientsName_p3.Text.Trim(), cbo.Id, cbo.Type);
        }

        private void ClientsGroup_p3_Click(object sender, EventArgs e)
        {
            FormUtils.GroupComboBoxSetting(sender, "clients");
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
                    importList = StringUtils.TableToEntity<SalePrice>(ds.Tables[0]);
                    importList.Remove(importList.First());
                }
                catch (Exception ex)
                {
                    msg = ex.Message;
                }
                MessageBox.Show(msg);
            }
            FormUtils.RefreshDataGridView(salePriceDgv8, importList);
            checkBtn.Enabled = true;
            importBtn.Enabled = false;
        }

        private void CheckBtn_Click(object sender, EventArgs e)
        {
            if (importList.Count == 0) return;
            foreach (var importData in importList)
            {
                importData.Needcheck = "1";
                importData.Checktext = "";
                DataChack(importData);
                string[] result = PMPriceDao.CheckData(importData.GetString("E"), "off");
                importData.Needcheck = result[0];
                importData.Checktext += result[1];
            }
            FormUtils.RefreshDataGridView(salePriceDgv8, importList);
            importBtn.Enabled = true;
        }

        private void ImportBtn_Click(object sender, EventArgs e)
        {
            importListTmp.Clear();
            foreach (var saleprice in importList)
            {
                if (!"1".Equals(saleprice.Needcheck))
                {
                    importListTmp.Add(saleprice);
                    continue;
                }
                string value = saleprice.GetString("E");
                string[] result = dao.SavePrice(value, "excel", "off");
                FormUtils.SetResult(result, resultMsg);
            }
            FormUtils.SendMsg(resultMsg);
            FormUtils.RefreshDataGridView(salePriceDgv8, importListTmp);
        }
        #endregion

        #region public
        private void DataChack(SalePrice data)
        {
        }

        private SalePrice OpenForm(int count ,SalePrice saleprice)
        {
            if (count == 0) return null;
            SetSalePriceForm form = new SetSalePriceForm()
            {
                StartPosition = FormStartPosition.CenterScreen,
            };
            if (count == 1)
                form.salePriceResult = saleprice;
            else
                form.salePriceResult.Outrate = saleprice.Outrate;
            if (form.ShowDialog(this) == DialogResult.OK)
                return form.salePriceResult;
            else
                return null;
        }

        private void RefreshDataGridView1()
        {
            priceList = StringUtils.TableToEntity<SalePrice>(dao.GetSalePrice(clientsCode_p1.Text.Trim(), goodsCode_p1.Text.Trim(), ((Dictionary)stopFlag_p1.SelectedValue).Code));
            FormUtils.RefreshDataGridView(salePriceDgv1, priceList);
        }

        private void RefreshDataGridView2()
        {
            clientsGroupList = StringUtils.TableToEntity<ClientsGroups>(dao.GetCliensGroups(clientsGroupCode_p2.Text.Trim(), clientsGroupName_p2.Text.Trim(),"00"));
            FormUtils.RefreshDataGridView(salePriceDgv2, clientsGroupList);
        }

        private void RefreshDataGridView3(string clientsName, string groupId, string type)
        {
            clientsPriceList.Clear();
            clientsList = StringUtils.TableToEntity<PubClients>(baseDao.GetClientsByGroupId("", clientsName, groupId, type, ""));
            FormUtils.RefreshDataGridView(salePriceDgv3, clientsList);
        }

        private void RefreshDataGridView4(string clientsId, string goodsCode, string goodsName, string goodsGroupId, string goodsGroupType)
        {
            clientsPriceList = StringUtils.TableToEntity<SalePrice>(dao.GetSalePriceByClientsId(clientsId, goodsCode, goodsName, goodsGroupId, goodsGroupType, ((Dictionary)stopFlag_p2.SelectedValue).Code));
            FormUtils.RefreshDataGridView(salePriceDgv4, clientsPriceList);
            salePriceDgv4.ClearSelection();
        }

        private void RefreshDataGridView5()
        {
            goodsGroupList = StringUtils.TableToEntity<GoodsGroups>(dao.GetGoodsGroups(goodsGroupCode_p3.Text.Trim(), goodsGroupName_p3.Text.Trim(),"00"));
            FormUtils.RefreshDataGridView(salePriceDgv5, goodsGroupList);
        }

        private void RefreshDataGridView6(string goodsName, string groupId, string type)
        {
            goodsPriceList.Clear();
            goodsList = StringUtils.TableToEntity<PubWaredict>(baseDao.GetGoodsByGroupId("", goodsName, groupId, type, ""));
            FormUtils.RefreshDataGridView(salePriceDgv6, goodsList);
        }

        private void RefreshDataGridView7(string goodsId, string clientsCode, string clientsName, string clientsGroupId, string clientsGroupType)
        {
            goodsPriceList = StringUtils.TableToEntity<SalePrice>(dao.GetSalePriceByGoodsId(goodsId, clientsCode, clientsName, clientsGroupId, clientsGroupType, ((Dictionary)stopFlag_p3.SelectedValue).Code));
            FormUtils.RefreshDataGridView(salePriceDgv7, goodsPriceList);
            salePriceDgv7.ClearSelection();
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
                ((SalePrice)dgvr.Cells[0].Value).Stopflag = stopFlag;
                string value = ((SalePrice)dgvr.Cells[0].Value).GetString("");
                string[] result = dao.SavePrice(value, "", "off");
                FormUtils.SetResult(result, resultMsg);
            }
            FormUtils.SendMsg(resultMsg);
        }
        #endregion

        private void SalePriceForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                switch (tabControl1.SelectedTab.Name)
                {
                    case "querySalePage":
                        QueryPriceBtn_Click(sender, e);
                        break;
                    case "clientsSalePricePage":
                        GoodsPriceQueryBtn_Click(sender, e);
                        break;
                    case "goodsSalePricePage":
                        ClientsPriceQueryBtn_Click(null, null);
                        break;
                    default: break;
                }
            }
        }

        private void TemplateDownload_Click(object sender, EventArgs e)
        {

        }
    }
}
