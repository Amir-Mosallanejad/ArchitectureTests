using FluentAssertions;
using NetArchTest.Rules;

namespace Architecture.Tests;

public class ArchitectureTests
{
    private const string DomainNamespace = "Domain";
    private const string InfrastructureNamespace = "Infrastructure";
    private const string ApplicationNamespace = "Application";
    private const string PresentationNamespace = "Presentation";
    private const string WebNamespace = "Web";

    [Fact]
    public void Domain_Should_HaveNoDependencyToOtherProjects()
    {
        //Arrange
        System.Reflection.Assembly assembly = typeof(Domain.AssemblyReference).Assembly;

        string[] otherProject =
         [
            ApplicationNamespace,
            InfrastructureNamespace,
            PresentationNamespace,
            WebNamespace,
        ];

        //Act
        TestResult result = Types.InAssembly(assembly).ShouldNot().HaveDependencyOnAll(otherProject).GetResult();

        //Assert
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Application_Should_HaveNoDependencyToOtherProjects()
    {
        //Arrange
        System.Reflection.Assembly assembly = typeof(Application.AssemblyReference).Assembly;

        string[] otherProject =
         [
            InfrastructureNamespace,
            PresentationNamespace,
            WebNamespace,
        ];

        //Act
        TestResult result = Types.InAssembly(assembly).ShouldNot().HaveDependencyOnAll(otherProject).GetResult();

        //Assert
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Infrastructure_Should_HaveNoDependencyToOtherProjects()
    {
        //Arrange
        System.Reflection.Assembly assembly = typeof(Infrastructure.AssemblyReference).Assembly;

        string[] otherProject =
         [
            PresentationNamespace,
            WebNamespace,
        ];

        //Act
        TestResult result = Types.InAssembly(assembly).ShouldNot().HaveDependencyOnAll(otherProject).GetResult();

        //Assert
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Presentation_Should_HaveNoDependencyToOtherProjects()
    {
        //Arrange
        System.Reflection.Assembly assembly = typeof(Presentation.AssemblyReference).Assembly;

        string[] otherProject =
         [
            InfrastructureNamespace,
            WebNamespace,
        ];

        //Act
        TestResult result = Types.InAssembly(assembly).ShouldNot().HaveDependencyOnAll(otherProject).GetResult();

        //Assert
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Handlers_Should_HaveDependencyOnDomain()
    {
        //Arrange
        System.Reflection.Assembly assembly = typeof(Application.AssemblyReference).Assembly;

        //Act
        TestResult result = Types.InAssembly(assembly).That().HaveNameEndingWith("Handler")
                                .Should()
                                .HaveDependencyOn(DomainNamespace)
                                .GetResult();

        //Assert
        result.IsSuccessful.Should().BeTrue();
    }

     [Fact]
    public void Controllers_Should_HaveDependencyOnMediatR()
    {
        //Arrange
        System.Reflection.Assembly assembly = typeof(Presentation.AssemblyReference).Assembly;

        //Act
        TestResult result = Types.InAssembly(assembly).That().HaveNameEndingWith("Controller")
                                .Should()
                                .HaveDependencyOn("MediatR")
                                .GetResult();

        //Assert
        result.IsSuccessful.Should().BeTrue();
    }
}
