using System;
using System.Windows.Forms;
using Helpers.IO;
using Helpers.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SqlStitcher.Forms;
using SqlStitcher.Forms.Contracts;
using SqlStitcher.Forms.Infrastructure;
using SqlStitcher.Forms.ViewModels;
using SqlStitcher.Forms.Views;
using SqlStitcher.Models;

namespace SqlStitcher.Test.Forms.Infrastructure
{
    [TestClass]
    public class ViewCatalogTests
    {
        [TestMethod]
        public void Construct()
        {
            // Execute
            new ViewCatalog();
        }

        [TestMethod]
        public void RegisterAndResolveForm()
        {
            // Setup
            var viewCatalog = new ViewCatalog();
            var testViewModel = new TestViewModel();

            var mockMostRecentFileList = new Mock<IRecentFileList>();
            var projectState = new ProjectState(mockMostRecentFileList.Object);
            TestState.MockContainerFactory.OverrideInstance<IProjectState>(projectState);

            // Execute
            viewCatalog.Register<TestViewModel, FTestView>();

            // Assert
            var view = viewCatalog.Resolve(testViewModel);
            Assert.IsNotNull(view);
            Assert.IsTrue(view is FTestView);
        }


        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RegisterFormMoreThanOnce()
        {
            // Setup
            var viewCatalog = new ViewCatalog();
            viewCatalog.Register<TestViewModel, FTestView>();

            // Execute
            viewCatalog.Register<TestViewModel, FTestView>();
        }
        
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ResolveFormNotExistingViewModel()
        {
            // Setup
            var viewCatalog = new ViewCatalog();
            var testViewModel = new TestViewModel();

            // Execute
            viewCatalog.Resolve(testViewModel);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ResolveFormWithNullViewModel()
        {
            // Setup
            var viewCatalog = new ViewCatalog();
            viewCatalog.Register<TestViewModel, FTestView>();

            // Execute
            viewCatalog.Resolve<TestViewModel>(viewModel: null);
        }

        [TestMethod]
        public void IsRegistered()
        {
            // Setup
            var viewCatalog = new ViewCatalog();
            var testViewModel = new TestViewModel();
            viewCatalog.Register<TestViewModel, FTestView>();

            // Execute
            var result = viewCatalog.IsRegistered(testViewModel);

            // Assert
            Assert.IsTrue(result);
        }


        [TestMethod]
        public void IsRegisteredWhenNotResolved()
        {
            // Setup
            var viewCatalog = new ViewCatalog();
            var testViewModel = new TestViewModel();

            // Execute
            var result = viewCatalog.IsRegistered(testViewModel);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void IsRegisteredWithNullViewModel()
        {
            // Setup
            var viewCatalog = new ViewCatalog();

            // Execute
            viewCatalog.IsRegistered<TestViewModel>(viewModel: null);
        }


        [TestMethod]
        public void ResolveWhereViewHasConstructorTakingViewModel()
        {
            // Setup
            var viewCatalog = new ViewCatalog();
            var viewModel = new TestViewModel();
            viewCatalog.Register<TestViewModel, FTestViewWithConstructor>();

            // Execute
            var view = viewCatalog.Resolve(viewModel);

            // Assert
            Assert.IsNotNull(view);
            Assert.IsTrue(view is FTestViewWithConstructor);
            Assert.IsInstanceOfType(view.ViewModel, typeof(TestViewModel));
        }
    }

    class TestViewModel : BaseViewModel
    {
        
    }

    class FTestView : Form, IForm<TestViewModel>
    {
        public TestViewModel ViewModel { get; set; }
    }

    class FTestViewWithConstructor : Form, IForm<TestViewModel>
    {
        public FTestViewWithConstructor(TestViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public TestViewModel ViewModel { get; set; }
    }
}
