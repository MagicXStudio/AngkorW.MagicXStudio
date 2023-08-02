using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MahMaterialDragablzMashUp;

namespace ImageStudio.ViewModels
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel()
        {

        }

        public ICommand LoadedCommand { get; } = new AnotherCommandImplementation(o => ApplyStyle(o as string));

        private static void ApplyStyle(string name)
        {



        }
    }
}
