using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Common.Interfaces;
using FillStrategies.Jobs;
using Match3.App;
using Match3.App.Interfaces;
using Match3.Core.Structs;
using Match3.Infrastructure.Interfaces;
using UnityEngine;

namespace FillStrategies
{
    public abstract class BaseFillStrategy : IBoardFillStrategy<IUnityGridSlot>
    {
        private readonly IItemsPool<IUnityItem> _itemsPool;
        private readonly IUnityGameBoardRenderer _gameBoardRenderer;

        protected BaseFillStrategy(IAppContext appContext)
        {
            _itemsPool = appContext.Resolve<IItemsPool<IUnityItem>>();
            _gameBoardRenderer = appContext.Resolve<IUnityGameBoardRenderer>();
        }

        public abstract string Name { get; }

        public virtual IEnumerable<IJob> GetFillJobs(IGameBoard<IUnityGridSlot> gameBoard)
        {
            var itemsToShow = new List<IUnityItem>();

            Generate(gameBoard, itemsToShow);
            int iterations = 1;
            while (MatchHelper<IUnityGridSlot>.IsPotentialMatch(gameBoard).Item1 
                   && MatchHelper<IUnityGridSlot>.HasPotentialMatch(gameBoard).Item1)
            {
                gameBoard.Clear();
                foreach (var item in itemsToShow)
                {
                    ReturnItemToPool(item);
                }
                itemsToShow.Clear();
                Generate(gameBoard, itemsToShow);
                iterations++;
            }
            
            Debug.LogError($"+++ iterations: {iterations}");
            
            return new[] { new ItemsShowJob(itemsToShow) };
        }

        private void Generate(IGameBoard<IUnityGridSlot> gameBoard, List<IUnityItem> itemsToShow)
        {
            for (var rowIndex = 0; rowIndex < gameBoard.RowCount; rowIndex++)
            {
                for (var columnIndex = 0; columnIndex < gameBoard.ColumnCount; columnIndex++)
                {
                    var gridSlot = gameBoard[rowIndex, columnIndex];
                    if (gridSlot.CanSetItem == false)
                    {
                        continue;
                    }

                    var item = GetItemFromPool();
                    item.SetWorldPosition(GetWorldPosition(gridSlot.GridPosition));
                    item.DebugCoord.text = $"{rowIndex}:{columnIndex}";

                    gridSlot.SetItem(item);
                    itemsToShow.Add(item);
                }
            }
        }

        public abstract IEnumerable<IJob> GetSolveJobs(IGameBoard<IUnityGridSlot> gameBoard,
            SolvedData<IUnityGridSlot> solvedData);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected Vector3 GetWorldPosition(GridPosition gridPosition)
        {
            return _gameBoardRenderer.GetWorldPosition(gridPosition);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected IUnityItem GetItemFromPool()
        {
            return _itemsPool.GetItem();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void ReturnItemToPool(IUnityItem item)
        {
            _itemsPool.ReturnItem(item);
        }
    }
}