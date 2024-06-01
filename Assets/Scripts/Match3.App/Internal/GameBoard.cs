using System;
using System.Runtime.CompilerServices;
using Match3.App.Interfaces;
using Match3.Core;
using Match3.Core.Interfaces;
using Match3.Core.Structs;

namespace Match3.App.Internal
{
    internal class GameBoard<TGridSlot> : IGameBoard<TGridSlot>, IDisposable where TGridSlot : IGridSlot
    {
        private int _rowCount;
        private int _columnCount;

        private TGridSlot[,] _gridSlots;

        public int RowCount => _rowCount;
        public int ColumnCount => _columnCount;

        public TGridSlot this[GridPosition gridPosition] => _gridSlots[gridPosition.RowIndex, gridPosition.ColumnIndex];
        public TGridSlot this[int rowIndex, int columnIndex] => _gridSlots[rowIndex, columnIndex];
        public string[,] Items
        {
            get
            {
                var newItems = new string[_rowCount, _columnCount];
                for (int row = 0; row < _rowCount; row++)
                {
                    for (int col = 0; col < _columnCount; col++)
                    {
                        newItems[row, col] = _gridSlots[row, col].Item.SpriteRenderer.sprite.name;
                    }
                }

                return newItems;
            }
        }

        public void SetGridSlots(TGridSlot[,] gridSlots)
        {
            if (_gridSlots != null)
            {
                // throw new InvalidOperationException("Grid slots have already been created.");
            }

            _rowCount = gridSlots.GetLength(0);
            _columnCount = gridSlots.GetLength(1);
            _gridSlots = gridSlots;
        }

        public bool IsPositionOnGrid(GridPosition gridPosition)
        {
            EnsureGridSlotsIsNotNull();

            return GridMath.IsPositionOnGrid(this, gridPosition);
        }

        public bool IsPositionOnBoard(GridPosition gridPosition)
        {
            return IsPositionOnGrid(gridPosition) &&
                   _gridSlots[gridPosition.RowIndex, gridPosition.ColumnIndex].CanContainItem;
        }

        public void ResetState()
        {
            _rowCount = 0;
            _columnCount = 0;
            _gridSlots = null;
        }

        public void Dispose()
        {
            Array.Clear(_gridSlots, 0, _gridSlots.Length);
            ResetState();
        }
        
        public void Clear()
        {
            if (_gridSlots == null)
            {
                return;
            }

            for (int x = 0; x < _gridSlots.GetLength(0); x++)
            {
                for (int y = 0; y < _gridSlots.GetLength(1); y++)
                {
                    _gridSlots[x, y].Clear();
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void EnsureGridSlotsIsNotNull()
        {
            if (_gridSlots == null)
            {
                throw new InvalidOperationException("Grid slots are not created.");
            }
        }
    }
}