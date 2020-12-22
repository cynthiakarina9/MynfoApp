using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Mynfo.Models
{
    public class TestModelRepository
    {
        private ObservableCollection<TestModel> testModelInfo;

        public ObservableCollection<TestModel> TestModelInfo
        {
            get { return testModelInfo; }
            set { this.testModelInfo = value; }
        }

        public TestModelRepository()
        {
            GenerateTestInfo();
        }

        internal void GenerateTestInfo()
        {
            testModelInfo = new ObservableCollection<TestModel>();
            testModelInfo.Add(new TestModel() { Name = "Object-Oriented Programming in C#", ImagePath = "Object-oriented programming is a programming paradigm based on the concept of objects" });
            testModelInfo.Add(new TestModel() { Name = "C# Code Contracts", ImagePath = "Code Contracts provide a way to convey code assumptions" });
            testModelInfo.Add(new TestModel() { Name = "Machine Learning Using C#", ImagePath = "You’ll learn several different approaches to applying machine learning" });
            testModelInfo.Add(new TestModel() { Name = "Neural Networks Using C#", ImagePath = "Neural networks are an exciting field of software development" });
            testModelInfo.Add(new TestModel() { Name = "Visual Studio Code", ImagePath = "It is a powerful tool for editing code and serves for end-to-end programming" });
            testModelInfo.Add(new TestModel() { Name = "Android Programming", ImagePath = "It is provides a useful overview of the Android application life cycle" });
            testModelInfo.Add(new TestModel() { Name = "iOS Succinctly", ImagePath = "It is for developers looking to step into frightening world of iPhone" });
            testModelInfo.Add(new TestModel() { Name = "Visual Studio 2015", ImagePath = "The new version of the widely-used integrated development environment" });
            testModelInfo.Add(new TestModel() { Name = "Xamarin.Forms", ImagePath = "Its creates mappings from its C# classes and controls directly" });
            testModelInfo.Add(new TestModel() { Name = "Windows Store Apps", ImagePath = "Windows Store apps present a radical shift in Windows development" });
        }
    }
}
