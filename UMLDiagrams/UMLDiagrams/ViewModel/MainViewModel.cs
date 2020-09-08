using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using UMLDiagrams.Model;
using GalaSoft.MvvmLight.Command;

namespace UMLDiagrams.ViewModel
{

    class MainViewModel : BaseViewModel
    {
        public Canvas Canvas { get; set; }

        JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
        public ObservableCollection<BaseShapeViewModel> BaseShapeViewModels { get; set; }                     
        public MainViewModel()
        {          
            BaseShapeViewModels = new ObservableCollection<BaseShapeViewModel>();         
        }

        private RelayCommand clear;
        private RelayCommand open;
        private RelayCommand delete;
        private RelayCommand saveJsonDiagram;
        private RelayCommand savePngDiagram;

        private RelayCommand createUseCase;
        private RelayCommand createRectangle;
        private RelayCommand createInitialNode;
        private RelayCommand createEndNode;
        private RelayCommand createDecisionNode;
        private RelayCommand createAction;
        private RelayCommand createTextBox;
        private RelayCommand createActor;
        private RelayCommand createSystem;
        private RelayCommand createClass;
        private RelayCommand createLine;

        public ICommand Clear
        {
            get
            {
                return clear ??
                    (
                        clear = new RelayCommand(() => { ClearCanvas(); })
                    ); ;
            }
        }
        public ICommand Open
        {
            get
            {
                return open ??
                    (
                        open = new RelayCommand(() => { OpenDiagram(); })
                    ); ;
            }
        }
        public ICommand Delete
        {
            get
            {
                return delete ??
                    (
                        delete = new RelayCommand(() => { DeleteElements(); })
                    ); ;
            }
        }
        public ICommand SaveJsonDiagram
        {
            get
            {
                return saveJsonDiagram ??
                    (
                        saveJsonDiagram = new RelayCommand(() => { SaveDiagramAsJson(); })
                    ); ;
            }
        }
        public ICommand SavePngDiagram
        {
            get
            {
                return savePngDiagram ??
                    (
                        savePngDiagram = new RelayCommand(() => { SaveDiagramAsPng(); })
                    ); ;
            }
        }

        public ICommand CreateLine
        {
            get
            {
                return createLine ??
                    (
                        createLine = new RelayCommand(() => { AddLine(); })
                    ); ;
            }
        }      
        public ICommand CreateClass
        {
            get
            {
                return createClass ??
                    (
                        createClass = new RelayCommand(() => { AddClass(); })
                    ); ;
            }
        }      
        public ICommand CreateSystem
        {
            get
            {
                return createSystem ??
                    (
                        createSystem = new RelayCommand(() => { AddSystem(); })
                    ); ;
            }
        }       
        public ICommand CreateActor
        {
            get
            {
                return createActor ??
                    (
                        createActor = new RelayCommand(() => { AddActor(); })
                    ); ;
            }
        }
        public ICommand CreateTextBox
        {
            get
            {
                return createTextBox ??
                    (
                        createTextBox = new RelayCommand(() => { AddTextBox(); })
                    ); ;
            }
        }       
        public ICommand CreateAction
        {
            get
            {
                return createAction ??
                    (
                        createAction = new RelayCommand(() => { AddAction(); })
                    ); ;
            }
        }
        public ICommand CreateDecisionNode
        {
            get
            {
                return createDecisionNode ??
                    (
                        createDecisionNode = new RelayCommand(() => { AddDecisionNode(); })
                    );
            }
        }      
        public ICommand CreateInitialNode
        {
            get
            {
                return createInitialNode ??
                    (
                        createInitialNode = new RelayCommand(() => { AddInitialNode(); })
                    );
            }
        }       
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
        public ICommand CreateEndNode
        {
            get
            {
                return createEndNode ??
                    (
                        createEndNode = new RelayCommand(() => { AddEndNode(); })
                    );
            }
        }
            
        private void AddTextBox()
        {
            TextViewModel textBoxViewModel = new TextViewModel { Left = 0, Top = 0, Height = 50, Width = 75,
                                                                 Angle = 0, Text = "System", IsSelected = false};
            BaseShapeViewModels.Add(textBoxViewModel);
        }
        private void AddAction()
        {
            ActionViewModel actionViewModel = new ActionViewModel { Left = 0, Top = 0, Height = 50, Width = 100, IsSelected = false,              
                                                                    Fill = "Aqua", Angle = 0, Stroke = "Black", RadiusX = 25, RadiusY = 25};
            BaseShapeViewModels.Add(actionViewModel);
        }
        private void AddDecisionNode()
        {
            DecisionNodeViewModel decisionViewModel = new DecisionNodeViewModel { Left = 0, Top = 0, Height = 50, Width = 75,
                                                                                  Fill = "White", Angle = 0, Stroke = "Black", IsSelected = false};
            BaseShapeViewModels.Add(decisionViewModel);
        }
        private void AddEndNode()
        {
            EndNodeViewModel endNodeViewModel = new EndNodeViewModel { Left = 0, Top = 0, Height = 50, Width = 50,
                                                                       Fill = "White", Angle = 0, Stroke = "Black", IsSelected = false};
            BaseShapeViewModels.Add(endNodeViewModel);
        }
        private void AddInitialNode()
        {
            InitialNodeViewModel initialNodeViewModel = new InitialNodeViewModel { Left = 0, Top = 0, Height = 50, Width = 50,
                                                                                   Fill = "Black", Angle = 0, Stroke = "Black", IsSelected = false};
            BaseShapeViewModels.Add(initialNodeViewModel);
        }
        private void AddUseCase()
        {          
            EllipseViewModel useCaseViewModel = new EllipseViewModel { Left = 0, Top = 0, Height = 50, Width = 100,
                                                                       Fill = "Aqua", Angle = 0, Stroke = "Black",IsSelected = false};
            BaseShapeViewModels.Add(useCaseViewModel);      
        }
        private void AddRectangle()
        {
            RectangleViewModel rectangleViewModel = new RectangleViewModel { Left = 0, Top = 0, Height = 50, Width = 100,
                                                                             Fill = "Aqua", Angle = 0, Stroke = "Black", IsSelected = false};
            BaseShapeViewModels.Add(rectangleViewModel);
        }
        private void AddActor()
        {
            ActorViewModel actorViewModel = new ActorViewModel { Left = 0, Top = 0, Height = 150, Width = 100,
                                                                 Fill = "White", Angle = 0, Stroke = "Black", IsSelected = false};
            BaseShapeViewModels.Add(actorViewModel);

        }
        private void AddSystem()
        {
            SystemViewModel systemViewModel = new SystemViewModel { Left = 0, Top = 0, Height = 700, Width = 500,
                                                                    Angle = 0, Stroke = "Black", IsSelected = false};
            BaseShapeViewModels.Add(systemViewModel);
        }
        private void AddClass()
        {
            ClassViewModel classViewModel = new ClassViewModel { Left = 0, Top = 0, Height = 50, Width = 100,
                                                                 Angle = 0, Stroke = "Black", IsSelected = false};
            BaseShapeViewModels.Add(classViewModel);
        }
        private void AddLine()
        {
            LineViewModel lineViewModel = new LineViewModel { Left = 0, Top = 0, Height = 50, Width = 100,
                                                             Angle = 0, Stroke = "Black", IsSelected = false};
            BaseShapeViewModels.Add(lineViewModel);
        }
        private void OpenDiagram()
        {
            var input = File.ReadAllText(@"E:\myJson.json");          
            var deserializedList = JsonConvert.DeserializeObject<ObservableCollection<BaseShapeViewModel>>(input, settings);

            if (BaseShapeViewModels != null && BaseShapeViewModels.Count > 0)
            {
                BaseShapeViewModels.Clear();

                foreach( var item in deserializedList)
                {
                    BaseShapeViewModels.Add(item);
                }               
            }
            else
            {
                foreach (var item in deserializedList)
                {
                    BaseShapeViewModels.Add(item);
                }
            }
        }
        private void ClearCanvas()
        {
            this.BaseShapeViewModels.Clear();
        }
        private void DeleteElements()
        {
            foreach(var model in BaseShapeViewModels.ToArray())
            {
                if(model.IsSelected == true)
                {
                    BaseShapeViewModels.Remove(model);
                }
            }
        }
        private void SaveDiagramAsJson()
        {           
            var output = JsonConvert.SerializeObject(BaseShapeViewModels, Formatting.Indented, settings);          
            File.WriteAllText(@"E:\myJson.json", output);            
        }
        private void SaveDiagramAsPng()
        {
            Rect bounds = VisualTreeHelper.GetDescendantBounds(Canvas);
            double dpi = 96d;

            RenderTargetBitmap rtb = new RenderTargetBitmap((int)bounds.Width, (int)bounds.Height, dpi, dpi, System.Windows.Media.PixelFormats.Default);

            DrawingVisual dv = new DrawingVisual();
            using (DrawingContext dc = dv.RenderOpen())
            {
                VisualBrush vb = new VisualBrush(Canvas);
                dc.DrawRectangle(vb, null, new Rect(new Point(), bounds.Size));
            }

            rtb.Render(dv);
            BitmapEncoder pngEncoder = new PngBitmapEncoder();
            pngEncoder.Frames.Add(BitmapFrame.Create(rtb));

            try
            {
                System.IO.MemoryStream ms = new System.IO.MemoryStream();

                pngEncoder.Save(ms);
                ms.Close();

                System.IO.File.WriteAllBytes(@"E:\myPng.png", ms.ToArray());
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
           
            
        }      
    }
}
