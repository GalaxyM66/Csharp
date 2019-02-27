using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
 
 
 

namespace PriceManager
{
    public partial class UsersForm : DockContent
    {
        SortableBindingList<PubEmp> empList = new SortableBindingList<PubEmp>();
        SortableBindingList<PubDept> deptList = new SortableBindingList<PubDept>();
        SortableBindingList<PubDept> empdeptList = new SortableBindingList<PubDept>();
        PMSystemDao dao = new PMSystemDao();
        PMBaseDao baseDao = new PMBaseDao();
        public UsersForm()
        {
            InitializeComponent();
        }

        private void UsersForm_Load(object sender, EventArgs e)
        {
            SortableBindingList<Dictionary> stopFlagList = StringUtils.TableToEntity<Dictionary>(baseDao.GetDictionaryByTypeId("1"));
            SortableBindingList<Dictionary> stopFlagList_s = StringUtils.TableToEntity<Dictionary>(baseDao.GetDictionaryByTypeId("1"));
            stopFlagList.Insert(0, new Dictionary { Name = "全部", Code = "" });
            FormUtils.SetComboBox(stopFlag_s, stopFlagList_s, "Name", "TagPtr");
            FormUtils.SetComboBox(stopFlag_q, stopFlagList, "Name", "TagPtr");
            FormUtils.SetComboBox(allowLogin_s, StringUtils.TableToEntity<Dictionary>(baseDao.GetDictionaryByTypeId("23")), "Name", "TagPtr");
            FormUtils.SetComboBox(empRole_s, StringUtils.TableToEntity<PubRole>(dao.GetAllRole()), "Rolename", "TagPtr");
        }

        private void QueryBtn_Click(object sender, EventArgs e)
        {
            InitForm();
            RefreshDataGridView1();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (StringUtils.IsNull(empId_s.Text))
            {
                MessageBox.Show("请选择用户");
                return;
            }
            string[] result = dao.SaveEmp(empId_s.Text.Trim(), empPwd_s.Text.Trim(), ((PubRole)(empRole_s.SelectedValue)).Roleid, ((Dictionary)stopFlag_s.SelectedValue).Code, ((Dictionary)allowLogin_s.SelectedValue).Code);
            if ("1".Equals(result[0]))
            {
                QueryBtn_Click(sender, e);
            }
            else
            {
                MessageBox.Show(result[1]);
            }
        }

        private void InBtn_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection row = userDgv3.SelectedRows;
            if (row.Count != 1) return;
            PubDept dept = (PubDept)(row[0].Cells[0].Value);
            empdeptList.Add(dept);
            deptList.Remove(dept);
        }

        private void OutBtn_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection row = userDgv2.SelectedRows;
            if (row.Count != 1) return;
            PubDept dept = (PubDept)(row[0].Cells[0].Value);
            deptList.Add(dept);
            empdeptList.Remove(dept);
        }
        //保存可登陆部门
        private void SaveDeptBtn_Click(object sender, EventArgs e)
        {
            if (StringUtils.IsNull(empId_s.Text))
            {
                MessageBox.Show("请选择用户");
                return;
            }

            string deptIdList = "";
            foreach (PubDept pd in empdeptList)
            {
                deptIdList += pd.Saledeptid + ",";
            }

            string[] result = dao.SaveEmpDept(deptIdList, empId_s.Text.Trim());
            if ("1".Equals(result[0]))
            {
                QueryBtn_Click(sender, e);
            }
            else
            {
                MessageBox.Show(result[1]);
            }
        }

        private void RefreshDataGridView1()
        {
            empList = StringUtils.TableToEntity<PubEmp>(dao.GetEmp(empCode_q.Text.Trim(), empName_q.Text.Trim(), ((Dictionary)stopFlag_q.SelectedValue).Code,""));
            FormUtils.RefreshDataGridView(userDgv1, empList);
            userDgv1.Focus();
        }
        private void RefreshDataGridView2()
        {
            empdeptList = StringUtils.TableToEntity<PubDept>(dao.GetEmpDept(empId_s.Text.Trim()));
            FormUtils.RefreshDataGridView(userDgv2, empdeptList);
        }
        private void RefreshDataGridView3()
        {
            deptList = StringUtils.TableToEntity<PubDept>(dao.GetEmpNoDept(empId_s.Text.Trim()));
            FormUtils.RefreshDataGridView(userDgv3, deptList);
        }

        private void InitForm()
        {
            empId_s.Text = "";
            empPwd_s.Text = "";
            stopFlag_s.SelectedIndex = 0;
            empRole_s.SelectedIndex = 0;
            allowLogin_s.SelectedIndex = 0;
            deptList.Clear();
            empdeptList.Clear();
        }

        private void UserDgv1_SelectionChanged(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection row = userDgv1.SelectedRows;
            if (row.Count != 1) return;
            PubEmp emp = (PubEmp)(row[0].Cells[0].Value);
            empId_s.Text = emp.Empid;
            empRole_s.SelectedIndex = empRole_s.FindString(emp.Rolename);
            stopFlag_s.SelectedIndex = stopFlag_s.FindString(emp.Stopflagname);
            allowLogin_s.SelectedIndex = allowLogin_s.FindString(emp.Allowloginname);
            //--可登陆部门
            RefreshDataGridView2();
            RefreshDataGridView3();
        }
        //从CMS获取用户
        private void ImputEmpBtn_Click(object sender, EventArgs e)
        {

        }

        private void ImputDeptBtn_Click(object sender, EventArgs e)
        {

        }
    }
}
