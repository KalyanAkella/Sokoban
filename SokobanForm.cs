using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Sokoban
{
    public partial class SokobanForm : Form
    {
        private readonly LevelManager levelManager = new LevelManager();

        public SokobanForm()
        {
            sokobanTableLayout = new SokobanTableLayout(levelManager.CurrentLevelContent);
            InitializeComponent();
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            levelManager.CurrentLevel++;
            prevButton.Enabled = levelManager.HasPreviousLevel;
            nextButton.Enabled = levelManager.HasNextLevel;
            levelLabel.Text = "Level: " + levelManager.CurrentLevel;
            moveLabel.Text = "Moves: " + (sokobanTableLayout.NumMoves = 0);
            sokobanTableLayout.CurrentContent = levelManager.CurrentLevelContent;
            sokobanTableLayout.ResetCurrentPosition();
            sokobanTableLayout.FlushStacks();
            sokobanTableLayout.PerformLayout();
        }

        private void prevButton_Click(object sender, EventArgs e)
        {
            levelManager.CurrentLevel--;
            prevButton.Enabled = levelManager.HasPreviousLevel;
            nextButton.Enabled = levelManager.HasNextLevel;
            levelLabel.Text = "Level: " + levelManager.CurrentLevel;
            moveLabel.Text = "Moves: " + (sokobanTableLayout.NumMoves = 0);
            sokobanTableLayout.CurrentContent = levelManager.CurrentLevelContent;
            sokobanTableLayout.ResetCurrentPosition();
            sokobanTableLayout.FlushStacks();
            sokobanTableLayout.PerformLayout();
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            moveLabel.Text = "Moves: " + (sokobanTableLayout.NumMoves = 0);
            sokobanTableLayout.CurrentContent = levelManager.CurrentLevelContent;
            sokobanTableLayout.ResetCurrentPosition();
            sokobanTableLayout.FlushStacks();
            sokobanTableLayout.PerformLayout();
        }

        private void undoButton_Click(object sender, EventArgs e)
        {
            moveLabel.Text = "Moves: " + (--sokobanTableLayout.NumMoves);
            sokobanTableLayout.UndoState();
        }

        private void redoButton_Click(object sender, EventArgs e)
        {
            moveLabel.Text = "Moves: " + (++sokobanTableLayout.NumMoves);
            sokobanTableLayout.RedoState();
        }

        protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            base.OnPreviewKeyDown(e);
            if (e.KeyCode == Keys.Up) handleUpArrow(e);
            else if (e.KeyCode == Keys.Down) handleDownArrow(e);
            else if (e.KeyCode == Keys.Left) handleLeftArrow(e);
            else if (e.KeyCode == Keys.Right) handleRightArrow(e);
        }

        public void handleRightArrow(PreviewKeyDownEventArgs e)
        {
            if (sokobanTableLayout.MoveCaret(SokobanMovement.RIGHT)) moveLabel.Text = "Moves: " + ++sokobanTableLayout.NumMoves;
        }

        public void handleLeftArrow(PreviewKeyDownEventArgs e)
        {
            if (sokobanTableLayout.MoveCaret(SokobanMovement.LEFT)) moveLabel.Text = "Moves: " + ++sokobanTableLayout.NumMoves;
        }

        public void handleDownArrow(PreviewKeyDownEventArgs e)
        {
            if (sokobanTableLayout.MoveCaret(SokobanMovement.DOWN)) moveLabel.Text = "Moves: " + ++sokobanTableLayout.NumMoves;
        }

        public void handleUpArrow(PreviewKeyDownEventArgs e)
        {
            if (sokobanTableLayout.MoveCaret(SokobanMovement.UP)) moveLabel.Text = "Moves: " + ++sokobanTableLayout.NumMoves;
        }

        private void sokobanUndoStackStatusChange(object sender, StackStatusEvent e)
        {
            if (e.NumStackItems == 0) undoButton.Enabled = false;
            else undoButton.Enabled = true;
        }

        private void sokobanRedoStackStatusChange(object sender, StackStatusEvent e)
        {
            if (e.NumStackItems == 0) redoButton.Enabled = false;
            else redoButton.Enabled = true;
        }
    }

    public enum SokobanMovement
    {
        UP, DOWN, LEFT, RIGHT
    }
    
    public class StackStatusEvent : EventArgs
    {
        private readonly int numStackItems;

        public StackStatusEvent(int numStackItems)
        {
            this.numStackItems = numStackItems;
        }

        public int NumStackItems
        {
            get { return numStackItems; }
        }
    }

    public delegate void StackStatusHandler(object sender, StackStatusEvent e);

    class SokobanTableLayout : TableLayoutPanel
    {
        private byte[,] currentContent;
        private int numMoves = 0;
        private int currentRow = -1, currentColumn = -1;
        private readonly Rectangle[,] cellBounds;
        private readonly Stack<SokobanState> undoStack;
        private readonly Stack<SokobanState> redoStack;

        public event StackStatusHandler UndoStackStatusChange;
        public event StackStatusHandler RedoStackStatusChange;

        public SokobanTableLayout(byte[,] currentContent)
        {
            this.currentContent = currentContent;
            cellBounds = new Rectangle[18,21];
            undoStack = new Stack<SokobanState>();
            redoStack = new Stack<SokobanState>();
        }

        protected virtual void OnUndoStackStatusChange(StackStatusEvent e)
        {
            if (UndoStackStatusChange != null)
            {
                UndoStackStatusChange(this, e);
            }
        }

        protected virtual void OnRedoStackStatusChange(StackStatusEvent e)
        {
            if (RedoStackStatusChange != null)
            {
                RedoStackStatusChange(this, e);
            }
        }

        private void PushStateOnUndoStack(SokobanState state)
        {
            undoStack.Push(state);
            OnUndoStackStatusChange(new StackStatusEvent(undoStack.Count));
        }

        private void PushStateOnRedoStack(SokobanState state)
        {
            redoStack.Push(state);
            OnRedoStackStatusChange(new StackStatusEvent(redoStack.Count));
        }

        private SokobanState PopStateFromUndoStack()
        {
            if (undoStack.Count == 0) return null;
            SokobanState poppedState = undoStack.Pop();
            OnUndoStackStatusChange(new StackStatusEvent(undoStack.Count));
            return poppedState;
        }

        private SokobanState PopStateFromRedoStack()
        {
            if (redoStack.Count == 0) return null;
            SokobanState poppedState = redoStack.Pop();
            OnRedoStackStatusChange(new StackStatusEvent(redoStack.Count));
            return poppedState;
        }

        private void FlushUndoStack()
        {
            undoStack.Clear();
            OnUndoStackStatusChange(new StackStatusEvent(undoStack.Count));
        }

        private void FlushRedoStack()
        {
            redoStack.Clear();
            OnRedoStackStatusChange(new StackStatusEvent(redoStack.Count));
        }

        public void FlushStacks()
        {
            FlushUndoStack();
            FlushRedoStack();
        }

        public byte[,] CurrentContent
        {
            get { return currentContent; }
            set { currentContent = value; }
        }

        public int NumMoves
        {
            get { return numMoves; }
            set { numMoves = value; }
        }

        public void ResetCurrentPosition()
        {
            currentRow = -1;
            currentColumn = -1;
        }

        protected override void OnCellPaint(TableLayoutCellPaintEventArgs e)
        {
            Graphics g = e.Graphics;
            int column = e.Column, row = e.Row;
            cellBounds[row,column] = e.CellBounds;
            switch (currentContent[row,column])
            {
                case 0:
                    g.FillRectangle(Brushes.Black, e.CellBounds);
                    break;
                case 1:
                    g.FillRectangle(Brushes.White, e.CellBounds);
                    break;
                case 2:
                    g.FillRectangle(new HatchBrush(HatchStyle.HorizontalBrick, Color.Black, Color.Red), e.CellBounds);
                    break;
                case 3:
                case 6:
                    g.FillRectangle(Brushes.LimeGreen, e.CellBounds);
                    g.DrawRectangle(Pens.DarkGreen, e.CellBounds.X + 1.5f, e.CellBounds.Y + 1.5f, e.CellBounds.Width - 3f, e.CellBounds.Height - 3f);
                    break;
                case 4:
                    g.FillRectangle(Brushes.LightGray, e.CellBounds);
                    g.DrawRectangle(Pens.Gray, e.CellBounds.X + 1.5f, e.CellBounds.Y + 1.5f, e.CellBounds.Width - 3f, e.CellBounds.Height - 3f);
                    break;
                case 5:
                    g.FillRectangle(Brushes.White, e.CellBounds);
                    g.FillEllipse(Brushes.Blue, e.CellBounds.X + 2, e.CellBounds.Y + 2, e.CellBounds.Width - 4, e.CellBounds.Height - 4);
                    currentRow = row;
                    currentColumn = column;
                    break;
                case 7:
                    g.FillRectangle(Brushes.LightGray, e.CellBounds);
                    g.DrawRectangle(Pens.Gray, e.CellBounds.X + 1.5f, e.CellBounds.Y + 1.5f, e.CellBounds.Width - 3f, e.CellBounds.Height - 3f);
                    g.FillEllipse(Brushes.Blue, e.CellBounds.X + 2, e.CellBounds.Y + 2, e.CellBounds.Width - 4, e.CellBounds.Height - 4);
                    currentRow = row;
                    currentColumn = column;
                    break;
            }
        }

        public bool MoveCaret(SokobanMovement movement)
        {
            FlushRedoStack();
            int prevRow, prevColumn, newRow, newColumn, nextNewRow, nextNewColumn;
            ComputeRowCols(out prevRow, out prevColumn, out newRow, out newColumn, out nextNewRow, out nextNewColumn, movement);
            if (!ValidateAndUpdateMovement(prevRow, prevColumn, newRow, newColumn, nextNewRow, nextNewColumn, movement)) return false;
            PaintCells(prevRow, prevColumn, currentRow, currentColumn, newRow, newColumn, nextNewRow, nextNewColumn);
            return true;
        }

        private void ComputeRowCols(out int prevRow, out int prevColumn, out int newRow, out int newColumn, out int nextNewRow, out int nextNewColumn, SokobanMovement movement)
        {
            prevRow = newRow = nextNewRow = currentRow;
            prevColumn = newColumn = nextNewColumn = currentColumn;
            switch (movement)
            {
                case SokobanMovement.UP:
                    prevRow = currentRow + 1;
                    newRow = currentRow - 1;
                    nextNewRow = newRow - 1;
                    prevColumn = currentColumn;
                    newColumn = currentColumn;
                    nextNewColumn = newColumn;
                    break;
                case SokobanMovement.DOWN:
                    prevRow = currentRow - 1;
                    newRow = currentRow + 1;
                    nextNewRow = newRow + 1;
                    prevColumn = currentColumn;
                    newColumn = currentColumn;
                    nextNewColumn = newColumn;
                    break;
                case SokobanMovement.LEFT:
                    prevRow = currentRow;
                    newRow = currentRow;
                    nextNewRow = newRow;
                    prevColumn = currentColumn + 1;
                    newColumn = currentColumn - 1;
                    nextNewColumn = newColumn - 1;
                    break;
                case SokobanMovement.RIGHT:
                    prevRow = currentRow;
                    newRow = currentRow;
                    nextNewRow = newRow;
                    prevColumn = currentColumn - 1;
                    newColumn = currentColumn + 1;
                    nextNewColumn = newColumn + 1;
                    break;
            }
        }

        private bool ValidateAndUpdateMovement(int prevRow, int prevColumn, int newRow, int newColumn, int nextNewRow, int nextNewColumn, SokobanMovement movement)
        {
            if (currentContent[newRow,newColumn] == 2 || currentContent[newRow,newColumn] == 0) return false;
            if ((currentContent[newRow,newColumn] == 3 || currentContent[newRow,newColumn] == 6) &&
                (currentContent[nextNewRow,nextNewColumn] == 3 || currentContent[nextNewRow,nextNewColumn] == 6 || currentContent[nextNewRow,nextNewColumn] == 2))
                return false;
            SaveState(prevRow, prevColumn, newRow, newColumn, nextNewRow, nextNewColumn, movement);
            if (currentContent[currentRow,currentColumn] == 5)   // On white
            {
                currentContent[currentRow,currentColumn] = 1;
                if (currentContent[newRow,newColumn] == 1)   // White
                    currentContent[newRow,newColumn] = 5;
                else if (currentContent[newRow,newColumn] == 4)  // Grey
                    currentContent[newRow,newColumn] = 7;
                else if (currentContent[newRow,newColumn] == 3)  // Green on White
                {
                    currentContent[newRow,newColumn] = 5;
                    if (currentContent[nextNewRow,nextNewColumn] == 1)
                        currentContent[nextNewRow,nextNewColumn] = 3;
                    else if (currentContent[nextNewRow,nextNewColumn] == 4)
                        currentContent[nextNewRow,nextNewColumn] = 6;
                }
                else if (currentContent[newRow,newColumn] == 6)  // Green on Grey
                {
                    currentContent[newRow,newColumn] = 7;
                    if (currentContent[nextNewRow,nextNewColumn] == 1)
                        currentContent[nextNewRow,nextNewColumn] = 3;
                    else if (currentContent[nextNewRow,nextNewColumn] == 4)
                        currentContent[nextNewRow,nextNewColumn] = 6;
                }
            }
            else if (currentContent[currentRow,currentColumn] == 7)  // On Grey
            {
                currentContent[currentRow,currentColumn] = 4;
                if (currentContent[newRow,newColumn] == 1)   // White
                    currentContent[newRow,newColumn] = 5;
                else if (currentContent[newRow,newColumn] == 4)  // Grey
                    currentContent[newRow,newColumn] = 7;
                else if (currentContent[newRow,newColumn] == 3)  // Green on White
                {
                    currentContent[newRow,newColumn] = 5;
                    if (currentContent[nextNewRow,nextNewColumn] == 1)
                        currentContent[nextNewRow,nextNewColumn] = 3;
                    else if (currentContent[nextNewRow,nextNewColumn] == 4)
                        currentContent[nextNewRow,nextNewColumn] = 6;
                }
                else if (currentContent[newRow,newColumn] == 6)  // Green on Grey
                {
                    currentContent[newRow,newColumn] = 7;
                    if (currentContent[nextNewRow,nextNewColumn] == 1)
                        currentContent[nextNewRow,nextNewColumn] = 3;
                    else if (currentContent[nextNewRow,nextNewColumn] == 4)
                        currentContent[nextNewRow,nextNewColumn] = 6;
                }
            }
            return true;
        }

        private void SaveState(int prevRow, int prevColumn, int newRow, int newColumn, int nextNewRow, int nextNewColumn, SokobanMovement movement)
        {
            PushStateOnUndoStack(new SokobanState(
                prevRow, prevColumn, currentContent[prevRow,prevColumn],
                currentRow, currentColumn, currentContent[currentRow,currentColumn],
                newRow, newColumn, currentContent[newRow,newColumn],
                nextNewRow, nextNewColumn, currentContent[nextNewRow,nextNewColumn], movement));
        }

        public void UndoState()
        {
            SokobanState stateFromStack = PopStateFromUndoStack();
            if (stateFromStack == null) return;
            int prevRow, prevColumn, newRow, newColumn, nextNewRow, nextNewColumn;
            ComputeRowCols(out prevRow, out prevColumn, out newRow, out newColumn, out nextNewRow, out nextNewColumn, stateFromStack.movement);
            PushStateOnRedoStack(new SokobanState(
                prevRow, prevColumn, currentContent[prevRow,prevColumn],
                currentRow, currentColumn, currentContent[currentRow,currentColumn],
                newRow, newColumn, currentContent[newRow,newColumn],
                nextNewRow, nextNewColumn, currentContent[nextNewRow,nextNewColumn], stateFromStack.movement));
            CommitState(stateFromStack);
        }

        public void RedoState()
        {
            SokobanState stateFromStack = PopStateFromRedoStack();
            if (stateFromStack == null) return;
            int prevRow, prevColumn, newRow, newColumn, nextNewRow, nextNewColumn;
            ComputeRowCols(out prevRow, out prevColumn, out newRow, out newColumn, out nextNewRow, out nextNewColumn, stateFromStack.movement);
            PushStateOnUndoStack(new SokobanState(
                prevRow, prevColumn, currentContent[prevRow, prevColumn],
                currentRow, currentColumn, currentContent[currentRow,currentColumn],
                newRow, newColumn, currentContent[newRow,newColumn],
                nextNewRow, nextNewColumn, currentContent[nextNewRow,nextNewColumn], stateFromStack.movement));
            CommitState(stateFromStack);
        }

        private void CommitState(SokobanState stateFromStack)
        {
            currentContent[stateFromStack.prevRow,stateFromStack.prevColumn] = stateFromStack.prevByte;
            currentContent[stateFromStack.currentRow,stateFromStack.currentColumn] = stateFromStack.currentByte;
            currentContent[stateFromStack.newRow,stateFromStack.newColumn] = stateFromStack.newByte;
            currentContent[stateFromStack.nextNewRow,stateFromStack.nextNewColumn] = stateFromStack.nextNewByte;
            PaintCells(stateFromStack.prevRow, stateFromStack.prevColumn, 
                       stateFromStack.currentRow, stateFromStack.currentColumn, 
                       stateFromStack.newRow, stateFromStack.newColumn,
                       stateFromStack.nextNewRow, stateFromStack.nextNewColumn);
        }

        private void PaintCells(int prevRow, int prevColumn, int currRow, int currColumn, int newRow, int newColumn, int nextNewRow, int nextNewColumn)
        {
            Graphics graphics = CreateGraphics();
            OnCellPaint(new TableLayoutCellPaintEventArgs(graphics, cellBounds[prevRow,prevColumn], cellBounds[prevRow,prevColumn], prevColumn, prevRow));
            OnCellPaint(new TableLayoutCellPaintEventArgs(graphics, cellBounds[currRow,currColumn], cellBounds[currRow,currColumn], currColumn, currRow));
            OnCellPaint(new TableLayoutCellPaintEventArgs(graphics, cellBounds[newRow,newColumn], cellBounds[newRow,newColumn], newColumn, newRow));
            OnCellPaint(new TableLayoutCellPaintEventArgs(graphics, cellBounds[nextNewRow,nextNewColumn], cellBounds[nextNewRow,nextNewColumn], nextNewColumn, nextNewRow));
        }
    }

    class SokobanState
    {
        public byte prevRow, prevColumn, prevByte;
        public byte currentRow, currentColumn, currentByte;
        public byte newRow, newColumn, newByte;
        public byte nextNewRow, nextNewColumn, nextNewByte;
        public SokobanMovement movement;

        public SokobanState(int prevRow, int prevColumn, byte prevByte, int currentRow, int currentColumn, byte currentByte, int newRow, int newColumn, byte newByte, int nextNewRow, int nextNewColumn, byte nextNewByte, SokobanMovement movement)
        {
            this.prevRow = (byte) prevRow;
            this.prevColumn = (byte) prevColumn;
            this.prevByte = prevByte;
            this.currentRow = (byte)currentRow;
            this.currentColumn = (byte) currentColumn;
            this.currentByte = currentByte;
            this.newRow = (byte) newRow;
            this.newColumn = (byte) newColumn;
            this.newByte = newByte;
            this.nextNewRow = (byte) nextNewRow;
            this.nextNewColumn = (byte) nextNewColumn;
            this.nextNewByte = nextNewByte;
            this.movement = movement;
        }
    }

    class SokobanButton : Button
    {
        private readonly SokobanForm sokobanForm;

        public SokobanButton(SokobanForm sokobanForm)
        {
            this.sokobanForm = sokobanForm;
        }

        protected override bool IsInputKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Up:
                case Keys.Down:
                case Keys.Left:
                case Keys.Right:
                    return true;
            }
            return base.IsInputKey(keyData);
        }

        protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            base.OnPreviewKeyDown(e);
            if (e.KeyCode == Keys.Up) sokobanForm.handleUpArrow(e);
            else if (e.KeyCode == Keys.Down) sokobanForm.handleDownArrow(e);
            else if (e.KeyCode == Keys.Left) sokobanForm.handleLeftArrow(e);
            else if (e.KeyCode == Keys.Right) sokobanForm.handleRightArrow(e);
        }
    }
}
