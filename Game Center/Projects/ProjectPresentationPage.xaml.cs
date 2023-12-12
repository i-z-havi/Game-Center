using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Game_Center.Projects
{
    /// <summary>
    /// Interaction logic for ProjectPresentationPage.xaml
    /// </summary>
    public partial class ProjectPresentationPage : Window
    {
        private Window? currentProject;
        public ProjectPresentationPage()
        {
            InitializeComponent();
        }

        public void OnStart(string title, string description, string technologies, ImageSource imgSrc, Window proj)
        {
            TitleLabel.Content = title;
            ProjectTechnologies.Text=technologies;
            ProjectText.Text = description;
            ProjectImage.Source = imgSrc;
            currentProject=proj;
        }

        private void OnImageClick(object sender, MouseButtonEventArgs e)
        {
            Hide();
            currentProject!.ShowDialog();
            Close();
        }

        private void ReturnToMain(object sender, MouseButtonEventArgs e)
        {
            currentProject.Close();
            Close();
        }

        private void CloseChildProject(object sender, EventArgs e)
        {
            currentProject.Close();
        }

        private void ProjectImage_MouseEnter(object sender, MouseEventArgs e)
        {
            ProjectImage.Opacity = 0.5;
        }

        private void ProjectImage_MouseLeave(object sender, MouseEventArgs e)
        {
            ProjectImage.Opacity = 1;
        }
    }
}
