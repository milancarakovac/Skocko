using System;
using System.Collections.Generic;
using System.Linq;
using Windows.System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Imaging;


namespace Skocko
{
    public sealed partial class MainPage : Page
    {
        private List<Button> buttonList = new List<Button>();
        private List<Type> finalCombination = new List<Type>();
        private List<Image> gameImages = new List<Image>();
        private Type []currentList = new Type[4];
        private Grid responseGrid;
        private Grid gameGrid;
        private Grid controlGrid;
        private Grid finalCombinationGrid;

        private int rows;
        private int columns;
        private int counter;
        private int nextToChange;
        private bool nextMove;
        private bool isChecked;
        private bool change;

        public MainPage()
        {
            this.InitializeComponent();
            this.PreviewKeyDown += MainPage_PreviewKeyDown;
            responseGrid = resultGrid.grid;
            gameGrid = mainGrid.grid;
            controlGrid = control.grid;
            finalCombinationGrid = finalGrid.grid;
            InitiateGame();
        }

        private void MainPage_PreviewKeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Space)
            {
                SpacePressed(sender,e);
                e.Handled = true;
            }
        }

        private void InitiateGame()
        {
            rows = 6;
            columns = 4;
            counter = 0;
            nextToChange = counter;
            nextMove = true;
            isChecked = true;
            change = false;
            CreateGrids();
            createControlGrid();
            GenerateFinalCombination();
        }

        private void GenerateFinalCombination()
        {
            clearFinalCombination();
            Random random = new Random();
            for (int i = 0; i < 4; i++)
            {
                int number = random.Next(6);
                switch (number)
                {
                    case 0:
                        {
                            finalCombination.Add(Type.Heart);
                            break;
                        }
                    case 1:
                        {
                            finalCombination.Add(Type.Club);
                            break;
                        }
                    case 2:
                        {
                            finalCombination.Add(Type.Spade);
                            break;
                        }
                    case 3:
                        {
                            finalCombination.Add(Type.Diamond);
                            break;
                        }
                    case 4:
                        {
                            finalCombination.Add(Type.Star);
                            break;
                        }
                    default:
                        {
                            finalCombination.Add(Type.Skocko);
                            break;
                        }
                }
            }
        }

        private void clearFinalCombination()
        {
            finalCombination = new List<Type>();
        }

        private void CreateGrids()
        {

            RowDefinition rowDefinition;
            ColumnDefinition columnDefinition;

            for(int i = 0; i < rows; i++)
            {
                rowDefinition = new RowDefinition();
                gameGrid.RowDefinitions.Add(rowDefinition);
            }
            for (int i = 0; i < columns; i++)
            {
                columnDefinition = new ColumnDefinition();
                gameGrid.ColumnDefinitions.Add(columnDefinition);
            }

            SetDefaultValues(gameGrid, true);

            for (int i = 0; i < rows; i++)
            {
                rowDefinition = new RowDefinition();
                responseGrid.RowDefinitions.Add(rowDefinition);
            }
            for (int i = 0; i < columns; i++)
            {
                columnDefinition = new ColumnDefinition();
                responseGrid.ColumnDefinitions.Add(columnDefinition);
            }

            SetDefaultValues(responseGrid, false);

            finalCombinationGrid.RowDefinitions.Add(new RowDefinition());
            for(int i = 0; i < columns; i++)
            {
                columnDefinition = new ColumnDefinition();
                finalCombinationGrid.ColumnDefinitions.Add(columnDefinition);
                Image image = new Image();
                finalCombinationGrid.Children.Add(image);
                Grid.SetColumn(image, i);
                Grid.SetRow(image, 0);
            }

        }

        private void SetDefaultValues(Grid grid, bool isGameGrid)
        {
            if (isGameGrid)
            {
                gameImages.Clear();
                for (int i = 0; i < rows; i++)
                    for (int j = 0; j < columns; j++)
                    {
                        Image image = new Image();
                        image.Tapped += Fix;
                        image.KeyDown += SpacePressed;
                        gameImages.Add(image);
                        grid.Children.Add(image);
                        Grid.SetColumn(image, j);
                        Grid.SetRow(image, i);
                    }
            }
            else
            {
                for (int i = 0; i < rows; i++)
                    for (int j = 0; j < columns; j++)
                    {
                        Image image = new Image();
                        image.KeyDown += SpacePressed;
                        grid.Children.Add(image);
                        Grid.SetColumn(image, j);
                        Grid.SetRow(image, i);
                    }
            }
        }

        private void Fix(object sender, RoutedEventArgs e)
        {
            if (nextToChange == counter)
            {
                Image image = (Image)sender;
                if (gameImages.IndexOf(image) <= counter)
                {
                    image.Source = new BitmapImage(new Uri(BaseUri, "Assets/Images/blueCircle.png"));
                    nextToChange = gameImages.IndexOf(image);
                    change = true;
                }
            }
        }

        private void createControlGrid()
        {
            controlGrid.RowDefinitions.Add(new RowDefinition());
            for (int i = 0; i < 6; i++) {
                ColumnDefinition columnDefinition = new ColumnDefinition();
                controlGrid.ColumnDefinitions.Add(columnDefinition);
            }
            Button heartButton = new Button();
            controlGrid.Children.Add(heartButton);
            Grid.SetRow(heartButton, 0);
            Grid.SetColumn(heartButton, 0);
            heartButton.Content = new Image
            {
                Source = new BitmapImage(new Uri(this.BaseUri, "Assets/Images/heart.png")),
                Width = 40,
                Height = 40
            };
            heartButton.Tapped += Button_Click;
            heartButton.KeyDown += SpacePressed;
            buttonList.Add(heartButton);
            Button clubButton = new Button();
            controlGrid.Children.Add(clubButton);
            Grid.SetRow(clubButton, 0);
            Grid.SetColumn(clubButton, 1);
            clubButton.Content = new Image
            {
                Source = new BitmapImage(new Uri(this.BaseUri, "Assets/Images/club.png")),
                Width = 40,
                Height = 40
            };
            clubButton.Tapped += Button_Click;
            clubButton.KeyDown += SpacePressed;
            buttonList.Add(clubButton);
            Button spadeButton = new Button();
            controlGrid.Children.Add(spadeButton);
            Grid.SetRow(spadeButton, 0);
            Grid.SetColumn(spadeButton, 2);
            spadeButton.Content = new Image
            {
                Source = new BitmapImage(new Uri(this.BaseUri, "Assets/Images/spade.png")),
                Width = 40,
                Height = 40
            };
            spadeButton.Tapped += Button_Click;
            spadeButton.KeyDown += SpacePressed;
            buttonList.Add(spadeButton);
            Button diamondButton = new Button();
            controlGrid.Children.Add(diamondButton);
            Grid.SetRow(diamondButton, 0);
            Grid.SetColumn(diamondButton, 3);
            diamondButton.Content = new Image
            {
                Source = new BitmapImage(new Uri(this.BaseUri, "Assets/Images/diamond.png")),
                Width = 40,
                Height = 40
            };
            diamondButton.Tapped += Button_Click;
            diamondButton.KeyDown += SpacePressed;
            buttonList.Add(diamondButton);
            Button starButton = new Button();
            controlGrid.Children.Add(starButton);
            Grid.SetRow(starButton, 0);
            Grid.SetColumn(starButton, 4);
            starButton.Content = new Image
            {
                Source = new BitmapImage(new Uri(this.BaseUri, "Assets/Images/star.png")),
                Width = 40,
                Height = 40
            };
            starButton.Tapped += Button_Click;
            starButton.KeyDown += SpacePressed;
            buttonList.Add(starButton);
            Button skockoButton = new Button();
            controlGrid.Children.Add(skockoButton);
            Grid.SetRow(skockoButton, 0);
            Grid.SetColumn(skockoButton, 6);
            skockoButton.Content = new Image
            {
                Source = new BitmapImage(new Uri(BaseUri, "Assets/Images/skocko.png")),
                Width = 40,
                Height = 40
            };
            skockoButton.Tapped += Button_Click;
            skockoButton.KeyDown += SpacePressed;
            buttonList.Add(skockoButton);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Image image = (Image)gameGrid.Children.ElementAt(nextToChange);
            int index = buttonList.IndexOf(button);
            if (nextMove)
            {
                if (nextToChange == counter)
                {
                    if (index == (int)Type.Heart)
                    {
                        currentList[nextToChange % 4] = Type.Heart;
                        image.Source = new BitmapImage(new Uri(BaseUri, "Assets/Images/heart.png"));
                    }
                    else if (index == (int)Type.Club)
                    {
                        currentList[nextToChange % 4] = Type.Club;
                        image.Source = new BitmapImage(new Uri(BaseUri, "Assets/Images/club.png"));
                    }
                    else if (index == (int)Type.Spade)
                    {
                        currentList[nextToChange % 4] = Type.Spade;
                        image.Source = new BitmapImage(new Uri(BaseUri, "Assets/Images/spade.png"));
                    }
                    else if (index == (int)Type.Diamond)
                    {
                        currentList[nextToChange % 4] = Type.Diamond;
                        image.Source = new BitmapImage(new Uri(BaseUri, "Assets/Images/diamond.png"));
                    }
                    else if (index == (int)Type.Star)
                    {
                        currentList[nextToChange % 4] = Type.Star;
                        image.Source = new BitmapImage(new Uri(BaseUri, "Assets/Images/star.png"));
                    }
                    else
                    {
                        currentList[nextToChange % 4] = Type.Skocko;
                        image.Source = new BitmapImage(new Uri(BaseUri, "Assets/Images/skocko.png"));
                    }
                }
                if (counter < 23 && isChecked)
                {
                    if (nextToChange == counter)
                        counter++;
                    nextToChange = counter;
                }
                if (counter > 0 && counter%4==0)
                {
                    nextMove = false;
                    isChecked = false;
                }
            }
            if (change)
            {
                if (index == (int)Type.Heart)
                {
                    currentList[nextToChange % 4] = Type.Heart;
                    image.Source = new BitmapImage(new Uri(BaseUri, "Assets/Images/heart.png"));
                }
                else if (index == (int)Type.Club)
                {
                    currentList[nextToChange % 4] = Type.Club;
                    image.Source = new BitmapImage(new Uri(BaseUri, "Assets/Images/club.png"));
                }
                else if (index == (int)Type.Spade)
                {
                    currentList[nextToChange % 4] = Type.Spade;
                    image.Source = new BitmapImage(new Uri(BaseUri, "Assets/Images/spade.png"));
                }
                else if (index == (int)Type.Diamond)
                {
                    currentList[nextToChange % 4] = Type.Diamond;
                    image.Source = new BitmapImage(new Uri(BaseUri, "Assets/Images/diamond.png"));
                }
                else if (index == (int)Type.Star)
                {
                    currentList[nextToChange % 4] = Type.Star;
                    image.Source = new BitmapImage(new Uri(BaseUri, "Assets/Images/star.png"));
                }
                else
                {
                    currentList[nextToChange % 4] = Type.Skocko;
                    image.Source = new BitmapImage(new Uri(BaseUri, "Assets/Images/skocko.png"));
                }
                nextToChange = counter;
                change = false;
            }
        }

        private bool checkScore()
        {
            if (counter % 4 == 0)
            {
                nextMove = true;
                isChecked = true;
            }
            int yellowCircle = 0;
            int redCircle = 0;
            for (int i = 0; i < 4; i++)
                if (finalCombination.ElementAt(i) == currentList[i]) redCircle++;
            if (redCircle == 4)
            {
                drawScore(redCircle, yellowCircle);
                return true;
            }
            yellowCircle = countElements() - redCircle;
            drawScore(redCircle, yellowCircle);
            return false;
        }

        private int countElements()
        {
            int match = 0;
            int numOfSpadesFinal = 0;
            int numOfHeartsFinal = 0;
            int numOfDiamondsFinal = 0;
            int numOfClubsFinal = 0;
            int numOfStarsFinal = 0;
            int numOfSkockosFinal = 0;
            int numOfSpadesCurr = 0;
            int numOfHeartsCurr = 0;
            int numOfDiamondsCurr = 0;
            int numOfClubsCurr = 0;
            int numOfStarsCurr = 0;
            int numOfSkockosCurr = 0;
            for (int i = 0; i < 4; i++)
            {
                switch (finalCombination.ElementAt(i))
                {
                    case Type.Heart:
                        {
                            numOfHeartsFinal++;
                            break;
                        }
                    case Type.Club:
                        {
                            numOfClubsFinal++;
                            break;
                        }
                    case Type.Spade:
                        {
                            numOfSpadesFinal++;
                            break;
                        }
                    case Type.Diamond:
                        {
                            numOfDiamondsFinal++;
                            break;
                        }
                    case Type.Star:
                        {
                            numOfStarsFinal++;
                            break;
                        }
                    default:
                        {
                            numOfSkockosFinal++;
                            break;
                        }
                }
                switch (currentList[i])
                {
                    case Type.Heart:
                        {
                            numOfHeartsCurr++;
                            break;
                        }
                    case Type.Club:
                        {
                            numOfClubsCurr++;
                            break;
                        }
                    case Type.Spade:
                        {
                            numOfSpadesCurr++;
                            break;
                        }
                    case Type.Diamond:
                        {
                            numOfDiamondsCurr++;
                            break;
                        }
                    case Type.Star:
                        {
                            numOfStarsCurr++;
                            break;
                        }
                    default:
                        {
                            numOfSkockosCurr++;
                            break;
                        }
                }
            }
            match += numOfStarsFinal < numOfStarsCurr ? numOfStarsFinal : numOfStarsCurr;
            match += numOfSpadesFinal < numOfSpadesCurr ? numOfSpadesFinal : numOfSpadesCurr;
            match += numOfDiamondsFinal < numOfDiamondsCurr ? numOfDiamondsFinal : numOfDiamondsCurr;
            match += numOfClubsFinal < numOfClubsCurr ? numOfClubsFinal : numOfClubsCurr;
            match += numOfSkockosFinal < numOfSkockosCurr ? numOfSkockosFinal : numOfSkockosCurr;
            match += numOfHeartsFinal < numOfHeartsCurr ? numOfHeartsFinal : numOfHeartsCurr;
            return match;
        }

        private void drawScore(int redCircle, int yellowCircle)
        {
            for(int i = 0; i < redCircle; i++)
            {
                ((Image)responseGrid.Children.ElementAt(counter - 4 + i)).Source = new BitmapImage(new Uri(BaseUri, "Assets/Images/redCircle.png"));
            }
            for(int i = 0; i < yellowCircle; i++)
            {
                ((Image)responseGrid.Children.ElementAt(counter - 4 + redCircle + i)).Source = new BitmapImage(new Uri(BaseUri, "Assets/Images/yellowCircle.png"));
            }
        }

        private async void GameOver(bool win)
        {
            DrawFinalCombination();
            MessageDialog dialog = new MessageDialog("Da li želite da započnete novu igru?");
            if (win)
                dialog.Title = "Pogodili ste traženu kombinaciju";
            else
                dialog.Title = "Nažalost niste pogodili traženu kombinaciju";
            dialog.Commands.Add(new UICommand("Da", command =>{ newGame(); }));
            dialog.Commands.Add(new UICommand("Ne", command => { Application.Current.Exit(); }));
            await dialog.ShowAsync();
        }

        private void DrawFinalCombination()
        {
            for(int i = 0; i < columns; i++)
            {
                switch (finalCombination.ElementAt(i))
                {
                    case Type.Heart:
                        {
                            ((Image)finalCombinationGrid.Children.ElementAt(i)).Source = new BitmapImage(new Uri(BaseUri, "Assets/Images/heart.png"));
                            break;
                        }
                    case Type.Club:
                        {
                            ((Image)finalCombinationGrid.Children.ElementAt(i)).Source = new BitmapImage(new Uri(BaseUri, "Assets/Images/club.png"));
                            break;
                        }
                    case Type.Spade:
                        {
                            ((Image)finalCombinationGrid.Children.ElementAt(i)).Source = new BitmapImage(new Uri(BaseUri, "Assets/Images/spade.png"));
                            break;
                        }
                    case Type.Diamond:
                        {
                            ((Image)finalCombinationGrid.Children.ElementAt(i)).Source = new BitmapImage(new Uri(BaseUri, "Assets/Images/diamond.png"));
                            break;
                        }
                    case Type.Star:
                        {
                            ((Image)finalCombinationGrid.Children.ElementAt(i)).Source = new BitmapImage(new Uri(BaseUri, "Assets/Images/star.png"));
                            break;
                        }
                    default:
                        {
                            ((Image)finalCombinationGrid.Children.ElementAt(i)).Source = new BitmapImage(new Uri(BaseUri, "Assets/Images/skocko.png"));
                            break;
                        }
                }
            }
        }

        private void newGame()
        {
            gameGrid.Children.Clear();
            SetDefaultValues(gameGrid, true);
            responseGrid.Children.Clear();
            SetDefaultValues(responseGrid, false);
            counter = 0;
            nextToChange = 0;
            GenerateFinalCombination();
            finalCombinationGrid.Children.Clear();
            for (int i = 0; i < columns; i++)
            {
                Image image = new Image();
                finalCombinationGrid.Children.Add(image);
                Grid.SetColumn(image, i);
                Grid.SetRow(image, 0);
            }
        }

        private void SpacePressed(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Space)
            {
                if (counter % 4 == 0 && nextToChange == counter && counter != 0)
                {
                    for (int i = 0; i < counter - counter % 4; i++)
                    {
                        gameImages.ElementAt(i).Tapped -= Fix;
                    }
                    if (checkScore())
                    {
                        GameOver(true);
                    }
                }
                if (counter == 23)
                {
                    counter++;
                    GameOver(checkScore());
                }
            }
        }
    }
}
