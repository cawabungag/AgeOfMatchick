using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Interfaces;
using Cysharp.Threading.Tasks;
using Match3.App;
using Match3.App.Interfaces;
using UnityEngine;

namespace Common
{
    public class GameScoreBoard : ISolvedSequencesConsumer<IUnityGridSlot>
    {
        public void OnSequencesSolved(SolvedData<IUnityGridSlot> solvedData)
        {
            foreach (var sequence in solvedData.SolvedSequences)
            {
                RegisterSequenceScore(sequence, solvedData.IsAutomaticMatch);
            }
        }

        private async Task RegisterSequenceScore(ItemSequence<IUnityGridSlot> sequence,
            bool solvedDataIsAutomaticMatch)
        {
            var ability = sequence.SolvedGridSlots.First().Item.SpriteRenderer.sprite.name.Replace("(Clone)", string.Empty);
            if (ability == "Gold")
            {
                var vector3s = sequence.SolvedGridSlots.Select(x => x.Item.GetWorldPosition()).ToArray();
                CurrencyUi.Instance.IncreaseGold(sequence.SolvedGridSlots.Count, vector3s);
                
                await UniTask.WaitForSeconds(0.5f);

                if (solvedDataIsAutomaticMatch)
                {
                    return;
                }
                BattleManager.Instance.EnemyTurn();
                BattleManager.Instance.ApplyBuffsAndDebuffs(false);
                BattleManager.Instance.ApplyBuffsAndDebuffs(true);
                return;
            }
            
            if (ability == "Silver")
            {
                var vector3s = sequence.SolvedGridSlots.Select(x => x.Item.GetWorldPosition()).ToArray();
                CurrencyUi.Instance.IncreaseSilver(sequence.SolvedGridSlots.Count, vector3s);
                
                await UniTask.WaitForSeconds(0.5f);
                
                BattleManager.Instance.EnemyTurn();
                BattleManager.Instance.ApplyBuffsAndDebuffs(false);
                BattleManager.Instance.ApplyBuffsAndDebuffs(true);
                
                if (solvedDataIsAutomaticMatch)
                {
                    return;
                }
                return;
            }

            BattleManager.Instance.AllyTurn(ability, sequence.SolvedGridSlots.Count);
            await UniTask.WaitForSeconds(0.5f);

            if (!solvedDataIsAutomaticMatch)
            {
                BattleManager.Instance.EnemyTurn();
                BattleManager.Instance.ApplyBuffsAndDebuffs(false);
                BattleManager.Instance.ApplyBuffsAndDebuffs(true);
            }
           
            await UniTask.WaitForSeconds(0.5f);

            
            Debug.Log(GetSequenceDescription(sequence));
            await UniTask.WaitForSeconds(0.5f);
        }

        private string GetSequenceDescription(ItemSequence<IUnityGridSlot> sequence)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("ContentId <color=yellow>");
            stringBuilder.Append(sequence.SolvedGridSlots[0].Item.ContentId);
            stringBuilder.Append("</color> | <color=yellow>");
            stringBuilder.Append(sequence.SequenceDetectorType.Name);
            stringBuilder.Append("</color> sequence of <color=yellow>");
            stringBuilder.Append(sequence.SolvedGridSlots.Count);
            stringBuilder.Append("</color> elements");

            return stringBuilder.ToString();
        }
    }
}