using AngleSharp.Dom;
using Microsoft.AspNetCore.Components;
using Pl.Components.Source.UI;

namespace Pl.Components.Tests;

public class ButtonTests : TestContext
{
    [Fact]
    public void DefaultParameters_RenderCorrectly()
    {
        // Arrange
        IRenderedComponent<Button> component = RenderComponent<Button>();

        // Assert
        IElement buttonElement = component.Find("button");
        buttonElement.MarkupMatches(
            """
            <button class="inline-flex items-center justify-center whitespace-nowrap rounded-md text-sm font-medium ring-offset-background transition-colors focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-ring focus-visible:ring-offset-2 disabled:pointer-events-none disabled:opacity-5 bg-primary text-primary-foreground hover:bg-primary/90 h-9 px-4 py-2" type="button"></button>
            """);
    }

    [Fact]
    public void OnClick_TriggerEventCallback()
    {
        // Arrange
        bool clicked = false;
        IRenderedComponent<Button> component = RenderComponent<Button>(parameters => parameters
            .Add(p => p.OnClick, EventCallback.Factory.Create(this, () => clicked = true))
        );

        // Act
        IElement buttonElement = component.Find("button");
        buttonElement.Click();

        // Assert
        clicked.Should().BeTrue();
    }

    [Theory]
    [InlineData(ButtonVariantType.Default, "bg-primary text-primary-foreground hover:bg-primary/90")]
    [InlineData(ButtonVariantType.Destructive, "bg-destructive text-destructive-foreground hover:bg-destructive/90")]
    [InlineData(ButtonVariantType.Outline, "border border-input bg-background hover:bg-accent hover:text-accent-foreground")]
    [InlineData(ButtonVariantType.Secondary, "bg-secondary text-secondary-foreground hover:bg-secondary/80")]
    [InlineData(ButtonVariantType.Ghost, "hover:bg-accent hover:text-accent-foreground")]
    [InlineData(ButtonVariantType.Link, "text-primary underline-offset-4 hover:underline")]
    public void VariantClasses_AppliedCorrectly(ButtonVariantType variantType, string expectedClass)
    {
        // Arrange
        IRenderedComponent<Button> component = RenderComponent<Button>(parameters => parameters
            .Add(p => p.Variant, variantType)
        );

        // Assert
        IElement buttonElement = component.Find("button");
        buttonElement.ClassList.Should().Contain(expectedClass.Split(' '));
    }

    [Theory]
    [InlineData(ButtonSizeType.Default, "h-9 px-4 py-2")]
    [InlineData(ButtonSizeType.Small, "h-8 rounded-md px-3 text-xs")]
    [InlineData(ButtonSizeType.Large, "h-10 rounded-md px-8")]
    [InlineData(ButtonSizeType.Full, "size-full")]
    [InlineData(ButtonSizeType.Icon, "h-9 w-9")]
    public void SizeClasses_AppliedCorrectly(ButtonSizeType sizeType, string expectedClass)
    {
        // Arrange
        IRenderedComponent<Button> component = RenderComponent<Button>(parameters => parameters
            .Add(p => p.Size, sizeType)
        );

        // Assert
        IElement buttonElement = component.Find("button");
        buttonElement.ClassList.Should().Contain(expectedClass.Split(' '));
    }

    [Theory]
    [InlineData(ButtonType.Button, "button")]
    [InlineData(ButtonType.Reset, "reset")]
    [InlineData(ButtonType.Submit, "submit")]
    public void HtmlType_AppliedCorrectly(ButtonType type, string expectedHtmlType)
    {
        // Arrange
        IRenderedComponent<Button> component = RenderComponent<Button>(parameters => parameters
            .Add(p => p.Type, type)
        );

        // Assert
        IElement buttonElement = component.Find("button");
        buttonElement.GetAttribute("type").Should().Be(expectedHtmlType);
    }

    [Fact]
    public void Disabled_AttributeAppliedCorrectly()
    {
        // Arrange
        IRenderedComponent<Button> component = RenderComponent<Button>(parameters => parameters
            .Add(p => p.Disabled, true)
        );

        // Assert
        IElement buttonElement = component.Find("button");
        buttonElement.HasAttribute("disabled").Should().BeTrue();
    }
}