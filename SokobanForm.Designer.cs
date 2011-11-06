using System.Windows.Forms;

namespace Sokoban
{
    partial class SokobanForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.topPanel = new System.Windows.Forms.TableLayoutPanel();
            this.nextButton = new SokobanButton(this);
            this.prevButton = new SokobanButton(this);
            this.undoButton = new SokobanButton(this);
            this.resetButton = new SokobanButton(this);
            this.redoButton = new SokobanButton(this);
            this.moveLabel = new ToolStripStatusLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.levelLabel = new Label();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.topPanel.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.topPanel);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.sokobanTableLayout);
            this.splitContainer1.Panel2.Controls.Add(this.statusStrip1);
            this.splitContainer1.Size = new System.Drawing.Size(442, 486);
            this.splitContainer1.SplitterDistance = 70;
            this.splitContainer1.TabIndex = 0;
            this.splitContainer1.TabStop = false;
            // 
            // topPanel
            // 
            this.topPanel.ColumnCount = 5;
            this.topPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.topPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.topPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.topPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.topPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.topPanel.Controls.Add(this.resetButton, 0, 1);
            this.topPanel.Controls.Add(this.prevButton, 1, 1);
            this.topPanel.Controls.Add(this.nextButton, 2, 1);
            this.topPanel.Controls.Add(this.undoButton, 3, 1);
            this.topPanel.Controls.Add(this.redoButton, 4, 1);
            this.topPanel.Controls.Add(this.levelLabel, 0, 0);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.RowCount = 2;
            this.topPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.topPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.topPanel.Size = new System.Drawing.Size(438, 66);
            this.topPanel.TabIndex = 0;
            this.topPanel.TabStop = false;
            // 
            // nextButton
            // 
            this.nextButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nextButton.Enabled = levelManager.HasNextLevel;
            this.nextButton.Location = new System.Drawing.Point(221, 29);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(103, 34);
            this.nextButton.TabIndex = 0;
            this.nextButton.TabStop = false;
            this.nextButton.Text = "&Next";
            this.nextButton.UseVisualStyleBackColor = true;
            this.nextButton.Click += new System.EventHandler(nextButton_Click);
            // 
            // prevButton
            // 
            this.prevButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.prevButton.Enabled = levelManager.HasPreviousLevel;
            this.prevButton.Location = new System.Drawing.Point(112, 29);
            this.prevButton.Name = "prevButton";
            this.prevButton.Size = new System.Drawing.Size(103, 34);
            this.prevButton.TabIndex = 0;
            this.prevButton.TabStop = false;
            this.prevButton.Text = "&Previous";
            this.prevButton.UseVisualStyleBackColor = true;
            this.prevButton.Click += new System.EventHandler(prevButton_Click);
            // 
            // undoButton
            // 
            this.undoButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.undoButton.Location = new System.Drawing.Point(330, 29);
            this.undoButton.Name = "undoButton";
            this.undoButton.Size = new System.Drawing.Size(105, 34);
            this.undoButton.TabIndex = 0;
            this.undoButton.TabStop = false;
            this.undoButton.Text = "&Undo";
            this.undoButton.UseVisualStyleBackColor = true;
            this.undoButton.Enabled = false;
            this.undoButton.Click += new System.EventHandler(undoButton_Click);
            this.sokobanTableLayout.UndoStackStatusChange += new StackStatusHandler(sokobanUndoStackStatusChange);
            // 
            // redoButton
            // 
            this.redoButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.redoButton.Location = new System.Drawing.Point(330, 29);
            this.redoButton.Name = "redoButton";
            this.redoButton.Size = new System.Drawing.Size(105, 34);
            this.redoButton.TabIndex = 0;
            this.redoButton.TabStop = false;
            this.redoButton.Text = "R&edo";
            this.redoButton.UseVisualStyleBackColor = true;
            this.redoButton.Enabled = false;
            this.redoButton.Click += new System.EventHandler(redoButton_Click);
            this.sokobanTableLayout.RedoStackStatusChange += new StackStatusHandler(sokobanRedoStackStatusChange);
            // 
            // resetButton
            // 
            this.resetButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resetButton.Location = new System.Drawing.Point(3, 29);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(103, 34);
            this.resetButton.TabIndex = 0;
            this.resetButton.TabStop = false;
            this.resetButton.Text = "&Reset";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(resetButton_Click);
            // 
            // levelLabel
            // 
            this.levelLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.levelLabel.AutoSize = true;
            this.topPanel.SetColumnSpan(this.levelLabel, 2);
            this.levelLabel.Location = new System.Drawing.Point(3, 6);
            this.levelLabel.Name = "levelLabel";
            this.levelLabel.Size = new System.Drawing.Size(212, 13);
            this.levelLabel.TabIndex = 4;
            this.levelLabel.Text = "Level: " + levelManager.CurrentLevel;
            // 
            // sokobanTableLayout
            // 
            this.sokobanTableLayout.BackColor = System.Drawing.Color.Black;
            this.sokobanTableLayout.ColumnCount = 21;
            this.sokobanTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.761906F));
            this.sokobanTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.761906F));
            this.sokobanTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.761906F));
            this.sokobanTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.761906F));
            this.sokobanTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.761906F));
            this.sokobanTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.761906F));
            this.sokobanTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.761906F));
            this.sokobanTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.761906F));
            this.sokobanTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.761906F));
            this.sokobanTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.761906F));
            this.sokobanTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.761906F));
            this.sokobanTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.761906F));
            this.sokobanTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.761906F));
            this.sokobanTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.761906F));
            this.sokobanTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.761906F));
            this.sokobanTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.761906F));
            this.sokobanTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.761906F));
            this.sokobanTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.761906F));
            this.sokobanTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.761906F));
            this.sokobanTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.761906F));
            this.sokobanTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.761906F));
            this.sokobanTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sokobanTableLayout.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.sokobanTableLayout.Location = new System.Drawing.Point(0, 0);
            this.sokobanTableLayout.Name = "sokobanTableLayout";
            this.sokobanTableLayout.RowCount = 18;
            this.sokobanTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.555555F));
            this.sokobanTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.555555F));
            this.sokobanTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.555555F));
            this.sokobanTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.555555F));
            this.sokobanTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.555555F));
            this.sokobanTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.555555F));
            this.sokobanTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.555555F));
            this.sokobanTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.555555F));
            this.sokobanTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.555555F));
            this.sokobanTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.555555F));
            this.sokobanTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.555555F));
            this.sokobanTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.555555F));
            this.sokobanTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.555555F));
            this.sokobanTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.555555F));
            this.sokobanTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.555555F));
            this.sokobanTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.555555F));
            this.sokobanTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.555555F));
            this.sokobanTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.555555F));
            this.sokobanTableLayout.Size = new System.Drawing.Size(438, 386);
            this.sokobanTableLayout.TabIndex = 0;
            this.sokobanTableLayout.TabStop = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {this.moveLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 386);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip1.Size = new System.Drawing.Size(438, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.TabStop = false;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // moveLabel
            // 
            this.moveLabel.Name = "moveLabel";
            this.moveLabel.Size = new System.Drawing.Size(45, 17);
            this.moveLabel.Text = "Moves: " + this.sokobanTableLayout.NumMoves;
            // 
            // SokobanForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 486);
            this.Controls.Add(this.splitContainer1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(450, 520);
            this.MinimumSize = new System.Drawing.Size(450, 520);
            this.Name = "SokobanForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sokoban";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.topPanel.ResumeLayout(false);
            this.topPanel.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private SplitContainer splitContainer1;
        private StatusStrip statusStrip1;
        private Label levelLabel;
        private TableLayoutPanel topPanel;
        private Button resetButton;
        private Button nextButton;
        private Button prevButton;
        private Button undoButton;
        private Button redoButton;
        private ToolStripStatusLabel moveLabel;
        private readonly SokobanTableLayout sokobanTableLayout;
    }
}

