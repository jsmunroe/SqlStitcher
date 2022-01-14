using System;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SqlStitcher.Forms.Contracts;
using SqlStitcher.Forms.Helpers;
using SqlStitcher.Forms.ViewModels;

namespace SqlStitcher.Test.Forms.Helpers
{
    [TestClass]
    public class ViewExtensionsTest
    {
        [TestMethod]
        public void ShowDialog()
        {
            // Setup
            var parentWindowMock = new Mock<IWin32Window>();
            var parentWindow = parentWindowMock.Object;

            var viewMock = new Mock<IForm<MainViewModel>>();
            var view = viewMock.Object;

            // Execute
            view.ShowDialog(parentWindow);

            // Assert
            viewMock.Verify(p => p.ShowDialog(parentWindow), Times.Once());
        }


        [TestMethod]
        public void ShowDialogWithNoParentWindow()
        {
            // Setup
            var viewMock = new Mock<IForm<MainViewModel>>();
            var view = (IView<MainViewModel>)viewMock.Object;

            // Execute
            view.ShowDialog();

            // Assert
            viewMock.Verify(p => p.ShowDialog(null), Times.Once());
        }


        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void ShowDialogOnNullForm()
        {
            // Setup
            IView<MainViewModel> view = null;

            // Execute
            view.ShowDialog();
        }


        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ShowDialogOnNotAForm()
        {
            // Setup
            var parentWindowMock = new Mock<IWin32Window>();
            var parentWindow = parentWindowMock.Object;

            var viewMock = new Mock<IView<MainViewModel>>();
            var view = viewMock.Object;

            // Execute
            view.ShowDialog(parentWindow);
        }


        [TestMethod]
        public void AsUserControl()
        {
            // Setup
            var viewMock = new Mock<UserControl>().As<IView<MainViewModel>>();
            var view = viewMock.Object;

            // Execute
            var result = view.AsUserControl();

            // Assert
            Assert.IsNotNull(result);
        }


        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AsUserControlOnNonUserControl()
        {
            // Setup
            var viewMock = new Mock<IForm<MainViewModel>>();
            var view = viewMock.Object;

            // Execute
            var result = view.AsUserControl();
        }

        [TestMethod]
        public void Show()
        {
            // Setup
            var parentWindowMock = new Mock<IWin32Window>();
            var parentWindow = parentWindowMock.Object;

            var viewMock = new Mock<IForm<MainViewModel>>();
            var view = viewMock.Object;

            // Execute
            view.Show(parentWindow);

            // Assert
            viewMock.Verify(p => p.Show(parentWindow), Times.Once());
        }


        [TestMethod]
        public void ShowWithNoParentWindow()
        {
            // Setup
            var viewMock = new Mock<IForm<MainViewModel>>();
            var view = (IView<MainViewModel>)viewMock.Object;

            // Execute
            view.Show();

            // Assert
            viewMock.Verify(p => p.Show(null), Times.Once());
        }


        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void ShowOnNullForm()
        {
            // Setup
            IView<MainViewModel> view = null;

            // Execute
            view.Show();
        }


        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ShowOnNotAForm()
        {
            // Setup
            var parentWindowMock = new Mock<IWin32Window>();
            var parentWindow = parentWindowMock.Object;

            var viewMock = new Mock<IView<MainViewModel>>();
            var view = viewMock.Object;

            // Execute
            view.Show(parentWindow);
        }

    }
}
