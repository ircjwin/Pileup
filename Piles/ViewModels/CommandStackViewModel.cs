using Piles.Commands;
using Piles.DbContexts;
using Piles.Models;
using Piles.Services;
using System;
using System.Collections.Generic;

namespace Piles.ViewModels
{
    public enum OperationType
    {
        Add,
        Modify,
        Remove,
    }

    public enum TargetType
    {
        Pile,
        PileCollection,
        Rumination,
        RuminationCollection,
    }

    public class CommandStackViewModel
    {
        private IList<IUndoable> _undoableCommands = new List<IUndoable>();
        private int _undoIndex;
        private readonly IPilesDbContextFactory _pilesDbContextFactory = new PilesDbContextFactory("Data Source=piles.db");

        private static readonly Lazy<CommandStackViewModel> lazy = new Lazy<CommandStackViewModel>(() => new CommandStackViewModel());
        public static CommandStackViewModel Instance { get { return lazy.Value; } }
        private CommandStackViewModel() { }

        public void AddCommand(IUndoable undoRedoCommand)
        {
            _undoableCommands.Add(undoRedoCommand);
            _undoIndex = _undoableCommands.Count - 1;
        }

        public void StepThroughCommands(bool back)
        {
            if (back)
            {
                _undoableCommands[_undoIndex].Undo();
                _undoIndex--;

                if (_undoIndex < 0)
                {
                    _undoIndex = 0;
                }
            }
            else
            {
                _undoableCommands[_undoIndex].Redo();
                _undoIndex++;

                if (_undoIndex >= _undoableCommands.Count)
                {
                    _undoIndex = _undoableCommands.Count - 1;
                }
            }
        }

        public void Save()
        {
            ICollection<(OperationType, Pile)> unsavedPiles = new List<(OperationType, Pile)>();
            ICollection<(OperationType, (Rumination, Pile))> unsavedRuminations = new List<(OperationType, (Rumination, Pile))>();

            for (int i = 0; i <= _undoIndex; i++)
            {
                if (_undoableCommands[i].TargetType == TargetType.Pile)
                {
                    unsavedPiles.Add((_undoableCommands[i].OperationType, (Pile)_undoableCommands[i].Target));
                }
                else if (_undoableCommands[i].TargetType == TargetType.Rumination)
                {
                    Tuple<Rumination, Pile> ruminationPile = (Tuple<Rumination, Pile>)_undoableCommands[i].Target;
                    unsavedRuminations.Add((_undoableCommands[i].OperationType, (ruminationPile.Item1, ruminationPile.Item2)));
                }
                else if (_undoableCommands[i].TargetType == TargetType.PileCollection)
                {
                    foreach (Pile pile in (ICollection<Pile>)_undoableCommands[i].Target)
                    {
                        unsavedPiles.Add((_undoableCommands[i].OperationType, pile));
                    }
                }
                else if (_undoableCommands[i].TargetType == TargetType.RuminationCollection)
                {
                    foreach ((Rumination, Pile) ruminationPile in (ICollection<(Rumination, Pile)>)_undoableCommands[i].Target)
                    {
                        unsavedRuminations.Add((_undoableCommands[i].OperationType, ruminationPile));
                    }
                }
            }

            IPileService pileService = new PileService(_pilesDbContextFactory);
            IRuminationService ruminationService = new RuminationService(_pilesDbContextFactory);
            pileService.Save(unsavedPiles);
            ruminationService.Save(unsavedRuminations);
        }
    }
}
