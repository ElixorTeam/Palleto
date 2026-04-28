// using AngleSharp.Dom;
// using Microsoft.AspNetCore.Components;
// using Pl.Components.Source.UI.Button;
// using TailwindMerge.Extensions;
//
// namespace Pl.Components.Tests;
//
// public class ButtonTests : TestContext
// {
//     public ButtonTests()
//     {
//         Services.AddTailwindMerge();
//     }
//
//     [Fact]
//     public void OnClick_TriggerEventCallback()
//     {
//         // Arrange
//         bool clicked = false;
//         IRenderedComponent<Button> component = RenderComponent<Button>(parameters => parameters
//             .Add(p => p.OnClick, EventCallback.Factory.Create(this, () => clicked = true))
//         );
//
//         // Act
//         IElement buttonElement = component.Find("button");
//         buttonElement.Click();
//
//         // Assert
//         clicked.Should().BeTrue();
//     }
//
//     [Fact]
//     public void Disabled_AttributeAppliedCorrectly()
//     {
//         // Arrange
//         IRenderedComponent<Button> component = RenderComponent<Button>(parameters => parameters
//             .Add(p => p.Disabled, true)
//         );
//
//         // Assert
//         IElement buttonElement = component.Find("button");
//         buttonElement.HasAttribute("disabled").Should().BeTrue();
//     }
//
//     [Fact]
//     public void Link_AttributeChangeButtonToATag()
//     {
//         // Arrange
//         const string link = "/link";
//         IRenderedComponent<Button> component = RenderComponent<Button>(parameters => parameters
//             .Add(p => p.Link, link)
//         );
//
//         // Assert
//         IElement buttonElement = component.Find("a");
//         buttonElement.Attributes["href"]?.Value.Should().Be(link);
//     }
//
//     [Fact]
//     public void AdditionalAttributes_ShouldBeAppliedToATag()
//     {
//         // Arrange
//         const string target = "_blank";
//         IRenderedComponent<Button> component = RenderComponent<Button>(parameters => parameters
//             .Add(p => p.Link, "/link")
//             .Add(p => p.AdditionalAttributes, new Dictionary<string, object> { { "target", target } })
//         );
//
//         // Assert
//         IElement buttonElement = component.Find("a");
//         buttonElement.Attributes["target"]?.Value.Should().Be(target);
//     }
// }