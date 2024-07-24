using AngleSharp.Dom;
using Ws.Components.Source.UI.Tabs;

namespace Ws.Components.Tests.TabsComponent;

public class TabsTests : TestContext
{
    [Fact]
    public void Tab_ShouldShowsCorrectContentOfActiveTab()
    {
        // Arrange
        using TestContext ctx = new();
        IRenderedComponent<TestTabsComponent> cut = ctx.RenderComponent<TestTabsComponent>();

        // Assert
        IElement tabContent = cut.Find("p");
        tabContent.InnerHtml.Should().Be("First tab");
    }

    [Fact]
    public void DefaultTab_ShouldBeSetCorrectly()
    {
        // Arrange
        IRenderedComponent<Tabs> cut = RenderComponent<Tabs>(parameters => parameters
            .Add(p => p.DefaultTab, "second")
            .AddChildContent<Tab>(p => p.Add(t => t.Id, "first").Add(t => t.ChildContent, "<p>First tab</p>"))
            .AddChildContent<Tab>(p => p.Add(t => t.Id, "second").Add(t => t.ChildContent, "<p>Second tab</p>"))
        );

        // Act
        string activeTabId = cut.Instance.ActiveTabId;

        // Assert
        activeTabId.Should().Be("second");
    }

    [Fact]
    public async Task ChangeTabMethod_ShouldChangeActiveTab()
    {
        // Arrange
        IRenderedComponent<TestTabsComponent> cut = RenderComponent<TestTabsComponent>();

        // Act
        await cut.InvokeAsync(() => cut.Instance.SetCurrentTabId("second"));

        // Assert
        Tabs tabInstance = cut.FindComponent<Tabs>().Instance;
        tabInstance.ActiveTabId.Should().Be("second");
    }

    [Fact]
    public void SettingActiveTabId_ShouldChangeActiveTab()
    {
        // Arrange
        IRenderedComponent<Tabs> cut = RenderComponent<Tabs>(parameters => parameters
            .Add(p => p.ActiveTabId, "second")
            .AddChildContent<Tab>(p => p.Add(t => t.Id, "first").Add(t => t.ChildContent, "<p>First tab</p>"))
            .AddChildContent<Tab>(p => p.Add(t => t.Id, "second").Add(t => t.ChildContent, "<p>Second tab</p>"))
        );

        // Act
        string activeTabId = cut.Instance.ActiveTabId;

        // Assert
        activeTabId.Should().Be("second");
    }

    [Fact]
    public void Tabs_ShouldBeRegisteredInCorrectOrder()
    {
        // Arrange
        IRenderedComponent<Tabs> cut = RenderComponent<Tabs>(parameters => parameters
            .AddChildContent<Tab>(p => p.Add(t => t.Id, "first").Add(t => t.ChildContent, "<p>First tab</p>"))
            .AddChildContent<Tab>(p => p.Add(t => t.Id, "second").Add(t => t.ChildContent, "<p>Second tab</p>"))
        );

        // Act
        List<Tab> tabs = cut.Instance.TabsList;

        // Assert
        tabs.Should().HaveCount(2);
        tabs[0].Id.Should().Be("first");
        tabs[1].Id.Should().Be("second");
    }

    [Fact]
    public void Tab_ShouldRegisterIdAndTitleCorrectly()
    {
        // Arrange
        IRenderedComponent<Tabs> cut = RenderComponent<Tabs>(parameters => parameters
            .AddChildContent<Tab>(p => p.Add(t => t.Id, "first").Add(t => t.Title, "First Tab").Add(t => t.ChildContent, "<p>First tab</p>"))
            .AddChildContent<Tab>(p => p.Add(t => t.Id, "second").Add(t => t.Title, "Second Tab").Add(t => t.ChildContent, "<p>Second tab</p>"))
        );

        // Act
        List<Tab> tabs = cut.Instance.TabsList;

        // Assert
        tabs.Should().HaveCount(2);
        tabs[0].Id.Should().Be("first");
        tabs[0].Title.Should().Be("First Tab");
        tabs[1].Id.Should().Be("second");
        tabs[1].Title.Should().Be("Second Tab");
    }

    [Fact]
    public void DefaultTab_ShouldWorkForSecondTab()
    {
        // Arrange
        IRenderedComponent<Tabs> cut = RenderComponent<Tabs>(parameters => parameters
            .Add(p => p.DefaultTab, "second")
            .AddChildContent<Tab>(p => p.Add(t => t.Id, "first").Add(t => t.ChildContent, "<p>First tab</p>"))
            .AddChildContent<Tab>(p => p.Add(t => t.Id, "second").Add(t => t.ChildContent, "<p>Second tab</p>"))
        );

        // Act
        string activeTabId = cut.Instance.ActiveTabId;

        // Assert
        activeTabId.Should().Be("second");
    }

    [Fact]
    public void Tabs_ShouldBeUnregisteredWhenRemovedFromMarkup()
    {
        // Arrange
        IRenderedComponent<Tabs> cut = RenderComponent<Tabs>(parameters => parameters
            .AddChildContent<Tab>(p => p.Add(t => t.Id, "first").Add(t => t.ChildContent, "<p>First tab</p>"))
            .AddChildContent<Tab>(p => p.Add(t => t.Id, "second").Add(t => t.ChildContent, "<p>Second tab</p>"))
        );

        // Act
        cut.SetParametersAndRender(parameters => parameters.AddChildContent<Tab>(p => p.Add(t => t.Id, "second").Add(t => t.ChildContent, "<p>Second tab</p>")));
        List<Tab> tabs = cut.Instance.TabsList;

        // Assert
        tabs.Should().HaveCount(1);
        tabs[0].Id.Should().Be("second");
    }

    [Fact]
    public void Tabs_ShouldHandleDuplicateTitles()
    {
        // Arrange
        IRenderedComponent<Tabs> cut = RenderComponent<Tabs>(parameters => parameters
            .AddChildContent<Tab>(p => p.Add(t => t.Id, "first").Add(t => t.Title, "Duplicate").Add(t => t.ChildContent, "<p>First tab</p>"))
            .AddChildContent<Tab>(p => p.Add(t => t.Id, "second").Add(t => t.Title, "Duplicate").Add(t => t.ChildContent, "<p>Second tab</p>"))
        );

        // Act
        List<Tab> tabs = cut.Instance.TabsList;

        // Assert
        tabs.Count.Should().Be(2);
        tabs[0].Title.Should().Be("Duplicate");
        tabs[1].Title.Should().Be("Duplicate");
    }

    [Fact]
    public void Tabs_ShouldHandleEmptyActiveTabId()
    {
        // Arrange
        IRenderedComponent<Tabs> cut = RenderComponent<Tabs>(parameters => parameters
            .AddChildContent<Tab>(p => p.Add(t => t.Id, "first").Add(t => t.ChildContent, "<p>First tab</p>"))
            .AddChildContent<Tab>(p => p.Add(t => t.Id, "second").Add(t => t.ChildContent, "<p>Second tab</p>"))
        );

        // Act
        string activeTabId = cut.Instance.ActiveTabId;

        // Assert
        activeTabId.Should().NotBeEmpty();
    }
}