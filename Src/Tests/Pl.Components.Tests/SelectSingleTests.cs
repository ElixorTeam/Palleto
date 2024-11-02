using AngleSharp.Dom;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FluentUI.AspNetCore.Components;
using Pl.Components.Source.UI.Select;

namespace Pl.Components.Tests;

public class SelectSingleTests : TestContext
{
    [Inject] private LibraryConfiguration LibraryConfiguration { get; set; } = new();

    public SelectSingleTests()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        Services.AddSingleton(LibraryConfiguration);
    }

    private IRenderedComponent<SelectSingle<T>> RenderComponentWithParameters<T>(Action<ComponentParameterCollectionBuilder<SelectSingle<T>>> parameters)
        => RenderComponent(parameters);

    [Fact]
    public void WhenItemIsSelected_ValueShouldChange()
    {
        // Arrange
        List<string> items = ["Item1", "Item2", "Item3"];

        // Act
        IRenderedComponent<SelectSingle<string>> cut = RenderComponentWithParameters<string>(parameters => parameters
            .Add(p => p.Items, items)
            .Add(p => p.ValueChanged, () => Task.CompletedTask)
            .Add(p => p.ItemDisplayName, item => item)
            .Add(p => p.Placeholder, "Select an item")
        );

        cut.Find("button").Click();

        cut.InvokeAsync(() => cut.FindAll("li > button").ElementAt(1).Click());
        cut.Render();

        // Assert
        cut.Instance.Value.Should().Be("Item2");
    }

    [Fact]
    public void WhenItemIsSelected_IsActiveShouldChange()
    {
        // Arrange
        List<string> items = ["Item1", "Item2", "Item3"];

        // Act
        IRenderedComponent<SelectSingle<string>> cut = RenderComponentWithParameters<string>(parameters => parameters
            .Add(p => p.Items, items)
            .Add(p => p.ItemDisplayName, item => item)
            .Add(p => p.Placeholder, "Select an item")
        );

        cut.Find("button").Click();

        cut.InvokeAsync(() => cut.FindAll("li > button").ElementAt(1).Click());
        cut.Render();
        cut.Find("button").Click();

        // Assert
        List<IElement> activeIcons = cut.FindAll("li > button > svg")
            .Where(icon => icon.ClassList.Contains("visible"))
            .ToList();

        activeIcons.Should().HaveCount(1);
    }

    [Fact]
    public void ShouldOpenDropdownMenu()
    {
        // Arrange
        List<string> items = ["Item1", "Item2", "Item3"];

        // Act
        IRenderedComponent<SelectSingle<string>> cut = RenderComponentWithParameters<string>(parameters => parameters
            .Add(p => p.Items, items)
            .Add(p => p.ItemDisplayName, item => item)
            .Add(p => p.Placeholder, "Select an item")
        );

        cut.Find("button").Click();

        // Assert
        cut.Find("[role='combobox']").Attributes["aria-expanded"]?.Value.Should().Be("True");
    }

    [Fact]
    public void ShouldSetAriaTagsCorrectly()
    {
        // Arrange
        List<string> items = ["Item1", "Item2", "Item3"];

        // Act
        IRenderedComponent<SelectSingle<string>> cut = RenderComponentWithParameters<string>(parameters => parameters
            .Add(p => p.Items, items)
            .Add(p => p.ItemDisplayName, item => item)
            .Add(p => p.Placeholder, "Select an item")
        );

        IElement combobox = cut.Find("[role='combobox']");

        // Assert
        combobox.Attributes["aria-haspopup"]?.Value.Should().Be("listbox");
        combobox.Attributes["aria-expanded"]?.Value.Should().Be("False");
    }

    [Fact]
    public void DoesNotSetSelectedItemIfComponentIsDisabled()
    {
        // Act
        IRenderedComponent<SelectSingle<string>> cut = RenderComponentWithParameters<string>(parameters => parameters
            .Add(p => p.Items, [])
            .Add(p => p.Disabled, true));

        IElement trigger = cut.Find("button");

        // Assert
        trigger.IsDisabled().Should().BeTrue();
    }

    [Fact]
    public async Task FiltersItemsBasedOnSearchString()
    {
        // Arrange
        string[] items = ["apple", "banana", "orange"];
        const string searchValue = "ban";

        // Act
        IRenderedComponent<SelectSingle<string>> cut = RenderComponentWithParameters<string>(parameters => parameters
            .Add(p => p.Items, items)
            .Add(p => p.Filterable, true));

        cut.Find("button").Click();

        IElement selectSearch = cut.Find("input");
        await cut.InvokeAsync(() =>
        {
            selectSearch = cut.Find("input");
            selectSearch.Input(searchValue);
        });

        // Assert
        selectSearch.GetAttribute("value").Should().Be(searchValue);

        cut.WaitForState(() => cut.FindAll("li").Count == 1); // wait for debounce
        cut.FindAll("li").Should().HaveCount(1);
    }

    [Fact]
    public void ShouldUpdateValueWhenParameterChangesExternally()
    {
        // Arrange
        List<string> items = ["Item1", "Item2", "Item3"];
        const string itemToFind = "Item3";

        // Act
        IRenderedComponent<SelectSingle<string>> cut = RenderComponentWithParameters<string>(parameters => parameters
            .Add(p => p.Items, items)
            .Add(p => p.Value, "Item1")
            .Add(p => p.ItemDisplayName, item => item)
            .Add(p => p.Placeholder, "Select an item")
        );

        cut.SetParametersAndRender(parameters => parameters
            .Add(p => p.Value, itemToFind));

        // Assert
        cut.Instance.Value.Should().Be(itemToFind);
    }

    [Fact]
    public void ShouldCallItemDisplayNameFunction()
    {
        // Arrange
        List<string> items = ["Item1", "Item2", "Item3"];
        Func<string, string> itemDisplayName = value => value.ToUpper();

        // Act
        IRenderedComponent<SelectSingle<string>> cut = RenderComponentWithParameters<string>(parameters => parameters
            .Add(p => p.Items, items)
            .Add(p => p.ItemDisplayName, itemDisplayName)
            .Add(p => p.Placeholder, "Select an item")
        );

        cut.Find("button").Click();

        // Assert
        string buttonText = cut.FindAll("li > button")[0].TextContent.Trim();
        buttonText.Should().Be("ITEM1");
        itemDisplayName.Invoke("Item1").Should().Be("ITEM1");
    }

    [Fact]
    public async Task ShouldHandleNullValueSelection()
    {
        // Arrange
        List<string?> items = ["Item1", "Item2", "Item3", null];

        // Act
        IRenderedComponent<SelectSingle<string?>> cut = RenderComponentWithParameters<string?>(parameters => parameters
            .Add(p => p.Items, items)
            .Add(p => p.Placeholder, "Select an item")
            .Add(p => p.Value, null)
        );

        cut.Find("button").Click();
        await cut.InvokeAsync(() => cut.FindAll("li > button").First().Click());
        cut.WaitForAssertion(() => cut.Instance.Value.Should().Be("Item1"));

        cut.Find("button").Click();
        await cut.InvokeAsync(() => cut.FindAll("li > button").Last().Click());
        cut.WaitForAssertion(() => cut.Instance.Value.Should().BeNull());
    }

    [Fact]
    public async Task ShouldDisplayEmptyPlaceholderWhenNoItemsMatchFilter()
    {
        // Arrange
        string[] items = ["apple", "banana", "orange"];
        const string searchValue = "xyz";
        const string emptyPlaceholderText = "No items found";

        // Act
        IRenderedComponent<SelectSingle<string>> cut = RenderComponentWithParameters<string>(parameters => parameters
            .Add(p => p.Items, items)
            .Add(p => p.Filterable, true)
            .Add(p => p.EmptyPlaceholder, emptyPlaceholderText)
        );

        cut.Find("button").Click();

        IElement selectSearch = cut.Find("input");
        await cut.InvokeAsync(() =>
        {
            selectSearch = cut.Find("input");
            selectSearch.Input(searchValue);
        });

        // Assert
        selectSearch.GetAttribute("value").Should().Be(searchValue);
        cut.WaitForState(() => cut.FindAll("li").Count == 0);
        IElement emptyPlaceholderDiv = cut.Find($"div:contains('{emptyPlaceholderText}')");
        emptyPlaceholderDiv.TextContent.Trim().Should().Be(emptyPlaceholderText);
    }
}