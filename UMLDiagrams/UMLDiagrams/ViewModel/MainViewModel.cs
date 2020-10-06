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
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Threading;
using UMLDiagrams.Resources;
using Microsoft.Win32;

namespace UMLDiagrams.ViewModel
{

    class MainViewModel : BaseViewModel
    {
        public Canvas Canvas { get; set; }
        public BaseShapeViewModel Connector1 { get; set; }
        public BaseShapeViewModel Connector2 { get; set; }       

        JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All ,
                                                                       PreserveReferencesHandling = PreserveReferencesHandling.Objects };
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
        private RelayCommand buildDiagram;

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
        public ICommand BuildDiagram
        {
            get
            {
                return buildDiagram ??
                    (
                        buildDiagram = new RelayCommand(() => { BuildSimpleDiagram(); })
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

        public ICommand AddNewLinePoint
        {
            get { return new RelayCommand<EventArgs>(AddNewLinePoints); }
        }
        private void AddNewLinePoints(EventArgs args)
        {
            MouseButtonEventArgs e = (MouseButtonEventArgs)args;
            var item = ((FrameworkElement)e.OriginalSource).DataContext as ConnectionViewModel;
            Point point = e.GetPosition(Canvas);
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
            {
                BaseShapeViewModels.Remove(item);
            }
            else
            {
                if (item is ConnectionViewModel)
                {
                    LinePointViewModel linePointViewModel = new LinePointViewModel
                    {
                        Left = point.X - 3.5,
                        Top = point.Y - 3.5,
                        Center = new Point(point.X, point.Y),
                        Height = 7,
                        Width = 7,
                        IsSelected = false,
                        Fill = "Red",
                        Stroke = "Black",
                        ZIndex = 20
                    };
                    ConnectionViewModel connectionViewModel1 = new ConnectionViewModel
                    {
                        Shape1 = item.Shape1,
                        Shape2 = linePointViewModel,
                        Left = 0,
                        Top = 0,
                        Height = 2000,
                        Width = 3000,
                        Stroke = "Black",
                        IsSelected = false,
                        ZIndex = 0
                    };
                    ConnectionViewModel connectionViewModel2 = new ConnectionViewModel
                    {
                        Shape1 = linePointViewModel,
                        Shape2 = item.Shape2,
                        Left = 0,
                        Top = 0,
                        Height = 2000,
                        Width = 3000,
                        Stroke = "Black",
                        IsSelected = false,
                        ZIndex = 0
                    };

                    BaseShapeViewModels.Add(linePointViewModel);
                    BaseShapeViewModels.Add(connectionViewModel1);
                    BaseShapeViewModels.Add(connectionViewModel2);
                    BaseShapeViewModels.Remove(item);
                }
            }
        }
        private void CreateConnectors(BaseShapeViewModel model)
        {
            ConnectorViewModel connectorViewModelLeft = new ConnectorViewModel
            {
                Left = model.Left - 15,
                Top = model.Top + (model.Height / 2) - 5,
                Center = new Point(model.Left - 10, model.Top + (model.Height / 2)),
                Height = 10,
                Width = 10,
                IsSelected = false,
                Fill = "Red",
                Stroke = "Black",
                ZIndex = 20
            };

            ConnectorViewModel connectorViewModelRight = new ConnectorViewModel
            {
                Left = model.Left + (model.Width) + 5,
                Top = model.Top + (model.Height / 2) - 5,
                Center = new Point(model.Left + (model.Width) + 10, model.Top + (model.Height / 2)),
                Height = 10,
                Width = 10,
                IsSelected = false,
                Fill = "Red",
                Stroke = "Black",
                ZIndex = 20
            };

            ConnectorViewModel connectorViewModelTop = new ConnectorViewModel
            {
                Left = model.Left + (model.Width / 2) - 5,
                Top = model.Top - 15,
                Center = new Point(model.Left + (model.Width / 2), model.Top - 10),
                Height = 10,
                Width = 10,
                IsSelected = false,
                Fill = "Red",
                Stroke = "Black",
                ZIndex = 20
            };

            ConnectorViewModel connectorViewModelBottom = new ConnectorViewModel
            {
                Left = model.Left + (model.Width / 2) - 5,
                Top = model.Top + model.Height + 5,
                Center = new Point(model.Left + (model.Width / 2), model.Top + model.Height + 10),
                Height = 10,
                Width = 10,
                IsSelected = false,
                Fill = "Red",
                Stroke = "Black",
                ZIndex = 20
            };

            model.ConnectorLeft = connectorViewModelLeft;
            model.ConnectorRight = connectorViewModelRight;
            model.ConnectorTop = connectorViewModelTop;
            model.ConnectorBottom = connectorViewModelBottom;

            BaseShapeViewModels.Add(model);
            BaseShapeViewModels.Add(connectorViewModelLeft);
            BaseShapeViewModels.Add(connectorViewModelRight);
            BaseShapeViewModels.Add(connectorViewModelTop);
            BaseShapeViewModels.Add(connectorViewModelBottom);
        }

        public ICommand StartDrawnLine
        {
            get { return new RelayCommand<EventArgs>(Start); }
        }
        private void Start(EventArgs args)
        {
            MouseButtonEventArgs e = (MouseButtonEventArgs)args;
            ContentControl content = ((ContentControl)e.Source);
            var model = content.DataContext as BaseShapeViewModel;

            if (model is ConnectorViewModel || (model is LinePointViewModel && (model.IsSelected ==true)) )
            {                
                Connector1 = model;               
                e.Handled = true;
            }
            
        }
       
        public ICommand EndDrawnLine
        {
            get { return new RelayCommand<EventArgs>(End); }
        }
        private void End(EventArgs args)
        {
            MouseButtonEventArgs e = (MouseButtonEventArgs)args;
            ContentControl content = ((ContentControl)e.Source);
            var model = content.DataContext as BaseShapeViewModel;

            if ( Connector1 != null && model != null
                && (model is ConnectorViewModel || model is LinePointViewModel)
                && !(model.Equals(Connector1)))
            {                  
                    Connector2 = model;
               
                ConnectionViewModel connection = new ConnectionViewModel
                {
                    Shape1 = Connector1,
                    Shape2 = Connector2,
                    Left = 0,
                    Top = 0,
                    Height = 2000,
                    Width = 3000,
                    Stroke = "Black",
                    IsSelected = false,
                    ZIndex = 0,
                    
                    };
                    BaseShapeViewModels.Add(connection);
                
                //Connector1.Fill = "Red";
                //Connector1.IsSelected = false;
                //Connector2.Fill = "Red";
                //Connector2.IsSelected = false;
                Connector1 = null;
                Connector2 = null;
                   
                           
            }

        
    }
              


        private void AddTextBox()
        {
            TextViewModel textBoxViewModel = new TextViewModel { Left = 0, Top = 0, Height = 50, Width = 75,
                                                                 Angle = 0, Text = "System", IsSelected = false,
                                                                 ZIndex = 30};
            BaseShapeViewModels.Add(textBoxViewModel);
        }
        private void AddAction()
        {
            ActionViewModel actionViewModel = new ActionViewModel { Left = 0, Top = 0, Height = 50, Width = 100,
                                                                    IsSelected = false, Fill = "Aqua", Angle = 0,
                                                                    Stroke = "Black", RadiusX = 25, RadiusY = 25};           
            CreateConnectors(actionViewModel);
        }
        private void AddDecisionNode()
        {
            DecisionNodeViewModel decisionViewModel = new DecisionNodeViewModel { Left = 0, Top = 0, Height = 50, Width = 75,
                                                                                  Fill = "White", Angle = 0, Stroke = "Black",
                                                                                  IsSelected = false};            
            CreateConnectors(decisionViewModel);
        }
        private void AddEndNode()
        {
            EndNodeViewModel endNodeViewModel = new EndNodeViewModel { Left = 0, Top = 0, Height = 30, Width = 30,
                                                                       Fill = "White", Angle = 0, Stroke = "Black",
                                                                       IsSelected = false};            
            CreateConnectors(endNodeViewModel);
        }
        private void AddInitialNode()
        {
            InitialNodeViewModel initialNodeViewModel = new InitialNodeViewModel { Left = 0, Top = 0, Height = 30, Width = 30,
                                                                                   Fill = "Black", Angle = 0,
                                                                                   Stroke = "Black", IsSelected = false};            
            CreateConnectors(initialNodeViewModel);
        }
        private void AddUseCase()
        {          
            EllipseViewModel useCaseViewModel = new EllipseViewModel { Left = 0, Top = 0, Height = 50, Width = 100,
                                                                       Fill = "Aqua", Angle = 0, Stroke = "Black",
                                                                       IsSelected = false};            
            CreateConnectors(useCaseViewModel);
        }
        private void AddRectangle()
        {
            RectangleViewModel rectangleViewModel = new RectangleViewModel { Left = 0, Top = 0, Height = 50, Width = 100,
                                                                             Fill = "Aqua", Angle = 0, Stroke = "Black",
                                                                             IsSelected = false};        
            CreateConnectors(rectangleViewModel);
        }
        private void AddActor()
        {
            ActorViewModel actorViewModel = new ActorViewModel { Left = 0, Top = 0, Height = 150, Width = 100,
                                                                 Fill = "White", Angle = 0, Stroke = "Black",
                                                                 IsSelected = false};                
            CreateConnectors(actorViewModel);

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
            CreateConnectors(classViewModel);
        }
        private void AddLine()
        {
            int count = 0;
            BaseShapeViewModel[] baseModels = new BaseShapeViewModel[2];

            foreach (var model in BaseShapeViewModels.ToArray())
            {
                if (model.IsSelected == true && (model is ConnectorViewModel || model is LinePointViewModel))
                {
                    if (count == 0)
                    {
                        baseModels[0] = model;
                        count++;
                    }
                    else
                    {
                        baseModels[1] = model;                      
                    }
                }
            }

            if ((baseModels[0] != null && baseModels[1] != null) &&( 
                (baseModels[0] is ConnectorViewModel && baseModels[1] is ConnectorViewModel) ||
                (baseModels[0] is LinePointViewModel && baseModels[1] is LinePointViewModel) ||
                (baseModels[0] is ConnectorViewModel && baseModels[1] is LinePointViewModel) ||
                (baseModels[0] is LinePointViewModel && baseModels[1] is ConnectorViewModel) ))
            {
                
                ConnectionViewModel connectionView = new ConnectionViewModel {
                    Shape1 = baseModels[0], Shape2 = baseModels[1],
                    Left = 0, Top = 0,
                    Height = 2000, Width = 3000,
                    Stroke = "Black",
                    IsSelected = false,
                    ZIndex = 0 
                };

                BaseShapeViewModels.Add(connectionView);
                baseModels[0].Fill = "Red";
                baseModels[1].Fill = "Red";
                baseModels[0].IsSelected = false;
                baseModels[1].IsSelected = false;
            }
        }
       
        private void OpenDiagram()
        {
            OpenFileDialog openJsonDialog = new OpenFileDialog();
            openJsonDialog.Filter = "Json files (*.json)|*.json";
            openJsonDialog.CheckFileExists = true;
            openJsonDialog.Multiselect = false;
            openJsonDialog.CheckFileExists = true;

            if (openJsonDialog.ShowDialog() == true)
            {
                if(openJsonDialog.FileName.Trim() != string.Empty )
                {
                    using (StreamReader r = new StreamReader(openJsonDialog.FileName))
                    {
                        string json = r.ReadToEnd();
                        var deserializedList = JsonConvert.DeserializeObject<ObservableCollection<BaseShapeViewModel>>(json, settings);
                        if (BaseShapeViewModels != null && BaseShapeViewModels.Count > 0)
                        {
                            BaseShapeViewModels.Clear();

                            foreach (var item in deserializedList)
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
                }
            };            
        }
        private void ClearCanvas()
        {
            this.BaseShapeViewModels.Clear();
        }
        private void DeleteElements()
        {
            foreach (var model in BaseShapeViewModels.ToArray())
            {   
                if (model.IsSelected == true && !(model is ConnectorViewModel) )
                {
                    BaseShapeViewModels.Remove(model);
                    BaseShapeViewModels.Remove(model.ConnectorLeft);
                    BaseShapeViewModels.Remove(model.ConnectorRight);
                    BaseShapeViewModels.Remove(model.ConnectorTop);
                    BaseShapeViewModels.Remove(model.ConnectorBottom);
                    
                    model.ConnectorBottom = null;
                    model.ConnectorLeft = null;
                    model.ConnectorRight = null;
                    model.ConnectorTop = null;                                    }                
            }
        }
        private void SaveDiagramAsJson()
        {
            if (BaseShapeViewModels.Count != 0)
            {
                var output = JsonConvert.SerializeObject(BaseShapeViewModels, Formatting.Indented, settings);

                SaveFileDialog saveJsonDialog = new SaveFileDialog();
                saveJsonDialog.FileName = "Diagram"; 
                saveJsonDialog.DefaultExt = ".json";
                saveJsonDialog.Filter = "Json files (*.json)|*.json";

                if (saveJsonDialog.ShowDialog() == true)
                {
                    if (saveJsonDialog.FileName.Trim() != string.Empty && saveJsonDialog.FileName.EndsWith(".json"))
                    {                        
                        File.WriteAllText(saveJsonDialog.FileName, output);
                    }
                    else
                    {
                        MessageBox.Show("Wrong file name or format");
                    }
                }
            }
            else
            {
                WarningMessage warningMessage = new WarningMessage();
                warningMessage.ShowDialog();
            }

            
        }
        private void SaveDiagramAsPng()
        {
            Rect bounds = VisualTreeHelper.GetDescendantBounds(Canvas);
            double dpi = 96d;

            if (!(bounds.IsEmpty))
            {
                SaveFileDialog savePngDialog = new SaveFileDialog();
                savePngDialog.FileName = "Diagram"; 
                savePngDialog.DefaultExt = ".png"; 
                savePngDialog.Filter = "Picture (*.png)|*.png";

                if (savePngDialog.ShowDialog() == true)
                {
                    if (savePngDialog.FileName.Trim() != string.Empty && savePngDialog.FileName.EndsWith(".png"))
                    {                        
                        RenderTargetBitmap rtb = new RenderTargetBitmap((int)bounds.Width, (int)bounds.Height,
                                                                 dpi, dpi, System.Windows.Media.PixelFormats.Default);

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

                            System.IO.File.WriteAllBytes(savePngDialog.FileName, ms.ToArray());
                        }
                        catch (Exception err)
                        {
                            MessageBox.Show(err.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Wrong file name or format");
                    }
                }               
            }

            else
            { 
                WarningMessage warningMessage = new WarningMessage();
                warningMessage.ShowDialog();
            }            
        }

        private void BuildSimpleDiagram()
        {
            var win = Application.Current.MainWindow;
            var help = new WindowInteropHelper(win);
            var handle = help.Handle;

            Thread t = new Thread(new ParameterizedThreadStart(SelfBuildingDiagram));
            t.Start(handle);
        }            
        private void SelfBuildingDiagram(object x)
        {            
            IntPtr handle = (IntPtr)x;
            
            Point p = new Point();

            GetCursorPos(ref p);
            ClientToScreen(handle, ref p);


            SetCursorPos(50, 200);
            Thread.Sleep(500);
            DoMouseLeftClick(50, 200);
            Thread.Sleep(500);
            DoMouseLeftClick(50, 200);
            int xPos = 50;
            int yPos = 200;
            Thread.Sleep(500);
            for (int i = 0; i < 100; i++)
            {
                
                SetCursorPos(xPos++, yPos--);
                Thread.Sleep(1);
            }
            
            MouseLeftClickMove(150, 100);
            Thread.Sleep(500);

            SetCursorPos(210, 77);
            Thread.Sleep(500);
            
            MouseLeftClickConnect(210, 77);
            Thread.Sleep(500);

            for (int i = 77; i < 100; i++)
            {
                SetCursorPos(290, i);
            }            
        }
        
        [DllImport("user32.dll")]
        public static extern bool ClientToScreen(IntPtr hWnd, ref Point point);

        [DllImport("user32.dll")]
        public static extern void mouse_event(int dsFlags, int dx, int dy, int cButtons, int dsExtraInfo);

        public const int MOUSEEVENTF_LEFTDOWN = 0x02;
        public const int MOUSEEVENTF_LEFTUP = 0x04;        

        private void DoMouseLeftClick( int x, int y)
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, x, y, 0, 0);
            
            mouse_event(MOUSEEVENTF_LEFTUP, x, y, 0, 0);
        }
        private void MouseLeftClickMove( int x,int y)
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, x, y, 0, 0);
            Thread.Sleep(1);
            while (x <= 350)
            {
                SetCursorPos(x, y);
                x++;
                Thread.Sleep(1);

            }
            mouse_event(MOUSEEVENTF_LEFTUP, 350, 100, 0, 0);
        }        
        private void MouseLeftClickConnect(int x, int y)
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, x, y, 0, 0);
            Thread.Sleep(1);
            while (x <= 290 )
            {
                SetCursorPos(x, y);
                x++;
                Thread.Sleep(1);
            }
            mouse_event(MOUSEEVENTF_LEFTUP, x, y, 0, 0);

        }

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(ref Point lpPoint);
        [DllImport("user32.dll")]
        public static extern long SetCursorPos(int x, int y);
    }
}
