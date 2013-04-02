using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AssinadorDigital
{
    public partial class frmIncludeSubFolders : Form
    {
        #region Constructor

        public frmIncludeSubFolders(string[] explorerSelectedItens, string action)
        {
            InitializeComponent();
            explorerItens = explorerSelectedItens;
            actionToPerform = action;
        }

        #endregion

        #region Private Properties

        private string[] explorerItens;
        private string actionToPerform;

        #endregion

        #region Events

        private void btnNo_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            switch(actionToPerform)
            {
                case "/r":
                    frmSelectDigitalSignatureToRemove FormSelectToRemove = new frmSelectDigitalSignatureToRemove(explorerItens, chkIncludeSubfolders.Checked);
                    FormSelectToRemove.Show();
                    this.Visible = false;
                    break;
                    /*
                frmSelectDigitalSignatureToRemove FormRemove = new frmSelectDigitalSignatureToRemove(explorerItens, chkIncludeSubfolders.Checked);
                FormRemove.Show();
                this.Visible = false;
                    break;
                    */
                case "/v":
                frmManageDigitalSignature FormManage = new frmManageDigitalSignature(explorerItens, chkIncludeSubfolders.Checked);
                FormManage.Show();
                this.Visible = false;
                break;
            }
        }

        #endregion
    }
}
