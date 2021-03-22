using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KOProject.Commands
{
    class ViewHistorySearchCommand : ICommand
    {
        private readonly MainWindowViewModel mainWindowViewModel;

        public event EventHandler CanExecuteChanged;

        public ViewHistorySearchCommand(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            mainWindowViewModel.ViewHistorySearch();
        }
    }
}
