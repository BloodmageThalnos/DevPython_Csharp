using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ScintillaNET;

namespace DevPython {
    partial class Main {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.controlContentTextBox = new System.Windows.Forms.TextBox();
            this.menubarMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitemFileNew = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.menuitemFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitemFileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitemFileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuitemFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitemEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitemEditUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.menuitemEditCut = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitemEditCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitemEditPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitemEditSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.menuitemEditFind = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitemEditFindNext = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitemEditReplace = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.menuitemFormatFont = new System.Windows.Forms.ToolStripMenuItem();
            this.debugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitemHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.menuitemAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.DonateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.controlStatusBar = new System.Windows.Forms.StatusStrip();
            this.controlCaretPositionLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.sciTextArea = new ScintillaNET.Scintilla();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.sciOutputArea = new ScintillaNET.Scintilla();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.sciLogArea = new ScintillaNET.Scintilla();
            this.menubarMain.SuspendLayout();
            this.controlStatusBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // controlContentTextBox
            // 
            this.controlContentTextBox.AcceptsReturn = true;
            this.controlContentTextBox.AcceptsTab = true;
            this.controlContentTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlContentTextBox.HideSelection = false;
            this.controlContentTextBox.Location = new System.Drawing.Point(0, 24);
            this.controlContentTextBox.Margin = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.controlContentTextBox.MaxLength = 0;
            this.controlContentTextBox.Multiline = true;
            this.controlContentTextBox.Name = "controlContentTextBox";
            this.controlContentTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.controlContentTextBox.Size = new System.Drawing.Size(843, 523);
            this.controlContentTextBox.TabIndex = 0;
            this.controlContentTextBox.Visible = false;
            this.controlContentTextBox.WordWrap = false;
            this.controlContentTextBox.TextChanged += new System.EventHandler(this.controlContentTextBox_TextChanged);
            this.controlContentTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.controlContentTextBox_KeyDown);
            this.controlContentTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.controlContentTextBox_KeyUp);
            this.controlContentTextBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.controlContentTextBox_MouseDown);
            // 
            // menubarMain
            // 
            this.menubarMain.GripMargin = new System.Windows.Forms.Padding(0);
            this.menubarMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menubarMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.menuitemEdit,
            this.debugToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menubarMain.Location = new System.Drawing.Point(0, 0);
            this.menubarMain.Name = "menubarMain";
            this.menubarMain.Padding = new System.Windows.Forms.Padding(0);
            this.menubarMain.Size = new System.Drawing.Size(843, 24);
            this.menubarMain.TabIndex = 1;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuitemFileNew,
            this.toolStripSeparator7,
            this.menuitemFileOpen,
            this.menuitemFileSave,
            this.menuitemFileSaveAs,
            this.toolStripSeparator1,
            this.toolStripSeparator2,
            this.menuitemFileExit});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(70, 24);
            this.fileToolStripMenuItem.Text = "文件(&F)";
            // 
            // menuitemFileNew
            // 
            this.menuitemFileNew.Name = "menuitemFileNew";
            this.menuitemFileNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.menuitemFileNew.Size = new System.Drawing.Size(190, 26);
            this.menuitemFileNew.Text = "新建(&N)";
            this.menuitemFileNew.Click += new System.EventHandler(this.menuitemFileNew_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(187, 6);
            // 
            // menuitemFileOpen
            // 
            this.menuitemFileOpen.Name = "menuitemFileOpen";
            this.menuitemFileOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.menuitemFileOpen.Size = new System.Drawing.Size(190, 26);
            this.menuitemFileOpen.Text = "打开(&O)";
            this.menuitemFileOpen.Click += new System.EventHandler(this.menuitemFileOpen_Click);
            // 
            // menuitemFileSave
            // 
            this.menuitemFileSave.Name = "menuitemFileSave";
            this.menuitemFileSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.menuitemFileSave.Size = new System.Drawing.Size(190, 26);
            this.menuitemFileSave.Text = "保存(&S)";
            this.menuitemFileSave.Click += new System.EventHandler(this.menuitemFileSave_Click);
            // 
            // menuitemFileSaveAs
            // 
            this.menuitemFileSaveAs.Name = "menuitemFileSaveAs";
            this.menuitemFileSaveAs.Size = new System.Drawing.Size(190, 26);
            this.menuitemFileSaveAs.Text = "另存为...";
            this.menuitemFileSaveAs.Click += new System.EventHandler(this.menuitemFileSaveAs_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(187, 6);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(187, 6);
            // 
            // menuitemFileExit
            // 
            this.menuitemFileExit.Name = "menuitemFileExit";
            this.menuitemFileExit.Size = new System.Drawing.Size(190, 26);
            this.menuitemFileExit.Text = "退出(&X)";
            this.menuitemFileExit.Click += new System.EventHandler(this.menuitemFileExit_Click);
            // 
            // menuitemEdit
            // 
            this.menuitemEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuitemEditUndo,
            this.toolStripSeparator5,
            this.menuitemEditCut,
            this.menuitemEditCopy,
            this.menuitemEditPaste,
            this.menuitemEditSelectAll,
            this.toolStripSeparator6,
            this.menuitemEditFind,
            this.menuitemEditFindNext,
            this.menuitemEditReplace,
            this.toolStripSeparator3,
            this.menuitemFormatFont});
            this.menuitemEdit.Name = "menuitemEdit";
            this.menuitemEdit.Size = new System.Drawing.Size(71, 24);
            this.menuitemEdit.Text = "编辑(&E)";
            this.menuitemEdit.DropDownOpening += new System.EventHandler(this.menuitemEdit_DropDownOpening);
            // 
            // menuitemEditUndo
            // 
            this.menuitemEditUndo.Name = "menuitemEditUndo";
            this.menuitemEditUndo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.menuitemEditUndo.Size = new System.Drawing.Size(190, 26);
            this.menuitemEditUndo.Text = "撤销(&Z)";
            this.menuitemEditUndo.Click += new System.EventHandler(this.menuitemEditUndo_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(187, 6);
            // 
            // menuitemEditCut
            // 
            this.menuitemEditCut.Name = "menuitemEditCut";
            this.menuitemEditCut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.menuitemEditCut.Size = new System.Drawing.Size(190, 26);
            this.menuitemEditCut.Text = "剪切(&X)";
            this.menuitemEditCut.Click += new System.EventHandler(this.menuitemEditCut_Click);
            // 
            // menuitemEditCopy
            // 
            this.menuitemEditCopy.Name = "menuitemEditCopy";
            this.menuitemEditCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.menuitemEditCopy.Size = new System.Drawing.Size(190, 26);
            this.menuitemEditCopy.Text = "复制(&C)";
            this.menuitemEditCopy.Click += new System.EventHandler(this.menuitemEditCopy_Click);
            // 
            // menuitemEditPaste
            // 
            this.menuitemEditPaste.Name = "menuitemEditPaste";
            this.menuitemEditPaste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.menuitemEditPaste.Size = new System.Drawing.Size(190, 26);
            this.menuitemEditPaste.Text = "粘贴(&V)";
            this.menuitemEditPaste.Click += new System.EventHandler(this.menuitemEditPaste_Click);
            // 
            // menuitemEditSelectAll
            // 
            this.menuitemEditSelectAll.Name = "menuitemEditSelectAll";
            this.menuitemEditSelectAll.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.menuitemEditSelectAll.Size = new System.Drawing.Size(190, 26);
            this.menuitemEditSelectAll.Text = "全选(&A)";
            this.menuitemEditSelectAll.Click += new System.EventHandler(this.menuitemEditSelectAll_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(187, 6);
            // 
            // menuitemEditFind
            // 
            this.menuitemEditFind.Name = "menuitemEditFind";
            this.menuitemEditFind.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.menuitemEditFind.Size = new System.Drawing.Size(190, 26);
            this.menuitemEditFind.Text = "查找(&F)";
            this.menuitemEditFind.Click += new System.EventHandler(this.menuitemEditFind_Click);
            // 
            // menuitemEditFindNext
            // 
            this.menuitemEditFindNext.Name = "menuitemEditFindNext";
            this.menuitemEditFindNext.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.menuitemEditFindNext.Size = new System.Drawing.Size(190, 26);
            this.menuitemEditFindNext.Text = "查找下一个";
            this.menuitemEditFindNext.Click += new System.EventHandler(this.menuitemEditFindNext_Click);
            // 
            // menuitemEditReplace
            // 
            this.menuitemEditReplace.Name = "menuitemEditReplace";
            this.menuitemEditReplace.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
            this.menuitemEditReplace.Size = new System.Drawing.Size(190, 26);
            this.menuitemEditReplace.Text = "替换(&H)";
            this.menuitemEditReplace.Click += new System.EventHandler(this.menuitemEditReplace_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(187, 6);
            // 
            // menuitemFormatFont
            // 
            this.menuitemFormatFont.Name = "menuitemFormatFont";
            this.menuitemFormatFont.Size = new System.Drawing.Size(190, 26);
            this.menuitemFormatFont.Text = "字体(&F)";
            // 
            // debugToolStripMenuItem
            // 
            this.debugToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runToolStripMenuItem});
            this.debugToolStripMenuItem.Name = "debugToolStripMenuItem";
            this.debugToolStripMenuItem.Size = new System.Drawing.Size(72, 24);
            this.debugToolStripMenuItem.Text = "运行(&R)";
            // 
            // runToolStripMenuItem
            // 
            this.runToolStripMenuItem.Name = "runToolStripMenuItem";
            this.runToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.runToolStripMenuItem.Size = new System.Drawing.Size(220, 26);
            this.runToolStripMenuItem.Text = "运行（不调试）";
            this.runToolStripMenuItem.Click += new System.EventHandler(this.runToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuitemHelp,
            this.toolStripSeparator4,
            this.menuitemAbout,
            this.DonateToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(74, 24);
            this.helpToolStripMenuItem.Text = "帮助(&H)";
            // 
            // menuitemHelp
            // 
            this.menuitemHelp.Name = "menuitemHelp";
            this.menuitemHelp.Size = new System.Drawing.Size(216, 26);
            this.menuitemHelp.Text = "Dev Python 帮助(&H)";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(213, 6);
            // 
            // menuitemAbout
            // 
            this.menuitemAbout.Name = "menuitemAbout";
            this.menuitemAbout.Size = new System.Drawing.Size(216, 26);
            this.menuitemAbout.Text = "关于 Dev Python(&A)";
            this.menuitemAbout.Click += new System.EventHandler(this.menuitemAbout_Click);
            // 
            // DonateToolStripMenuItem
            // 
            this.DonateToolStripMenuItem.Name = "DonateToolStripMenuItem";
            this.DonateToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.DonateToolStripMenuItem.Text = "赞助...";
            // 
            // controlStatusBar
            // 
            this.controlStatusBar.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.controlStatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.controlCaretPositionLabel});
            this.controlStatusBar.Location = new System.Drawing.Point(0, 523);
            this.controlStatusBar.Name = "controlStatusBar";
            this.controlStatusBar.Padding = new System.Windows.Forms.Padding(19, 0, 1, 0);
            this.controlStatusBar.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.controlStatusBar.Size = new System.Drawing.Size(843, 24);
            this.controlStatusBar.SizingGrip = false;
            this.controlStatusBar.TabIndex = 2;
            // 
            // controlCaretPositionLabel
            // 
            this.controlCaretPositionLabel.AutoSize = false;
            this.controlCaretPositionLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.controlCaretPositionLabel.Name = "controlCaretPositionLabel";
            this.controlCaretPositionLabel.Size = new System.Drawing.Size(219, 19);
            this.controlCaretPositionLabel.Text = "Ln {LineNumber}, Col {ColumnNumber}";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.sciTextArea);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(843, 499);
            this.splitContainer1.SplitterDistance = 281;
            this.splitContainer1.TabIndex = 3;
            // 
            // sciTextArea
            // 
            this.sciTextArea.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.sciTextArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sciTextArea.FontQuality = ScintillaNET.FontQuality.LcdOptimized;
            this.sciTextArea.HScrollBar = false;
            this.sciTextArea.Lexer = ScintillaNET.Lexer.Python;
            this.sciTextArea.Location = new System.Drawing.Point(0, 0);
            this.sciTextArea.Name = "sciTextArea";
            this.sciTextArea.Size = new System.Drawing.Size(843, 281);
            this.sciTextArea.TabIndex = 0;
            // this.sciTextArea.
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(843, 214);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.sciOutputArea);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(835, 185);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "输出";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // sciOutputArea
            // 
            this.sciOutputArea.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.sciOutputArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sciOutputArea.Location = new System.Drawing.Point(3, 3);
            this.sciOutputArea.Name = "sciOutputArea";
            this.sciOutputArea.Size = new System.Drawing.Size(829, 179);
            this.sciOutputArea.TabIndex = 1;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(835, 185);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "调试";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.sciLogArea);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(835, 185);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "编译日志";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // sciLogArea
            // 
            this.sciLogArea.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.sciLogArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sciLogArea.Location = new System.Drawing.Point(3, 3);
            this.sciLogArea.Name = "sciLogArea";
            this.sciLogArea.Size = new System.Drawing.Size(829, 179);
            this.sciLogArea.TabIndex = 0;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(843, 547);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.controlStatusBar);
            this.Controls.Add(this.controlContentTextBox);
            this.Controls.Add(this.menubarMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menubarMain;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "Main";
            this.Text = "{DocumentName} - Dev Python";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Main_FormClosed);
            this.Load += new System.EventHandler(this.Main_Load);
            this.menubarMain.ResumeLayout(false);
            this.menubarMain.PerformLayout();
            this.controlStatusBar.ResumeLayout(false);
            this.controlStatusBar.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        Scintilla TextArea
        {
            get { return sciTextArea; }
            set { sciTextArea = value; }
        }

        private void InitializeTextarea()
        {
            // INITIAL VIEW CONFIG
            TextArea.WrapMode = WrapMode.None;
            TextArea.IndentationGuides = IndentView.LookBoth;

            // STYLING
            InitColors();
            InitSyntaxColoring();

            // NUMBER MARGIN
            InitNumberMargin();

            // BOOKMARK MARGIN
            InitBookmarkMargin();

            // CODE FOLDING MARGIN
            InitCodeFolding();

            // DRAG DROP
            InitDragDropFile();
        }

        public void InitDragDropFile()
        {

            TextArea.AllowDrop = true;
            TextArea.DragEnter += delegate (object sender, DragEventArgs e) {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                    e.Effect = DragDropEffects.Copy;
                else
                    e.Effect = DragDropEffects.None;
            };
            TextArea.DragDrop += delegate (object sender, DragEventArgs e)
            {

                // get file drop
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {

                    Array a = (Array)e.Data.GetData(DataFormats.FileDrop);
                    if (a != null)
                    {

                        string path = a.GetValue(0).ToString();

                        Open(path);

                    }
                }
            };
        }

        public static Color IntToColor(int rgb)
        {
            return Color.FromArgb(255, (byte)(rgb >> 16), (byte)(rgb >> 8), (byte)rgb);
        }

        public void InvokeIfNeeded(Action action)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(action);
            }
            else
            {
                action.Invoke();
            }
        }

        private void LoadDataFromFile(string path)
        {
            if (File.Exists(path))
            {
                //FileName.Text = Path.GetFileName(path);
                TextArea.Text = File.ReadAllText(path);
            }
        }

        private void InitColors()
        {

            TextArea.SetSelectionBackColor(true, IntToColor(0x114D9C));
        }

        private void InitSyntaxColoring()
        {

            // Configure the default style
            TextArea.StyleResetDefault();
            TextArea.Styles[Style.Default].Font = "Consolas";
            TextArea.Styles[Style.Default].Size = 10;
            TextArea.Styles[Style.Default].BackColor = IntToColor(0x212121);
            TextArea.Styles[Style.Default].ForeColor = IntToColor(0xFFFFFF);
            TextArea.StyleClearAll();

            // Configure the CPP (C#) lexer styles
            TextArea.Styles[Style.Cpp.Identifier].ForeColor = IntToColor(0xD0DAE2);
            TextArea.Styles[Style.Cpp.Comment].ForeColor = IntToColor(0xBD758B);
            TextArea.Styles[Style.Cpp.CommentLine].ForeColor = IntToColor(0x40BF57);
            TextArea.Styles[Style.Cpp.CommentDoc].ForeColor = IntToColor(0x2FAE35);
            TextArea.Styles[Style.Cpp.Number].ForeColor = IntToColor(0xFFFF00);
            TextArea.Styles[Style.Cpp.String].ForeColor = IntToColor(0xFFFF00);
            TextArea.Styles[Style.Cpp.Character].ForeColor = IntToColor(0xE95454);
            TextArea.Styles[Style.Cpp.Preprocessor].ForeColor = IntToColor(0x8AAFEE);
            TextArea.Styles[Style.Cpp.Operator].ForeColor = IntToColor(0xE0E0E0);
            TextArea.Styles[Style.Cpp.Regex].ForeColor = IntToColor(0xff00ff);
            TextArea.Styles[Style.Cpp.CommentLineDoc].ForeColor = IntToColor(0x77A7DB);
            TextArea.Styles[Style.Cpp.Word].ForeColor = IntToColor(0x48A8EE);
            TextArea.Styles[Style.Cpp.Word2].ForeColor = IntToColor(0xF98906);
            TextArea.Styles[Style.Cpp.CommentDocKeyword].ForeColor = IntToColor(0xB3D991);
            TextArea.Styles[Style.Cpp.CommentDocKeywordError].ForeColor = IntToColor(0xFF0000);
            TextArea.Styles[Style.Cpp.GlobalClass].ForeColor = IntToColor(0x48A8EE);

            TextArea.Lexer = Lexer.Cpp;

            TextArea.SetKeywords(0, "class extends implements import interface new case do while else if for in switch throw get set function var try catch finally while with default break continue delete return each const namespace package include use is as instanceof typeof author copy default deprecated eventType example exampleText exception haxe inheritDoc internal link mtasc mxmlc param private return see serial serialData serialField since throws usage version langversion playerversion productversion dynamic private public partial static intrinsic internal native override protected AS3 final super this arguments null Infinity NaN undefined true false abstract as base bool break by byte case catch char checked class const continue decimal default delegate do double descending explicit event extern else enum false finally fixed float for foreach from goto group if implicit in int interface internal into is lock long new null namespace object operator out override orderby params private protected public readonly ref return switch struct sbyte sealed short sizeof stackalloc static string select this throw true try typeof uint ulong unchecked unsafe ushort using var virtual volatile void while where yield");
            TextArea.SetKeywords(1, "void Null ArgumentError arguments Array Boolean Class Date DefinitionError Error EvalError Function int Math Namespace Number Object RangeError ReferenceError RegExp SecurityError String SyntaxError TypeError uint XML XMLList Boolean Byte Char DateTime Decimal Double Int16 Int32 Int64 IntPtr SByte Single UInt16 UInt32 UInt64 UIntPtr Void Path File System Windows Forms ScintillaNET");

        }

        private void OnTextChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// the background color of the text area
        /// </summary>
        private const int BACK_COLOR = 0x2A211C;

        /// <summary>
        /// default text color of the text area
        /// </summary>
        private const int FORE_COLOR = 0xB7B7B7;

        /// <summary>
        /// change this to whatever margin you want the line numbers to show in
        /// </summary>
        private const int NUMBER_MARGIN = 1;

        /// <summary>
        /// change this to whatever margin you want the bookmarks/breakpoints to show in
        /// </summary>
        private const int BOOKMARK_MARGIN = 2;
        private const int BOOKMARK_MARKER = 2;

        /// <summary>
        /// change this to whatever margin you want the code folding tree (+/-) to show in
        /// </summary>
        private const int FOLDING_MARGIN = 3;

        /// <summary>
        /// set this true to show circular buttons for code folding (the [+] and [-] buttons on the margin)
        /// </summary>
        private const bool CODEFOLDING_CIRCULAR = true;

        private void InitNumberMargin()
        {

            TextArea.Styles[Style.LineNumber].BackColor = IntToColor(BACK_COLOR);
            TextArea.Styles[Style.LineNumber].ForeColor = IntToColor(FORE_COLOR);
            TextArea.Styles[Style.IndentGuide].ForeColor = IntToColor(FORE_COLOR);
            TextArea.Styles[Style.IndentGuide].BackColor = IntToColor(BACK_COLOR);

            var nums = TextArea.Margins[NUMBER_MARGIN];
            nums.Width = 30;
            nums.Type = MarginType.Number;
            nums.Sensitive = true;
            nums.Mask = 0;
        }

        private void InitBookmarkMargin()
        {

            //TextArea.SetFoldMarginColor(true, IntToColor(BACK_COLOR));

            var margin = TextArea.Margins[BOOKMARK_MARGIN];
            margin.Width = 20;
            margin.Sensitive = true;
            margin.Type = MarginType.Symbol;
            margin.Mask = (1 << BOOKMARK_MARKER);
            //margin.Cursor = MarginCursor.Arrow;

            var marker = TextArea.Markers[BOOKMARK_MARKER];
            marker.Symbol = MarkerSymbol.Circle;
            marker.SetBackColor(IntToColor(0xFF003B));
            marker.SetForeColor(IntToColor(0x000000));
            marker.SetAlpha(100);

        }

        private void InitCodeFolding()
        {

            TextArea.SetFoldMarginColor(true, IntToColor(BACK_COLOR));
            TextArea.SetFoldMarginHighlightColor(true, IntToColor(BACK_COLOR));

            // Enable code folding
            TextArea.SetProperty("fold", "1");
            TextArea.SetProperty("fold.compact", "1");

            // Configure a margin to display folding symbols
            TextArea.Margins[FOLDING_MARGIN].Type = MarginType.Symbol;
            TextArea.Margins[FOLDING_MARGIN].Mask = Marker.MaskFolders;
            TextArea.Margins[FOLDING_MARGIN].Sensitive = true;
            TextArea.Margins[FOLDING_MARGIN].Width = 20;

            // Set colors for all folding markers
            for (int i = 25; i <= 31; i++)
            {
                TextArea.Markers[i].SetForeColor(IntToColor(BACK_COLOR)); // styles for [+] and [-]
                TextArea.Markers[i].SetBackColor(IntToColor(FORE_COLOR)); // styles for [+] and [-]
            }

            // Configure folding markers with respective symbols
            TextArea.Markers[Marker.Folder].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CirclePlus : MarkerSymbol.BoxPlus;
            TextArea.Markers[Marker.FolderOpen].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CircleMinus : MarkerSymbol.BoxMinus;
            TextArea.Markers[Marker.FolderEnd].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CirclePlusConnected : MarkerSymbol.BoxPlusConnected;
            TextArea.Markers[Marker.FolderMidTail].Symbol = MarkerSymbol.TCorner;
            TextArea.Markers[Marker.FolderOpenMid].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CircleMinusConnected : MarkerSymbol.BoxMinusConnected;
            TextArea.Markers[Marker.FolderSub].Symbol = MarkerSymbol.VLine;
            TextArea.Markers[Marker.FolderTail].Symbol = MarkerSymbol.LCorner;

            // Enable automatic folding
            TextArea.AutomaticFold = (AutomaticFold.Show | AutomaticFold.Click | AutomaticFold.Change);

        }

        private System.Windows.Forms.TextBox controlContentTextBox;
        private System.Windows.Forms.MenuStrip menubarMain;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuitemFileNew;
        private System.Windows.Forms.ToolStripMenuItem menuitemFileOpen;
        private System.Windows.Forms.ToolStripMenuItem menuitemFileSave;
        private System.Windows.Forms.ToolStripMenuItem menuitemFileSaveAs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem menuitemEdit;
        private System.Windows.Forms.ToolStripMenuItem menuitemEditFind;
        private System.Windows.Forms.ToolStripMenuItem menuitemEditFindNext;
        private System.Windows.Forms.ToolStripMenuItem menuitemEditReplace;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem menuitemEditSelectAll;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuitemHelp;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem menuitemAbout;
        private System.Windows.Forms.ToolStripMenuItem menuitemEditUndo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem menuitemEditCut;
        private System.Windows.Forms.ToolStripMenuItem menuitemEditCopy;
        private System.Windows.Forms.ToolStripMenuItem menuitemEditPaste;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.StatusStrip controlStatusBar;
        private System.Windows.Forms.ToolStripStatusLabel controlCaretPositionLabel;
        private System.Windows.Forms.ToolStripMenuItem debugToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private ScintillaNET.Scintilla sciTextArea;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private ScintillaNET.Scintilla sciOutputArea;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private ScintillaNET.Scintilla sciLogArea;
        private ToolStripMenuItem menuitemFormatFont;
        private ToolStripMenuItem DonateToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator7;
        private ToolStripMenuItem menuitemFileExit;
    }
}