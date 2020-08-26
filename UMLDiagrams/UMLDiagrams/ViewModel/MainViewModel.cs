using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UMLDiagrams.Model;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace UMLDiagrams.ViewModel
{
    class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {          
            BaseShapeViewModels = new ObservableCollection<BaseShapeViewModel>();            
        }

        public ObservableCollection<BaseShapeViewModel> BaseShapeViewModels { get; set; }

       
        private RelayCommand createUseCase;
        private RelayCommand createRectangle;

        public ICommand CreateUseCase
        {
            get
            {
                return createUseCase ?? 
                    (
                        createUseCase = new RelayCommand(() => { AddUseCase(); })                    
                    );
            }
        }
        public ICommand CreateRectangle
        {
            get
            {
                return createRectangle ??
                    (
                        createRectangle = new RelayCommand(() => { AddRectangle(); })
                    );
            }
        }
        private void AddUseCase()
        {
            Random rnd = new Random();
            
            int valueLeft = rnd.Next(0, 200);
            int valueTop = rnd.Next(0, 200);
            
            EllipseViewModel useCaseViewModel = new EllipseViewModel {Left=valueLeft,Top=valueTop, Height = 100, Width = 200, Fill="Blue"};
            BaseShapeViewModels.Add(useCaseViewModel);
      
        }
        private void AddRectangle()
        {
            Random rnd = new Random();
            
            int valueLeft = rnd.Next(0, 200);
            int valueTop = rnd.Next(0, 200);

            RectangleViewModel rectangleViewModel = new RectangleViewModel { Left = valueLeft, Top = valueTop, Height = 100, Width = 200, Fill = "Blue" };
            BaseShapeViewModels.Add(rectangleViewModel);
        }
    }
}
