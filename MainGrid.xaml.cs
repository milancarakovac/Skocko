using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Skocko
{
    sealed partial class MainGrid : Page
    {
        public Grid grid;
        public MainGrid()
        {
            this.InitializeComponent();
            this.Background = new SolidColorBrush(Colors.Transparent);
            grid = entryGrid;
        }
    }
}
